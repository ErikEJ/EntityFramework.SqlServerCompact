using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore
{
    public class SqlCeServiceCollectionExtensionsTest : RelationalServiceCollectionExtensionsTestBase
    {
        public SqlCeServiceCollectionExtensionsTest()
            : base(SqlCeTestHelpers.Instance)
        {
        }
    }
}
