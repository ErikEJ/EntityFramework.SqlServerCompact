using System;
using Microsoft.Data.Entity.FunctionalTests.TestModels.Northwind;
using Microsoft.Data.Entity.Infrastructure;

namespace Microsoft.Data.Entity.FunctionalTests.TestModels
{
    public class SqlCeNorthwindContext : NorthwindContext
    {
        public SqlCeNorthwindContext(IServiceProvider serviceProvider, DbContextOptions options)
            : base(serviceProvider, options)
        {
        }
#if SQLCE35
        public static SqlCeTestStore GetSharedStore() => SqlCeTestStore.GetOrCreateShared("NorthwindEF735", () => { });
#else
        public static SqlCeTestStore GetSharedStore() => SqlCeTestStore.GetOrCreateShared("NorthwindEF7", () => { });
#endif
    }
}
