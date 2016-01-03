using System;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Extensions.Logging;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace Microsoft.Data.Entity
{
    public class DbLoggerProvider : ILoggerProvider
    {
        private readonly Action<string> _writeAction;

        public DbLoggerProvider([NotNull] Action<string> writeAction)
        {
            Check.NotNull(writeAction, nameof(writeAction));

            _writeAction = writeAction;
        }

        private static readonly string[] _whitelist = 
        {
            typeof(Storage.Internal.RelationalCommandBuilderFactory).FullName
        };

        public ILogger CreateLogger(string name)
        {
            if (_whitelist.Contains(name))
            {
                return new DbSimpleLogger(_writeAction);
            }

            return NullLogger.Instance;
        }

        public void Dispose()
        {
        }
    }
}
