using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Storage.Internal
{
    public static class SqlCeLoggerExtensions
    {
        public static void LogWarning(
            [NotNull] this ILogger logger,
            SqlCeEventId eventId,
            [NotNull] Func<string> formatter)
            => logger.Log<object>(LogLevel.Warning, (int)eventId, eventId, null, (_, __) => formatter());
    }
}