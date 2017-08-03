using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class DataAnnotationSqlCeFixture : DataAnnotationFixtureBase<SqlCeTestStore>
    {
        public static readonly string DatabaseName = "DataAnnotations";

        private readonly DbContextOptions _options;

        private readonly string _connectionString = SqlCeTestStore.CreateConnectionString(DatabaseName);

        public TestSqlLoggerFactory TestSqlLoggerFactory { get; } = new TestSqlLoggerFactory();

        public DataAnnotationSqlCeFixture()
        {
             var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlCe()
                .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                .AddSingleton<ILoggerFactory>(TestSqlLoggerFactory)
                .BuildServiceProvider();

            _options = new DbContextOptionsBuilder()
                .EnableSensitiveDataLogging()
                .UseInternalServiceProvider(serviceProvider)
                .ConfigureWarnings(w =>
                {
                    w.Default(WarningBehavior.Throw);
                    w.Ignore(CoreEventId.SensitiveDataLoggingEnabledWarning);
                }).Options;
        }

        public override SqlCeTestStore CreateTestStore()
            => SqlCeTestStore.GetOrCreateShared(DatabaseName, () =>
                {
                    var options = new DbContextOptionsBuilder(_options)
                        .UseSqlCe(_connectionString, b => b.ApplyConfiguration())
                        .Options;

                    using (var context = new DataAnnotationContext(options))
                    {
                        context.Database.EnsureClean();
                        DataAnnotationModelInitializer.Seed(context);
                    }
                });

        public override DataAnnotationContext CreateContext(SqlCeTestStore testStore)
        {
            var options = new DbContextOptionsBuilder(_options)
                .UseSqlCe(testStore.Connection, b => b.ApplyConfiguration())
                .Options;

            var context = new DataAnnotationContext(options);
            context.Database.UseTransaction(testStore.Transaction);
            return context;
        }
    }
}
