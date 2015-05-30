using System;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ErikEJ.Data.Entity.SqlServerCompact.Extensions;
using JetBrains.Annotations;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Metadata;
using Microsoft.Data.Entity.Relational.Update;
using Microsoft.Data.Entity.Update;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.Logging;

namespace ErikEJ.Data.Entity.SqlServerCe.Update
{
    public class SqlCeModificationCommandBatch : SingularModificationCommandBatch
    {
        public SqlCeModificationCommandBatch(
            [NotNull] ISqlGenerator sqlGenerator,
            [NotNull] IRelationalMetadataExtensionProvider metadataExtensionProvider)
            : base(sqlGenerator, metadataExtensionProvider)
        {
        }

        public override int Execute(
            RelationalTransaction transaction,
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
                                if (returningReader != null)
                                {
                                    returningReader.Dispose();
                                }
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
                        Microsoft.Data.Entity.Relational.Strings.UpdateStoreException,
                        context,
                        ex);
                }
            }

            return commandIndex;
        }

        public override Task<int> ExecuteAsync(
            RelationalTransaction transaction, 
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

            return Task.FromResult(Execute(transaction, typeMapper, context, logger));
        }

        private Tuple<string, string> SplitCommandText(string commandText)
        {
            //TODO ErikEJ Improve this for readablility and performance, if possible
            var test = ";" + Environment.NewLine + "SELECT ";
            string item1 = commandText;
            string item2 = string.Empty;

            if (commandText.Contains(test))
            {
                item1 = commandText.Substring(0, commandText.IndexOf(test) + 1);
                item2 = commandText.Substring(commandText.LastIndexOf(test) + 1);
            }

            return new Tuple<string, string>(item1, item2);
        }

        private int ConsumeResultSetWithoutPropagation(int commandIndex, DbDataReader reader, DbContext context)
        {
            var expectedRowsAffected = 1;
            var rowsAffected = reader.RecordsAffected;
            ++commandIndex;

            if (rowsAffected != expectedRowsAffected)
            {
                throw new DbUpdateConcurrencyException(
                    Microsoft.Data.Entity.Relational.Strings.UpdateConcurrencyException(expectedRowsAffected, rowsAffected),
                    context);
            }

            return commandIndex;
        }
       
        private int ConsumeResultSetWithPropagation(int commandIndex, DbDataReader reader, DbDataReader returningReader, DbContext context)
        {
            var tableModification = ModificationCommands[commandIndex];
            Debug.Assert(tableModification.RequiresResultPropagation);

            var expectedRowsAffected = 1;

            reader.Read();
            var rowsAffected = reader.RecordsAffected;
            if (rowsAffected != expectedRowsAffected)
            {
                throw new DbUpdateConcurrencyException(
                    Microsoft.Data.Entity.Relational.Strings.UpdateConcurrencyException(expectedRowsAffected, rowsAffected),
                    context);
            }

            returningReader.Read();
            rowsAffected = reader.RecordsAffected;
            if (rowsAffected != expectedRowsAffected)
            {
                throw new DbUpdateConcurrencyException(
                    Microsoft.Data.Entity.Relational.Strings.UpdateConcurrencyException(expectedRowsAffected, rowsAffected),
                    context);
            }

            tableModification.PropagateResults(tableModification.ValueBufferFactory.CreateValueBuffer(returningReader));

            return ++commandIndex;
        }
    }
}
