using Microsoft.EntityFrameworkCore.Query;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class AsNoTrackingSqlCeTest : AsNoTrackingTestBase<NorthwindQuerySqlCeFixture>
    {
        public AsNoTrackingSqlCeTest(NorthwindQuerySqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}
