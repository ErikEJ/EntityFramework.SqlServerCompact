using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class QueryNavigationsSqlCeTest : QueryNavigationsTestBase<NorthwindQuerySqlCeFixture<NoopModelCustomizer>>
    {
        public QueryNavigationsSqlCeTest(
                NorthwindQuerySqlCeFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
                : base(fixture)
        {
            fixture.TestSqlLoggerFactory.Clear();
        }

        //[Fact(Skip = "SQLCE limitation")]
        //public override void Navigation_with_collection_with_nullable_type_key()
        //{
        //    base.Navigation_with_collection_with_nullable_type_key();
        //}

        //[Fact(Skip = "SQLCE limitation")]
        //public override void Navigation_in_subquery_referencing_outer_query_with_client_side_result_operator_and_count()
        //{
        //    base.Navigation_in_subquery_referencing_outer_query_with_client_side_result_operator_and_count();
        //}

        //[Fact(Skip = "SQLCE limitation")]
        //public override Task Collection_where_nav_prop_sum_async()
        //{
        //    return Task.CompletedTask;
        //}

    }
}
