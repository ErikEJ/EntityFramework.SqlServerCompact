using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;
using Microsoft.EntityFrameworkCore.Query;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class OneToOneQuerySqlCeFixture : OneToOneQueryFixtureBase
    {
        private readonly DbContextOptions _options;
        private readonly SqlCeTestStore _testStore;

        public TestSqlLoggerFactory TestSqlLoggerFactory { get; } = new TestSqlLoggerFactory();

        public OneToOneQuerySqlCeFixture()
        {
            _testStore = SqlCeTestStore.CreateScratch(true);

            _options = new DbContextOptionsBuilder()
                .UseSqlCe(_testStore.ConnectionString, b => b.ApplyConfiguration())
                .UseInternalServiceProvider(new ServiceCollection()
                    .AddEntityFrameworkSqlCe()
                    .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                    .AddSingleton<ILoggerFactory>(TestSqlLoggerFactory)
                    .BuildServiceProvider())
                .Options;

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

        public void Dispose() => _testStore.Dispose();
    }
}
