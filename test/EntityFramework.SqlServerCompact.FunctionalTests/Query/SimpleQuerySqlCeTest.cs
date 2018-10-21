using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;
using Xunit.Abstractions;

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

        [Fact(Skip = "SQLCE limitation")]
        public override async Task Select_bool_closure_with_order_by_property_with_cast_to_nullable(bool isAsync)
        {
            await base.Select_bool_closure_with_order_by_property_with_cast_to_nullable(isAsync);
        }

        //[Fact(Skip = "SQLCE limitation")]
        //public override void Let_entity_equality_to_other_entity()
        //{
        //    base.Let_entity_equality_to_other_entity();
        //}

        //[Fact(Skip = "SQLCE limitation")]
        //public override void Let_entity_equality_to_null()
        //{
        //    base.Let_entity_equality_to_null();
        //}

        //[Fact(Skip = "SQLCE limitation")]
        //public override void Let_subquery_with_multiple_occurences()
        //{
        //    base.Let_subquery_with_multiple_occurences();
        //}

        //[Fact(Skip = "SQLCE limitation")]
        //public override void SelectMany_primitive_select_subquery()
        //{
        //    base.SelectMany_primitive_select_subquery();
        //}

        //[Fact(Skip = "SQLCE limitation")]
        //public override void Where_subquery_FirstOrDefault_is_null()
        //{
        //    base.Where_subquery_FirstOrDefault_is_null();
        //}

        //[Fact(Skip = "SQLCE limitation")]
        //public override void Project_single_element_from_collection_with_multiple_OrderBys_Take_and_FirstOrDefault_2()
        //{
        //    base.Project_single_element_from_collection_with_multiple_OrderBys_Take_and_FirstOrDefault_2();
        //}

        //[Fact(Skip = "SQLCE limitation")]
        //public override void Project_single_element_from_collection_with_OrderBy_Take_and_FirstOrDefault_with_parameter()
        //{
        //    base.Project_single_element_from_collection_with_OrderBy_Take_and_FirstOrDefault_with_parameter();
        //}

        //[Fact(Skip = "SQLCE limitation")]
        //public override void Project_single_element_from_collection_with_OrderBy_over_navigation_Take_and_FirstOrDefault()
        //{
        //    base.Project_single_element_from_collection_with_OrderBy_over_navigation_Take_and_FirstOrDefault();
        //}

        //[Fact(Skip = "SQLCE limitation")]
        //public override void Project_single_element_from_collection_with_OrderBy_Skip_and_FirstOrDefault()
        //{
        //    base.Project_single_element_from_collection_with_OrderBy_Skip_and_FirstOrDefault();
        //}

        //[Fact(Skip = "SQLCE limitation")]
        //public override void Project_single_element_from_collection_with_OrderBy_Take_and_FirstOrDefault()
        //{
        //    base.Project_single_element_from_collection_with_OrderBy_Take_and_FirstOrDefault();
        //}

        //[Fact(Skip = "SQLCE limitation")]
        //public override void Project_single_element_from_collection_with_multiple_OrderBys_Take_and_FirstOrDefault()
        //{
        //    base.Project_single_element_from_collection_with_multiple_OrderBys_Take_and_FirstOrDefault();
        //}

        //[Fact(Skip = "SQLCE limitation")]
        //public override void Project_single_element_from_collection_with_OrderBy_Distinct_and_FirstOrDefault()
        //{
        //    base.Project_single_element_from_collection_with_OrderBy_Distinct_and_FirstOrDefault();
        //}

        private void AssertSql(params string[] expected)
            => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);

        private void AssertContainsSql(params string[] expected)
            => Fixture.TestSqlLoggerFactory.AssertBaseline(expected, assertOrder: false);

        protected override void ClearLog()
            => Fixture.TestSqlLoggerFactory.Clear();
    }
}
