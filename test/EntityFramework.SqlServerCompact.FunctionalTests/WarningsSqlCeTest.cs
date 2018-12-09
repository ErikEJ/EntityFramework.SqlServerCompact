﻿using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class WarningsSqlCeTest : WarningsTestBase<QueryNoClientEvalSqlCeFixture>
    {
        public WarningsSqlCeTest(QueryNoClientEvalSqlCeFixture fixture)
            : base(fixture)
        {
            fixture.TestSqlLoggerFactory.Clear();
        }

        [Fact(Skip = "SQLCE limitation")]
        public override void LastOrDefault_with_order_by_does_not_issue_client_eval_warning()
        {
            base.LastOrDefault_with_order_by_does_not_issue_client_eval_warning();
        }

        public override void Does_not_throw_for_top_level_single()
        {
            base.Does_not_throw_for_top_level_single();

            Assert.Equal(
                @"SELECT TOP(2) [x].[OrderID], [x].[CustomerID], [x].[EmployeeID], [x].[OrderDate]
FROM [Orders] AS [x]
WHERE [x].[OrderID] = 10248",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Last_without_order_by_issues_client_eval_warning()
        {
            base.Last_without_order_by_issues_client_eval_warning();

            Assert.Contains(
                CoreStrings.LogFirstWithoutOrderByAndFilter.GenerateMessage(
                    "(from Customer <generated>_1 in DbSet<Customer> select [<generated>_1]).Last()"),
                Fixture.TestSqlLoggerFactory.Log.Select(l => l.Message));

        }

        public override void Paging_operation_without_orderby_issues_warning()
        {
            base.Paging_operation_without_orderby_issues_warning();

            Assert.Contains(
                CoreStrings.LogRowLimitingOperationWithoutOrderBy.GenerateMessage(
                    "(from Customer <generated>_2 in DbSet<Customer> select [<generated>_2]).Skip(__p_0).Take(__p_1)"),
                Fixture.TestSqlLoggerFactory.Log.Select(l => l.Message));
        }

        [Fact(Skip="SQLCE limitation")]
        public override void FirstOrDefault_without_orderby_and_filter_issues_warning_subquery()
        {
            base.FirstOrDefault_without_orderby_and_filter_issues_warning_subquery();

            Assert.Contains(
                CoreStrings.LogFirstWithoutOrderByAndFilter.GenerateMessage(
                    "(from Order <generated>_1 in [c].Orders select (Nullable<int>)[<generated>_1].OrderID).FirstOrDefaul..."),
                Fixture.TestSqlLoggerFactory.Log.Select(l => l.Message));
        }

        public override void FirstOrDefault_without_orderby_but_with_filter_doesnt_issue_warning()
        {
            base.FirstOrDefault_without_orderby_but_with_filter_doesnt_issue_warning();

            Assert.DoesNotContain(
                CoreStrings.LogFirstWithoutOrderByAndFilter.GenerateMessage(
                    @"(from Customer c in DbSet<Customer> where c.CustomerID == ""ALFKI"" select c).FirstOrDefault()"),
                Fixture.TestSqlLoggerFactory.Log.Select(l => l.Message));
        }

        public override void Single_SingleOrDefault_without_orderby_doesnt_issue_warning()
        {
            base.Single_SingleOrDefault_without_orderby_doesnt_issue_warning();

            Assert.DoesNotContain(
                CoreStrings.LogFirstWithoutOrderByAndFilter.GenerateMessage(
                    @"(from Customer c in DbSet<Customer> where c.CustomerID == ""ALFKI"" select c).Single()"),
                Fixture.TestSqlLoggerFactory.Log.Select(l => l.Message));
        }

        public override void Comparing_collection_navigation_to_null_issues_possible_unintended_consequences_warning()
        {
            base.Comparing_collection_navigation_to_null_issues_possible_unintended_consequences_warning();

            Assert.Contains(CoreStrings.LogPossibleUnintendedCollectionNavigationNullComparison.GenerateMessage("Orders"),
                Fixture.TestSqlLoggerFactory.Log.Select(l => l.Message));
        }

        public override void Comparing_two_collections_together_issues_possible_unintended_reference_comparison_warning()
        {
            base.Comparing_two_collections_together_issues_possible_unintended_reference_comparison_warning();

            Assert.Contains(CoreStrings.LogPossibleUnintendedReferenceComparison.GenerateMessage("[c].Orders", "[c].Orders"),
                Fixture.TestSqlLoggerFactory.Log.Select(l => l.Message));
        }

        private string Sql => Fixture.TestSqlLoggerFactory.Sql;
    }
}
