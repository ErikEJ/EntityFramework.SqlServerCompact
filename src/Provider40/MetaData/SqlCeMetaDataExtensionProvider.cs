using Microsoft.Data.Entity.Metadata;

namespace Microsoft.Data.Entity.SqlServerCompact.Metadata
{
    public class SqlCeMetadataExtensionProvider : IRelationalMetadataExtensionProvider
    {
        public virtual IRelationalEntityTypeAnnotations For(IEntityType entityType) => entityType.SqlCe();
        public virtual IRelationalForeignKeyAnnotations For(IForeignKey foreignKey) => foreignKey.SqlCe();
        public virtual IRelationalIndexAnnotations For(IIndex index) => index.SqlCe();
        public virtual IRelationalKeyAnnotations For(IKey key) => key.SqlCe();
        public virtual IRelationalModelAnnotations For(IModel model) => model.SqlCe();
        public virtual IRelationalPropertyAnnotations For(IProperty property) => property.SqlCe();
    }
}
