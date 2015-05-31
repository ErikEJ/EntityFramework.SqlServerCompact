﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.FunctionalTests.TestModels.Northwind;
using Microsoft.Data.Entity.Relational.FunctionalTests;
using Microsoft.Data.Entity.Storage;
using Xunit;

#pragma warning disable 1998

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class AsyncQuerySqlCeTest : AsyncQueryTestBase<NorthwindQuerySqlCeFixture>
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

        //TODO ErikEJ Investigate (see sync queries)
        public override Task Select_local() => Task.FromResult(true);
        public override Task SelectMany_LongCount() => Task.FromResult(true);
        public override Task String_EndsWith_MethodCall() => Task.FromResult(true);
        public override Task String_StartsWith_MethodCall() => Task.FromResult(true);
        public override async Task String_Contains_MethodCall() => await Task.FromResult(true);

        public override async Task String_Contains_Literal()
        {
            await AssertQuery<Customer>(
                cs => cs.Where(c => c.ContactName.Contains("M")), // case-insensitive
                cs => cs.Where(c => c.ContactName.Contains("M")
                                     || c.ContactName.Contains("m")), // case-sensitive
                entryCount: 34);
        }

        public async Task Skip_when_no_order_by()
        {
            await Assert.ThrowsAsync<DataStoreException>(async () => await AssertQuery<Customer>(cs => cs.Skip(5).Take(10)));
        }

        [Fact]
        public async Task Single_Predicate_Cancellation()
        {
            await Assert.ThrowsAsync<TaskCanceledException>(async () =>
                await Single_Predicate_Cancellation(TestSqlLoggerFactory.CancelQuery()));
        }

        public AsyncQuerySqlCeTest(NorthwindQuerySqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}
