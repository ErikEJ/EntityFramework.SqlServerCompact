using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class IncludeSqlCeFixture : NorthwindQuerySqlCeFixture<NoopModelCustomizer>
    {
        public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
            => base.AddOptions(builder).ConfigureWarnings(c => c.Log(CoreEventId.IncludeIgnoredWarning));
    }
}
