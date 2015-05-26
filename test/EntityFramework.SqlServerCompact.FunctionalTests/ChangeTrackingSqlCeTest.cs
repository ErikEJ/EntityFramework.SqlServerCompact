using Microsoft.Data.Entity.FunctionalTests;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class ChangeTrackingSqlCeTest : ChangeTrackingTestBase<NorthwindQuerySqlCeFixture>
    {
        public ChangeTrackingSqlCeTest(NorthwindQuerySqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}


