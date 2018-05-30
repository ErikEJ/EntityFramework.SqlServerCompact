using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class AsyncQueryNavigationsSqlCeTests : AsyncQueryNavigationsTestBase<NorthwindQuerySqlCeFixture<NoopModelCustomizer>>
    {
        public AsyncQueryNavigationsSqlCeTests(
            NorthwindQuerySqlCeFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            // TestSqlLoggerFactory.CaptureOutput(testOutputHelper);
        }
    }
}
