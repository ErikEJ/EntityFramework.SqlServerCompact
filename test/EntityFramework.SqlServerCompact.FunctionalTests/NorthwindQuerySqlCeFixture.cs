using System;
using System.Threading;
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

        private readonly SqlCeTestStore _testStore = SqlCeNorthwindContext.GetSharedStore();
        private readonly TestSqlLoggerFactory _testSqlLoggerFactory = new TestSqlLoggerFactory();

        public NorthwindQuerySqlCeFixture()
        {
            _serviceProvider
                = new ServiceCollection()
                    .AddEntityFramework()
                    .AddSqlCe()
                    .ServiceCollection()
                    .AddSingleton(TestSqlCeModelSource.GetFactory(OnModelCreating))
                    .AddInstance<ILoggerFactory>(_testSqlLoggerFactory)
                    .BuildServiceProvider();

            _options = BuildOptions();

            _serviceProvider.GetRequiredService<ILoggerFactory>().MinimumLevel = LogLevel.Debug;
        }

        protected DbContextOptions BuildOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder();

            var sqlServerDbContextOptionsBuilder
                = optionsBuilder.UseSqlCe(_testStore.Connection.ConnectionString);

            ConfigureOptions(sqlServerDbContextOptionsBuilder);

            return optionsBuilder.Options;
        }

        protected virtual void ConfigureOptions(SqlCeDbContextOptionsBuilder sqlCeDbContextOptionsBuilder)
        {
        }

        public override NorthwindContext CreateContext()
        {
            var context = new SqlCeNorthwindContext(_serviceProvider, _options);

            context.ChangeTracker.AutoDetectChangesEnabled = false;

            return context;
        }

        public void Dispose() => _testStore.Dispose();

        public override CancellationToken CancelQuery() => _testSqlLoggerFactory.CancelQuery();
    }
}
