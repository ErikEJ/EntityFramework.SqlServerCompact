using System;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class SqlCeDatabaseFacadeExtensions
    {
        /// <summary>
        /// Configures the database to log all SQL statements
        /// </summary>
        /// <param name="database"> The database facade.</param>
        /// <param name="writeAction">The method to write the log</param>
        public static void Log([NotNull] this DatabaseFacade database, Action<string> writeAction)
        {
            Check.NotNull(database, nameof(database));
            Check.NotNull(writeAction, nameof(writeAction));

            var loggerFactory =  database.GetService<ILoggerFactory>();
            loggerFactory.AddProvider(new DbLoggerProvider(writeAction));
        }
    }
}
