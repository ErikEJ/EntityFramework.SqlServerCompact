using System.Data.Common;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational;
using System.Data.SqlServerCe;
using Microsoft.Framework.Logging;
using ErikEJ.Data.Entity.SqlServerCompact.Extensions;

namespace ErikEJ.Data.Entity.SqlServerCompact
{
    public class SqlServerCompactDataStoreConnection : RelationalConnection, ISqlServerCompactConnection
    {
        public SqlServerCompactDataStoreConnection([NotNull] IDbContextOptions options, [NotNull] ILoggerFactory loggerFactory)
            : base(options, loggerFactory)
        {
        }

        public void CreateDatabase()
        {
            var connection = DbConnection as SqlCeConnection;
            connection.CreateEmptyDatabase();
        }

        public bool Exists()
        {
            var connection = DbConnection as SqlCeConnection;
            return connection.Exists();
        }

        public void Delete()
        {
            DbConnection.Close();
            var connection = DbConnection as SqlCeConnection;
            connection.Drop();
        }

        protected override DbConnection CreateDbConnection() => new SqlCeConnection(ConnectionString);

    }
}
