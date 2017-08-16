using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Specification.Tests;

namespace Microsoft.EntityFrameworkCore
{
    public class ConcurrencyDetectorSqlServerTest : ConcurrencyDetectorRelationalTest<NorthwindQuerySqlCeFixture>
    {
        public ConcurrencyDetectorSqlServerTest(NorthwindQuerySqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}