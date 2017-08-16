using Microsoft.EntityFrameworkCore.Query;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class ChangeTrackingSqlCeTest : ChangeTrackingTestBase<NorthwindQuerySqlCeFixture>
    {
        public ChangeTrackingSqlCeTest(NorthwindQuerySqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}


