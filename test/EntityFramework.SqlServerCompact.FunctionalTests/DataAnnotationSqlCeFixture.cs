using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class DataAnnotationSqlCeFixture : DataAnnotationFixtureBase<SqlCeTestStore>
    {
        public static readonly string DatabaseName = "DataAnnotations";

        private readonly IServiceProvider _serviceProvider;

        private readonly string _connectionString = SqlCeTestStore.CreateConnectionString(DatabaseName);

        public DataAnnotationSqlCeFixture()
        {
            _serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlCe()
                .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                .AddSingleton<ILoggerFactory>(new TestSqlLoggerFactory())
                .BuildServiceProvider();
        }

        public override SqlCeTestStore CreateTestStore()
            => SqlCeTestStore.GetOrCreateShared(DatabaseName, () =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder();
                    optionsBuilder
                        .UseSqlCe(_connectionString, b => b.ApplyConfiguration())
                        .UseInternalServiceProvider(_serviceProvider);

                    using (var context = new DataAnnotationContext(optionsBuilder.Options))
                    {
                        context.Database.EnsureClean();
                        DataAnnotationModelInitializer.Seed(context);

                        TestSqlLoggerFactory.Reset();
                    }
                });

        public override DataAnnotationContext CreateContext(SqlCeTestStore testStore)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder
                .EnableSensitiveDataLogging()
                .UseSqlCe(testStore.Connection)
                .UseInternalServiceProvider(_serviceProvider);

            var context = new DataAnnotationContext(optionsBuilder.Options);
            //context.Database.UseTransaction(testStore.Transaction);
            return context;
        }
    }
}
