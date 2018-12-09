using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class QueryFilterFuncletizationSqlCeTest
        : QueryFilterFuncletizationTestBase<QueryFilterFuncletizationSqlCeTest.QueryFilterFuncletizationSqlCeFixture>
    {
        public QueryFilterFuncletizationSqlCeTest(
            QueryFilterFuncletizationSqlCeFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            Fixture.TestSqlLoggerFactory.Clear();
            //Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
        }

        public class QueryFilterFuncletizationSqlCeFixture : QueryFilterFuncletizationRelationalFixture
        {
            protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;
        }
    }
}
