using System;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Data.Entity;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
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
            private readonly EntityOptions _options;

            public NullKeysSqlServerCeFixture()
            {
                _serviceProvider = new ServiceCollection()
                    .AddEntityFramework()
                    .AddSqlCe()
                    .ServiceCollection()
                    .AddSingleton(TestSqlCeModelSource.GetFactory(OnModelCreating))
                    .BuildServiceProvider();

                var optionsBuilder = new EntityOptionsBuilder();
                optionsBuilder.UseSqlCe(SqlCeTestStore.CreateConnectionString("StringsContext"));
                _options = optionsBuilder.Options;

                EnsureCreated();
            }

            public override DbContext CreateContext()
            {
                return new DbContext(_serviceProvider, _options);
            }
        }
    }
}
