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

                //TODO ErikEJ report this to fail
                //var result3 = ctx.FunkyCustomers.Where(c => c.FirstName.StartsWith(null)).Select(c => c.FirstName).ToList();
                //Assert.True(0 == result3.Count);

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

                //var result8 = ctx.FunkyCustomers.Where(c => !c.FirstName.StartsWith(null)).Select(c => c.FirstName).ToList();
                //Assert.True(0 == result8.Count);
            }
        }

        public override void String_starts_with_on_argument_with_wildcard_constant()
        {
            //base.String_starts_with_on_argument_with_wildcard_constant();
        }

        public override void String_ends_with_equals_nullable_column()
        {
//            base.String_ends_with_equals_nullable_column();

//            Assert.Equal(
//                @"SELECT [c].[Id], [c].[FirstName], [c].[LastName], [c].[NullableBool], [c2].[Id], [c2].[FirstName], [c2].[LastName], [c2].[NullableBool]
//FROM [FunkyCustomer] AS [c]
//CROSS JOIN [FunkyCustomer] AS [c2]
//WHERE CASE
//    WHEN (RIGHT([c].[FirstName], LEN([c2].[LastName])) = [c2].[LastName]) OR ([c2].[LastName] = N'')
//    THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
//END = [c].[NullableBool]",
//                Sql);
        }

        public override void String_ends_with_not_equals_nullable_column()
        {
//            base.String_ends_with_not_equals_nullable_column();

//            Assert.Equal(
//                @"SELECT [c].[Id], [c].[FirstName], [c].[LastName], [c].[NullableBool], [c2].[Id], [c2].[FirstName], [c2].[LastName], [c2].[NullableBool]
//FROM [FunkyCustomer] AS [c]
//CROSS JOIN [FunkyCustomer] AS [c2]
//WHERE (CASE
//    WHEN (RIGHT([c].[FirstName], LEN([c2].[LastName])) = [c2].[LastName]) OR ([c2].[LastName] = N'')
//    THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
//END <> [c].[NullableBool]) OR [c].[NullableBool] IS NULL",
//                Sql);
        }

        public override void String_ends_with_inside_conditional()
        {
            //base.String_ends_with_inside_conditional();
        }

        public override void String_ends_with_inside_conditional_negated()
        {
            //base.String_ends_with_inside_conditional_negated();
        }

        public override void String_ends_with_on_argument_with_wildcard_column()
        {
            //base.String_ends_with_on_argument_with_wildcard_column();
        }

        public override void String_ends_with_on_argument_with_wildcard_column_negated()
        {
            //base.String_ends_with_on_argument_with_wildcard_column_negated();
        }

        public override void String_ends_with_on_argument_with_wildcard_constant()
        {
            //base.String_ends_with_on_argument_with_wildcard_constant();
        }

        public override void String_ends_with_on_argument_with_wildcard_parameter()
        {
            //base.String_ends_with_on_argument_with_wildcard_parameter();
        }

        protected override void ClearLog() => TestSqlLoggerFactory.Reset();

        private const string FileLineEnding = @"
";

        private static string Sql => TestSqlLoggerFactory.Sql.Replace(Environment.NewLine, FileLineEnding);
    }
}
