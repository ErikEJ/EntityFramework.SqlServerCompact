using System;
using Microsoft.EntityFrameworkCore.TestModels.TransportationModel;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Specification.Tests;
using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;

namespace Microsoft.EntityFrameworkCore
{
    public class TableSplittingSqlCeTest : TableSplittingTestBase<SqlCeTestStore>
    {
        private readonly string _connectionString = SqlCeTestStore.CreateConnectionString(DatabaseName);
        public TestSqlLoggerFactory TestSqlLoggerFactory { get; } = new TestSqlLoggerFactory();

        public override SqlCeTestStore CreateTestStore(Action<ModelBuilder> onModelCreating)
            => SqlCeTestStore.GetOrCreateShared(DatabaseName, () =>
            {
                var optionsBuilder = new DbContextOptionsBuilder()
                    .UseSqlCe(_connectionString, b => b.ApplyConfiguration().CommandTimeout(300))
                    .EnableSensitiveDataLogging()
                    .UseInternalServiceProvider(BuildServiceProvider(onModelCreating));

                using (var context = new TransportationContext(optionsBuilder.Options))
                {
                    context.Database.EnsureCreated();
                    context.Seed();
                }
            });

        public override TransportationContext CreateContext(SqlCeTestStore testStore, Action<ModelBuilder> onModelCreating)
        {
            var optionsBuilder = new DbContextOptionsBuilder()
                .UseSqlCe(testStore.Connection, b => b.ApplyConfiguration().CommandTimeout(300))
                .EnableSensitiveDataLogging()
                .UseInternalServiceProvider(BuildServiceProvider(onModelCreating));

            var context = new TransportationContext(optionsBuilder.Options);
            context.Database.UseTransaction(testStore.Transaction);
            return context;
        }

        private IServiceProvider BuildServiceProvider(Action<ModelBuilder> onModelCreating)
            => new ServiceCollection()
                .AddEntityFrameworkSqlCe()
                .AddSingleton(TestModelSource.GetFactory(onModelCreating))
                .AddSingleton<ILoggerFactory>(TestSqlLoggerFactory)
                .BuildServiceProvider();
    }
}