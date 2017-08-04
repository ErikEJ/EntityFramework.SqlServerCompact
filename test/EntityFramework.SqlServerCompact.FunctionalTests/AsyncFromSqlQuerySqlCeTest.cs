using Microsoft.EntityFrameworkCore.Query;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class AsyncFromSqlQuerySqlCeTest : AsyncFromSqlQueryTestBase<NorthwindQuerySqlCeFixture>
    {
        public AsyncFromSqlQuerySqlCeTest(NorthwindQuerySqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}
