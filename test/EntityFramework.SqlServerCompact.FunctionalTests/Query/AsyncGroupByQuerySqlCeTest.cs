using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class AsyncGroupByQuerySqlCeTest : AsyncGroupByQueryTestBase<NorthwindQuerySqlCeFixture<NoopModelCustomizer>>
    {
        // ReSharper disable once UnusedParameter.Local
        public AsyncGroupByQuerySqlCeTest(NorthwindQuerySqlCeFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            Fixture.TestSqlLoggerFactory.Clear();
            //Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
        }

        //[Fact(Skip= "SQLCE limitation")]
        //public override async Task Select_nested_collection_with_groupby()
        //{
        //    await base.Select_nested_collection_with_groupby();
        //}

        //[Fact(Skip = "SQLCE limitation")]
        //public override async Task GroupBy_OrderBy_count_Select_sum()
        //{
        //    await base.GroupBy_OrderBy_count_Select_sum();
        //}
    }
}
