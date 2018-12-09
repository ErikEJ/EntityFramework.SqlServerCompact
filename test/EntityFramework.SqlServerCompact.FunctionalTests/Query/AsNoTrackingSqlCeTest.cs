using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class AsNoTrackingSqlCeTest : AsNoTrackingTestBase<NorthwindQuerySqlCeFixture<NoopModelCustomizer>>
    {
        public AsNoTrackingSqlCeTest(NorthwindQuerySqlCeFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }       
    }
}
