using JetBrains.Annotations;
using Microsoft.Data.Entity.Internal;

namespace Microsoft.Data.Entity.Infrastructure
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