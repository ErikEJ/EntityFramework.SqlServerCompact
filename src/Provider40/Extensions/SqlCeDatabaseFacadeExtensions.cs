using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Reflection;

// ReSharper disable once CheckNamespace

namespace Microsoft.EntityFrameworkCore
{
    public static class SqlCeDatabaseFacadeExtensions
    {
        /// <summary>
        /// Configures the database to log all SQL statements
        /// </summary>
        /// <param name="database"> The database facade.</param>
        /// <param name="writeAction">The method to write the log</param>
        public static void Log([NotNull] this DatabaseFacade database, [NotNull] Action<string> writeAction)
        {
            Check.NotNull(database, nameof(database));
            Check.NotNull(writeAction, nameof(writeAction));

            var loggerFactory = database.GetService<ILoggerFactory>();
            loggerFactory.AddProvider(new DbLoggerProvider(writeAction));
        }

        /// <summary>
        ///     <para>
        ///         Returns true if the database provider currently in use is the SQL Server Compact provider.
        ///     </para>
        ///     <para>
        ///         This method can only be used after the <see cref="DbContext" /> has been configured because
        ///         it is only then that the provider is known. This means that this method cannot be used
        ///         in <see cref="DbContext.OnConfiguring" /> because this is where application code sets the
        ///         provider to use as part of configuring the context.
        ///     </para>
        /// </summary>
        /// <param name="database"> The facade from <see cref="DbContext.Database" />. </param>
        /// <returns> True if SQL Compact is being used; false otherwise. </returns>
        public static bool IsSqlCe([NotNull] this DatabaseFacade database)
            => database.ProviderName.Equals(
                typeof(SqlCeOptionsExtension).GetTypeInfo().Assembly.GetName().Name,
                StringComparison.Ordinal);
    }
}
