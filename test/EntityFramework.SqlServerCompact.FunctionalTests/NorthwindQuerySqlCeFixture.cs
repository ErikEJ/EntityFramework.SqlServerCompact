using System;
using System.Threading;
using Microsoft.Data.Entity.FunctionalTests.TestModels;
using Microsoft.Data.Entity.FunctionalTests.TestModels.Northwind;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.Data.Entity.FunctionalTests
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

            var sqlCeDbContextOptionsBuilder
                = optionsBuilder.UseSqlCe(_testStore.Connection.ConnectionString);

            sqlCeDbContextOptionsBuilder.LogSqlParameterValues();

            ConfigureOptions(sqlCeDbContextOptionsBuilder);

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
