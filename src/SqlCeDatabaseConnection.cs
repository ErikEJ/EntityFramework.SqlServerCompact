using System.Data.Common;
using System.Data.SqlServerCe;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational;
using Microsoft.Framework.Logging;

namespace Microsoft.Data.Entity.SqlServerCompact
{
    public class SqlCeDatabaseConnection : RelationalConnection
    { 
        public SqlCeDatabaseConnection([NotNull] IDbContextOptions options, [NotNull] ILoggerFactory loggerFactory)
            : base(options, loggerFactory)
        {
        }

        protected override DbConnection CreateDbConnection() => new SqlCeConnection(ConnectionString);

        public override bool IsMultipleActiveResultSetsEnabled => true;
    }
}
