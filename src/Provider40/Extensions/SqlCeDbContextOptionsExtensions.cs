using System.Data.Common;
using System.Data.SqlServerCe;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Utilities;

// ReSharper disable CheckNamespace

namespace Microsoft.EntityFrameworkCore
{
    public static class SqlCeDbContextOptionsExtensions
    {
#if SQLCE35
#else
        /// <summary>
        ///     Configures the context to connect to a Microsoft SQL Server Compact database.
        /// </summary>
        /// <param name="optionsBuilder"> The options for the context. </param>
        /// <param name="connectionStringBuilder"> The connection string builder for the database to connect to. </param>
        /// <returns> An options builder to allow additional SQL Server Compact specific configuration. </returns>
        public static SqlCeDbContextOptionsBuilder UseSqlCe([NotNull] this DbContextOptionsBuilder optionsBuilder, [NotNull] SqlCeConnectionStringBuilder connectionStringBuilder)
        {
            Check.NotNull(optionsBuilder, nameof(optionsBuilder));
            Check.NotNull(connectionStringBuilder, nameof(connectionStringBuilder));

            var extension = GetOrCreateExtension(optionsBuilder);
            extension.ConnectionString = connectionStringBuilder.ConnectionString;
            extension.MaxBatchSize = 1;
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            return new SqlCeDbContextOptionsBuilder(optionsBuilder);
        }
#endif
        /// <summary>
        ///     Configures the context to connect to a Microsoft SQL Server Compact database.
        /// </summary>
        /// <param name="optionsBuilder"> The options for the context. </param>
        /// <param name="connectionString"> The connection string for the database to connect to. </param>
        /// <returns> An options builder to allow additional SQL Server Compact specific configuration. </returns>
        public static SqlCeDbContextOptionsBuilder UseSqlCe([NotNull] this DbContextOptionsBuilder optionsBuilder, [NotNull] string connectionString)
        {
            Check.NotNull(optionsBuilder, nameof(optionsBuilder));
            Check.NotEmpty(connectionString, nameof(connectionString));

            var extension = GetOrCreateExtension(optionsBuilder);
            extension.ConnectionString = connectionString;
            extension.MaxBatchSize = 1;
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            return new SqlCeDbContextOptionsBuilder(optionsBuilder);
        }

        /// <summary>
        ///     Configures the context to connect to a Microsoft SQL Server Compact database.
        /// </summary>
        /// <param name="optionsBuilder"> The options for the context. </param>
        /// <param name="connection">
        ///     An existing <see cref="DbConnection" /> to be used to connect to the database. If the connection is
        ///     in the open state then EF will not open or close the connection. If the connection is in the closed
        ///     state then EF will open and close the connection as needed.
        /// </param>
        /// <returns> An options builder to allow additional SQL Server Compact specific configuration. </returns> 
        public static SqlCeDbContextOptionsBuilder UseSqlCe([NotNull] this DbContextOptionsBuilder optionsBuilder, [NotNull] DbConnection connection)
        {
            Check.NotNull(optionsBuilder, nameof(optionsBuilder));
            Check.NotNull(connection, nameof(connection));

            var extension = GetOrCreateExtension(optionsBuilder);
            extension.Connection = connection;
            extension.MaxBatchSize = 1;
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            return new SqlCeDbContextOptionsBuilder(optionsBuilder);
        }

        private static SqlCeOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder optionsBuilder)
        {
            var existingExtension = optionsBuilder.Options.FindExtension<SqlCeOptionsExtension>();

            return existingExtension != null
                ? new SqlCeOptionsExtension(existingExtension)
                : new SqlCeOptionsExtension();
        }
    }
}
