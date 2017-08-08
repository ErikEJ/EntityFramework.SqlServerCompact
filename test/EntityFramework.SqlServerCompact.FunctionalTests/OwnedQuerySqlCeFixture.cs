using System;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Specification.Tests;
using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class OwnedQuerySqlCeFixture : OwnedQueryFixtureBase, IDisposable
    {
        private readonly DbContextOptions _options;
        private readonly SqlCeTestStore _testStore;

        public TestSqlLoggerFactory TestSqlLoggerFactory { get; } = new TestSqlLoggerFactory();

        public OwnedQuerySqlCeFixture()
        {
            _testStore = SqlCeTestStore.Create("OwnedQueryTest");

            _options = new DbContextOptionsBuilder()
                .UseSqlCe(_testStore.ConnectionString, b => b.ApplyConfiguration())
                .UseInternalServiceProvider(
                    new ServiceCollection()
                        .AddEntityFrameworkSqlCe()
                        .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                        .AddSingleton<ILoggerFactory>(TestSqlLoggerFactory)
                        .BuildServiceProvider(validateScopes: true))
                .Options;

            using (var context = new DbContext(_options))
            {
                context.Database.EnsureCreated();

                AddTestData(context);
            }
        }

        public DbContext CreateContext() => new DbContext(_options);

        public void Dispose() => _testStore.Dispose();
    }
}