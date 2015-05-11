using Microsoft.Data.Entity.FunctionalTests;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class IncludeAsyncSqliteTest : IncludeAsyncTestBase<NorthwindQuerySqlServerCeFixture>
    {
        public IncludeAsyncSqliteTest(NorthwindQuerySqlServerCeFixture fixture)
            : base(fixture)
        {
        }
    }
}


