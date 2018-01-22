using System;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore
{
    public class TransactionSqlCeTest : TransactionTestBase<TransactionSqlCeTest.TransactionSqlCeFixture>
    {
        public TransactionSqlCeTest(TransactionSqlCeFixture fixture)
            : base(fixture)
        {
        }

        protected override bool SnapshotSupported => true;

#if NET461
        protected override bool AmbientTransactionsSupported => true;
#endif

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
                            .ConfigureWarnings(w => w.Log(RelationalEventId.QueryClientEvaluationWarning)))
                    .MaxBatchSize(1);
                return builder;
            }
        }
    }
}
