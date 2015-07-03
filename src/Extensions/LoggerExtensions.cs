using System;
using System.Data.Common;
using System.Globalization;
using System.Text;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational;
using Microsoft.Framework.Logging;

namespace Microsoft.Data.Entity.SqlServerCompact.Extensions
{
    internal static class RelationalLoggerExtensions
    {
        public static void LogSql([NotNull] this ILogger logger, [NotNull] string sql)
            => logger.LogVerbose(RelationalLoggingEventIds.ExecutingSql, sql);

        public static void LogParameters([NotNull] this ILogger logger, [NotNull] DbParameterCollection parameters)
        {
            if (parameters.Count == 0)
            {
                return;
            }
            var paramList = new StringBuilder();

            paramList.AppendFormat(CultureInfo.InvariantCulture, "{0}: {1}", (parameters[0]).ParameterName, parameters[0].Value);
            for (var i = 1; i < parameters.Count; i++)
            {
                paramList.AppendLine();
                paramList.AppendFormat(CultureInfo.InvariantCulture, "{0}: {1}", (parameters[i]).ParameterName, (parameters[i]).Value);
            }
            logger.LogDebug(RelationalLoggingEventIds.ExecutingSql, paramList.ToString());
        }

        public static void LogCommand([NotNull] this ILogger logger, [NotNull] DbCommand command)
        {
            var scope = Guid.NewGuid();

            using (logger.BeginScopeImpl(scope))
            {
                logger.LogParameters(command.Parameters);
                logger.LogSql(command.CommandText);
            }
        }
    }
}
