using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class NorthwindQueryWithForcedClientEvalSqlCeFixture : NorthwindQuerySqlCeFixture<NoopModelCustomizer>
    {
        protected override ITestStoreFactory TestStoreFactory => SqlCeNorthwindTestStoreFactory.Instance;

        public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
        {
            var optionsBuilder = base.AddOptions(builder);
            new SqlCeDbContextOptionsBuilder(optionsBuilder).UseClientEvalForUnsupportedSqlConstructs();
            return optionsBuilder;
        }
    }
}
