using Microsoft.EntityFrameworkCore.Specification.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;
using Microsoft.EntityFrameworkCore.TestModels.FunkyDataModel;

namespace Microsoft.EntityFrameworkCore.SqlCe.FunctionalTests
{
    public class FunkyDataQuerySqlCeFixture : FunkyDataQueryFixtureBase<SqlCeTestStore>
    {
        public const string DatabaseName = "FunkyDataQueryTest";

        private readonly DbContextOptions _options;

        private readonly string _connectionString = SqlCeTestStore.CreateConnectionString(DatabaseName);

        public TestSqlLoggerFactory TestSqlLoggerFactory { get; } = new TestSqlLoggerFactory();

        public FunkyDataQuerySqlCeFixture()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlCe()
                .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                .AddSingleton<ILoggerFactory>(TestSqlLoggerFactory)
                .BuildServiceProvider();

            _options = new DbContextOptionsBuilder()
               .EnableSensitiveDataLogging()
               .UseInternalServiceProvider(serviceProvider)
               .Options;
        }

        public override SqlCeTestStore CreateTestStore()
        {
            return SqlCeTestStore.GetOrCreateShared(DatabaseName, () =>
            {
                var optionsBuilder = new DbContextOptionsBuilder(_options)
                    .UseSqlCe(_connectionString, b => b.ApplyConfiguration());

                using (var context = new FunkyDataContext(optionsBuilder.Options))
                {
                    context.Database.EnsureClean();
                    FunkyDataModelInitializer.Seed(context);
                }
            });
        }

        public override FunkyDataContext CreateContext(SqlCeTestStore testStore)
        {
            var options = new DbContextOptionsBuilder(_options)
                    .UseSqlCe(testStore.Connection, b => b.ApplyConfiguration())
                    .Options;

            var context = new FunkyDataContext(options);

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            context.Database.UseTransaction(testStore.Transaction);

            return context;
        }
    }
}
