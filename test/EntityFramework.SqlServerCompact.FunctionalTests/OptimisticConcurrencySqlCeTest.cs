using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore
{
    public class OptimisticConcurrencySqlCeTest : OptimisticConcurrencyTestBase<F1SqlCeFixture>
    {
        public OptimisticConcurrencySqlCeTest(F1SqlCeFixture fixture)
            : base(fixture)
        {
        }

        protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
            => facade.UseTransaction(transaction.GetDbTransaction());
    }
}
