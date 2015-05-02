using System.Data.Common;
using ErikEJ.Data.Entity.SqlServerCompact;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity
{
    public static class SqlServerCompactDbContextOptionsBuilderExtensions
    {
        public static RelationalDbContextOptionsBuilder UseSqlServerCompact([NotNull] this DbContextOptionsBuilder options, [NotNull] string connectionString)
        {
            Check.NotNull(options, nameof(options));
            Check.NotEmpty(connectionString, nameof(connectionString));

            var extension = GetOrCreateExtension(options);
            extension.ConnectionString = connectionString;
            extension.MaxBatchSize = 1;
            ((IOptionsBuilderExtender)options).AddOrUpdateExtension(extension);

            return new RelationalDbContextOptionsBuilder(options);
        }

        public static RelationalDbContextOptionsBuilder UseSqlServerCompact([NotNull] this DbContextOptionsBuilder options, [NotNull] DbConnection connection)
        {
            Check.NotNull(options, nameof(options));
            Check.NotNull(connection, nameof(connection));

            var extension = GetOrCreateExtension(options);
            extension.Connection = connection;
            extension.MaxBatchSize = 1;
            ((IOptionsBuilderExtender)options).AddOrUpdateExtension(extension);

            return new RelationalDbContextOptionsBuilder(options);
        }

        private static SqlServerCompactOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder options)
        {
            var existingExtension = options.Options.FindExtension<SqlServerCompactOptionsExtension>();

            return existingExtension != null
                ? new SqlServerCompactOptionsExtension(existingExtension)
                : new SqlServerCompactOptionsExtension();
        }
    }
}
