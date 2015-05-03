using JetBrains.Annotations;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Metadata;
using Microsoft.Data.Entity.Relational.Update;

namespace ErikEJ.Data.Entity.SqlServerCe.Update
{
    public class SqlServerCeModificationCommandBatch : SingularModificationCommandBatch
    {
        public SqlServerCeModificationCommandBatch([NotNull] ISqlGenerator sqlGenerator)
            : base(sqlGenerator)
        {
        }

        public override IRelationalPropertyExtensions GetPropertyExtensions(IProperty property) => 
            property.Relational();
    }
}
