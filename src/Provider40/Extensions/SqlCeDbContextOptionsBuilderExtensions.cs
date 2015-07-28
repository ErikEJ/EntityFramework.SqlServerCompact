using System.Data.Common;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.SqlServerCompact;
using Microsoft.Data.Entity.SqlServerCompact.Extensions;
using Microsoft.Data.Entity.Utilities;

// ReSharper disable CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class SqlCeDbContextOptionsBuilderExtensions
    {
        public static SqlCeDbContextOptionsBuilder UseSqlCe([NotNull] this DbContextOptionsBuilder options, [NotNull] string connectionString)
        {
            Check.NotNull(options, nameof(options));
            Check.NotEmpty(connectionString, nameof(connectionString));

            var extension = GetOrCreateExtension(options);
            extension.ConnectionString = connectionString;
            extension.MaxBatchSize = 1;
            ((IDbContextOptionsBuilderInfrastructure)options).AddOrUpdateExtension(extension);

            return new SqlCeDbContextOptionsBuilder(options);
        }

        public static SqlCeDbContextOptionsBuilder UseSqlCe([NotNull] this DbContextOptionsBuilder options, [NotNull] DbConnection connection)
        {
            Check.NotNull(options, nameof(options));
            Check.NotNull(connection, nameof(connection));

            var extension = GetOrCreateExtension(options);
            extension.Connection = connection;
            extension.MaxBatchSize = 1;
            ((IDbContextOptionsBuilderInfrastructure)options).AddOrUpdateExtension(extension);

            return new SqlCeDbContextOptionsBuilder(options);
        }

        private static SqlCeOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder options)
        {
            var existingExtension = options.Options.FindExtension<SqlCeOptionsExtension>();

            return existingExtension != null
                ? new SqlCeOptionsExtension(existingExtension)
                : new SqlCeOptionsExtension();
        }
    }
}
