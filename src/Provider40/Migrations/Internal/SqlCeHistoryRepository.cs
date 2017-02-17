using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace Microsoft.EntityFrameworkCore.Migrations.Internal
{
    public class SqlCeHistoryRepository : HistoryRepository
    {
        public SqlCeHistoryRepository(
            [NotNull] IDatabaseCreator databaseCreator,
            [NotNull] IRawSqlCommandBuilder sqlCommandBuilder,
            [NotNull] ISqlCeDatabaseConnection connection,
            [NotNull] IDbContextOptions options,
            [NotNull] IMigrationsModelDiffer modelDiffer,
            [NotNull] IMigrationsSqlGenerator migrationSqlGenerator,
            [NotNull] IRelationalAnnotationProvider annotations,
            [NotNull] ISqlGenerationHelper sqlGenerationHelper)
            : base(
                  databaseCreator,
                  sqlCommandBuilder,
                  connection,
                  options,
                  modelDiffer,
                  migrationSqlGenerator,
                  annotations,
                  sqlGenerationHelper)
        {
        }

        protected override string ExistsSql

            => "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" +
                SqlGenerationHelper.EscapeLiteral(TableName) + 
                "' AND TABLE_TYPE <> N'SYSTEM TABLE'";

        protected override bool InterpretExistsResult(object value) => value != DBNull.Value;

        public override string GetCreateIfNotExistsScript() => GetCreateScript();

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
