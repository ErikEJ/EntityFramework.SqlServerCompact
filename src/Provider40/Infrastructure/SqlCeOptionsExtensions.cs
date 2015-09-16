using JetBrains.Annotations;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.DependencyInjection;

namespace Microsoft.Data.Entity.Infrastructure
{
    public class SqlCeOptionsExtension : RelationalOptionsExtension
    {
        public SqlCeOptionsExtension()
        {
        }

        public SqlCeOptionsExtension([NotNull] SqlCeOptionsExtension copyFrom)
            : base(copyFrom)
        {
        }

        public override void ApplyServices(EntityFrameworkServicesBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            builder.AddSqlCe();
        }
    }
}
