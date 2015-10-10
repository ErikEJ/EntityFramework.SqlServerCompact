using System.Linq;
using Microsoft.Data.Entity.Extensions.Logging;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace Microsoft.Data.Entity
{
    public class DbLoggerProvider : ILoggerProvider
    {
        private static readonly string[] Whitelist = new string[]
        {
                typeof(Update.Internal.BatchExecutor).FullName,
                typeof(Query.QueryContextFactory).FullName
        };

        public ILogger CreateLogger(string name)
        {
            if (Whitelist.Contains(name))
            {
                return new DbConsoleLogger();
            }

            return NullLogger.Instance;
        }

        public void Dispose()
        {
        }
    }
}
