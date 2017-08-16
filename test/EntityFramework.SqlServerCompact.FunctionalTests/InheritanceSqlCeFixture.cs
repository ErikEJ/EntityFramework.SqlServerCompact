using Microsoft.EntityFrameworkCore.Specification.Tests;
using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;
using Microsoft.EntityFrameworkCore.TestModels.Inheritance;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class InheritanceSqlCeFixture : InheritanceRelationalFixture<SqlCeTestStore>
    {
        protected virtual string DatabaseName => "InheritanceSqlCeTest";

        private readonly DbContextOptions _options;

        public TestSqlLoggerFactory TestSqlLoggerFactory { get; } = new TestSqlLoggerFactory();

        public InheritanceSqlCeFixture()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlCe()
                .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                .AddSingleton<ILoggerFactory>(TestSqlLoggerFactory)
                .BuildServiceProvider(validateScopes: true);

            _options = new DbContextOptionsBuilder()
                .EnableSensitiveDataLogging()
                .UseInternalServiceProvider(serviceProvider)
                .Options;
        }

        public override SqlCeTestStore CreateTestStore()
        {
            return SqlCeTestStore.GetOrCreateShared(
                DatabaseName, () =>
                {
                    using (var context = new InheritanceContext(
                        new DbContextOptionsBuilder(_options)
                            .UseSqlCe(
                                SqlCeTestStore.CreateConnectionString(DatabaseName),
                                b => b.ApplyConfiguration())
                            .Options))
                    {
                        context.Database.EnsureCreated();
                        InheritanceModelInitializer.SeedData(context);
                    }
                });
        }

        public override InheritanceContext CreateContext(SqlCeTestStore testStore)
        {
            var context = new InheritanceContext(
                new DbContextOptionsBuilder(_options)
                    .UseSqlCe(testStore.Connection, b => b.ApplyConfiguration())
                    .Options);

            context.Database.UseTransaction(testStore.Transaction);

            return context;
        }
    }
}