using System;
using Microsoft.EntityFrameworkCore.Specification.Tests.TestModels.ComplexNavigationsModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class ComplexNavigationsQuerySqlCeFixture : ComplexNavigationsQueryRelationalFixture<SqlCeTestStore>
    {
        public static readonly string DatabaseName = "ComplexNavigations";

        private readonly IServiceProvider _serviceProvider;

        private readonly string _connectionString = SqlCeTestStore.CreateConnectionString(DatabaseName);

        public ComplexNavigationsQuerySqlCeFixture()
        {
            _serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlCe()
                .AddSingleton(TestSqlCeModelSource.GetFactory(OnModelCreating))
                .AddSingleton<ILoggerFactory>(new TestSqlLoggerFactory())
                .BuildServiceProvider();
        }

        public override SqlCeTestStore CreateTestStore() =>
            SqlCeTestStore.GetOrCreateShared(
                DatabaseName,
                () =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder();
                    optionsBuilder
                        .UseSqlCe(_connectionString)
                        .UseInternalServiceProvider(_serviceProvider);

                    using (var context = new ComplexNavigationsContext(optionsBuilder.Options))
                    {
                        context.Database.EnsureClean();
                        ComplexNavigationsModelInitializer.Seed(context);

                        TestSqlLoggerFactory.Reset();
                    }
                });

        public override ComplexNavigationsContext CreateContext(SqlCeTestStore testStore)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder
                .UseSqlCe(testStore.Connection)
                .UseInternalServiceProvider(_serviceProvider);

            var context = new ComplexNavigationsContext(optionsBuilder.Options);
            context.Database.UseTransaction(testStore.Transaction);

            return context;
        }
    }
}

