using Microsoft.EntityFrameworkCore.FunctionalTests.TestModels.Northwind;

namespace Microsoft.EntityFrameworkCore.FunctionalTests.TestModels
{
    public class SqlCeNorthwindContext : NorthwindContext
    {
        public SqlCeNorthwindContext(DbContextOptions options)
            : base(options)
        {
        }
#if SQLCE35
        public static SqlCeTestStore GetSharedStore() => SqlCeTestStore.GetOrCreateShared("NorthwindEF735", () => { });
#else
        public static SqlCeTestStore GetSharedStore() => SqlCeTestStore.GetOrCreateShared("NorthwindEF7", () => { });
#endif
    }
}
