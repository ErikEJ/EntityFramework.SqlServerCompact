using System;
using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Query;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class NullKeysSqlCeTest : NullKeysTestBase<NullKeysSqlCeTest.NullKeysSqlCeFixture>
    {
        public NullKeysSqlCeTest(NullKeysSqlCeFixture fixture)
            : base(fixture)
        {
        }

        public class NullKeysSqlCeFixture : NullKeysFixtureBase, IDisposable
        {
            private readonly DbContextOptions _options;
            private readonly SqlCeTestStore _testStore;

            public NullKeysSqlCeFixture()
            {
                var name = "StringsContext";
                var connectionString = SqlCeTestStore.CreateConnectionString(name);

                _options = new DbContextOptionsBuilder()
                    .UseSqlCe(connectionString, b => b.ApplyConfiguration())
                    .UseInternalServiceProvider(new ServiceCollection()
                        .AddEntityFrameworkSqlCe()
                        .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                        .BuildServiceProvider())
                    .Options;

                _testStore = SqlCeTestStore.GetOrCreateShared(name, EnsureCreated);
            }

            public override DbContext CreateContext()
                => new DbContext(_options);

            public void Dispose() => _testStore.Dispose();
        }
    }
}