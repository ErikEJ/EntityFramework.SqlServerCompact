using System;
using Microsoft.Data.Entity.FunctionalTests.TestModels.Northwind;
using Microsoft.Data.Entity.Infrastructure;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests.TestModels
{
    public class SqlCeNorthwindContext : NorthwindContext
    {
        public SqlCeNorthwindContext(IServiceProvider serviceProvider, DbContextOptions options)
            : base(serviceProvider, options)
        {
        }

        public static SqlCeTestStore GetSharedStore() => SqlCeTestStore.GetOrCreateShared("NorthwindEF7", () => { });
    }
}
