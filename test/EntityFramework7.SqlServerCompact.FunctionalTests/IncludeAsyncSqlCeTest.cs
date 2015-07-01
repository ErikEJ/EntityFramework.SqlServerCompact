using Microsoft.Data.Entity.FunctionalTests;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class IncludeAsyncSqlCeTest : IncludeAsyncTestBase<NorthwindQuerySqlCeFixture>
    {
        public IncludeAsyncSqlCeTest(NorthwindQuerySqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}


