namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class OptimisticConcurrencySqlCeTest : OptimisticConcurrencyTestBase<SqlCeTestStore, F1SqlCeFixture>
    {
        public OptimisticConcurrencySqlCeTest(F1SqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}
