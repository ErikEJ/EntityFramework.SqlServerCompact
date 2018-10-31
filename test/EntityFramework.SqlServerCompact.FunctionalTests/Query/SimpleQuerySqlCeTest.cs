using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using Xunit;
using Xunit.Abstractions;
using System.Linq;

// ReSharper disable UnusedParameter.Local
// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore.Query
{
    public partial class SimpleQuerySqlCeTest : SimpleQueryTestBase<NorthwindQuerySqlCeFixture<NoopModelCustomizer>>
    {
        public SimpleQuerySqlCeTest(NorthwindQuerySqlCeFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            Fixture.TestSqlLoggerFactory.Clear();
            //Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
        }

        public override async Task String_Contains_MethodCall(bool isAsync)
        {
            await AssertQuery<Customer>(
                isAsync,
                cs => cs.Where(c => c.ContactName.Contains(LocalMethod1())), // case-insensitive
                cs => cs.Where(c => c.ContactName.Contains(LocalMethod1().ToLower()) || c.ContactName.Contains(LocalMethod1().ToUpper())), // case-sensitive
                entryCount: 34);
        }

        public override async Task String_Contains_Literal(bool isAsync)
        {
            await AssertQuery<Customer>(
                isAsync,
                cs => cs.Where(c => c.ContactName.Contains("M")), // case-insensitive
                cs => cs.Where(c => c.ContactName.Contains("M") || c.ContactName.Contains("m")), // case-sensitive
                entryCount: 34);
        }

        //TODO ErikEJ Investigate
        [Theory(Skip = "Investigate")]
        public override Task OrderBy_coalesce_take_distinct(bool isAsync)
        {
            return base.OrderBy_coalesce_take_distinct(isAsync);
        }

        //TODO ErikEJ Investigate - https://github.com/aspnet/EntityFrameworkCore/issues/13786
        [Theory(Skip = "Investigate")]
        public override Task Average_over_nested_subquery_is_client_eval(bool isAsync)
        {
            return base.Average_over_nested_subquery_is_client_eval(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override async Task Select_bool_closure_with_order_by_property_with_cast_to_nullable(bool isAsync)
        {
            await base.Select_bool_closure_with_order_by_property_with_cast_to_nullable(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task OrderBy_correlated_subquery2(bool isAsync)
        {
            return base.OrderBy_correlated_subquery2(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Average_on_float_column_in_subquery_with_cast(bool isAsync)
        {
            return base.Average_on_float_column_in_subquery_with_cast(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Project_single_element_from_collection_with_OrderBy_over_navigation_Take_and_FirstOrDefault(bool isAsync)
        {
            return base.Project_single_element_from_collection_with_OrderBy_over_navigation_Take_and_FirstOrDefault(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Let_entity_equality_to_other_entity(bool isAsync)
        {
            return base.Let_entity_equality_to_other_entity(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Project_single_element_from_collection_with_OrderBy_Distinct_and_FirstOrDefault(bool isAsync)
        {
            return base.Project_single_element_from_collection_with_OrderBy_Distinct_and_FirstOrDefault(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Project_single_element_from_collection_with_multiple_OrderBys_Take_and_FirstOrDefault_2(bool isAsync)
        {
            return base.Project_single_element_from_collection_with_multiple_OrderBys_Take_and_FirstOrDefault_2(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task OrderBy_empty_list_does_not_contains(bool isAsync)
        {
            return base.OrderBy_empty_list_does_not_contains(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Let_entity_equality_to_null(bool isAsync)
        {
            return base.Let_entity_equality_to_null(isAsync);
        }


        [Theory(Skip = "SQLCE limitation")]
        public override Task Where_subquery_FirstOrDefault_is_null(bool isAsync)
        {
            return base.Where_subquery_FirstOrDefault_is_null(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Subquery_is_not_null_translated_correctly(bool isAsync)
        {
            return base.Subquery_is_not_null_translated_correctly(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Min_over_subquery_is_client_eval(bool isAsync)
        {
            return base.Min_over_subquery_is_client_eval(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task OrderBy_coalesce_skip_take_distinct_take(bool isAsync)
        {
            return base.OrderBy_coalesce_skip_take_distinct_take(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Collection_navigation_equal_to_null_for_subquery(bool isAsync)
        {
            return base.Collection_navigation_equal_to_null_for_subquery(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Sum_over_subquery_is_client_eval(bool isAsync)
        {
            return base.Sum_over_subquery_is_client_eval(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override void Select_nested_collection_multi_level4()
        {
            base.Select_nested_collection_multi_level4();
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Project_single_element_from_collection_with_OrderBy_Skip_and_FirstOrDefault(bool isAsync)
        {
            return base.Project_single_element_from_collection_with_OrderBy_Skip_and_FirstOrDefault(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Let_subquery_with_multiple_occurences(bool isAsync)
        {
            return base.Let_subquery_with_multiple_occurences(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Sum_on_float_column_in_subquery(bool isAsync)
        {
            return base.Sum_on_float_column_in_subquery(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task OrderBy_empty_list_contains(bool isAsync)
        {
            return base.OrderBy_empty_list_contains(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Project_single_element_from_collection_with_multiple_OrderBys_Take_and_FirstOrDefault(bool isAsync)
        {
            return base.Project_single_element_from_collection_with_multiple_OrderBys_Take_and_FirstOrDefault(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Dependent_to_principal_navigation_equal_to_null_for_subquery(bool isAsync)
        {
            return base.Dependent_to_principal_navigation_equal_to_null_for_subquery(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Select_bool_closure_with_order_parameter_with_cast_to_nullable(bool isAsync)
        {
            return base.Select_bool_closure_with_order_parameter_with_cast_to_nullable(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task DTO_subquery_orderby(bool isAsync)
        {
            return base.DTO_subquery_orderby(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override void OrderBy_any()
        {
            base.OrderBy_any();
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task OrderBy_coalesce_skip_take_distinct(bool isAsync)
        {
            return base.OrderBy_coalesce_skip_take_distinct(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Anonymous_subquery_orderby(bool isAsync)
        {
            return base.Anonymous_subquery_orderby(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Collection_navigation_equality_rewrite_for_subquery(bool isAsync)
        {
            return base.Collection_navigation_equality_rewrite_for_subquery(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Max_over_subquery_is_client_eval(bool isAsync)
        {
            return base.Max_over_subquery_is_client_eval(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override void Select_nested_collection_multi_level3()
        {
            base.Select_nested_collection_multi_level3();
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Max_over_sum_subquery_is_client_eval(bool isAsync)
        {
            return base.Max_over_sum_subquery_is_client_eval(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override void Select_Where_Subquery_Deep_First()
        {
            base.Select_Where_Subquery_Deep_First();
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Project_single_element_from_collection_with_OrderBy_Take_and_FirstOrDefault(bool isAsync)
        {
            return base.Project_single_element_from_collection_with_OrderBy_Take_and_FirstOrDefault(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Complex_nested_query_doesnt_try_binding_to_grandparent_when_parent_returns_complex_result(bool isAsync)
        {
            return base.Complex_nested_query_doesnt_try_binding_to_grandparent_when_parent_returns_complex_result(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Complex_nested_query_properly_binds_to_grandparent_when_parent_returns_scalar_result(bool isAsync)
        {
            return base.Complex_nested_query_properly_binds_to_grandparent_when_parent_returns_scalar_result(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override void Select_nested_collection_multi_level6()
        {
            base.Select_nested_collection_multi_level6();
        }

        [Theory(Skip = "SQLCE limitation")]
        public override void Select_nested_collection_count_using_DTO()
        {
            base.Select_nested_collection_count_using_DTO();
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Select_nested_collection_count_using_anonymous_type(bool isAsync)
        {
            return base.Select_nested_collection_count_using_anonymous_type(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task SelectMany_primitive_select_subquery(bool isAsync)
        {
            return base.SelectMany_primitive_select_subquery(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task OrderBy_correlated_subquery1(bool isAsync)
        {
            return base.OrderBy_correlated_subquery1(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Average_over_subquery_is_client_eval(bool isAsync)
        {
            return base.Average_over_subquery_is_client_eval(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Sum_over_nested_subquery_is_client_eval(bool isAsync)
        {
            return base.Sum_over_nested_subquery_is_client_eval(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Project_single_element_from_collection_with_OrderBy_Take_and_FirstOrDefault_with_parameter(bool isAsync)
        {
            return base.Project_single_element_from_collection_with_OrderBy_Take_and_FirstOrDefault_with_parameter(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override void Select_nested_collection_multi_level5()
        {
            base.Select_nested_collection_multi_level5();
        }

        [Theory(Skip = "SQLCE limitation")]
        public override void Select_nested_collection_multi_level2()
        {
            base.Select_nested_collection_multi_level2();
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Subquery_is_null_translated_correctly(bool isAsync)
        {
            return base.Subquery_is_null_translated_correctly(isAsync);
        }

        [Fact(Skip = "SQLCE limitation - views not supported")]
        public override void Query_backed_by_database_view()
        {
            base.Query_backed_by_database_view();
        }

        [Fact(Skip = "SQLCE limitation")]
        public override void QueryType_with_nav_defining_query()
        {
            base.QueryType_with_nav_defining_query();
        }
    }
}
