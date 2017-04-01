using System;
using JetBrains.Annotations;

namespace Microsoft.EntityFrameworkCore.Migrations.Internal
{
    public class SqlCeHistoryRepository : HistoryRepository
    {
        public SqlCeHistoryRepository([NotNull] HistoryRepositoryDependencies dependencies)
            : base(dependencies)
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
