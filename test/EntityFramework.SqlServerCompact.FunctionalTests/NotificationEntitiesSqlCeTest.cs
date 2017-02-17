using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class NotificationEntitiesSqlCeTest
        : NotificationEntitiesTestBase<SqlCeTestStore, NotificationEntitiesSqlCeTest.NotificationEntitiesSqlCeFixture>
    {
        public NotificationEntitiesSqlCeTest(NotificationEntitiesSqlCeFixture fixture)
            : base(fixture)
        {
        }

        public class NotificationEntitiesSqlCeFixture : NotificationEntitiesFixtureBase
        {
            private const string DatabaseName = "NotificationEntities";
            private readonly DbContextOptions _options;

            public NotificationEntitiesSqlCeFixture()
            {
                _options = new DbContextOptionsBuilder()
                    .UseSqlCe(SqlCeTestStore.CreateConnectionString(DatabaseName), b => b.ApplyConfiguration())
                    .UseInternalServiceProvider(new ServiceCollection()
                        .AddEntityFrameworkSqlCe()
                        .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                        .BuildServiceProvider())
                    .Options;
            }

            public override DbContext CreateContext()
                => new DbContext(_options);

            public override SqlCeTestStore CreateTestStore()
                => SqlCeTestStore.GetOrCreateShared(DatabaseName, EnsureCreated);


        }
    }
}
