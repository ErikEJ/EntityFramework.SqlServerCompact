using System.Data.Common;
using System.Data.SqlServerCe;
using JetBrains.Annotations;

namespace Microsoft.EntityFrameworkCore.Storage.Internal
{
    public class SqlCeDatabaseConnection : RelationalConnection, ISqlCeDatabaseConnection
    {
        public SqlCeDatabaseConnection([NotNull] RelationalConnectionDependencies dependencies)
            : base(dependencies)
        {
        }

        protected override DbConnection CreateDbConnection() => new SqlCeConnection(ConnectionString);

        public override bool IsMultipleActiveResultSetsEnabled => true;
    }
}
