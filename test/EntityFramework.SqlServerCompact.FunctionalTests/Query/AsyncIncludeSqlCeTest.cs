using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class AsyncIncludeSqlCeTest : IncludeAsyncTestBase<IncludeSqlCeFixture>
    {
        public AsyncIncludeSqlCeTest(IncludeSqlCeFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            //fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
        }

        [Fact(Skip = "SQLCE limitation")]
        public override Task Include_collection_order_by_subquery()
        {
            return base.Include_collection_order_by_subquery();
        }

        [SqlServerCondition(SqlServerCondition.SupportsOffset)] 
        public override Task Include_duplicate_reference()
        {
            return base.Include_duplicate_reference();
        }
    }
}
