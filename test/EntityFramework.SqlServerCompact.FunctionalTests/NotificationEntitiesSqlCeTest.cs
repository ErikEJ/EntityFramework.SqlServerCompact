using System;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class NotificationEntitiesSqlCeTest
        : NotificationEntitiesTestBase<NotificationEntitiesSqlCeTest.NotificationEntitiesSqlCeFixture>
    {
        public NotificationEntitiesSqlCeTest(NotificationEntitiesSqlCeFixture fixture)
            : base(fixture)
        {
        }

        public class NotificationEntitiesSqlCeFixture : NotificationEntitiesFixtureBase
        {
            private readonly IServiceProvider _serviceProvider;
            private readonly DbContextOptions _options;

            public NotificationEntitiesSqlCeFixture()
            {
                _serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkSqlCe()
                    .AddSingleton(TestSqlCeModelSource.GetFactory(OnModelCreating))
                    .BuildServiceProvider();

                var optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder
                    .UseSqlCe(SqlCeTestStore.CreateConnectionString("NotificationEntities"))
                    .UseInternalServiceProvider(_serviceProvider);
                _options = optionsBuilder.Options;

                EnsureCreated();
            }

            public override DbContext CreateContext()
                => new DbContext(_options);

            protected override void EnsureClean(DbContext context)
                => context.Database.EnsureClean();
        }
    }
}
