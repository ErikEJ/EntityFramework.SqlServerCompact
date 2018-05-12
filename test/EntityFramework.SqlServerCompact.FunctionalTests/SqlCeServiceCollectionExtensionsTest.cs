using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore
{
    public class SqlCeServiceCollectionExtensionsTest : EntityFrameworkServiceCollectionExtensionsTestBase
    {
        public SqlCeServiceCollectionExtensionsTest()
            : base(SqlCeTestHelpers.Instance)
        {
        }
    }
}
