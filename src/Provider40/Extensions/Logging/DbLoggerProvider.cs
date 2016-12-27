using System;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore
{
    internal class DbLoggerProvider : ILoggerProvider
    {
        private readonly Action<string> _writeAction;

        public DbLoggerProvider([NotNull] Action<string> writeAction)
        {
            Check.NotNull(writeAction, nameof(writeAction));

            _writeAction = writeAction;
        }

        private static readonly string[] _whitelist = 
        {
            typeof(Storage.IRelationalCommandBuilderFactory).FullName
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
