using System;
using Microsoft.EntityFrameworkCore.Specification.Tests;
using Xunit;
using Xunit.Abstractions;
using System.Linq;

namespace Microsoft.EntityFrameworkCore.SqlCe.FunctionalTests
{
    public class FunkyDataQuerySqlCeTest : FunkyDataQueryTestBase<SqlCeTestStore, FunkyDataQuerySqlCeFixture>
    {
        public FunkyDataQuerySqlCeTest(FunkyDataQuerySqlCeFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            //TestSqlLoggerFactory.CaptureOutput(testOutputHelper);
        }

        [Fact]
        public void String_starts_with_on_argument_with_wildcard_constantForSqlCe()
        {
            using (var ctx = CreateContext())
            {
                var result1 = ctx.FunkyCustomers.Where(c => c.FirstName.StartsWith("%B")).Select(c => c.FirstName).ToList();
                var expected1 = ctx.FunkyCustomers.Select(c => c.FirstName).ToList().Where(c => c != null && c.StartsWith("%B"));
                Assert.True(expected1.Count() == result1.Count);

                var result2 = ctx.FunkyCustomers.Where(c => c.FirstName.StartsWith("a_")).Select(c => c.FirstName).ToList();
                var expected2 = ctx.FunkyCustomers.Select(c => c.FirstName).ToList().Where(c => c != null && c.StartsWith("a_"));
                Assert.True(expected2.Count() == result2.Count);

                var result4 = ctx.FunkyCustomers.Where(c => c.FirstName.StartsWith("")).Select(c => c.FirstName).ToList();
                Assert.True(ctx.FunkyCustomers.Count() == result4.Count);

                var result5 = ctx.FunkyCustomers.Where(c => c.FirstName.StartsWith("_Ba_")).Select(c => c.FirstName).ToList();
                var expected5 = ctx.FunkyCustomers.Select(c => c.FirstName).ToList().Where(c => c != null && c.StartsWith("_Ba_"));
                Assert.True(expected5.Count() == result5.Count);

                var result6 = ctx.FunkyCustomers.Where(c => !c.FirstName.StartsWith("%B%a%r")).Select(c => c.FirstName).ToList();
                var expected6 = ctx.FunkyCustomers.Select(c => c.FirstName).ToList().Where(c => c != null && !c.StartsWith("%B%a%r"));
                Assert.True(expected6.Count() == result6.Count);

                var result7 = ctx.FunkyCustomers.Where(c => !c.FirstName.StartsWith("")).Select(c => c.FirstName).ToList();
                Assert.True(0 == result7.Count);
            }
        }

        [Fact(Skip = "SQL CE limitation")]
        public override void String_starts_with_on_argument_with_wildcard_constant()
        {
            //base.String_starts_with_on_argument_with_wildcard_constant();
        }

        public override void String_ends_with_equals_nullable_column()
        {
            base.String_ends_with_equals_nullable_column();

            Assert.Equal(
                @"SELECT [c].[Id], [c].[FirstName], [c].[LastName], [c].[NullableBool], [c2].[Id], [c2].[FirstName], [c2].[LastName], [c2].[NullableBool]
FROM [FunkyCustomer] AS [c]
CROSS JOIN [FunkyCustomer] AS [c2]
WHERE CASE
    WHEN (SUBSTRING([c].[FirstName], (LEN([c].[FirstName]) + 1) - LEN([c2].[LastName]), LEN([c2].[LastName])) = [c2].[LastName]) OR ([c2].[LastName] = N'')
    THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
END = [c].[NullableBool]",
                Sql);
        }

        public override void String_ends_with_not_equals_nullable_column()
        {
            base.String_ends_with_not_equals_nullable_column();

            Assert.Equal(
                @"SELECT [c].[Id], [c].[FirstName], [c].[LastName], [c].[NullableBool], [c2].[Id], [c2].[FirstName], [c2].[LastName], [c2].[NullableBool]
FROM [FunkyCustomer] AS [c]
CROSS JOIN [FunkyCustomer] AS [c2]
WHERE (CASE
    WHEN (SUBSTRING([c].[FirstName], (LEN([c].[FirstName]) + 1) - LEN([c2].[LastName]), LEN([c2].[LastName])) = [c2].[LastName]) OR ([c2].[LastName] = N'')
    THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
END <> [c].[NullableBool]) OR [c].[NullableBool] IS NULL",
                Sql);
        }

        [Fact(Skip = "Investigate why!")]
        public override void String_ends_with_on_argument_with_wildcard_constant()
        {
            //base.String_ends_with_on_argument_with_wildcard_constant();
        }

        protected override void ClearLog() => TestSqlLoggerFactory.Reset();

        private const string FileLineEnding = @"
";

        private static string Sql => TestSqlLoggerFactory.Sql.Replace(Environment.NewLine, FileLineEnding);
    }
}
