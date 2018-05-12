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

        [ConditionalFact(Skip = "SQLCE limitation")]
        public override void Select_join_with_key_selector_being_a_subquery()
        {
            base.Select_join_with_key_selector_being_a_subquery();
        }

        [ConditionalFact(Skip = "SQLCE limitation")]
        public override void Join_navigations_in_inner_selector_translated_to_multiple_subquery_without_collision()
        {
            base.Join_navigations_in_inner_selector_translated_to_multiple_subquery_without_collision();
        }

        [ConditionalFact(Skip = "SQLCE limitation")]
        public override void Join_navigation_in_inner_selector_translated_to_subquery()
        {
            base.Join_navigation_in_inner_selector_translated_to_subquery();
        }


        [ConditionalFact(Skip = "SQLCE limitation")]
        public override void GroupJoin_in_subquery_with_client_projection_nested1()
        {
            base.GroupJoin_in_subquery_with_client_projection_nested1();
        }

        [ConditionalFact(Skip = "SQLCE limitation")]
        public override void GroupJoin_in_subquery_with_client_projection_nested2()
        {
            base.GroupJoin_in_subquery_with_client_projection_nested2();
        }

        [ConditionalFact(Skip = "SQLCE limitation")]
        public override void GroupJoin_in_subquery_with_client_result_operator()
        {
            base.GroupJoin_in_subquery_with_client_result_operator();
        }

        [ConditionalFact(Skip = "SQLCE limitation")]
        public override void Explicit_GroupJoin_in_subquery_with_scalar_result_operator()
        {
            base.Explicit_GroupJoin_in_subquery_with_scalar_result_operator();
        }

        [ConditionalFact(Skip = "SQLCE limitation")]
        public override void Explicit_GroupJoin_in_subquery_with_multiple_result_operator_distinct_count_materializes_main_clause()
        {
            base.Explicit_GroupJoin_in_subquery_with_multiple_result_operator_distinct_count_materializes_main_clause();
        }

        [ConditionalFact(Skip = "SQLCE limitation")]
        public override void Member_doesnt_get_pushed_down_into_subquery_with_result_operator()
        {
            base.Member_doesnt_get_pushed_down_into_subquery_with_result_operator();
        }

        [Fact]
        public override void Simple_owned_level1()
        {
            base.Simple_owned_level1();

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[Id], [l1].[OneToOne_Required_PK_Date], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Level2_Name], [l1].[OneToOne_Optional_PK_InverseId]
FROM [Level1] AS [l1]");
        }

        [Fact]
        public override void Simple_owned_level1_convention()
        {
            base.Simple_owned_level1_convention();

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name]
FROM [Level1] AS [l]");
        }

        [Fact]
        public override void Simple_owned_level1_level2()
        {
            base.Simple_owned_level1_level2();

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[Id], [l1].[OneToOne_Required_PK_Date], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Level2_Name], [l1].[OneToOne_Optional_PK_InverseId], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Level3_Name], [l1].[Level3_OneToOne_Optional_PK_InverseId]
FROM [Level1] AS [l1]");
        }

        [Fact]
        public override void Simple_owned_level1_level2_level3()
        {
            base.Simple_owned_level1_level2_level3();

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[Id], [l1].[OneToOne_Required_PK_Date], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Level2_Name], [l1].[OneToOne_Optional_PK_InverseId], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Level3_Name], [l1].[Level3_OneToOne_Optional_PK_InverseId], [l1].[Id], [l1].[Level3_Optional_Id], [l1].[Level3_Required_Id], [l1].[Level4_Name], [l1].[Level4_OneToOne_Optional_PK_InverseId]
FROM [Level1] AS [l1]");
        }

        [Fact]
        public override void Level4_Include()
        {
            base.Level4_Include();
        }

        [ConditionalFact(Skip = "issue #4311")]
        public override void Nested_group_join_with_take()
        {
            base.Nested_group_join_with_take();
        }

        [ConditionalFact]
        public override void Explicit_GroupJoin_in_subquery_with_unrelated_projection2()
        {
            base.Explicit_GroupJoin_in_subquery_with_unrelated_projection2();

            AssertSql(
                @"SELECT [t1].[Id]
FROM (
    SELECT DISTINCT [l1].*
    FROM [Level1] AS [l1]
    LEFT JOIN (
        SELECT [t].*
        FROM [Level1] AS [t]
        WHERE [t].[Id] IS NOT NULL
    ) AS [t0] ON [l1].[Id] = [t0].[Level1_Optional_Id]
    WHERE ([t0].[Level2_Name] <> N'Foo') OR [t0].[Level2_Name] IS NULL
) AS [t1]");
        }

        [ConditionalFact]
        public override void Result_operator_nav_prop_reference_optional_via_DefaultIfEmpty()
        {
            base.Result_operator_nav_prop_reference_optional_via_DefaultIfEmpty();

            AssertSql(
                @"SELECT SUM(CASE
    WHEN [t0].[Id] IS NULL
    THEN 0 ELSE [t0].[Level1_Required_Id]
END)
FROM [Level1] AS [l1]
LEFT JOIN (
    SELECT [t].*
    FROM [Level1] AS [t]
    WHERE [t].[Id] IS NOT NULL
) AS [t0] ON [l1].[Id] = [t0].[Level1_Optional_Id]");
        }

        private void AssertSql(params string[] expected)
        {
            //string[] expectedFixed = new string[expected.Length];
            //int i = 0;
            //foreach (var item in expected)
            //{
            //    expectedFixed[i++] = item.Replace("\r\n", "\n");
            //}
            Fixture.TestSqlLoggerFactory.AssertBaseline(expected);
        }
    }
}
