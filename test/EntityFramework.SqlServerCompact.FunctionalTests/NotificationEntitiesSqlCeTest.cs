using Microsoft.EntityFrameworkCore.TestUtilities;
namespace Microsoft.EntityFrameworkCore
{
    public class NotificationEntitiesSqlCeTest
        : NotificationEntitiesTestBase<NotificationEntitiesSqlCeTest.NotificationEntitiesSqlCeFixture>
    {
        public NotificationEntitiesSqlCeTest(NotificationEntitiesSqlCeFixture fixture)
            : base(fixture)
        {
        }

        public class NotificationEntitiesSqlCeFixture : NotificationEntitiesFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;
        }
    }
}
