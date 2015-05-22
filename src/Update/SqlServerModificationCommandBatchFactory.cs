using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Metadata;
using Microsoft.Data.Entity.Relational.Update;

namespace ErikEJ.Data.Entity.SqlServerCe.Update
{
    public class SqlServerCeModificationCommandBatchFactory : ModificationCommandBatchFactory
    {
        public SqlServerCeModificationCommandBatchFactory([NotNull] ISqlGenerator sqlGenerator)
            : base(sqlGenerator)
        {
        }

        public override ModificationCommandBatch Create(
           IDbContextOptions options,
           IRelationalMetadataExtensionProvider metadataExtensionProvider)
           => new SqlServerCeModificationCommandBatch(SqlGenerator, metadataExtensionProvider);
    }
}
