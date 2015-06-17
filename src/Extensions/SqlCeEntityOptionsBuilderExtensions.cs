using System.Data.Common;
using ErikEJ.Data.Entity.SqlServerCe;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Utilities;

// ReSharper disable CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class SqlCeDbContextOptionsBuilderExtensions
    {
        public static RelationalDbContextOptionsBuilder UseSqlCe([NotNull] this DbContextOptionsBuilder options, [NotNull] string connectionString)
        {
            Check.NotNull(options, nameof(options));
            Check.NotEmpty(connectionString, nameof(connectionString));

            var extension = GetOrCreateExtension(options);
            extension.ConnectionString = connectionString;
            extension.MaxBatchSize = 1;
            ((IDbContextOptionsBuilderInfrastructure)options).AddOrUpdateExtension(extension);

            return new RelationalDbContextOptionsBuilder(options);
        }

        public static RelationalDbContextOptionsBuilder UseSqlCe([NotNull] this DbContextOptionsBuilder options, [NotNull] DbConnection connection)
        {
            Check.NotNull(options, nameof(options));
            Check.NotNull(connection, nameof(connection));

            var extension = GetOrCreateExtension(options);
            extension.Connection = connection;
            extension.MaxBatchSize = 1;
            ((IDbContextOptionsBuilderInfrastructure)options).AddOrUpdateExtension(extension);

            return new RelationalDbContextOptionsBuilder(options);
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
