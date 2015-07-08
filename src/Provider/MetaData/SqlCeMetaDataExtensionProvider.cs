using Microsoft.Data.Entity.Metadata;

namespace Microsoft.Data.Entity.SqlServerCompact.MetaData
{
    public class SqlCeMetadataExtensionProvider : IRelationalMetadataExtensionProvider
    {
        // TODO: Update with #875
        public virtual IRelationalEntityTypeAnnotations Extensions(IEntityType entityType) => entityType.Relational();
        public virtual IRelationalForeignKeyAnnotations Extensions(IForeignKey foreignKey) => foreignKey.Relational();
        public virtual IRelationalIndexAnnotations Extensions(IIndex index) => index.Relational();
        public virtual IRelationalKeyAnnotations Extensions(IKey key) => key.Relational();
        public virtual IRelationalModelAnnotations Extensions(IModel model) => model.Relational();
        public virtual IRelationalPropertyAnnotations Extensions(IProperty property) => property.Relational();
    }
}
