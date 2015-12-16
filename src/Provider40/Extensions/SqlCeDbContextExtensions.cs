using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class SqlCeDbContextExtensions
    {
        /// <summary>
        ///     Configures the context to log all SQL statements to the Console
        /// </summary>
        /// <param name="context"> The context. </param>
        public static void LogToConsole([NotNull] this DbContext context)
        {
            Check.NotNull(context, nameof(context));
            var loggerFactory = context.GetService<ILoggerFactory>();
            loggerFactory.AddProvider(new DbLoggerProvider());
        }
    }
}
