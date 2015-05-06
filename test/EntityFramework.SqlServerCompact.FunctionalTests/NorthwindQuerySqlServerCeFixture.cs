using System;
using ErikEJ.Data.Entity.SqlServerCe.FunctionalTests.TestModels;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.FunctionalTests.TestModels.Northwind;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational.FunctionalTests;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class NorthwindQuerySqlServerCeFixture : NorthwindQueryRelationalFixture, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DbContextOptions _options;
        private readonly SqlServerCeTestStore _testStore;

        public NorthwindQuerySqlServerCeFixture()
        {
            _testStore = SqlServerCeNorthwindContext.GetSharedStore();

            _serviceProvider = new ServiceCollection()
                .AddEntityFramework()
                .AddSqlServerCe()
                .ServiceCollection()
                .AddSingleton(TestSqlServerCeModelSource.GetFactory(OnModelCreating))
                .AddInstance<ILoggerFactory>(new TestSqlLoggerFactory())
                .BuildServiceProvider();

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServerCe(_testStore.Connection.ConnectionString);
            _options = optionsBuilder.Options;

            _serviceProvider.GetRequiredService<ILoggerFactory>()
                .MinimumLevel = LogLevel.Debug;
        }

        public override NorthwindContext CreateContext() => new SqlServerCeNorthwindContext(_serviceProvider, _options);

        public void Dispose() => _testStore.Dispose();
    }
}
