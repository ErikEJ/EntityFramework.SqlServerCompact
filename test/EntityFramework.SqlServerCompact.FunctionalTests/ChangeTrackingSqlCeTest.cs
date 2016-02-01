namespace Microsoft.EntityFrameworkCore.FunctionalTests
{
    public class ChangeTrackingSqlCeTest : ChangeTrackingTestBase<NorthwindQuerySqlCeFixture>
    {
        public ChangeTrackingSqlCeTest(NorthwindQuerySqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}


