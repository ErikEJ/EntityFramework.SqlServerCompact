using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class AsyncGearsOfWarQuerySqlCeTest : AsyncGearsOfWarQueryTestBase<GearsOfWarQuerySqlCeFixture>
    {
        public AsyncGearsOfWarQuerySqlCeTest(GearsOfWarQuerySqlCeFixture fixture, ITestOutputHelper testOutputHelper)
        : base(fixture)
        {
            Fixture.TestSqlLoggerFactory.Clear();
            //Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
        }
    }
}
