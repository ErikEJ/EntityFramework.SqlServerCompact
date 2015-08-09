using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.SqlServerCompact.Metadata;
using Microsoft.Data.Entity.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class SqlCeMetadataExtensions
    {
        public static IRelationalEntityTypeAnnotations SqlCe([NotNull] this IEntityType entityType)
            => new RelationalEntityTypeAnnotations(Check.NotNull(entityType, nameof(entityType)), SqlCeAnnotationNames.Prefix);

        public static RelationalEntityTypeAnnotations SqlCe([NotNull] this EntityType entityType)
            => (RelationalEntityTypeAnnotations)SqlCe((IEntityType)entityType);

        public static IRelationalForeignKeyAnnotations SqlCe([NotNull] this IForeignKey foreignKey)
            => new RelationalForeignKeyAnnotations(Check.NotNull(foreignKey, nameof(foreignKey)), SqlCeAnnotationNames.Prefix);

        public static RelationalForeignKeyAnnotations SqlCe([NotNull] this ForeignKey foreignKey)
            => (RelationalForeignKeyAnnotations)SqlCe((IForeignKey)foreignKey);

        public static IRelationalIndexAnnotations SqlCe([NotNull] this IIndex index)
            => new RelationalIndexAnnotations(Check.NotNull(index, nameof(index)), SqlCeAnnotationNames.Prefix);

        public static RelationalIndexAnnotations SqlCe([NotNull] this Index index)
            => (RelationalIndexAnnotations)SqlCe((IIndex)index);

        public static IRelationalKeyAnnotations SqlCe([NotNull] this IKey key)
            => new RelationalKeyAnnotations(Check.NotNull(key, nameof(key)), SqlCeAnnotationNames.Prefix);

        public static RelationalKeyAnnotations SqlCe([NotNull] this Key key)
            => (RelationalKeyAnnotations)SqlCe((IKey)key);

        public static RelationalModelAnnotations SqlCe([NotNull] this Model model)
            => (RelationalModelAnnotations)SqlCe((IModel)model);

        public static IRelationalModelAnnotations SqlCe([NotNull] this IModel model)
            => new RelationalModelAnnotations(Check.NotNull(model, nameof(model)), SqlCeAnnotationNames.Prefix);

        public static IRelationalPropertyAnnotations SqlCe([NotNull] this IProperty property)
            => new RelationalPropertyAnnotations(Check.NotNull(property, nameof(property)), SqlCeAnnotationNames.Prefix);

        public static RelationalPropertyAnnotations SqlCe([NotNull] this Property property)
            => (RelationalPropertyAnnotations)SqlCe((IProperty)property);
    }
}
