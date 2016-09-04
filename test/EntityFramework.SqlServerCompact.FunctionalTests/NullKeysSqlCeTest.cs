using System;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class NullKeysSqlServerCeTest : NullKeysTestBase<NullKeysSqlServerCeTest.NullKeysSqlServerCeFixture>
    {
        public NullKeysSqlServerCeTest(NullKeysSqlServerCeFixture fixture)
            : base(fixture)
        {
        }

        public class NullKeysSqlServerCeFixture : NullKeysFixtureBase
        {
            private readonly IServiceProvider _serviceProvider;
            private readonly DbContextOptions _options;

            public NullKeysSqlServerCeFixture()
            {
                _serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkSqlCe()
                    .AddSingleton(TestSqlCeModelSource.GetFactory(OnModelCreating))
                    .BuildServiceProvider();

                var optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder
                    .UseSqlCe(SqlCeTestStore.CreateConnectionString("StringsContext"))
                    .UseInternalServiceProvider(_serviceProvider);
                _options = optionsBuilder.Options;

                EnsureCreated();
            }

            public override DbContext CreateContext()
            {
                return new DbContext(_options);
            }

            protected override void EnsureClean(DbContext context)
            {
                //TODO EEJJ Why is EnsureClean broken?
                //context.Database.EnsureClean();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}
