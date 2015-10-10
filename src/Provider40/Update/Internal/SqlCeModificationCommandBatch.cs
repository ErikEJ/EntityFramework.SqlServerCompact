using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.ChangeTracking.Internal;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.Logging;

namespace Microsoft.Data.Entity.Update.Internal
{
    public class SqlCeModificationCommandBatch : SingularModificationCommandBatch
    {
        private readonly IRelationalValueBufferFactoryFactory _valueBufferFactoryFactory;

        public SqlCeModificationCommandBatch(
            [NotNull] IRelationalCommandBuilderFactory commandBuilderFactory,
            [NotNull] IRelationalValueBufferFactoryFactory valueBufferFactoryFactory,
            [NotNull] IUpdateSqlGenerator sqlGenerator)
            : base(commandBuilderFactory, sqlGenerator, valueBufferFactoryFactory)
        {
            _valueBufferFactoryFactory = valueBufferFactoryFactory;
        }

        public override void Execute(IRelationalConnection connection, ILogger logger)
        {
            Check.NotNull(connection, nameof(connection));
            Check.NotNull(logger, nameof(logger));

            var initialCommandText = GetCommandText();
            Tuple<string, string> commandText = SplitCommandText(initialCommandText);

            Debug.Assert(ResultSetEnds.Count == ModificationCommands.Count);

            var commandIndex = 0;
            using (var storeCommand = CreateStoreCommand(commandText.Item1, connection))
            {
                if (logger.IsEnabled(LogLevel.Verbose))
                {
                    logger.LogCommand(storeCommand);
                }

                try
                {
                    using (var reader = storeCommand.ExecuteReader())
                    {
                        DbCommand returningCommand = null;
                        DbDataReader returningReader = null;
                        try
                        {
                            if (commandText.Item2.Length > 0)
                            {
                                returningCommand = CreateStoreCommand(commandText.Item2,  connection);
                                returningReader = returningCommand.ExecuteReader();
                            }
                            commandIndex = ModificationCommands[commandIndex].RequiresResultPropagation
                            ? ConsumeResultSetWithPropagation(commandIndex, reader, returningReader)
                            : ConsumeResultSetWithoutPropagation(commandIndex, reader);

                            Debug.Assert(commandIndex == ModificationCommands.Count, "Expected " + ModificationCommands.Count + " results, got " + commandIndex);
                        }
                        finally
                        {
                            if (returningCommand != null)
                            {
                                returningCommand.Dispose();
                                returningReader?.Dispose();
                            }
                        }
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
        }

        public override Task ExecuteAsync(IRelationalConnection connection, ILogger logger, CancellationToken cancellationToken = default(CancellationToken))
        {
            Check.NotNull(connection, nameof(connection));
            Check.NotNull(logger, nameof(logger));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.Run(() => Execute(connection, logger), cancellationToken);
        }

        private Tuple<string, string> SplitCommandText(string commandText)
        {
            var stringToFind = ";" + Environment.NewLine + "SELECT ";
            var stringToFindIndex = commandText.IndexOf(stringToFind, StringComparison.OrdinalIgnoreCase);

            if (stringToFindIndex > 0)
            {
                return new Tuple<string, string>(
                    commandText.Substring(0, stringToFindIndex + 1),
                    commandText.Substring(commandText.LastIndexOf(stringToFind, StringComparison.OrdinalIgnoreCase) + 1));
            }
            return new Tuple<string, string>(
                commandText,
                string.Empty);
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

        private IRelationalValueBufferFactory CreateValueBufferFactory(IReadOnlyList<ColumnModification> columnModifications)
             => _valueBufferFactoryFactory 
                 .Create( 
                     columnModifications
                         .Where(c => c.IsRead)
                         .Select(c => c.Property.ClrType)
                         .ToArray(), 
                     indexMap: null); 


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

        private IReadOnlyList<InternalEntityEntry> AggregateEntries(int endIndex, int commandCount)
        {
            var entries = new List<InternalEntityEntry>();
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
