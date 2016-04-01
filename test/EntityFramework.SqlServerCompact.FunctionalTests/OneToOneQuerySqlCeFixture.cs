using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.FunctionalTests
{
    public class OneToOneQuerySqlCeFixture : OneToOneQueryFixtureBase
    {
        private readonly DbContextOptions _options;
        private readonly IServiceProvider _serviceProvider;

        public OneToOneQuerySqlCeFixture()
        {
            _serviceProvider
                = new ServiceCollection()
                    .AddEntityFrameworkSqlCe()
                    .AddSingleton(TestSqlCeModelSource.GetFactory(OnModelCreating))
                    .AddSingleton<ILoggerFactory>(new TestSqlLoggerFactory())
                    .BuildServiceProvider();

            var database = SqlCeTestStore.CreateScratch(createDatabase: true);

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder
                .UseSqlCe(database.Connection.ConnectionString)
                .UseInternalServiceProvider(_serviceProvider);
            _options = optionsBuilder.Options;

            using (var context = new DbContext(_options))
            {
                context.Database.EnsureCreated();

                AddTestData(context);
            }
        }

        public DbContext CreateContext()
        {
            return new DbContext(_options);
        }
    }
}
