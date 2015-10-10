using System;
using Microsoft.Data.Entity.FunctionalTests.TestModels.NullSemantics;
using Microsoft.Data.Entity.FunctionalTests.TestModels.NullSemanticsModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.Data.Entity.FunctionalTests
{
    public class NullSemanticsQuerySqlCeFixture : NullSemanticsQueryRelationalFixture<SqlCeTestStore>
    {
        public static readonly string DatabaseName = "NullSemanticsQueryTest";

        private readonly IServiceProvider _serviceProvider;

        private readonly string _connectionString = SqlCeTestStore.CreateConnectionString(DatabaseName);

        public NullSemanticsQuerySqlCeFixture()
        {
            _serviceProvider = new ServiceCollection()
                .AddEntityFramework()
                .AddSqlCe()
                .ServiceCollection()
                .AddSingleton(TestSqlCeModelSource.GetFactory(OnModelCreating))
                .AddInstance<ILoggerFactory>(new TestSqlLoggerFactory())
                .BuildServiceProvider();
        }

        public override SqlCeTestStore CreateTestStore()
        {
            return SqlCeTestStore.GetOrCreateShared(DatabaseName, () =>
            {
                var optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder.UseSqlCe(_connectionString);

                using (var context = new NullSemanticsContext(_serviceProvider, optionsBuilder.Options))
                {
                    // TODO: Delete DB if model changed

                    if (context.Database.EnsureCreated())
                    {
                        NullSemanticsModelInitializer.Seed(context);
                    }

                    TestSqlLoggerFactory.SqlStatements.Clear();
                }
            });
        }

        public override NullSemanticsContext CreateContext(SqlCeTestStore testStore, bool useRelationalNulls)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            var sqlCeOptions = optionsBuilder.UseSqlCe(testStore.Connection);

            sqlCeOptions.LogSqlParameterValues();

            if (useRelationalNulls)
            {
                sqlCeOptions.UseRelationalNulls();
            }

            var context = new NullSemanticsContext(_serviceProvider, optionsBuilder.Options);
            context.Database.UseTransaction(testStore.Transaction);
            return context;
        }
    }
}