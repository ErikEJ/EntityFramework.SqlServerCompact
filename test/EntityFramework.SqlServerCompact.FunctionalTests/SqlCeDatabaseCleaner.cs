﻿using System;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Tests;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class SqlCeDatabaseCleaner : RelationalDatabaseCleaner
    {
        protected override IInternalDatabaseModelFactory CreateDatabaseModelFactory(ILoggerFactory loggerFactory)
            => new SqlCeDatabaseModelFactory(loggerFactory);

        protected override bool AcceptIndex(IndexModel index)
            => !index.Name.StartsWith("PK_", StringComparison.Ordinal)
               && !index.Name.StartsWith("AK_", StringComparison.Ordinal);
    }
}
