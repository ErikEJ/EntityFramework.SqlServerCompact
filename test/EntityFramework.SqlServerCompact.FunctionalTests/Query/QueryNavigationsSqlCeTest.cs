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

        [Theory(Skip = "SQLCE limitation")]
        public override async Task Collection_select_nav_prop_predicate(bool isAsync)
        {
            await base.Collection_select_nav_prop_predicate(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Project_single_scalar_value_subquery_is_properly_inlined(bool isAsync)
        {
            return base.Project_single_scalar_value_subquery_is_properly_inlined(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Select_multiple_complex_projections(bool isAsync)
        {
            return base.Select_multiple_complex_projections(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Collection_where_nav_prop_sum(bool isAsync)
        {
            return base.Collection_where_nav_prop_sum(isAsync);
        }


        [Theory(Skip = "SQLCE limitation")]
        public override Task Collection_where_nav_prop_count_reverse(bool isAsync)
        {
            return base.Collection_where_nav_prop_count_reverse(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Select_count_plus_sum(bool isAsync)
        {
            return base.Select_count_plus_sum(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Collection_select_nav_prop_count(bool isAsync)
        {
            return base.Collection_select_nav_prop_count(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Collection_select_nav_prop_long_count(bool isAsync)
        {
            return base.Collection_select_nav_prop_long_count(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Collection_select_nav_prop_all(bool isAsync)
        {
            return base.Collection_select_nav_prop_all(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Collection_select_nav_prop_any(bool isAsync)
        {
            return base.Collection_select_nav_prop_any(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Collection_select_nav_prop_sum(bool isAsync)
        {
            return base.Collection_select_nav_prop_sum(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Collection_where_nav_prop_count(bool isAsync)
        {
            return base.Collection_where_nav_prop_count(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Collection_orderby_nav_prop_count(bool isAsync)
        {
            return base.Collection_orderby_nav_prop_count(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Collection_where_nav_prop_sum_async()
        {
            return base.Collection_where_nav_prop_sum_async();
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Collection_select_nav_prop_first_or_default_then_nav_prop_nested(bool isAsync)
        {
            return base.Collection_select_nav_prop_first_or_default_then_nav_prop_nested(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Collection_select_nav_prop_first_or_default_then_nav_prop_nested_with_orderby(bool isAsync)
        {
            return base.Collection_select_nav_prop_first_or_default_then_nav_prop_nested_with_orderby(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Collection_select_nav_prop_first_or_default_then_nav_prop_nested_using_property_method(bool isAsync)
        {
            return base.Collection_select_nav_prop_first_or_default_then_nav_prop_nested_using_property_method(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Navigation_with_collection_with_nullable_type_key(bool isAsync)
        {
            return base.Navigation_with_collection_with_nullable_type_key(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override void Navigation_in_subquery_referencing_outer_query_with_client_side_result_operator_and_count()
        {
            base.Navigation_in_subquery_referencing_outer_query_with_client_side_result_operator_and_count();
        }

        [Theory(Skip = "SQLCE limitation")]
        public override void Navigation_in_subquery_referencing_outer_query()
        {
            base.Navigation_in_subquery_referencing_outer_query();
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Select_collection_FirstOrDefault_project_single_column1(bool isAsync)
        {
            return base.Select_collection_FirstOrDefault_project_single_column1(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Select_collection_FirstOrDefault_project_single_column2(bool isAsync)
        {
            return base.Select_collection_FirstOrDefault_project_single_column2(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Project_single_scalar_value_subquery_in_query_with_optional_navigation_works(bool isAsync)
        {
            return base.Project_single_scalar_value_subquery_in_query_with_optional_navigation_works(isAsync);
        }
    }
}
