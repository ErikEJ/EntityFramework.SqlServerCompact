using Microsoft.Data.Entity.FunctionalTests;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class AsNoTrackingSqlServerCeTest : AsNoTrackingTestBase<NorthwindQuerySqlServerCeFixture>
    {
        public AsNoTrackingSqlServerCeTest(NorthwindQuerySqlServerCeFixture fixture)
            : base(fixture)
        {
        }
    }
}
