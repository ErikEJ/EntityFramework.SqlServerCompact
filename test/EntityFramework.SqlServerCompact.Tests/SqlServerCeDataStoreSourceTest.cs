using Microsoft.Data.Entity;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.Tests
{
    public class SqlServerCeDataStoreSourceTest
    {
        [Fact]
        public void Returns_appropriate_name()
        {
            Assert.Equal(typeof(SqlServerCeDataStore).Name, new SqlServerCeDataStoreSource().Name);
        }

        [Fact]
        public void Is_configured_when_configuration_contains_associated_extension()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlCe("Database=Crunchie");

            Assert.True(new SqlServerCeDataStoreSource().IsConfigured(optionsBuilder.Options));
        }

        [Fact]
        public void Can_not_be_auto_configured()
        {
            var optionsBuilder = new DbContextOptionsBuilder();

            var dataStoreSource = new SqlServerCeDataStoreSource();
            dataStoreSource.AutoConfigure(optionsBuilder);

            Assert.False(dataStoreSource.IsConfigured(optionsBuilder.Options));
        }

        [Fact]
        public void Is_not_configured_when_configuration_does_not_contain_associated_extension()
        {
            var optionsBuilder = new DbContextOptionsBuilder();

            Assert.False(new SqlServerCeDataStoreSource().IsConfigured(optionsBuilder.Options));
        }
    }
}
