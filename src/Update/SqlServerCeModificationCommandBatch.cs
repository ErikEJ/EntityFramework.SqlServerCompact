using System;
using System.Data.Common;
using System.Diagnostics;
using JetBrains.Annotations;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.ChangeTracking.Internal;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Metadata;
using Microsoft.Data.Entity.Relational.Update;
using Microsoft.Data.Entity.Update;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.Logging;

namespace ErikEJ.Data.Entity.SqlServerCe.Update
{
    public class SqlServerCeModificationCommandBatch : SingularModificationCommandBatch
    {
        public SqlServerCeModificationCommandBatch([NotNull] ISqlGenerator sqlGenerator)
            : base(sqlGenerator)
        {
        }

        public override IRelationalPropertyExtensions GetPropertyExtensions(IProperty property) => 
            property.Relational();

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

            var commandText = GetCommandText();

            Debug.Assert(ResultSetEnds.Count == ModificationCommands.Count);

            var commandIndex = 0;
            using (var storeCommand = CreateStoreCommand(commandText, transaction.DbTransaction, typeMapper, transaction.Connection?.CommandTimeout))
            {
                if (logger.IsEnabled(LogLevel.Verbose))
                {
                    //TODO Cant log!?
                    //logger.LogCommand(storeCommand);
                }

                try
                {
                    using (var reader = storeCommand.ExecuteReader())
                    {
                        commandIndex = ModificationCommands[commandIndex].RequiresResultPropagation
                        ? ConsumeResultSetWithPropagation(commandIndex, reader, context)
                        : ConsumeResultSetWithoutPropagation(commandIndex, reader, context);

                        Debug.Assert(commandIndex == ModificationCommands.Count, "Expected " + ModificationCommands.Count + " results, got " + commandIndex);
                    }
                }
                catch (DbUpdateException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new DbUpdateException(
                        Strings.UpdateStoreException,
                        context,
                        ex);
                }
            }

            return commandIndex;
        }

        private int ConsumeResultSetWithoutPropagation(int commandIndex, DbDataReader reader, DbContext context)
        {
            var expectedRowsAffected = 1;
            var rowsAffected = reader.RecordsAffected;
            ++commandIndex;

            if (rowsAffected != expectedRowsAffected)
            {
                throw new DbUpdateConcurrencyException(
                    Strings.UpdateConcurrencyException(expectedRowsAffected, rowsAffected),
                    context);
            }

            return commandIndex;
        }

        //TODO Wait for update EF binaries available from MyGet
        private int ConsumeResultSetWithPropagation(int commandIndex, DbDataReader reader, DbContext context)
        {
            return 1;

            //var rowsAffected = 0;
            //var valueReader = new RelationalTypedValueReader(reader);
            //do
            //{
            //    var tableModification = ModificationCommands[commandIndex];
            //    Debug.Assert(tableModification.RequiresResultPropagation);

            //    if (!reader.Read())
            //    {
            //        var expectedRowsAffected = rowsAffected + 1;
            //        while (++commandIndex < ResultSetEnds.Count
            //               && !ResultSetEnds[commandIndex - 1])
            //        {
            //            expectedRowsAffected++;
            //        }

            //        throw new DbUpdateConcurrencyException(
            //            Strings.UpdateConcurrencyException(expectedRowsAffected, rowsAffected),
            //            context);
            //    }

            //    tableModification.PropagateResults(valueReader);
            //    rowsAffected++;
            //}
            //while (++commandIndex < ResultSetEnds.Count
            //       && !ResultSetEnds[commandIndex - 1]);

            //return commandIndex;
        }
    }
}
