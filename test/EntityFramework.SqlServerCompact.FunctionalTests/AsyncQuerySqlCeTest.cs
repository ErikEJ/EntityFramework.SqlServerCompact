using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Specification.Tests.TestModels.Northwind;
using Xunit;

#pragma warning disable 1998

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class AsyncQuerySqlServerTest : AsyncQueryTestBase<NorthwindQuerySqlCeFixture>
    {
        //TODO ErikEJ await fix Broken by recent query update in core https://github.com/aspnet/EntityFramework/issues/2626
        public override async Task OrderBy_correlated_subquery_lol()
        {
            //base.OrderBy_correlated_subquery_lol();
        }

        //TODO ErikEJ await fix Broken by recent query update in core
        public override async Task Where_query_composition()
        {
            //base.Where_query_composition();
        }

        //TODO ErikEJ await fix Broken by recent query update in core
        public override async Task Where_shadow_subquery_first()
        {
            //base.Where_shadow_subquery_first();
        }

        //TODO ErikEJ await fix Broken by recent query update in core
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
                await Single_Predicate_Cancellation(Fixture.CancelQuery()));
        }

        public AsyncQuerySqlServerTest(NorthwindQuerySqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}
