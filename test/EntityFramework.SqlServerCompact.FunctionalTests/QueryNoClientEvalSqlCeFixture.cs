namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class QueryNoClientEvalSqlCeFixture : NorthwindQuerySqlCeFixture
    {
        protected override DbContextOptionsBuilder ConfigureOptions(DbContextOptionsBuilder dbContextOptionsBuilder)
            => dbContextOptionsBuilder.ConfigureWarnings(c => c.Default(WarningBehavior.Throw));
    }
}