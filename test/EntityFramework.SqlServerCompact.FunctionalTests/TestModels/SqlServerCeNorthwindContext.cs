using System;
using Microsoft.Data.Entity.FunctionalTests.TestModels.Northwind;
using Microsoft.Data.Entity.Infrastructure;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests.TestModels
{
    public class SqlServerCeNorthwindContext : NorthwindContext
    {
        public SqlServerCeNorthwindContext(IServiceProvider serviceProvider, DbContextOptions options)
            : base(serviceProvider, options)
        {
        }

        public static SqlServerCeTestStore GetSharedStore() => SqlServerCeTestStore.GetOrCreateShared("nw40", () => { });
    }
}
