using System.Data.SqlServerCe;
using System.IO;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Storage.Internal
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
            Check.NotNull(connection, nameof(connection));
            return File.Exists(SqlCeHelper.PathFromConnectionString(connection.ConnectionString));
        }

        public static void Drop([NotNull] this SqlCeConnection connection, bool throwOnOpen = true)
        {
            Check.NotNull(connection, nameof(connection));

            if (throwOnOpen)
            {
                connection.Open();
            }
            connection.Close();
            var path = SqlCeHelper.PathFromConnectionString(connection.ConnectionString);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
