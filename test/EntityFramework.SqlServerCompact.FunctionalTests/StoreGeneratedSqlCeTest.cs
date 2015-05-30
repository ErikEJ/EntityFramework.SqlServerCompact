using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Framework.DependencyInjection;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class StoreGeneratedSqlCeTest
        : StoreGeneratedTestBase<SqlCeTestStore, StoreGeneratedSqlCeTest.StoreGeneratedSqlCeFixture>
    {
        public StoreGeneratedSqlCeTest(StoreGeneratedSqlCeFixture fixture)
            : base(fixture)
        {
        }

        public class StoreGeneratedSqlCeFixture : StoreGeneratedFixtureBase
        {
            private const string DatabaseName = "StoreGeneratedTest";

            private readonly IServiceProvider _serviceProvider;

            public StoreGeneratedSqlCeFixture()
            {
                _serviceProvider = new ServiceCollection()
                    .AddEntityFramework()
                    .AddSqlCe()
                    .ServiceCollection()
                    .AddSingleton(TestSqlCeModelSource.GetFactory(OnModelCreating))
                    .BuildServiceProvider();
            }

            public override SqlCeTestStore CreateTestStore()
            {
                return SqlCeTestStore.GetOrCreateShared(DatabaseName, () =>
                {
                    var optionsBuilder = new EntityOptionsBuilder();
                    optionsBuilder.UseSqlCe(SqlCeTestStore.CreateConnectionString(DatabaseName));

                    using (var context = new StoreGeneratedContext(_serviceProvider, optionsBuilder.Options))
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
                    }
                });
            }

            public override DbContext CreateContext(SqlCeTestStore testStore)
            {
                var optionsBuilder = new EntityOptionsBuilder();
                optionsBuilder.UseSqlCe(testStore.Connection);

                var context = new StoreGeneratedContext(_serviceProvider, optionsBuilder.Options);
                context.Database.AsRelational().Connection.UseTransaction(testStore.Transaction);

                return context;
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Gumball>(b =>
                {
                    b.Property(e => e.Id)
                        .ForSqlCe().UseIdentity();

                    b.Property(e => e.Identity)
                        .DefaultValue("Banana Joe");

                    b.Property(e => e.IdentityReadOnlyBeforeSave)
                        .DefaultValue("Doughnut Sheriff");

                    b.Property(e => e.IdentityReadOnlyAfterSave)
                        .DefaultValue("Anton");

                    b.Property(e => e.Computed)
                        .DefaultValue("Alan");

                    b.Property(e => e.ComputedReadOnlyBeforeSave)
                        .DefaultValue("Carmen");

                    b.Property(e => e.ComputedReadOnlyAfterSave)
                        .DefaultValue("Tina Rex");
                });
            }
        }
    }
}
