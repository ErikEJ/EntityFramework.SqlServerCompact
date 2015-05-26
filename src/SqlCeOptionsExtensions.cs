using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.DependencyInjection;

namespace ErikEJ.Data.Entity.SqlServerCe
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
