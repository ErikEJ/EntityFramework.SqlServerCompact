using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class InheritanceSqlCeFixture : InheritanceRelationalFixture
    {
        protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;
    }
}