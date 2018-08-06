using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;

namespace Microsoft.EntityFrameworkCore
{
    public class TransactionSqlCeTest : TransactionTestBase<TransactionSqlCeTest.TransactionSqlCeFixture>
    {
        public TransactionSqlCeTest(TransactionSqlCeFixture fixture)
            : base(fixture)
        {
        }

        [Fact(Skip="ErikEJ investigate fail")]
        public override void BeginTransaction_can_be_used_after_enlisted_transaction_if_connection_closed()
        {
            base.BeginTransaction_can_be_used_after_enlisted_transaction_if_connection_closed();
        }

        [Theory(Skip = "SQLCE limitation")]
        public override async Task QueryAsync_uses_explicit_transaction(bool autoTransaction)
        {
            await base.QueryAsync_uses_explicit_transaction(autoTransaction);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override void Query_uses_explicit_transaction(bool autoTransaction)
        {
            base.Query_uses_explicit_transaction(autoTransaction);
        }

        [Fact(Skip = "SQLCE limitation")]
        public override void UseTransaction_throws_if_another_transaction_started()
        {
            base.UseTransaction_throws_if_another_transaction_started();
        }

        [Theory(Skip = "ErikEJ investigate fail")]
        public override void SaveChanges_throws_for_suppressed_ambient_transactions(bool connectionString)
        {
            base.SaveChanges_throws_for_suppressed_ambient_transactions(connectionString);
        }



        protected override bool SnapshotSupported => true;

        protected override bool AmbientTransactionsSupported => true;

        protected override DbContext CreateContextWithConnectionString()
        {
            var options = Fixture.AddOptions(
                    new DbContextOptionsBuilder()
                        .UseSqlCe(TestStore.ConnectionString))
                .UseInternalServiceProvider(Fixture.ServiceProvider);

            return new DbContext(options.Options);
        }

        public class TransactionSqlCeFixture : TransactionFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;

            protected override void Seed(DbContext context)
            {
                base.Seed(context);
            }

            public override void Reseed()
            {
                using (var context = CreateContext())
                {
                    context.Set<TransactionCustomer>().RemoveRange(context.Set<TransactionCustomer>());
                    context.SaveChanges();

                    base.Seed(context);
                }
            }

            public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
            {
                new SqlCeDbContextOptionsBuilder(
                        base.AddOptions(builder)
                            .ConfigureWarnings(
                                w => w.Log(RelationalEventId.QueryClientEvaluationWarning)
                                      .Log(CoreEventId.FirstWithoutOrderByAndFilterWarning)))
                    .MaxBatchSize(1);
                return builder;
            }
        }

    }
}
