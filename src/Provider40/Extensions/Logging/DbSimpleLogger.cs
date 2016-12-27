using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Extensions.Logging
{
    internal class DbSimpleLogger : ILogger
    {
        private readonly Action<string> _writeAction;

        public DbSimpleLogger([NotNull] Action<string> writeAction)
        {
            Check.NotNull(writeAction, nameof(writeAction));
            _writeAction = writeAction;
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (eventId.Id == (int)RelationalEventId.ExecutedCommand)
            {
                var data = state as IEnumerable<KeyValuePair<string, object>>;
                if (data != null)
                {
                    var commandText = data.Single(p => p.Key == "CommandText").Value;
                    if (string.IsNullOrEmpty(commandText?.ToString()))
                        return;
                    var message = $"{Environment.NewLine}{formatter(state, exception)}";
                    _writeAction(message);
                }
            }

        }

        public IDisposable BeginScope<TState>(TState state) => null;
    }
}
