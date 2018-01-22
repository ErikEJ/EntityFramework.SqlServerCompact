using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class NullSemanticsQuerySqlCeFixture : NullSemanticsQueryRelationalFixture
    {
        protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;
    }
}