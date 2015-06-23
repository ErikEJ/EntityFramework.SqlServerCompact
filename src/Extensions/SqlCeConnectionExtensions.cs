using System.Data.SqlServerCe;
using System.IO;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Utilities;

namespace ErikEJ.Data.Entity.SqlServerCe.Extensions
{
    public static class SqlCeConnectionExtensions
    {
        public static void CreateEmptyDatabase([NotNull] this SqlCeConnection connection)
        {
            Check.NotNull(connection, nameof(connection));

            using (var engine = new SqlCeEngine(connection.ConnectionString))
            {
                engine.CreateDatabase();
            }
        }

        public static bool Exists([NotNull] this SqlCeConnection connection)
        {
            return File.Exists(PathFromConnectionString(connection.ConnectionString));
        }

        public static void Drop([NotNull] this SqlCeConnection connection, bool throwOnOpen = true)
        {
            if (throwOnOpen)
            {
                connection.Open();
            }
            connection.Close();
            var path = PathFromConnectionString(connection.ConnectionString);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private static string PathFromConnectionString(string connectionString)
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
            using (SqlCeReplication repl = new SqlCeReplication())
            {
                repl.SubscriberConnectionString = connectionString;
                return repl.SubscriberConnectionString;
            }
        }
    }
}
