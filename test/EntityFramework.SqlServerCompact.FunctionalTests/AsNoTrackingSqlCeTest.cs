namespace Microsoft.EntityFrameworkCore.FunctionalTests
{
    public class AsNoTrackingSqlCeTest : AsNoTrackingTestBase<NorthwindQuerySqlCeFixture>
    {
        public AsNoTrackingSqlCeTest(NorthwindQuerySqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}
