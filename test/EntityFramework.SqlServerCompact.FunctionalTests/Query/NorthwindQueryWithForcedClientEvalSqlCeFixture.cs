using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class NorthwindQueryWithForcedClientEvalSqlCeFixture<TModelCustomizer> : NorthwindQueryRelationalFixture<TModelCustomizer>
        where TModelCustomizer : IModelCustomizer, new()
    {
        protected override ITestStoreFactory TestStoreFactory => SqlCeNorthwindTestStoreFactory.Instance;

        public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
        {
            builder.UseSqlCe(
                    TestStore.ConnectionString,
                    b =>
                    {
                        b.UseClientEvalForUnsupportedSqlConstructs(true);
                    });
            return base.AddOptions(builder);
        }
    }
}
