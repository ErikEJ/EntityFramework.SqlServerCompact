using System.Data.SqlServerCe;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Tests
{
    public class SqlCeDataStoreConnectionTest
    {
        [Fact]
        public void Creates_SQL_ServerCe_connection_string()
        {
            using (var connection = new SqlCeDatabaseConnection(CreateDependencies()))
            {
                Assert.IsType<SqlCeConnection>(connection.DbConnection);
            }
        }

        public static RelationalConnectionDependencies CreateDependencies(DbContextOptions options = null)
        {
            options = options
                      ?? new DbContextOptionsBuilder()
                          .UseSqlCe(@"Data Source=C:\data\EF7SQLCE.sdf;")
                          .Options;

            return new RelationalConnectionDependencies(options, new Logger<SqlCeDatabaseConnection>(new LoggerFactory()), new DiagnosticListener("Fake"));
        }
    }
}
