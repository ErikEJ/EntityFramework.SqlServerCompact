using ErikEJ.Data.Entity.SqlServerCe.FunctionalTests;

namespace EntityFramework7.SqlServerCompact40.Design.FunctionalTest
{
    public class E2EFixture
    {
        public E2EFixture()
        {
            SqlCeTestStore.GetOrCreateShared("E2E", () => { });
        }
    }
}
