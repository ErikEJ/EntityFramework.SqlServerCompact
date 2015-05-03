using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.DependencyInjection;

namespace ErikEJ.Data.Entity.SqlServerCe
{
    public class SqlServerCeOptionsExtension : RelationalOptionsExtension
    {
        public SqlServerCeOptionsExtension()
        {
        }

        public SqlServerCeOptionsExtension([NotNull] SqlServerCeOptionsExtension copyFrom)
            : base(copyFrom)
        {
        }

        public override void ApplyServices(EntityFrameworkServicesBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            //TODO - but requires everything to be implemented!
            //builder.AddSqlServerCe();
        }
    }
}
