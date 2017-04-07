using System;
using Microsoft.EntityFrameworkCore.Specification.Tests.TestModels.Inheritance;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class InheritanceSqlCeFixture : InheritanceRelationalFixture
    {
        private readonly DbContextOptions _options;
        private readonly SqlCeTestStore _testStore;

        public InheritanceSqlCeFixture()
        {
            var serviceProvider = new ServiceCollection()
                            .AddEntityFrameworkSqlCe()
                            .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                            .AddSingleton<ILoggerFactory>(new TestSqlLoggerFactory())
                            .BuildServiceProvider();

            _testStore = SqlCeTestStore.CreateScratch(createDatabase: true);

            _options = new DbContextOptionsBuilder()
                .EnableSensitiveDataLogging()
                .UseSqlCe(_testStore.Connection, b => b.ApplyConfiguration())
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            using (var context = CreateContext())
            {
                context.Database.EnsureCreated();
                SeedData(context);
            }
        }

        public override InheritanceContext CreateContext() => new InheritanceContext(_options);
    }
}
