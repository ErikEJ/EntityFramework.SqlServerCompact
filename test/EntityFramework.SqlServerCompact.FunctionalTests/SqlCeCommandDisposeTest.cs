using System.Data.SqlClient;
using System.Data.SqlServerCe;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    class SqlCeCommandDisposeTest
    {
        [Fact]
        public void SqlCeCommandCannotBeLoggedAfterDispose()
        {
            var command = new SqlCeCommand();

            command.CommandText = "foo";
            command.Parameters.Add(new SqlCeParameter());
            command.Dispose();

            Assert.Equal(command.CommandText, string.Empty);
            Assert.Equal(command.Parameters.Count, 0);
        }

        [Fact]
        public void SqlCommandCanBeLoggedAfterDispose()
        {
            var command = new SqlCommand();

            command.CommandText = "bar";
            command.Parameters.Add(new SqlParameter());
            command.Dispose();

            Assert.Equal(command.CommandText, "bar");
            Assert.Equal(command.Parameters.Count, 1);
        }
    }
}
