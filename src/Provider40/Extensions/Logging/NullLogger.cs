using System;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Extensions.Logging
{
    internal class NullLogger : ILogger
    {
        public static NullLogger Instance { get; } = new NullLogger();

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => false;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
        }
    }
}
