namespace Microsoft.Data.Entity.Metadata
{
    public class SqlCeAnnotationProvider : IRelationalAnnotationProvider
    {
        public virtual IRelationalEntityTypeAnnotations For(IEntityType entityType) => entityType.SqlCe();
        public virtual IRelationalForeignKeyAnnotations For(IForeignKey foreignKey) => foreignKey.SqlCe();
        public virtual IRelationalIndexAnnotations For(IIndex index) => index.SqlCe();
        public virtual IRelationalKeyAnnotations For(IKey key) => key.SqlCe();
        public virtual IRelationalModelAnnotations For(IModel model) => model.SqlCe();
        public virtual IRelationalPropertyAnnotations For(IProperty property) => property.SqlCe();
    }
}
