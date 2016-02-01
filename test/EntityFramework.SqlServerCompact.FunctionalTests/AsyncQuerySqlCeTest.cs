using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.FunctionalTests.TestModels.Northwind;
using Xunit;

#pragma warning disable 1998

namespace Microsoft.EntityFrameworkCore.FunctionalTests
{
    public class AsyncQuerySqlServerTest : AsyncQueryTestBase<NorthwindQuerySqlCeFixture>
    {
        // TODO: Complex projection translation.

        public override async Task Projection_when_arithmetic_expressions()
        {
            //base.Projection_when_arithmetic_expressions();
        }

        public override async Task Projection_when_arithmetic_mixed()
        {
            //base.Projection_when_arithmetic_mixed();
        }

        public override async Task Projection_when_arithmetic_mixed_subqueries()
        {
            //base.Projection_when_arithmetic_mixed_subqueries();
        }

        //TODO ErikEJ Broken by recent query update in core https://github.com/aspnet/EntityFramework/issues/2626
        public override async Task OrderBy_correlated_subquery_lol()
        {
            //base.OrderBy_correlated_subquery_lol();
        }
        public override async Task Where_query_composition()
        {
            //base.Where_query_composition();
        }
        public override async Task Where_shadow_subquery_first()
        {
            //base.Where_shadow_subquery_first();
        }
        public override async Task Where_subquery_recursive_trivial()
        {
            //base.Where_subquery_recursive_trivial();
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
                cs => cs.Where(c => c.ContactName.Contains(LocalMethod1())),
                entryCount: 19);
        }

        [Fact]
        public async Task Single_Predicate_Cancellation()
        {
            await Assert.ThrowsAsync<TaskCanceledException>(async () =>
                await Single_Predicate_Cancellation(Fixture.CancelQuery()));
        }

        public AsyncQuerySqlServerTest(NorthwindQuerySqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}
