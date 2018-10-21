using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class GroupByQuerySqlCeTest : GroupByQueryTestBase<NorthwindQuerySqlCeFixture<NoopModelCustomizer>>
    {
        // ReSharper disable once UnusedParameter.Local
        public GroupByQuerySqlCeTest(NorthwindQuerySqlCeFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            Fixture.TestSqlLoggerFactory.Clear();
            //Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override async Task GroupBy_OrderBy_count_Select_sum(bool isAsync)
        {
            await base.GroupBy_OrderBy_count_Select_sum(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override void Select_nested_collection_with_groupby()
        {
            base.Select_nested_collection_with_groupby();
        }
    }
}
