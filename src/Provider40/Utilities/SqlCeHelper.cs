using System.Data.SqlServerCe;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class SqlCeHelper
    {
        public static string PathFromConnectionString([NotNull] string connectionString)
        {
            Check.NotNull(connectionString, nameof(connectionString));
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
