using System;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Update.Internal
{
    public class SqlCeModificationCommandBatch : AffectedCountModificationCommandBatch
    {
        private readonly IRelationalCommandBuilderFactory _commandBuilderFactory;

        public SqlCeModificationCommandBatch(
            [NotNull] IRelationalCommandBuilderFactory commandBuilderFactory,
            [NotNull] ISqlGenerator sqlGenerator,
            [NotNull] ISqlCeUpdateSqlGenerator updateSqlGenerator,
            [NotNull] IRelationalValueBufferFactoryFactory valueBufferFactoryFactory)
            : base(
                commandBuilderFactory,
                sqlGenerator,
                updateSqlGenerator,
                valueBufferFactoryFactory)
        {
            _commandBuilderFactory = commandBuilderFactory;
        }

        public override void Execute(IRelationalConnection connection)
        {
            Check.NotNull(connection, nameof(connection));

            var commandTexts = SplitCommandText(GetCommandText());

            var relationalCommand = CreateStoreCommand(commandTexts.Item1);
            try
            {

                using (var reader = relationalCommand.ExecuteReader(connection))
                {
                    Consume(reader.DbDataReader, commandTexts.Item2, connection);
                }
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DbUpdateException(
                    "An error occurred while updating the entries. See the inner exception for details.",
                    ex);
            }
        }

        private void Consume(DbDataReader reader, string returningCommandText, IRelationalConnection connection)
        {
            Debug.Assert(ResultSetEnds.Count == ModificationCommands.Count);
            var commandIndex = 0;

            try
            {
                if (ModificationCommands[0].RequiresResultPropagation && returningCommandText != null)
                {
                    var returningCommand = CreateStoreCommand(returningCommandText);

                    using (var returningReader = returningCommand.ExecuteReader(connection))
                    {
                        commandIndex = ConsumeResultSetWithPropagation(commandIndex, 
                            reader,
                            returningReader.DbDataReader);
                    }
                }
                else
                {
                    commandIndex = ConsumeResultSetWithoutPropagation(commandIndex, reader);
                }

                Debug.Assert(commandIndex == ModificationCommands.Count, "Expected " + ModificationCommands.Count + " results, got " + commandIndex);
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DbUpdateException(
                    RelationalStrings.UpdateStoreException,
                    ex,
                    ModificationCommands[commandIndex].Entries);
            }
        }

        private IRelationalCommand CreateStoreCommand(string commandText, bool includeParameters = true)
        {
            var commandBuilder = _commandBuilderFactory
                .Create()
                .Append(commandText);

            if (!includeParameters) return commandBuilder.BuildRelationalCommand();
            foreach (var columnModification in ModificationCommands.SelectMany(t => t.ColumnModifications))
            {
                if (columnModification.ParameterName != null)
                {
                    commandBuilder.AddParameter(
                        SqlGenerator.GenerateParameterName(columnModification.ParameterName),
                        columnModification.Value,
                        columnModification.Property);
                }

                if (columnModification.OriginalParameterName != null)
                {
                    commandBuilder.AddParameter(
                        SqlGenerator.GenerateParameterName(columnModification.OriginalParameterName),
                        columnModification.OriginalValue,
                        columnModification.Property);
                }
            }

            return commandBuilder.BuildRelationalCommand();
        }

        public override Task ExecuteAsync(IRelationalConnection connection, CancellationToken cancellationToken = default(CancellationToken))
        {
            Check.NotNull(connection, nameof(connection));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.Run(() => Execute(connection), cancellationToken);
        }

        private Tuple<string, string> SplitCommandText(string commandText)
        {
            var stringToFind = ";" + Environment.NewLine + "SELECT ";
            var stringToFindIndex = commandText.IndexOf(stringToFind, StringComparison.OrdinalIgnoreCase);

            if (stringToFindIndex > 0)
            {
                return new Tuple<string, string>(
                    commandText.Substring(0, stringToFindIndex + 1).Trim(),
                    commandText.Substring(commandText.LastIndexOf(stringToFind, StringComparison.OrdinalIgnoreCase) + 1).Trim());
            }
            return new Tuple<string, string>(commandText.Trim(), null);
        }

        protected override int ConsumeResultSetWithoutPropagation(int commandIndex, [NotNull] DbDataReader reader)
        {
            const int expectedRowsAffected = 1;
            var rowsAffected = reader.RecordsAffected;

            ++commandIndex;

            if (rowsAffected != expectedRowsAffected)
            {
                ThrowAggregateUpdateConcurrencyException(commandIndex, expectedRowsAffected, rowsAffected);
            }

            return commandIndex;
        }

        private int ConsumeResultSetWithPropagation(int commandIndex, DbDataReader reader, DbDataReader returningReader)
        {
            var tableModification = ModificationCommands[commandIndex];

            Debug.Assert(tableModification.RequiresResultPropagation);

            ++commandIndex;

            reader.Read();

            if (reader.RecordsAffected != 1)
            {
                ThrowAggregateUpdateConcurrencyException(commandIndex, 1, 0);
            }

            returningReader.Read();

            var valueBufferFactory = CreateValueBufferFactory(tableModification.ColumnModifications);

            tableModification.PropagateResults(valueBufferFactory.Create(returningReader));

            return commandIndex;
        }

        protected override bool CanAddCommand([NotNull] ModificationCommand modificationCommand)
            => ModificationCommands.Count == 0;

        protected override bool IsCommandTextValid()
            => true;
    }
}
