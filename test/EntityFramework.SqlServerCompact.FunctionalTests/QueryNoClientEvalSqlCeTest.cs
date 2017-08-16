using Microsoft.EntityFrameworkCore.Query;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class QueryNoClientEvalSqlCeTest : QueryNoClientEvalTestBase<QueryNoClientEvalSqlCeFixture>
    {
        public QueryNoClientEvalSqlCeTest(QueryNoClientEvalSqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}