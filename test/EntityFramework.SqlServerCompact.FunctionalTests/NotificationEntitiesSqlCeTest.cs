using System;
using Microsoft.EntityFrameworkCore.FunctionalTests;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.SqlCe.FunctionalTests
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
                    .AddEntityFramework()
                    .AddSqlCe()
                    .ServiceCollection()
                    .AddSingleton(TestSqlCeModelSource.GetFactory(OnModelCreating))
                    .BuildServiceProvider();

                var optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder.UseSqlCe(SqlCeTestStore.CreateConnectionString("NotificationEntities"));
                _options = optionsBuilder.Options;

                EnsureCreated();
            }

            public override DbContext CreateContext()
                => new DbContext(_serviceProvider, _options);
        }
    }
}
