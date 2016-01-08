using System;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Extensions.Logging;

namespace Microsoft.Data.Entity.Extensions.Logging
{
    internal class DbSimpleLogger : ILogger
    {
        private readonly Action<string> _writeAction;

        public DbSimpleLogger([NotNull] Action<string> writeAction)
        {
            Check.NotNull(writeAction, nameof(writeAction));
            _writeAction = writeAction;
        }

        public void Log(LogLevel logLevel, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            var message = $"{Environment.NewLine}{formatter(state, exception)}";
            _writeAction(message);
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public IDisposable BeginScopeImpl(object state) => null;
    }
}
