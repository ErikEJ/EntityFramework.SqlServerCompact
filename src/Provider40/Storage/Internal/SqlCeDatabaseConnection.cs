using System.Data.Common;
using System.Data.SqlServerCe;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCore.SqlCe.Storage.Internal
{
    public class SqlCeDatabaseConnection : RelationalConnection, ISqlCeDatabaseConnection
    {
        public SqlCeDatabaseConnection([NotNull] RelationalConnectionDependencies dependencies)
            : base(dependencies)
        {
        }

        protected override DbConnection CreateDbConnection() => new SqlCeConnection(ConnectionString);

        public override bool IsMultipleActiveResultSetsEnabled => true;

        protected override bool SupportsAmbientTransactions => true;
    }
}
