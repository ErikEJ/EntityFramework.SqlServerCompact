using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class ComplexNavigationsWeakQuerySqlCeTest : ComplexNavigationsWeakQueryTestBase<ComplexNavigationsWeakQuerySqlCeFixture>
    {
        public ComplexNavigationsWeakQuerySqlCeTest(
            ComplexNavigationsWeakQuerySqlCeFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            Fixture.TestSqlLoggerFactory.Clear();
            //Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override async Task Select_join_with_key_selector_being_a_subquery(bool isAsync)
        {
            await base.Select_join_with_key_selector_being_a_subquery(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override async Task Where_navigation_property_to_collection2(bool isAsync)
        {
            await base.Where_navigation_property_to_collection2(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override async Task Where_navigation_property_to_collection(bool isAsync)
        {
            await base.Where_navigation_property_to_collection(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override async Task Where_navigation_property_to_collection_of_original_entity_type(bool isAsync)
        {
            await base.Where_navigation_property_to_collection_of_original_entity_type(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override async Task Join_navigation_in_inner_selector_translated_to_subquery(bool isAsync)
        {
            await base.Join_navigation_in_inner_selector_translated_to_subquery(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override async Task GroupJoin_in_subquery_with_client_result_operator(bool isAsync)
        {
            await base.GroupJoin_in_subquery_with_client_result_operator(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override async Task GroupJoin_in_subquery_with_client_projection_nested2(bool isAsync)
        {
            await base.GroupJoin_in_subquery_with_client_projection_nested2(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override async Task Join_navigations_in_inner_selector_translated_to_multiple_subquery_without_collision(bool isAsync)
        {
            await base.Join_navigations_in_inner_selector_translated_to_multiple_subquery_without_collision(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override async Task Project_collection_navigation_count(bool isAsync)
        {
            await base.Project_collection_navigation_count(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override async Task Explicit_GroupJoin_in_subquery_with_scalar_result_operator(bool isAsync)
        {
            await base.Explicit_GroupJoin_in_subquery_with_scalar_result_operator(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override async Task Explicit_GroupJoin_in_subquery_with_multiple_result_operator_distinct_count_materializes_main_clause(bool isAsync)
        {
            await base.Explicit_GroupJoin_in_subquery_with_multiple_result_operator_distinct_count_materializes_main_clause(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override async Task Member_doesnt_get_pushed_down_into_subquery_with_result_operator(bool isAsync)
        {
            await base.Member_doesnt_get_pushed_down_into_subquery_with_result_operator(isAsync);
        }
    }
}
