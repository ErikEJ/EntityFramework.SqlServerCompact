using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class ChangeTrackingSqlCeTest : ChangeTrackingTestBase<NorthwindQuerySqlCeFixture<NoopModelCustomizer>>
    {
        public ChangeTrackingSqlCeTest(NorthwindQuerySqlCeFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }

        protected override NorthwindContext CreateNoTrackingContext()
            => new NorthwindRelationalContext(
                new DbContextOptionsBuilder(Fixture.CreateOptions())
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options);
    }
}


