using Microsoft.EntityFrameworkCore.TestModels.Northwind;

namespace Microsoft.EntityFrameworkCore.Specification.Tests.TestModels
{
    public class SqlCeNorthwindContext : NorthwindContext
    {
        public static readonly string DatabaseName = SqlCeStoreName;
        public static readonly string ConnectionString = SqlCeTestStore.CreateConnectionString(DatabaseName);

        public SqlCeNorthwindContext(DbContextOptions options,
           QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll)
            : base(options, queryTrackingBehavior)
        {
        }
#if SQLCE35
        private const string SqlCeStoreName = "NorthwindEF735";
        public static SqlCeTestStore GetSharedStore() => SqlCeTestStore.GetOrCreateShared(SqlCeStoreName, () => { });
#else
        private const string SqlCeStoreName = "NorthwindEF7";
        public static SqlCeTestStore GetSharedStore() => SqlCeTestStore.GetOrCreateShared(SqlCeStoreName, () => { });
#endif
    }
}
