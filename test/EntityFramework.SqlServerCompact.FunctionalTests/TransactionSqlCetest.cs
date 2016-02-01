using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore.FunctionalTests
{
    public class TransactionSqlCeTest : TransactionTestBase<SqlCeTestStore, TransactionSqlCeFixture>
    {
        public TransactionSqlCeTest(TransactionSqlCeFixture fixture)
            : base(fixture)
        {
        }

        public override Task QueryAsync_uses_explicit_transaction() => Task.FromResult(true);

        public override void Query_uses_explicit_transaction() => Task.FromResult(true);

        protected override bool SnapshotSupported => false;
    }
}
