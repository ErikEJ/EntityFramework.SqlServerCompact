using System;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.FunctionalTests
{
    public abstract class GraphUpdatesSqlCeTestBase<TFixture> : GraphUpdatesTestBase<SqlCeTestStore, TFixture>
        where TFixture : GraphUpdatesSqlCeTestBase<TFixture>.GraphUpdatesSqlCeFixtureBase, new()
    {
        protected GraphUpdatesSqlCeTestBase(TFixture fixture)
            : base(fixture)
        {
        }

        public abstract class GraphUpdatesSqlCeFixtureBase : GraphUpdatesFixtureBase
        {
            private readonly IServiceProvider _serviceProvider;

            protected GraphUpdatesSqlCeFixtureBase()
            {
                _serviceProvider = new ServiceCollection()
                    .AddEntityFramework()
                    .AddSqlCe()
                    .ServiceCollection()
                    .AddSingleton(TestSqlCeModelSource.GetFactory(OnModelCreating))
                    .BuildServiceProvider();
            }

            protected abstract string DatabaseName { get; }

            public override SqlCeTestStore CreateTestStore()
            {
                return SqlCeTestStore.GetOrCreateShared(DatabaseName, () =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder();
                    optionsBuilder.UseSqlCe(SqlCeTestStore.CreateConnectionString(DatabaseName));

                    using (var context = new GraphUpdatesContext(_serviceProvider, optionsBuilder.Options))
                    {
                        context.Database.EnsureDeleted();
                        if (context.Database.EnsureCreated())
                        {
                            Seed(context);
                        }
                    }
                });
            }

            public override DbContext CreateContext(SqlCeTestStore testStore)
            {
                var optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder.UseSqlCe(testStore.Connection);

                var context = new GraphUpdatesContext(_serviceProvider, optionsBuilder.Options);
                context.Database.UseTransaction(testStore.Transaction);
                return context;
            }
        }
    }
}
