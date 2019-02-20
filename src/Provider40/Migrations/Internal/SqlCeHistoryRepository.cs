using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore.SqlCe.Migrations.Internal
{
    public class SqlCeHistoryRepository : HistoryRepository
    {
        public SqlCeHistoryRepository([NotNull] HistoryRepositoryDependencies dependencies)
            : base(dependencies)
        {
        }

        protected override string ExistsSql
        {
            get
            {
                var stringTypeMapping = Dependencies.TypeMappingSource.FindMapping(typeof(string));

                return "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = " +
                    stringTypeMapping.GenerateSqlLiteral(
                        SqlGenerationHelper.DelimitIdentifier(TableName)) +
                " AND TABLE_TYPE <> N'SYSTEM TABLE'";
            }
        }

        protected override bool InterpretExistsResult(object value) => Convert.ToInt64(value) != 0;

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
