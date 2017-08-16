using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Query;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class MappingQuerySqlCeFixture : MappingQueryFixtureBase
    {
        private readonly DbContextOptions _options;
        private readonly SqlCeTestStore _testDatabase;

        public TestSqlLoggerFactory TestSqlLoggerFactory { get; } = new TestSqlLoggerFactory();

        public MappingQuerySqlCeFixture()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlCe()
                .AddSingleton<ILoggerFactory>(TestSqlLoggerFactory)
                .BuildServiceProvider();

            _testDatabase = SqlCeTestStore.GetNorthwindStore();

            var optionsBuilder = new DbContextOptionsBuilder().UseModel(CreateModel());
            optionsBuilder
                .UseSqlCe(_testDatabase.Connection.ConnectionString)
                .UseInternalServiceProvider(serviceProvider);
            _options = optionsBuilder.Options;
        }

        public DbContext CreateContext()
        {
            var context = new DbContext(_options);

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return context;
        }

        public void Dispose()
        {
            _testDatabase.Dispose();
        }

        protected override string DatabaseSchema { get; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MappingQueryTestBase.MappedCustomer>(e =>
            {
                e.Property(c => c.CompanyName2).Metadata.Relational().ColumnName = "CompanyName";
                e.Metadata.Relational().TableName = "Customers";
            });
        }
    }
}
