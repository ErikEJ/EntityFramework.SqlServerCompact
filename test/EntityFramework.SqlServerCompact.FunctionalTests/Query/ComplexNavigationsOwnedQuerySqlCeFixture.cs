using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class ComplexNavigationsOwnedQuerySqlCeFixture : ComplexNavigationsOwnedQueryRelationalFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;
    }
}