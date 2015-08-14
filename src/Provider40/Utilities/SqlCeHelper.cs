using System.Data.SqlServerCe;

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class SqlCeHelper
    {

        public static string PathFromConnectionString(string connectionString)
        {
#if SQLCE35
            var conn = new SqlCeConnection(GetFullConnectionString(connectionString));
            return conn.Database;
#else
            var sb = new SqlCeConnectionStringBuilder(GetFullConnectionString(connectionString));
            return sb.DataSource;
#endif
        }

        private static string GetFullConnectionString(string connectionString)
        {
            using (var repl = new SqlCeReplication())
            {
                repl.SubscriberConnectionString = connectionString;
                return repl.SubscriberConnectionString;
            }
        }
    }
}
