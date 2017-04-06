using System;
using Microsoft.EntityFrameworkCore.Specification.Tests.TestModels.ComplexNavigationsModel;
using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class ComplexNavigationsQuerySqlCeFixture
        : ComplexNavigationsQueryRelationalFixture<SqlCeTestStore>
    {
        public static readonly string DatabaseName = "ComplexNavigations";

        private readonly DbContextOptions _options;

        private readonly string _connectionString
            = SqlCeTestStore.CreateConnectionString(DatabaseName);

        public ComplexNavigationsQuerySqlCeFixture()
        {
             var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlCe()
                .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                .AddSingleton<ILoggerFactory>(new TestSqlLoggerFactory())
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

                    TestSqlLoggerFactory.Reset();
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
    }
}