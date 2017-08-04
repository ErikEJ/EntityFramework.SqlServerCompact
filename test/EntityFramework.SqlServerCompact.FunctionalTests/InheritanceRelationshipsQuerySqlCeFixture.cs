using System;
using Microsoft.EntityFrameworkCore.TestModels.InheritanceRelationships;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Query;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class InheritanceRelationshipsQuerySqlCeFixture : InheritanceRelationshipsQueryRelationalFixture<SqlCeTestStore>
    {
        public static readonly string DatabaseName = "InheritanceRelationships";

        private readonly IServiceProvider _serviceProvider;

        public TestSqlLoggerFactory TestSqlLoggerFactory { get; } = new TestSqlLoggerFactory();

        private readonly string _connectionString = SqlCeTestStore.CreateConnectionString(DatabaseName);

        public InheritanceRelationshipsQuerySqlCeFixture()
        {
            _serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlCe()
                .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                .AddSingleton<ILoggerFactory>(TestSqlLoggerFactory)
                .BuildServiceProvider();
        }

        public override SqlCeTestStore CreateTestStore()
        {
            return SqlCeTestStore.GetOrCreateShared(DatabaseName, () =>
            {
                var optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder
                    .UseSqlCe(_connectionString)
                    .UseInternalServiceProvider(_serviceProvider);

                using (var context = new InheritanceRelationshipsContext(optionsBuilder.Options))
                {
                    context.Database.EnsureClean();
                    InheritanceRelationshipsModelInitializer.Seed(context);
                }
            });
        }

        public override InheritanceRelationshipsContext CreateContext(SqlCeTestStore testStore)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder
                .UseSqlCe(testStore.Connection)
                .UseInternalServiceProvider(_serviceProvider);

            var context = new InheritanceRelationshipsContext(optionsBuilder.Options);
            context.Database.UseTransaction(testStore.Transaction);
            return context;
        }
    }
}
