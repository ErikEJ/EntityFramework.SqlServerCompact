using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore
{
    public class CompositeKeyEndToEndSqlCeTest : CompositeKeyEndToEndTestBase<CompositeKeyEndToEndSqlCeTest.CompositeKeyEndToEndSqlCeFixture>
    {
        public CompositeKeyEndToEndSqlCeTest(CompositeKeyEndToEndSqlCeFixture fixture)
            : base(fixture)
        {
        }
        
        public class CompositeKeyEndToEndSqlCeFixture : CompositeKeyEndToEndFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;
        }
    }
}
