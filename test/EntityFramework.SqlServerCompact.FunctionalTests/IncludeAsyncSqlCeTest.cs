using Microsoft.EntityFrameworkCore.Query;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class IncludeAsyncSqlCeTest : IncludeAsyncTestBase<NorthwindQuerySqlCeFixture>
    {
        public IncludeAsyncSqlCeTest(NorthwindQuerySqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}


