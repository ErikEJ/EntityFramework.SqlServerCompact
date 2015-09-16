using System.Data.SqlServerCe;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Storage;
using Microsoft.Framework.Logging;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.Tests
{
    public class SqlCeDataStoreConnectionTest
    {
        [Fact]
        public void Creates_SQL_ServerCe_connection_string()
        {
            using (var connection = new SqlCeDatabaseConnection(CreateOptions(), new LoggerFactory()))
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
