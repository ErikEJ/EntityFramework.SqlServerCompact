using Microsoft.Data.Entity;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.Tests
{
    public class SqlCeDataStoreSourceTest
    {
        [Fact]
        public void Returns_appropriate_name()
        {
            Assert.Equal(typeof(SqlCeDataStore).Name, new SqlCeDataStoreSource().Name);
        }

        [Fact]
        public void Is_configured_when_configuration_contains_associated_extension()
        {
            var optionsBuilder = new EntityOptionsBuilder();
            optionsBuilder.UseSqlCe("Database=Crunchie");

            Assert.True(new SqlCeDataStoreSource().IsConfigured(optionsBuilder.Options));
        }

        [Fact]
        public void Can_not_be_auto_configured()
        {
            var optionsBuilder = new EntityOptionsBuilder();

            var dataStoreSource = new SqlCeDataStoreSource();
            dataStoreSource.AutoConfigure(optionsBuilder);

            Assert.False(dataStoreSource.IsConfigured(optionsBuilder.Options));
        }

        [Fact]
        public void Is_not_configured_when_configuration_does_not_contain_associated_extension()
        {
            var optionsBuilder = new EntityOptionsBuilder();

            Assert.False(new SqlCeDataStoreSource().IsConfigured(optionsBuilder.Options));
        }
    }
}
