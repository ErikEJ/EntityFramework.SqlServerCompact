using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Specification.Tests.TestModels.Northwind;
using Xunit;

#pragma warning disable 1998

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class AsyncQuerySqlCeTest : AsyncQueryTestBase<NorthwindQuerySqlCeFixture>
    {
        [Fact(Skip = "SQL CE limitation")]
        public override async Task Sum_over_subquery_is_client_eval()
        {
            //return base.Sum_over_subquery_is_client_eval();
        }

        [Fact(Skip = "SQL CE limitation")]
        public override async Task Min_over_subquery_is_client_eval()
        {
            //return base.Min_over_subquery_is_client_eval();
        }

        [Fact(Skip = "SQL CE limitation")]
        public override async Task Max_over_subquery_is_client_eval()
        {
            //return base.Max_over_subquery_is_client_eval();
        }

        [Fact(Skip = "SQL CE limitation")]
        public override async Task Average_over_subquery_is_client_eval()
        {
            //return base.Average_over_subquery_is_client_eval();
        }

        [Fact(Skip = "SQL CE limitation")]
        public override async Task OrderBy_correlated_subquery_lol()
        {
            //return base.OrderBy_correlated_subquery_lol();
        }

        [Fact(Skip = "SQL CE limitation")]
        public override async Task SelectMany_primitive_select_subquery()
        {
            //return base.SelectMany_primitive_select_subquery();
        }

        public override async Task String_Contains_Literal()
        {
            await AssertQuery<Customer>(
                cs => cs.Where(c => c.ContactName.Contains("M")), // case-insensitive
                cs => cs.Where(c => c.ContactName.Contains("M")
                                     || c.ContactName.Contains("m")), // case-sensitive
                entryCount: 34);
        }

        public override async Task String_Contains_MethodCall()
        {
            await AssertQuery<Customer>(
                cs => cs.Where(c => c.ContactName.Contains(LocalMethod1())), // case-insensitive
                cs => cs.Where(c => c.ContactName.Contains(LocalMethod1().ToLower()) || c.ContactName.Contains(LocalMethod1().ToUpper())), // case-sensitive
                entryCount: 34);
        }

        [Fact]
        public async Task Single_Predicate_Cancellation()
        {
            await Assert.ThrowsAsync<TaskCanceledException>(async () =>
                await Single_Predicate_Cancellation(Fixture.TestSqlLoggerFactory.CancelQuery()));
        }

        public AsyncQuerySqlCeTest(NorthwindQuerySqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}
