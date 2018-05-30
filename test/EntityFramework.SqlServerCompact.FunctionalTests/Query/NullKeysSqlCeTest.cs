using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class NullKeysSqlCeTest : NullKeysTestBase<NullKeysSqlCeTest.NullKeysSqlCeFixture>
    {
        public NullKeysSqlCeTest(NullKeysSqlCeFixture fixture)
            : base(fixture)
        {
        }

        public class NullKeysSqlCeFixture : NullKeysFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;
        }
    }
}