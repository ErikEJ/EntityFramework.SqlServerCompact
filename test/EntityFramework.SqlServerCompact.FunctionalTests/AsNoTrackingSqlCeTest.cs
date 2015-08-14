using Microsoft.Data.Entity.FunctionalTests;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class AsNoTrackingSqlCeTest : AsNoTrackingTestBase<NorthwindQuerySqlCeFixture>
    {
        public AsNoTrackingSqlCeTest(NorthwindQuerySqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}
