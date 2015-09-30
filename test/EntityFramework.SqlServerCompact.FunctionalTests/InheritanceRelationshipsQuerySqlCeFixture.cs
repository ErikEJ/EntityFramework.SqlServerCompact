using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.FunctionalTests.TestModels.InheritanceRelationships;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class InheritanceRelationshipsQuerySqlCeFixture : InheritanceRelationshipsQueryRelationalFixture<SqlCeTestStore>
    {
        public static readonly string DatabaseName = "InheritanceRelationships";

        private readonly IServiceProvider _serviceProvider;

        private readonly string _connectionString = SqlCeTestStore.CreateConnectionString(DatabaseName);

        public InheritanceRelationshipsQuerySqlCeFixture()
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

                using (var context = new InheritanceRelationshipsContext(_serviceProvider, optionsBuilder.Options))
                {
                    // TODO: Delete DB if model changed
                    context.Database.EnsureDeleted();
                    if (context.Database.EnsureCreated())
                    {
                        InheritanceRelationshipsModelInitializer.Seed(context);
                    }

                    TestSqlLoggerFactory.SqlStatements.Clear();
                }
            });
        }

        public override InheritanceRelationshipsContext CreateContext(SqlCeTestStore testStore)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlCe(testStore.Connection);

            var context = new InheritanceRelationshipsContext(_serviceProvider, optionsBuilder.Options);
            context.Database.UseTransaction(testStore.Transaction);
            return context;
        }
    }
}
