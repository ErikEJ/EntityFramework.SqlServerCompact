using System.Linq;
using Microsoft.Data.Entity.Extensions.Logging;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace Microsoft.Data.Entity
{
    public class DbLoggerProvider : ILoggerProvider
    {
        private static readonly string[] _whitelist = 
        {
            typeof(Storage.Internal.RelationalCommandBuilderFactory).FullName
        };

        public ILogger CreateLogger(string name)
        {
            if (_whitelist.Contains(name))
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
