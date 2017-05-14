using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class InheritanceSqlCeFixture : InheritanceRelationalFixture
    {
        public InheritanceSqlCeFixture()
        {
            using (var context = CreateContext())
            {
                context.Database.EnsureClean();
                context.Database.EnsureCreated();

                SeedData(context);
            }
        }

        public TestSqlLoggerFactory TestSqlLoggerFactory { get; } = new TestSqlLoggerFactory();

        public override DbContextOptions BuildOptions()
        {
            var testStore = SqlCeTestStore.CreateScratch(createDatabase: true);

            return
                new DbContextOptionsBuilder()
                    .EnableSensitiveDataLogging()
                    .UseSqlCe(
                        testStore.Connection,
                        b => b.ApplyConfiguration())
                    .UseInternalServiceProvider(
                        new ServiceCollection()
                            .AddEntityFrameworkSqlCe()
                            .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                            .AddSingleton<ILoggerFactory>(TestSqlLoggerFactory)
                            .BuildServiceProvider())
                    .Options;
        }
    }
}
