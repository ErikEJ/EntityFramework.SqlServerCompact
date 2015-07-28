using Microsoft.Data.Entity.Metadata;

namespace Microsoft.Data.Entity.SqlServerCompact.MetaData
{
    public class SqlCeMetadataExtensionProvider : IRelationalMetadataExtensionProvider
    {
        //TODO ErikEJ Implement Metadata
        public virtual IRelationalEntityTypeAnnotations For(IEntityType entityType) => null;
        public virtual IRelationalForeignKeyAnnotations For(IForeignKey foreignKey) => null;
        public virtual IRelationalIndexAnnotations For(IIndex index) => null;
        public virtual IRelationalKeyAnnotations For(IKey key) => null;
        public virtual IRelationalModelAnnotations For(IModel model) => null;
        public virtual IRelationalPropertyAnnotations For(IProperty property) => null;
    }
}
