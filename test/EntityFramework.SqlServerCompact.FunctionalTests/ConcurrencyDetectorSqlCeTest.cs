using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore
{
    public class ConcurrencyDetectorSqlCeTest : ConcurrencyDetectorRelationalTest<NorthwindQuerySqlCeFixture<NoopModelCustomizer>>
    {
        public ConcurrencyDetectorSqlCeTest(NorthwindQuerySqlCeFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }
    }
}
