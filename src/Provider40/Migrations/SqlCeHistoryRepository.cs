using System;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.SqlServerCompact.Metadata;
using Microsoft.Data.Entity.Migrations;

namespace Microsoft.Data.Entity.SqlServerCompact.Migrations
{
    public class SqlCeHistoryRepository : HistoryRepository
    {
        public SqlCeHistoryRepository(
            [NotNull] IDatabaseCreator databaseCreator,
            [NotNull] ISqlStatementExecutor executor,
            [NotNull] IRelationalConnection connection,
            [NotNull] IDbContextOptions options,
            [NotNull] IMigrationsModelDiffer modelDiffer,
            [NotNull] SqlCeMigrationsSqlGenerator migrationSqlGenerator,
            [NotNull] SqlCeMetadataExtensionProvider annotations,
            [NotNull] SqlCeUpdateSqlGenerator sql)
            : base(
                  databaseCreator,
                  executor,
                  connection,
                  options,
                  modelDiffer,
                  migrationSqlGenerator,
                  annotations,
                  sql)
        {
        }

        protected override string ExistsSql

            => "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" +
                Sql.EscapeLiteral(TableName) + 
                "' AND TABLE_TYPE <> N'SYSTEM TABLE'";

        protected override bool InterpretExistsResult(object value) => value != DBNull.Value;

        public override string GetCreateIfNotExistsScript()
        {
            return GetCreateScript();
        }

        public override string GetBeginIfNotExistsScript(string migrationId)
        {
            throw new NotSupportedException("Generating idempotent scripts for migration is not currently supported by SQL Server Compact");
        }

        public override string GetBeginIfExistsScript(string migrationId)
        {
            throw new NotSupportedException("Generating idempotent scripts for migration is not currently supported by SQL Server Compact");
        }

        public override string GetEndIfScript()
        {
            throw new NotSupportedException("Generating idempotent scripts for migration is not currently supported by SQL Server Compact");
        }
    }
}
