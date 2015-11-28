using System;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.Data.Entity.FunctionalTests
{
    public class OneToOneQuerySqlCeFixture : OneToOneQueryFixtureBase
    {
        private readonly DbContextOptions _options;
        private readonly IServiceProvider _serviceProvider;

        public OneToOneQuerySqlCeFixture()
        {
            _serviceProvider
                = new ServiceCollection()
                    .AddEntityFramework()
                    .AddSqlCe()
                    .ServiceCollection()
                    .AddSingleton(TestSqlCeModelSource.GetFactory(OnModelCreating))
                    .AddSingleton<ILoggerFactory>(new TestSqlLoggerFactory())
                    .BuildServiceProvider();

            var database = SqlCeTestStore.CreateScratch(createDatabase: true);

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlCe(database.Connection.ConnectionString);
            _options = optionsBuilder.Options;

            using (var context = new DbContext(_serviceProvider, _options))
            {
                context.Database.EnsureCreated();

                AddTestData(context);
            }
        }

        public DbContext CreateContext()
        {
            return new DbContext(_serviceProvider, _options);
        }
    }
}
