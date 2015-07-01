using Microsoft.Data.Entity.FunctionalTests;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class OptimisticConcurrencySqlCeTest : OptimisticConcurrencyTestBase<SqlCeTestStore, F1SqlCeFixture>
    {
        public OptimisticConcurrencySqlCeTest(F1SqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}
