using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Specification.Tests.TestModels.ComplexNavigationsModel;
using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class ComplexNavigationsOwnedQuerySqlCeFixture
        : ComplexNavigationsOwnedQueryFixtureBase<SqlCeTestStore>
    {
        public static readonly string DatabaseName = "ComplexNavigationsOwned";

        private readonly DbContextOptions _options;

        private readonly string _connectionString
            = SqlCeTestStore.CreateConnectionString(DatabaseName);

        public TestSqlLoggerFactory TestSqlLoggerFactory { get; } = new TestSqlLoggerFactory();

        public ComplexNavigationsOwnedQuerySqlCeFixture()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlCe()
                .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                .AddSingleton<ILoggerFactory>(TestSqlLoggerFactory)
                .BuildServiceProvider();

            _options = new DbContextOptionsBuilder()
                .EnableSensitiveDataLogging()
                .UseSqlCe(_connectionString, b => b.ApplyConfiguration())
                .UseInternalServiceProvider(serviceProvider).Options;
        }

        public override SqlCeTestStore CreateTestStore()
        {
            return SqlCeTestStore.GetOrCreateShared(DatabaseName, () =>
            {
                using (var context = new ComplexNavigationsContext(_options))
                {
                    context.Database.EnsureCreated();
                    ComplexNavigationsModelInitializer.Seed(context);
                }
            });
        }

        public override ComplexNavigationsContext CreateContext(SqlCeTestStore testStore)
        {
            var context = new ComplexNavigationsContext(_options);

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            context.Database.UseTransaction(testStore.Transaction);

            return context;
        }

        protected override void Configure(ReferenceOwnershipBuilder<Level1, Level2> l2)
        {
            base.Configure(l2);

            l2.ForSqlCeToTable("Level2");
        }

        protected override void Configure(ReferenceOwnershipBuilder<Level2, Level3> l3)
        {
            base.Configure(l3);

            l3.ForSqlCeToTable("Level3");
        }

        protected override void Configure(ReferenceOwnershipBuilder<Level3, Level4> l4)
        {
            base.Configure(l4);

            l4.ForSqlCeToTable("Level4");
        }
    }
}