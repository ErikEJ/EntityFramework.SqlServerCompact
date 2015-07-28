using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;

namespace Microsoft.Data.Entity.SqlServerCompact.Extensions
{
    public class SqlCeDbContextOptionsBuilder : RelationalDbContextOptionsBuilder<SqlCeDbContextOptionsBuilder, SqlCeOptionsExtension>
    {
        public SqlCeDbContextOptionsBuilder([NotNull] DbContextOptionsBuilder optionsBuilder)
            : base(optionsBuilder)
        {
        }

        protected override SqlCeOptionsExtension CloneExtension()
            => new SqlCeOptionsExtension(OptionsBuilder.Options.GetExtension<SqlCeOptionsExtension>());
    }
}