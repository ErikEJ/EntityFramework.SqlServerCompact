namespace Microsoft.EntityFrameworkCore.FunctionalTests
{
    public class OptimisticConcurrencySqlCeTest : OptimisticConcurrencyTestBase<SqlCeTestStore, F1SqlCeFixture>
    {
        public OptimisticConcurrencySqlCeTest(F1SqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}
