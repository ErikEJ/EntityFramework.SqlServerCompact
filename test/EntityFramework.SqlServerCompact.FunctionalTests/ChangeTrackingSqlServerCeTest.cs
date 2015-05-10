using ErikEJ.Data.Entity.SqlServerCe.FunctionalTests;
using Microsoft.Data.Entity.FunctionalTests;

namespace ErikEJ.Data.Entity.SqlServerCompact.FunctionalTests
{
    public class ChangeTrackingSqlServerCeTest : ChangeTrackingTestBase<NorthwindQuerySqlServerCeFixture>
    {
        public ChangeTrackingSqlServerCeTest(NorthwindQuerySqlServerCeFixture fixture)
            : base(fixture)
        {
        }
    }
}


