using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class IncludeAsyncSqlCeTest : IncludeAsyncTestBase<NorthwindQuerySqlCeFixture<NoopModelCustomizer>>
    {
        public IncludeAsyncSqlCeTest(NorthwindQuerySqlCeFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }
    }
}


