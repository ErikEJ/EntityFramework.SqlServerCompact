using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.EntityFrameworkCore.Utilities
{
    public class SqlCeDatabaseCleaner : RelationalDatabaseCleaner
    {
        protected override IDatabaseModelFactory CreateDatabaseModelFactory(ILoggerFactory loggerFactory)
            => new SqlCeDatabaseModelFactory(
                new DiagnosticsLogger<DbLoggerCategory.Scaffolding>(
                    loggerFactory,
                    new LoggingOptions(),
                    new DiagnosticListener("Fake")));

        protected override bool AcceptIndex(IndexModel index)
            => !index.Name.StartsWith("PK_", StringComparison.Ordinal)
               && !index.Name.StartsWith("AK_", StringComparison.Ordinal);
    }
}
