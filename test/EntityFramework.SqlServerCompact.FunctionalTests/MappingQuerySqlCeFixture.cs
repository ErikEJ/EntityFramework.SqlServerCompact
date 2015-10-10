using System;
using Microsoft.Data.Entity.FunctionalTests.TestModels;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.Data.Entity.FunctionalTests
{
    public class MappingQuerySqlCeFixture : MappingQueryFixtureBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DbContextOptions _options;
        private readonly SqlCeTestStore _testDatabase;

        public MappingQuerySqlCeFixture()
        {
            _serviceProvider = new ServiceCollection()
                .AddEntityFramework()
                .AddSqlCe()
                .ServiceCollection()
                .AddInstance<ILoggerFactory>(new TestSqlLoggerFactory())
                .BuildServiceProvider();

            _testDatabase = SqlCeNorthwindContext.GetSharedStore();

            var optionsBuilder = new DbContextOptionsBuilder().UseModel(CreateModel());
            optionsBuilder.UseSqlCe(_testDatabase.Connection.ConnectionString);
            _options = optionsBuilder.Options;
        }

        public DbContext CreateContext()
        {
            return new DbContext(_serviceProvider, _options);
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
