using System.Data.SqlServerCe;
using System.IO;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class SqlCeConnectionExtensions
    {
        public static void CreateEmptyDatabase([NotNull] this SqlCeConnection connection)
        {
            //TODO ErikEJ Make this more robust
            Check.NotNull(connection, nameof(connection));

            using (var engine = new SqlCeEngine(connection.ConnectionString))
            {
                engine.CreateDatabase();
            }
        }

        public static bool Exists([NotNull] this SqlCeConnection connection)
        {
            Check.NotNull(connection, nameof(connection));
            //TODO ErikEJ Make this more robust
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
