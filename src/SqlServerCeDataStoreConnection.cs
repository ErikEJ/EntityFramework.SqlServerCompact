using System.Data.Common;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational;
using System.Data.SqlServerCe;
using Microsoft.Framework.Logging;
using ErikEJ.Data.Entity.SqlServerCe.Extensions;

namespace ErikEJ.Data.Entity.SqlServerCe
{
    public class SqlServerCeDataStoreConnection : RelationalConnection, ISqlServerCeConnection
    {
        public SqlServerCeDataStoreConnection([NotNull] IDbContextOptions options, [NotNull] ILoggerFactory loggerFactory)
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
