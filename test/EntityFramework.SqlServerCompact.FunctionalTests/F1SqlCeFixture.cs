using System;
using Microsoft.EntityFrameworkCore.Specification.Tests.TestModels.ConcurrencyModel;
using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class F1SqlCeFixture : F1RelationalFixture<SqlCeTestStore>
    {
        public static readonly string DatabaseName = "OptimisticConcurrencyTest";

        private readonly IServiceProvider _serviceProvider;

        private readonly string _connectionString = SqlCeTestStore.CreateConnectionString(DatabaseName);

        public F1SqlCeFixture()
        {
            _serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlCe()
                .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                .AddSingleton<ILoggerFactory>(new TestSqlLoggerFactory())
                .BuildServiceProvider();
        }

        public override SqlCeTestStore CreateTestStore()
        {
            return SqlCeTestStore.GetOrCreateShared(DatabaseName, () =>
            {
                var optionsBuilder = new DbContextOptionsBuilder()
                    .UseSqlCe(_connectionString, b => b.ApplyConfiguration())
                    .UseInternalServiceProvider(_serviceProvider);

                using (var context = new F1Context(optionsBuilder.Options))
                {
                    context.Database.EnsureClean();
                    ConcurrencyModelInitializer.Seed(context);

                    TestSqlLoggerFactory.Reset();
                }
            });
        }

        public override F1Context CreateContext(SqlCeTestStore testStore)
        {
            var optionsBuilder = new DbContextOptionsBuilder()
                .UseSqlCe(testStore.Connection, b => b.ApplyConfiguration())
                .UseInternalServiceProvider(_serviceProvider);

            var context = new F1Context(optionsBuilder.Options);
            context.Database.UseTransaction(testStore.Transaction);
            return context;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chassis>().Property<byte[]>("Version").IsRowVersion();
            modelBuilder.Entity<Driver>().Property<byte[]>("Version").IsRowVersion();

            modelBuilder.Entity<Team>().Property<byte[]>("Version")
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();
        }
    }
}