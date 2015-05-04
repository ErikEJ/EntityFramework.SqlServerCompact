using System.Reflection;
using ErikEJ.Data.Entity.SqlServerCe;

namespace Microsoft.Data.Entity.SqlServer.Tests
{
    public class ApiConsistencyTest : ApiConsistencyTestBase
    {
        protected override Assembly TargetAssembly => typeof(SqlServerCeDataStore).Assembly;
    }
}
