using System;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class PropertyValuesSqlCeTest
        : PropertyValuesTestBase<SqlCeTestStore, PropertyValuesSqlCeTest.PropertyValuesSqlCeFixture>
    {
        public PropertyValuesSqlCeTest(PropertyValuesSqlCeFixture fixture)
            : base(fixture)
        {
        }

        public class PropertyValuesSqlCeFixture : PropertyValuesFixtureBase
        {
            private const string DatabaseName = "PropertyValues";

            private readonly IServiceProvider _serviceProvider;

            public PropertyValuesSqlCeFixture()
            {
                _serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkSqlCe()
                    .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                    .BuildServiceProvider();
            }

            public override SqlCeTestStore CreateTestStore()
            {
                return SqlCeTestStore.GetOrCreateShared(DatabaseName, () =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder()
                        .UseSqlCe(SqlCeTestStore.CreateConnectionString(DatabaseName))
                        .UseInternalServiceProvider(_serviceProvider);

                    using (var context = new AdvancedPatternsMasterContext(optionsBuilder.Options))
                    {
                        context.Database.EnsureClean();
                        Seed(context);
                    }
                });
            }

            public override DbContext CreateContext(SqlCeTestStore testStore)
            {
                var optionsBuilder = new DbContextOptionsBuilder()
                    .UseSqlCe(testStore.Connection)
                    .UseInternalServiceProvider(_serviceProvider);

                var context = new AdvancedPatternsMasterContext(optionsBuilder.Options);
                context.Database.UseTransaction(testStore.Transaction);

                return context;
            }
        }
    }
}
