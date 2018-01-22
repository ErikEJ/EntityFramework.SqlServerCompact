using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class AsyncGroupByQuerySqlCeTest : AsyncGroupByQueryTestBase<NorthwindQuerySqlCeFixture<NoopModelCustomizer>>
    {
        // ReSharper disable once UnusedParameter.Local
        public AsyncGroupByQuerySqlCeTest(NorthwindQuerySqlCeFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            Fixture.TestSqlLoggerFactory.Clear();
            //Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
        }

        public override async Task GroupBy_Composite_Select_Average()
        {
            await base.GroupBy_Composite_Select_Average();

            AssertSql(
                @"SELECT AVG(CAST([o].[OrderID] AS float))
FROM [Orders] AS [o]
GROUP BY [o].[CustomerID], [o].[EmployeeID]");
        }

        private void AssertSql(params string[] expected)
            => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);
    }
}
