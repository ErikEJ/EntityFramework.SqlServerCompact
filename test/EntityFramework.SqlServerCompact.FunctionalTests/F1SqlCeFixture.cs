using System;
using Microsoft.EntityFrameworkCore.FunctionalTests.TestModels.ConcurrencyModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.FunctionalTests
{
    public class F1SqlCeFixture : F1RelationalFixture<SqlCeTestStore>
    {
        public static readonly string DatabaseName = "OptimisticConcurrencyTest";

        private readonly IServiceProvider _serviceProvider;

        private readonly string _connectionString = SqlCeTestStore.CreateConnectionString(DatabaseName);

        public F1SqlCeFixture()
        {
            _serviceProvider = new ServiceCollection()
                .AddEntityFramework()
                .AddSqlCe()
                .ServiceCollection()
                .AddSingleton(TestSqlCeModelSource.GetFactory(OnModelCreating))
                .AddSingleton<ILoggerFactory>(new TestSqlLoggerFactory())
                .BuildServiceProvider();
        }

        public override SqlCeTestStore CreateTestStore()
        {
            return SqlCeTestStore.GetOrCreateShared(DatabaseName, () =>
            {
                var optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder.UseSqlCe(_connectionString);

                using (var context = new F1Context(_serviceProvider, optionsBuilder.Options))
                {
                    // TODO: Delete DB if model changed
                    context.Database.EnsureDeleted();
                    if (context.Database.EnsureCreated())
                    {
                        ConcurrencyModelInitializer.Seed(context);
                    }

                    TestSqlLoggerFactory.SqlStatements.Clear();
                }
            });
        }

        public override F1Context CreateContext(SqlCeTestStore testStore)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlCe(testStore.Connection);

            var context = new F1Context(_serviceProvider, optionsBuilder.Options);
            context.Database.UseTransaction(testStore.Transaction);
            return context;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Team>(b =>
            {
                b.Property(t => t.Id)
                    .ValueGeneratedNever();
            });
        }
    }
}
