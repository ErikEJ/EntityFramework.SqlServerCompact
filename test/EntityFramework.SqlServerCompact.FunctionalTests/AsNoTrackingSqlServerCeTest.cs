using Microsoft.Data.Entity.FunctionalTests;
using Xunit;

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
