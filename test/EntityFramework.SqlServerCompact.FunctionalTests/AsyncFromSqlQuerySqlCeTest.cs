namespace Microsoft.EntityFrameworkCore.FunctionalTests
{
    public class AsyncFromSqlQuerySqlCeTest : AsyncFromSqlQueryTestBase<NorthwindQuerySqlCeFixture>
    {
        public AsyncFromSqlQuerySqlCeTest(NorthwindQuerySqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}
