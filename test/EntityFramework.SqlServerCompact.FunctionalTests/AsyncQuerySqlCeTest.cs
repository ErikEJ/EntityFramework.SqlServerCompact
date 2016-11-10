using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Specification.Tests.TestModels.Northwind;
using Xunit;

#pragma warning disable 1998

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class AsyncQuerySqlServerTest : AsyncQueryTestBase<NorthwindQuerySqlCeFixture>
    {
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
