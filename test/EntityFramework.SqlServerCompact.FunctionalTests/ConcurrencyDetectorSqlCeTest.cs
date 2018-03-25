using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore
{
    public class ConcurrencyDetectorSqlServerTest : ConcurrencyDetectorRelationalTest<NorthwindQuerySqlCeFixture<NoopModelCustomizer>>
    {
        public ConcurrencyDetectorSqlServerTest(NorthwindQuerySqlCeFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }
    }
}