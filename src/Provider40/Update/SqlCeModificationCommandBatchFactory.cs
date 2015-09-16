using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;

namespace Microsoft.Data.Entity.Update
{
    public class SqlCeModificationCommandBatchFactory : ModificationCommandBatchFactory
    {
        public SqlCeModificationCommandBatchFactory([NotNull] IUpdateSqlGenerator sqlGenerator)
            : base(sqlGenerator)
        {
        }

        public override ModificationCommandBatch Create(
           IDbContextOptions options,
           IRelationalMetadataExtensionProvider metadataExtensionProvider)
           => new SqlCeModificationCommandBatch(UpdateSqlGenerator);
    }
}
