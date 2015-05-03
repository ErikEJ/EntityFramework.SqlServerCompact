using JetBrains.Annotations;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;

namespace ErikEJ.Data.Entity.SqlServerCe.Migrations
{
    public class SqlServerCeModelDiffer : ModelDiffer, ISqlServerCeModelDiffer
    {
        public SqlServerCeModelDiffer([NotNull] ISqlServerCeTypeMapper typeMapper)
            : base(typeMapper)
        {
        }
    }
}
