using System;
using Microsoft.EntityFrameworkCore.Specification.Tests.TestModels.NullSemanticsModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class NullSemanticsQuerySqlCeFixture : NullSemanticsQueryRelationalFixture<SqlCeTestStore>
    {
        public static readonly string DatabaseName = "NullSemanticsQueryTest";

        private readonly IServiceProvider _serviceProvider;

        private readonly string _connectionString = SqlCeTestStore.CreateConnectionString(DatabaseName);

        public NullSemanticsQuerySqlCeFixture()
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
                    var optionsBuilder = new DbContextOptionsBuilder();
                    optionsBuilder
                        .UseSqlCe(_connectionString)
                        .UseInternalServiceProvider(_serviceProvider);

                    using (var context = new NullSemanticsContext(optionsBuilder.Options))
                    {
                        context.Database.EnsureClean();
                        NullSemanticsModelInitializer.Seed(context);

                        TestSqlLoggerFactory.Reset();
                    }
                });
        }

        public override NullSemanticsContext CreateContext(SqlCeTestStore testStore, bool useRelationalNulls)
        {
            var context = new NullSemanticsContext(new DbContextOptionsBuilder()
                .EnableSensitiveDataLogging()
                .UseInternalServiceProvider(_serviceProvider)
                .UseSqlCe(
                    testStore.Connection,
                    b =>
                        {
                            if (useRelationalNulls)
                            {
                                b.UseRelationalNulls();
                            }
                        }).Options);

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            context.Database.UseTransaction(testStore.Transaction);

            return context;
        }
    }
}