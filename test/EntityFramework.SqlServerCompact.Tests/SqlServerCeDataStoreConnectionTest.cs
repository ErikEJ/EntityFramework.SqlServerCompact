using System.Data.SqlServerCe;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.Logging;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.Tests
{
    public class SqlServerCeDataStoreConnectionTest
    {
        [Fact]
        public void Creates_SQL_ServerCe_connection_string()
        {
            using (var connection = new SqlServerCeDataStoreConnection(CreateOptions(), new LoggerFactory()))
            {
                Assert.IsType<SqlCeConnection>(connection.DbConnection);
            }
        }

        public static IDbContextOptions CreateOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServerCe(@"Data Source=C:\data\EF7SQLCE.sdf;");

            return optionsBuilder.Options;
        }
    }
}
