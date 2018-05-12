using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore
{
    public class ConcurrencyDetectorSqlCeTest : ConcurrencyDetectorRelationalTestBase<NorthwindQuerySqlCeFixture<NoopModelCustomizer>>
    {
        public ConcurrencyDetectorSqlCeTest(NorthwindQuerySqlCeFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }
    }
}
