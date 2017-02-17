using System;
using Microsoft.EntityFrameworkCore.Specification.Tests.TestModels.Inheritance;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class InheritanceSqlCeFixture : InheritanceRelationalFixture
    {
        private readonly DbContextOptions _options;
        private readonly IServiceProvider _serviceProvider;

        public InheritanceSqlCeFixture()
        {
            _serviceProvider
                = new ServiceCollection()
                    .AddEntityFrameworkSqlCe()
                    .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                    .AddSingleton<ILoggerFactory>(new TestSqlLoggerFactory())
                    .BuildServiceProvider();

            var testStore = SqlCeTestStore.CreateScratch(createDatabase: true);

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder
                .EnableSensitiveDataLogging()
                .UseSqlCe(testStore.Connection)
                .UseInternalServiceProvider(_serviceProvider);

            _options = optionsBuilder.Options;

            using (var context = CreateContext())
            {
                context.Database.EnsureClean();
                SeedData(context);
            }
        }

        public override InheritanceContext CreateContext() => new InheritanceContext(_options);
    }
}
