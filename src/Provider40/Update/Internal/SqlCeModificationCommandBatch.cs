using System;
using System.Collections.Generic;
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
    public class SqlCeModificationCommandBatch : SingularModificationCommandBatch
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

            //Debug.Assert(ResultSetEnds.Count == ModificationCommands.Count);

            var commandTexts = SplitCommandText(GetCommandText());
            //var commandIndex = 0;

            var relationalCommand = CreateStoreCommand(commandTexts.Item1);
            try
            {

                using (var reader = relationalCommand.ExecuteReader(connection))
                {
                    Consume(reader.DbDataReader, commandTexts.Item2, connection);

                    //    RelationalDataReader returningReader = null;
                    //    try
                    //    {
                    //        if (commandTexts.Item2.Length > 0)
                    //        {
                    //            var returningCommand = CreateStoreCommand(commandTexts.Item2, false);
                    //            returningReader = returningCommand.ExecuteReader(connection);
                    //        }
                    //        if (ModificationCommands[commandIndex].RequiresResultPropagation && returningReader != null)
                    //        {
                    //            commandIndex = ConsumeResultSetWithPropagation(commandIndex, reader.DbDataReader, returningReader.DbDataReader);
                    //        }
                    //        else
                    //        {
                    //            commandIndex = ConsumeResultSetWithoutPropagation(commandIndex, reader.DbDataReader);
                    //        }

                    //        Debug.Assert(commandIndex == ModificationCommands.Count, "Expected " + ModificationCommands.Count + " results, got " + commandIndex);
                    //    }
                    //    finally
                    //    {
                    //        returningReader?.Dispose();
                    //    }
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
                if (ModificationCommands[commandIndex].RequiresResultPropagation && returningCommandText != null)
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
            const int expectedRowsAffected = 1;
            var tableModification = ModificationCommands[commandIndex];

            Debug.Assert(tableModification.RequiresResultPropagation);

            ++commandIndex;

            reader.Read();

            var rowsAffected = reader.RecordsAffected;
            if (rowsAffected != expectedRowsAffected)
            {
                ThrowAggregateUpdateConcurrencyException(commandIndex, expectedRowsAffected, rowsAffected);
            }

            returningReader.Read();

            var valueBufferFactory = CreateValueBufferFactory(tableModification.ColumnModifications);

            tableModification.PropagateResults(valueBufferFactory.Create(returningReader));

            return commandIndex;
        }

        private IReadOnlyList<IUpdateEntry> AggregateEntries(int endIndex, int commandCount)
        {
            var entries = new List<IUpdateEntry>();
            for (var i = endIndex - commandCount; i < endIndex; i++)
            {
                entries.AddRange(ModificationCommands[i].Entries);
            }
            return entries;
        }

        protected override void ThrowAggregateUpdateConcurrencyException( 
             int commandIndex,
             int expectedRowsAffected,
             int rowsAffected)
         { 
             throw new DbUpdateConcurrencyException( 
                 RelationalStrings.UpdateConcurrencyException(expectedRowsAffected, rowsAffected), 
                 AggregateEntries(commandIndex, expectedRowsAffected)); 
         }
    }
}
