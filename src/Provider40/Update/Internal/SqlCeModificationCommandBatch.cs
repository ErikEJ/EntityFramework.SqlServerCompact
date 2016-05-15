using System;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Update.Internal
{
    public class SqlCeModificationCommandBatch : AffectedCountModificationCommandBatch
    {
        private bool _returnFirstCommandText;

        public SqlCeModificationCommandBatch(
            [NotNull] IRelationalCommandBuilderFactory commandBuilderFactory,
            [NotNull] ISqlGenerationHelper sqlGenerationHelper,
            [NotNull] ISqlCeUpdateSqlGenerator updateSqlGenerator,
            [NotNull] IRelationalValueBufferFactoryFactory valueBufferFactoryFactory)
            : base(
                commandBuilderFactory,
                sqlGenerationHelper,
                updateSqlGenerator,
                valueBufferFactoryFactory)
        {
        }


        public override void Execute(IRelationalConnection connection)
        {
            Check.NotNull(connection, nameof(connection));

            _returnFirstCommandText = true;
            var relationalCommand = CreateStoreCommand();
            try
            {
#if DEBUG
                //System.Diagnostics.Debug.WriteLine(GetCommandText());
#endif
                using (var reader = relationalCommand.RelationalCommand.ExecuteReader(connection, relationalCommand.ParameterValues))
                {
                    Consume(reader.DbDataReader, GetCommandText(), connection);
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
            var commandIndex = 0;

            try
            {
                if (ModificationCommands[0].RequiresResultPropagation && (returningCommandText != null))
                {
                    _returnFirstCommandText = false;
                    var returningCommand = CreateStoreCommand();

                    using (var returningReader = returningCommand.RelationalCommand.ExecuteReader(connection, returningCommand.ParameterValues))
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

        public override Task ExecuteAsync(IRelationalConnection connection, CancellationToken cancellationToken = default(CancellationToken))
        {
            Check.NotNull(connection, nameof(connection));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.Run(() => Execute(connection), cancellationToken);
        }

        protected override string GetCommandText()
        {
            var commandTexts = SplitCommandText(base.GetCommandText());
            return _returnFirstCommandText ? commandTexts.Item1 : commandTexts.Item2;
        }

        private Tuple<string, string> SplitCommandText(string commandText)
        {
            var stringToFind = SqlGenerationHelper.StatementTerminator + Environment.NewLine + "SELECT ";
            var stringToFindIndex = commandText.IndexOf(stringToFind, StringComparison.OrdinalIgnoreCase);

            if (stringToFindIndex > 0)
            {
                return new Tuple<string, string>(
                    commandText.Substring(0, stringToFindIndex + 1).Trim(),
                    commandText.Substring(commandText.LastIndexOf(stringToFind, StringComparison.OrdinalIgnoreCase) + 1).Trim());
            }
            return new Tuple<string, string>(commandText.Trim(), null);
        }

        protected override int ConsumeResultSetWithoutPropagation(int commandIndex, DbDataReader reader)
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

        protected override bool CanAddCommand(ModificationCommand modificationCommand)
            => ModificationCommands.Count == 0;

        protected override bool IsCommandTextValid()
            => true;
    }
}
