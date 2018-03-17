using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class AsTrackingSqlCeTest : AsTrackingTestBase<NorthwindQuerySqlCeFixture<NoopModelCustomizer>>
    {
        public AsTrackingSqlCeTest(NorthwindQuerySqlCeFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }
    }
}
