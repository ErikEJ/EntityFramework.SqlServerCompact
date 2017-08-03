using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public abstract class GraphUpdatesSqlCeTestBase<TFixture> : GraphUpdatesTestBase<SqlCeTestStore, TFixture>
        where TFixture : GraphUpdatesSqlCeTestBase<TFixture>.GraphUpdatesSqlCeFixtureBase, new()
    {
        protected GraphUpdatesSqlCeTestBase(TFixture fixture)
            : base(fixture)
        {
        }

        protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
            => facade.UseTransaction(transaction.GetDbTransaction());

        public abstract class GraphUpdatesSqlCeFixtureBase : GraphUpdatesFixtureBase
        {
            private readonly IServiceProvider _serviceProvider;
            private DbContextOptions _options;

            protected GraphUpdatesSqlCeFixtureBase()
            {
                _serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkSqlCe()
                    .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                    .BuildServiceProvider();
            }

            protected abstract string DatabaseName { get; }

            public override SqlCeTestStore CreateTestStore()
            {
                var testStore = SqlCeTestStore.CreateScratch(true);

                _options = new DbContextOptionsBuilder()
                    .UseSqlCe(testStore.Connection, b => b.ApplyConfiguration())
                    .UseInternalServiceProvider(_serviceProvider)
                    .Options;

                using (var context = new GraphUpdatesContext(_options))
                {
                    context.Database.EnsureClean();
                    Seed(context);
                }

                return testStore;
            }

            public override DbContext CreateContext(SqlCeTestStore testStore)
                => new GraphUpdatesContext(_options);
        }
    }
}