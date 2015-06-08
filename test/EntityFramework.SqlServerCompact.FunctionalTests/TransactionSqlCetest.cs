using Microsoft.Data.Entity.Relational.FunctionalTests;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    //TODO ErikEJ Skipping for now due to unsupported trans level in use
    //ability to override is fixed in EF7 codebase, waiting for latest MyGet build
    //public class TransactionSqlCeTest : TransactionTestBase<SqlCeTestStore, TransactionSqlCeFixture>
    //{
    //    public TransactionSqlCeTest(TransactionSqlCeFixture fixture)
    //        : base(fixture)
    //    {
    //    }

    //    protected override bool SnapshotSupported => false;
    //}
}
