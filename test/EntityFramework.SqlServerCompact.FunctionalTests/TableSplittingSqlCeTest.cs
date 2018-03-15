using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore
{
    public class TableSplittingSqlCeTest : TableSplittingTestBase
    {
        public TableSplittingSqlCeTest(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;
    }
}