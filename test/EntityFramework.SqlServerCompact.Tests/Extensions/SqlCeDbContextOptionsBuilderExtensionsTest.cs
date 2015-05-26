using System.Data.SqlServerCe;
using System.Linq;
using Microsoft.Data.Entity;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.Extensions
{
    public class SqlCeDbContextOptionsBuilderExtensionsTest
    {
        [Fact]
        public void Can_add_extension_with_connection_string()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlCe("Data Source=C:\\data\\Unicorn.sdf");

            var extension = optionsBuilder.Options.Extensions.OfType<SqlCeOptionsExtension>().Single();

            Assert.Equal("Data Source=C:\\data\\Unicorn.sdf", extension.ConnectionString);
            Assert.Equal(1, extension.MaxBatchSize);
            Assert.Null(extension.Connection);
        }

        [Fact]
        public void Can_add_extension_with_connection_string_using_generic_options()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlCe("Data Source=C:\\data\\Multicorn.sdf");

            var extension = optionsBuilder.Options.Extensions.OfType<SqlCeOptionsExtension>().Single();

            Assert.Equal("Data Source=C:\\data\\Multicorn.sdf", extension.ConnectionString);
            Assert.Equal(1, extension.MaxBatchSize);
            Assert.Null(extension.Connection);
        }

        [Fact]
        public void Can_add_extension_with_connection()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            var connection = new SqlCeConnection();

            optionsBuilder.UseSqlCe(connection);

            var extension = optionsBuilder.Options.Extensions.OfType<SqlCeOptionsExtension>().Single();

            Assert.Same(connection, extension.Connection);
            Assert.Equal(1, extension.MaxBatchSize);
            Assert.Null(extension.ConnectionString);
        }

        [Fact]
        public void Can_add_extension_with_connection_using_generic_options()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            var connection = new SqlCeConnection();

            optionsBuilder.UseSqlCe(connection);

            var extension = optionsBuilder.Options.Extensions.OfType<SqlCeOptionsExtension>().Single();

            Assert.Same(connection, extension.Connection);
            Assert.Equal(1, extension.MaxBatchSize);
            Assert.Null(extension.ConnectionString);
        }
    }
}
