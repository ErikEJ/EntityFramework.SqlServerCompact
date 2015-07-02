using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Update;

namespace Microsoft.Data.Entity.SqlServerCompact.Update
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
