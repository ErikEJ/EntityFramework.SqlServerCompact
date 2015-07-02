using Microsoft.Data.Entity.FunctionalTests;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class AsyncFromSqlQuerySqlCeTest : AsyncFromSqlQueryTestBase<NorthwindQuerySqlCeFixture>
    {
        public AsyncFromSqlQuerySqlCeTest(NorthwindQuerySqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}
