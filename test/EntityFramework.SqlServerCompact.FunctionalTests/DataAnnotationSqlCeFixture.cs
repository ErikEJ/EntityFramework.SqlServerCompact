using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;

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
                .AddSingleton(TestSqlCeModelSource.GetFactory(OnModelCreating))
                .AddSingleton<ILoggerFactory>(new TestSqlLoggerFactory())
                .BuildServiceProvider();
        }

        public override ModelValidator ThrowingValidator
            => new ThrowingModelValidator(
                _serviceProvider.GetService<ILogger<RelationalModelValidator>>(),
                new SqlCeAnnotationProvider(),
                new SqlCeTypeMapper());

        private class ThrowingModelValidator : RelationalModelValidator
        {
            public ThrowingModelValidator(
                ILogger<RelationalModelValidator> loggerFactory,
                IRelationalAnnotationProvider relationalExtensions,
                IRelationalTypeMapper typeMapper)
                : base(loggerFactory, relationalExtensions, typeMapper)
            {
            }

            protected override void ShowWarning(string message)
            {
                throw new InvalidOperationException(message);
            }
        }

        public override SqlCeTestStore CreateTestStore()
        {
            return SqlCeTestStore.GetOrCreateShared(DatabaseName, () =>
            {
                var optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder
                    .UseSqlCe(_connectionString)
                    .UseInternalServiceProvider(_serviceProvider);

                using (var context = new DataAnnotationContext(optionsBuilder.Options))
                {
                    context.Database.EnsureClean();
                    DataAnnotationModelInitializer.Seed(context);

                    TestSqlLoggerFactory.Reset();
                }
            });
        }

        public override DataAnnotationContext CreateContext(SqlCeTestStore testStore)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder
                .EnableSensitiveDataLogging()
                .UseSqlCe(testStore.Connection)
                .UseInternalServiceProvider(_serviceProvider);

            var context = new DataAnnotationContext(optionsBuilder.Options);
            context.Database.UseTransaction(testStore.Transaction);
            return context;

        }
    }
}
