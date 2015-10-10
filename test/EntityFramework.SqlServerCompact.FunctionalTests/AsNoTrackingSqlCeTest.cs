namespace Microsoft.Data.Entity.FunctionalTests
{
    public class AsNoTrackingSqlCeTest : AsNoTrackingTestBase<NorthwindQuerySqlCeFixture>
    {
        public AsNoTrackingSqlCeTest(NorthwindQuerySqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}
