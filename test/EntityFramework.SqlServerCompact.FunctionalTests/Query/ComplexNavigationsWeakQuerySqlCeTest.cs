using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;
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

        //[ConditionalFact(Skip = "SQLCE limitation")]
        //public override void Select_join_with_key_selector_being_a_subquery()
        //{
        //    base.Select_join_with_key_selector_being_a_subquery();
        //}

        //[ConditionalFact(Skip = "SQLCE limitation")]
        //public override void Join_navigations_in_inner_selector_translated_to_multiple_subquery_without_collision()
        //{
        //    base.Join_navigations_in_inner_selector_translated_to_multiple_subquery_without_collision();
        //}

        //[ConditionalFact(Skip = "SQLCE limitation")]
        //public override void Join_navigation_in_inner_selector_translated_to_subquery()
        //{
        //    base.Join_navigation_in_inner_selector_translated_to_subquery();
        //}


        //[ConditionalFact(Skip = "SQLCE limitation")]
        //public override void GroupJoin_in_subquery_with_client_projection_nested1()
        //{
        //    base.GroupJoin_in_subquery_with_client_projection_nested1();
        //}

        //[ConditionalFact(Skip = "SQLCE limitation")]
        //public override void GroupJoin_in_subquery_with_client_projection_nested2()
        //{
        //    base.GroupJoin_in_subquery_with_client_projection_nested2();
        //}

        //[ConditionalFact(Skip = "SQLCE limitation")]
        //public override void GroupJoin_in_subquery_with_client_result_operator()
        //{
        //    base.GroupJoin_in_subquery_with_client_result_operator();
        //}

        //[ConditionalFact(Skip = "SQLCE limitation")]
        //public override void Explicit_GroupJoin_in_subquery_with_scalar_result_operator()
        //{
        //    base.Explicit_GroupJoin_in_subquery_with_scalar_result_operator();
        //}

        //[ConditionalFact(Skip = "SQLCE limitation")]
        //public override void Explicit_GroupJoin_in_subquery_with_multiple_result_operator_distinct_count_materializes_main_clause()
        //{
        //    base.Explicit_GroupJoin_in_subquery_with_multiple_result_operator_distinct_count_materializes_main_clause();
        //}

        //[ConditionalFact(Skip = "SQLCE limitation")]
        //public override void Member_doesnt_get_pushed_down_into_subquery_with_result_operator()
        //{
        //    base.Member_doesnt_get_pushed_down_into_subquery_with_result_operator();
        //}

    }
}
