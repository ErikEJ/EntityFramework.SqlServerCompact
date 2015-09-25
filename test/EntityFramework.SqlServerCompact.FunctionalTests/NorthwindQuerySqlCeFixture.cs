using System;
using ErikEJ.Data.Entity.SqlServerCe.FunctionalTests.TestModels;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.FunctionalTests.TestModels.Northwind;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class NorthwindQuerySqlCeFixture : NorthwindQueryRelationalFixture, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DbContextOptions _options;
        private readonly SqlCeTestStore _testStore;

        public NorthwindQuerySqlCeFixture()
        {
            _testStore = SqlCeNorthwindContext.GetSharedStore();

            _serviceProvider = new ServiceCollection()
                .AddEntityFramework()
                .AddSqlCe()
                .ServiceCollection()
                .AddSingleton(TestSqlCeModelSource.GetFactory(OnModelCreating))
                .AddInstance<ILoggerFactory>(new TestSqlLoggerFactory())
                .BuildServiceProvider();

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlCe(_testStore.Connection.ConnectionString);
            _options = optionsBuilder.Options;

            _serviceProvider.GetRequiredService<ILoggerFactory>()
                .MinimumLevel = LogLevel.Debug;
        }

        public override NorthwindContext CreateContext() => CreateContext(useRelationalNulls: false);

        public override NorthwindContext CreateContext(bool useRelationalNulls)
        {
            RelationalOptionsExtension.Extract(_options).UseRelationalNulls = useRelationalNulls;

            var context = new SqlCeNorthwindContext(_serviceProvider, _options);

            context.ChangeTracker.AutoDetectChangesEnabled = false;

            return context;
        }

        public void Dispose() => _testStore.Dispose();
    }
}
