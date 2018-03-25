using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore
{
    public class SqlCeServiceCollectionExtensionsTest : EntityFrameworkServiceCollectionExtensionsTest
    {
        public SqlCeServiceCollectionExtensionsTest()
            : base(SqlCeTestHelpers.Instance)
        {
        }
    }
}