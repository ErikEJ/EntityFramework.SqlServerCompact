using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.ChangeTracking.Internal;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.SqlServerCompact.Extensions;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Update;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.Logging;

namespace Microsoft.Data.Entity.SqlServerCompact.Update
{
    public class SqlCeModificationCommandBatch : SingularModificationCommandBatch
    {
        public SqlCeModificationCommandBatch(
            [NotNull] IUpdateSqlGenerator sqlGenerator)
            : base(sqlGenerator)
        {
        }

        public override void Execute(
            IRelationalTransaction transaction,
            IRelationalTypeMapper typeMapper,
            DbContext context,
            ILogger logger)
        {
            Check.NotNull(transaction, nameof(transaction));
            Check.NotNull(typeMapper, nameof(typeMapper));
            Check.NotNull(context, nameof(context));
            Check.NotNull(logger, nameof(logger));

            var initialCommandText = GetCommandText();
            Tuple<string, string> commandText = SplitCommandText(initialCommandText);

            Debug.Assert(ResultSetEnds.Count == ModificationCommands.Count);

            var commandIndex = 0;
            using (var storeCommand = CreateStoreCommand(commandText.Item1, transaction.DbTransaction, typeMapper, transaction.Connection?.CommandTimeout))
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
                                returningCommand = CreateStoreCommand(commandText.Item2, transaction.DbTransaction, typeMapper, transaction.Connection?.CommandTimeout);
                                returningReader = returningCommand.ExecuteReader();
                            }
                            commandIndex = ModificationCommands[commandIndex].RequiresResultPropagation
                            ? ConsumeResultSetWithPropagation(commandIndex, reader, returningReader, context)
                            : ConsumeResultSetWithoutPropagation(commandIndex, reader, context);

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

        public override Task ExecuteAsync(
            IRelationalTransaction transaction, 
            IRelationalTypeMapper typeMapper, 
            DbContext context, 
            ILogger logger, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            Check.NotNull(transaction, nameof(transaction));
            Check.NotNull(typeMapper, nameof(typeMapper));
            Check.NotNull(context, nameof(context));
            Check.NotNull(logger, nameof(logger));

            cancellationToken.ThrowIfCancellationRequested();

            return Task.Run(() => Execute(transaction, typeMapper, context, logger));
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

        protected override int ConsumeResultSetWithoutPropagation(int commandIndex, DbDataReader reader, DbContext context)
        {
            const int expectedRowsAffected = 1;
            var rowsAffected = reader.RecordsAffected;

            ++commandIndex;

            if (rowsAffected != expectedRowsAffected)
            {
                throw new DbUpdateConcurrencyException(
                    Relational.Internal.Strings.UpdateConcurrencyException(expectedRowsAffected, rowsAffected),
                    AggregateEntries(commandIndex, expectedRowsAffected));
            }

            return commandIndex;
        }

        private int ConsumeResultSetWithPropagation(int commandIndex, DbDataReader reader, DbDataReader returningReader, DbContext context)
        {
            const int expectedRowsAffected = 1;
            var tableModification = ModificationCommands[commandIndex];

            Debug.Assert(tableModification.RequiresResultPropagation);

            ++commandIndex;

            reader.Read();

            var rowsAffected = reader.RecordsAffected;
            if (rowsAffected != expectedRowsAffected)
            {
                throw new DbUpdateConcurrencyException(
                    Relational.Internal.Strings.UpdateConcurrencyException(expectedRowsAffected, rowsAffected),
                    AggregateEntries(commandIndex, expectedRowsAffected));
            }

            returningReader.Read();

            tableModification.PropagateResults(tableModification.ValueBufferFactory.Create(returningReader));

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
    }
}
