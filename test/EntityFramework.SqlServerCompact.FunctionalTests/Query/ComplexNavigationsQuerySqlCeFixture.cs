using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class ComplexNavigationsQuerySqlCeFixture : ComplexNavigationsQueryRelationalFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;
    }
}