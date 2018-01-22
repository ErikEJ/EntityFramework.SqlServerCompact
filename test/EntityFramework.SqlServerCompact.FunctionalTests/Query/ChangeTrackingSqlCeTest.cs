using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class ChangeTrackingSqlCeTest : ChangeTrackingTestBase<NorthwindQuerySqlCeFixture<NoopModelCustomizer>>
    {
        public ChangeTrackingSqlCeTest(NorthwindQuerySqlCeFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }
    }
}


