using System;
using Microsoft.EntityFrameworkCore.Specification.Tests.TestModels.NullSemanticsModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class NullSemanticsQuerySqlCeFixture : NullSemanticsQueryRelationalFixture<SqlCeTestStore>
    {
        public static readonly string DatabaseName = "NullSemanticsQueryTest";

        private readonly DbContextOptions _options;

        private readonly string _connectionString = SqlCeTestStore.CreateConnectionString(DatabaseName);

        public NullSemanticsQuerySqlCeFixture()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlCe()
                .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                .AddSingleton<ILoggerFactory>(new TestSqlLoggerFactory())
                .BuildServiceProvider();

            _options = new DbContextOptionsBuilder()
                .EnableSensitiveDataLogging()
                .UseInternalServiceProvider(serviceProvider)
                .Options;
        }

        public override SqlCeTestStore CreateTestStore()
        {
            return SqlCeTestStore.GetOrCreateShared(DatabaseName, () =>
                {
                    using (var context = new NullSemanticsContext(new DbContextOptionsBuilder(_options)
                        .UseSqlCe(_connectionString, b => b.ApplyConfiguration()).Options))
                    { 
                        context.Database.EnsureClean();
                        NullSemanticsModelInitializer.Seed(context);

                        TestSqlLoggerFactory.Reset();
                    }
                });
        }

        public override NullSemanticsContext CreateContext(SqlCeTestStore testStore, bool useRelationalNulls)
        {
            var options = new DbContextOptionsBuilder(_options)
                .UseSqlCe(
                    testStore.Connection,
                    b =>
                    {
                        b.ApplyConfiguration();
                        if (useRelationalNulls)
                        {
                            b.UseRelationalNulls();
                        }
                    }).Options;

            var context = new NullSemanticsContext(options);

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            context.Database.UseTransaction(testStore.Transaction);

            return context;
        }
    }
}