using Microsoft.Data.Entity.FunctionalTests;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class QueryNoClientEvalSqlCeTest : QueryNoClientEvalTestBase<QueryNoClientEvalSqlCeFixture>
    {
        public QueryNoClientEvalSqlCeTest(QueryNoClientEvalSqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}