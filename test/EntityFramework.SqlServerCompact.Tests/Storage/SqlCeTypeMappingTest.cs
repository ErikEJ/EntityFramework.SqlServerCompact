using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Microsoft.EntityFrameworkCore.Storage
{
    public class SqlCeTypeMappingTest : RelationalTypeMappingTest
    {
        protected override DbCommand CreateTestCommand()
            => new SqlCommand();

        protected override DbType DefaultParameterType
            => DbType.Int32;
    }
}
