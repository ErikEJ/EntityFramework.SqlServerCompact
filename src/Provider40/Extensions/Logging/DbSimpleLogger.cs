using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
            if (eventId.Id == RelationalEventId.CommandExecuting.Id)
            {
                var data = state as IEnumerable<KeyValuePair<string, object>>;
                if (data != null)
                {
                    var dataList = data.ToList();
                    var commandText = dataList.Single(p => p.Key == "commandText").Value;
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
