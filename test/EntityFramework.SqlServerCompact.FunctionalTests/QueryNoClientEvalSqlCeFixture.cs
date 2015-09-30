using Microsoft.Data.Entity.Infrastructure;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class QueryNoClientEvalSqlCeFixture : NorthwindQuerySqlCeFixture
    {
        protected override void ConfigureOptions(SqlCeDbContextOptionsBuilder sqlCeDbContextOptionsBuilder)
            => sqlCeDbContextOptionsBuilder.DisableQueryClientEvaluation();
    }
}