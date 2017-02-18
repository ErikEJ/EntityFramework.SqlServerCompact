namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class SqlCeServiceCollectionExtensionsTest : EntityFrameworkServiceCollectionExtensionsTest
    {
        public SqlCeServiceCollectionExtensionsTest()
            : base(SqlCeTestHelpers.Instance)
        {
        }
    }
}