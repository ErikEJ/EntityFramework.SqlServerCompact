using System.Data.SqlServerCe;
using System.Linq;
using ErikEJ.Data.Entity.SqlServerCompact;
using Xunit;

namespace Microsoft.Data.Entity.SqlServerCompact.Extensions
{
    public class SqlServerCompactDbContextOptionsBuilderExtensionsTest
    {
        [Fact]
        public void Can_add_extension_with_connection_string()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServerCompact("Data Source=C:\\data\\Unicorn.sdf");

            var extension = optionsBuilder.Options.Extensions.OfType<SqlServerCompactOptionsExtension>().Single();

            Assert.Equal("Data Source=C:\\data\\Unicorn.sdf", extension.ConnectionString);
            Assert.Equal(1, extension.MaxBatchSize);
            Assert.Null(extension.Connection);
        }

        [Fact]
        public void Can_add_extension_with_connection_string_using_generic_options()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlServerCompact("Data Source=C:\\data\\Multicorn.sdf");

            var extension = optionsBuilder.Options.Extensions.OfType<SqlServerCompactOptionsExtension>().Single();

            Assert.Equal("Data Source=C:\\data\\Multicorn.sdf", extension.ConnectionString);
            Assert.Equal(1, extension.MaxBatchSize);
            Assert.Null(extension.Connection);
        }

        [Fact]
        public void Can_add_extension_with_connection()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            var connection = new SqlCeConnection();

            optionsBuilder.UseSqlServerCompact(connection);

            var extension = optionsBuilder.Options.Extensions.OfType<SqlServerCompactOptionsExtension>().Single();

            Assert.Same(connection, extension.Connection);
            Assert.Equal(1, extension.MaxBatchSize);
            Assert.Null(extension.ConnectionString);
        }

        [Fact]
        public void Can_add_extension_with_connection_using_generic_options()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            var connection = new SqlCeConnection();

            optionsBuilder.UseSqlServerCompact(connection);

            var extension = optionsBuilder.Options.Extensions.OfType<SqlServerCompactOptionsExtension>().Single();

            Assert.Same(connection, extension.Connection);
            Assert.Equal(1, extension.MaxBatchSize);
            Assert.Null(extension.ConnectionString);
        }
    }
}
