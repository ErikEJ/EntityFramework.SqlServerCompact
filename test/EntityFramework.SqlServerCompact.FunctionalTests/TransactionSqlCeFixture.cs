using System;
using System.Data.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.FunctionalTests
{
    public class TransactionSqlCeFixture : TransactionFixtureBase<SqlCeTestStore>
    {
        private readonly IServiceProvider _serviceProvider;

        public TransactionSqlCeFixture()
        {
            _serviceProvider = new ServiceCollection()
                .AddEntityFramework()
                .AddSqlCe()
                .ServiceCollection()
                .AddSingleton(TestSqlCeModelSource.GetFactory(OnModelCreating))
                .BuildServiceProvider();
        }

        public override SqlCeTestStore CreateTestStore()
        {
            var db = SqlCeTestStore.CreateScratch(createDatabase: true);

            using (var context = CreateContext(db))
            {
                Seed(context);
            }

            return db;
        }

        public override DbContext CreateContext(SqlCeTestStore testStore)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlCe(testStore.Connection.ConnectionString);

            return new DbContext(_serviceProvider, optionsBuilder.Options);
        }

        public override DbContext CreateContext(DbConnection connection)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlCe(connection);

            return new DbContext(_serviceProvider, optionsBuilder.Options);
        }
    }
}
