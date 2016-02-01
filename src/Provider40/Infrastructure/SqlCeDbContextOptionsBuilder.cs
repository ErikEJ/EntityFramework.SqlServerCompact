using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace Microsoft.EntityFrameworkCore.Infrastructure
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