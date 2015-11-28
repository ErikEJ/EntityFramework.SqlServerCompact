using Microsoft.Data.Entity.Infrastructure;

namespace Microsoft.Data.Entity.FunctionalTests
{
    public class QueryNoClientEvalSqlCeFixture : NorthwindQuerySqlCeFixture
    {
        protected override void ConfigureOptions(SqlCeDbContextOptionsBuilder sqlCeDbContextOptionsBuilder)
            => sqlCeDbContextOptionsBuilder.QueryClientEvaluationBehavior(QueryClientEvaluationBehavior.Throw);
    }
}