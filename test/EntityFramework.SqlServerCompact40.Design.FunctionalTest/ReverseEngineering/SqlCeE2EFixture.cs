using Microsoft.EntityFrameworkCore.FunctionalTests;

namespace EntityFramework.SqlServerCompact40.Design.FunctionalTest.ReverseEngineering
{
    public class SqlCeE2EFixture
    {
        public SqlCeE2EFixture()
        {
            SqlCeTestStore.GetOrCreateShared("E2E", () => { });
        }
    }
}
