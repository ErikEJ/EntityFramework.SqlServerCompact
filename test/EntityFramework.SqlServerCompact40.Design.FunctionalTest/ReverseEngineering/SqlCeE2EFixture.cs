using ErikEJ.Data.Entity.SqlServerCe.FunctionalTests;

namespace EntityFramework7.SqlServerCompact40.Design.FunctionalTest.ReverseEngineering
{
    public class SqlCeE2EFixture
    {
        public SqlCeE2EFixture()
        {
            SqlCeTestStore.GetOrCreateShared("E2E", () => { });
        }
    }
}
