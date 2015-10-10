using System.Data.SqlServerCe;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Storage.Internal;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Microsoft.Data.Entity.Tests
{
    public class SqlCeDataStoreConnectionTest
    {
        [Fact]
        public void Creates_SQL_ServerCe_connection_string()
        {
            using (var connection = new SqlCeDatabaseConnection(CreateOptions(), new Logger<SqlCeDatabaseConnection>(new LoggerFactory())))
            {
                Assert.IsType<SqlCeConnection>(connection.DbConnection);
            }
        }

        public static IDbContextOptions CreateOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlCe(@"Data Source=C:\data\EF7SQLCE.sdf;");

            return optionsBuilder.Options;
        }
    }
}
