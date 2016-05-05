using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class QueryNoClientEvalSqlCeFixture : NorthwindQuerySqlCeFixture
    {
        protected override void ConfigureOptions(SqlCeDbContextOptionsBuilder sqlCeDbContextOptionsBuilder)
            => sqlCeDbContextOptionsBuilder.QueryClientEvaluationBehavior(QueryClientEvaluationBehavior.Throw);
    }
}