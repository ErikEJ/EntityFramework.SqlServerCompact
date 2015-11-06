using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Microsoft.Data.Entity.Storage
{
    public class SqlServerTypeMappingTest : RelationalTypeMappingTest
    {
        protected override DbCommand CreateTestCommand()
            => new SqlCommand();

        protected override DbType DefaultParameterType
            => DbType.Int32;
    }
}
