using System.Reflection;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.SqlServerCompact;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Tests;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.Tests
{
    public class SqlCeDataStoreSourceTest
    {
        [Fact]
        public void Is_configured_when_configuration_contains_associated_extension()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlCe("Data Source=Crunchie");

            Assert.True(new DatabaseProvider<SqlCeDatabaseProviderServices, SqlCeOptionsExtension>().IsConfigured(optionsBuilder.Options));
        }

        [Fact]
        public void Is_not_configured_when_configuration_does_not_contain_associated_extension()
        {
            var optionsBuilder = new DbContextOptionsBuilder();

            Assert.False(new DatabaseProvider<SqlCeDatabaseProviderServices, SqlCeOptionsExtension>().IsConfigured(optionsBuilder.Options));
        }

        [Fact]
        public void Returns_appropriate_name()
        {
            Assert.Equal(
                typeof(SqlCeDatabase).GetTypeInfo().Assembly.GetName().Name,
                new SqlCeDatabaseProviderServices(SqlCeTestHelpers.Instance.CreateServiceProvider()).InvariantName);
        }
    }
}
