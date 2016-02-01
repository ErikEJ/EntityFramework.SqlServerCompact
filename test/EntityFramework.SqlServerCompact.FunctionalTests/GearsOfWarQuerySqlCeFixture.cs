using System;
using Microsoft.EntityFrameworkCore.FunctionalTests.TestModels.GearsOfWarModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.FunctionalTests
{
    public class GearsOfWarQuerySqlCeFixture : GearsOfWarQueryRelationalFixture<SqlCeTestStore>
    {
        public static readonly string DatabaseName = "GearsOfWarQueryTest";

        private readonly IServiceProvider _serviceProvider;

        private readonly string _connectionString = SqlCeTestStore.CreateConnectionString(DatabaseName);

        public GearsOfWarQuerySqlCeFixture()
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

                using (var context = new GearsOfWarContext(_serviceProvider, optionsBuilder.Options))
                {
                    // TODO: Delete DB if model changed

                    if (context.Database.EnsureCreated())
                    {
                        GearsOfWarModelInitializer.Seed(context);
                    }

                    TestSqlLoggerFactory.SqlStatements.Clear();
                }
            });
        }

        public override GearsOfWarContext CreateContext(SqlCeTestStore testStore)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder
                .EnableSensitiveDataLogging()
                .UseSqlCe(testStore.Connection);

            var context = new GearsOfWarContext(_serviceProvider, optionsBuilder.Options);
            context.Database.UseTransaction(testStore.Transaction);
            return context;
        }
    }
}
