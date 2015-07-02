using Microsoft.Data.Entity.FunctionalTests;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class IncludeSqlCeTest : IncludeTestBase<NorthwindQuerySqlCeFixture>
    {
        public IncludeSqlCeTest(NorthwindQuerySqlCeFixture fixture)
            : base(fixture)
        {
        }

        private static string Sql => TestSqlLoggerFactory.Sql;
    }
}
