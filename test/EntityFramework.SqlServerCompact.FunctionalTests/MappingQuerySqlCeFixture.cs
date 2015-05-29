using System;
using ErikEJ.Data.Entity.SqlServerCe.FunctionalTests.TestModels;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational.FunctionalTests;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class MappingQuerySqlCeFixture : MappingQueryFixtureBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly EntityOptions _options;
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

            var optionsBuilder = new EntityOptionsBuilder().UseModel(CreateModel());
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
                // TODO: Use .SqlCe() when available - if it is not removed!
                e.Property(c => c.CompanyName2).Metadata.Relational().Column = "CompanyName";
                e.Metadata.Relational().Table = "Customers";
            });
        }
    }
}
