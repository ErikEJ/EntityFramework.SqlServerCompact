using System;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.Logging;

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class SqlCeDbContextExtensions
    {
        public static void LogToConsole([NotNull] this DbContext context)
        {
            Check.NotNull(context, nameof(context));
            var loggerFactory = ((IAccessor<IServiceProvider>)context).GetService<ILoggerFactory>();
            loggerFactory.AddProvider(new DbLoggerProvider());
        }
    }
}
