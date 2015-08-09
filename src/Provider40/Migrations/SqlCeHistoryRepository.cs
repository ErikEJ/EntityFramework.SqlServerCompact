using System;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Data.Entity.Migrations.History;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.SqlServerCompact.Metadata;

namespace Microsoft.Data.Entity.SqlServerCompact.Migrations
{
    public class SqlCeHistoryRepository : HistoryRepository
    {
        private readonly SqlCeUpdateSqlGenerator _sql;

        public SqlCeHistoryRepository(
            [NotNull] IDatabaseCreator databaseCreator,
            [NotNull] ISqlStatementExecutor executor,
            [NotNull] IRelationalConnection connection,
            [NotNull] IMigrationModelFactory modelFactory,
            [NotNull] IDbContextOptions options,
            [NotNull] IModelDiffer modelDiffer,
            [NotNull] SqlCeMigrationSqlGenerator migrationSqlGenerator,
            [NotNull] SqlCeMetadataExtensionProvider annotations,
            [NotNull] SqlCeUpdateSqlGenerator updateSqlGenerator,
            [NotNull] IServiceProvider serviceProvider)
            : base(
                  databaseCreator,
                  executor,
                  connection,
                  modelFactory,
                  options,
                  modelDiffer,
                  migrationSqlGenerator,
                  annotations,
                  updateSqlGenerator,
                  serviceProvider)
        {
            Check.NotNull(updateSqlGenerator, nameof(updateSqlGenerator));

            _sql = updateSqlGenerator;
        }

        protected override string ExistsSql
            => "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " +
                _sql.EscapeLiteral(TableName) + 
                "' AND TABLE_TYPE<> N'SYSTEM TABLE'";

        protected override bool Exists(object value) => (long)value != 0L;

        public override string GetCreateIfNotExistsScript()
        {
            throw new NotSupportedException("Generating idempotent scripts for migration is not currently supported by SQL Server Compact");
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
