using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class QueryTaggingSqlCeTest : QueryTaggingTestBase<NorthwindQuerySqlCeFixture<NoopModelCustomizer>>
    {
        public QueryTaggingSqlCeTest(
            NorthwindQuerySqlCeFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
        }
    }
}
