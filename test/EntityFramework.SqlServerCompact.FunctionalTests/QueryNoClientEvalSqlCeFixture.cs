using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore.FunctionalTests
{
    public class QueryNoClientEvalSqlCeFixture : NorthwindQuerySqlCeFixture
    {
        protected override void ConfigureOptions(SqlCeDbContextOptionsBuilder sqlCeDbContextOptionsBuilder)
            => sqlCeDbContextOptionsBuilder.QueryClientEvaluationBehavior(QueryClientEvaluationBehavior.Throw);
    }
}