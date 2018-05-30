using Microsoft.EntityFrameworkCore.TestUtilities;
using System.IO;

namespace Microsoft.EntityFrameworkCore
{
    public class MigrationsSqlCeFixture : MigrationsFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;

        public MigrationsSqlCeFixture()
        {
            if (File.Exists("TransactionSuppressed.sdf"))
            {
                File.Delete("TransactionSuppressed.sdf");
            }
        }

        public override MigrationsContext CreateContext()
        {
            var options = AddOptions(
                    new DbContextOptionsBuilder()
                        .UseSqlCe(TestStore.ConnectionString, b => b.ApplyConfiguration()))
                .UseInternalServiceProvider(ServiceProvider)
                .Options;
            return new MigrationsContext(options);
        }
    }
}