using System;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Extensions.Logging;

//TODO ErikEJ Remove this later if possible

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity.Storage
{
    public static class LogCommandExtension
    {
        public static void LogCommand([NotNull] this ISensitiveDataLogger logger, [NotNull] DbCommand command)
        {
            Check.NotNull(logger, nameof(logger));
            Check.NotNull(command, nameof(command));

            logger.LogInformation(
                RelationalLoggingEventId.ExecutingCommand,
                () =>
                {
                    var logParameterValues
                        = command.Parameters.Count > 0
                          && logger.LogSensitiveData;

                    return new DbCommandLogData(
                        command.CommandText.TrimEnd(),
                        command.CommandType,
                        command.CommandTimeout,
                        command.Parameters
                            .Cast<DbParameter>()
                            .ToDictionary(p => p.ParameterName, p => logParameterValues ? p.Value : "?"));
                },
                state =>
                    RelationalStrings.RelationalLoggerExecutingCommand(
                        state.Parameters
                            .Select(kv => $"{kv.Key}='{Convert.ToString(kv.Value, CultureInfo.InvariantCulture)}'")
                            .Join(),
                        state.CommandType,
                        state.CommandTimeout,
                        Environment.NewLine,
                        state.CommandText));
        }

        private static void LogInformation<TState>(
            this ISensitiveDataLogger logger, RelationalLoggingEventId eventId, Func<TState> state, Func<TState, string> formatter)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.Log(LogLevel.Information, (int)eventId, state(), null, (s, _) => formatter((TState)s));
            }
        }

    }
}
