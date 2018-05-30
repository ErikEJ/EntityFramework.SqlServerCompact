using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class AsyncFromSqlQuerySqlCeTest : AsyncFromSqlQueryTestBase<NorthwindQuerySqlCeFixture<NoopModelCustomizer>>
    {
        public AsyncFromSqlQuerySqlCeTest(NorthwindQuerySqlCeFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }
    }
}
