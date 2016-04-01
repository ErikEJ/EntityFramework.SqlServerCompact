using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.EntityFrameworkCore
{
    public static class SqlCeMetadataExtensions
    {
        public static IRelationalEntityTypeAnnotations SqlCe([NotNull] this IEntityType entityType)
            => new RelationalEntityTypeAnnotations(Check.NotNull(entityType, nameof(entityType)), SqlCeFullAnnotationNames.Instance);

        public static RelationalEntityTypeAnnotations SqlCe([NotNull] this IMutableEntityType entityType)
            => (RelationalEntityTypeAnnotations)SqlCe((IEntityType)entityType);

        public static IRelationalForeignKeyAnnotations SqlCe([NotNull] this IForeignKey foreignKey)
            => new RelationalForeignKeyAnnotations(Check.NotNull(foreignKey, nameof(foreignKey)), SqlCeFullAnnotationNames.Instance);

        public static RelationalForeignKeyAnnotations SqlCe([NotNull] this IMutableForeignKey foreignKey)
            => (RelationalForeignKeyAnnotations)SqlCe((IForeignKey)foreignKey);

        public static IRelationalIndexAnnotations SqlCe([NotNull] this IIndex index)
            => new RelationalIndexAnnotations(Check.NotNull(index, nameof(index)), SqlCeFullAnnotationNames.Instance);

        public static RelationalIndexAnnotations SqlCe([NotNull] this IMutableIndex index)
            => (RelationalIndexAnnotations)SqlCe((IIndex)index);

        public static IRelationalKeyAnnotations SqlCe([NotNull] this IKey key)
            => new RelationalKeyAnnotations(Check.NotNull(key, nameof(key)), SqlCeFullAnnotationNames.Instance);

        public static RelationalKeyAnnotations SqlCe([NotNull] this IMutableKey key)
            => (RelationalKeyAnnotations)SqlCe((IKey)key);

        public static RelationalModelAnnotations SqlCe([NotNull] this IMutableModel model)
            => (RelationalModelAnnotations)SqlCe((IModel)model);

        public static IRelationalModelAnnotations SqlCe([NotNull] this IModel model)
            => new RelationalModelAnnotations(Check.NotNull(model, nameof(model)), SqlCeFullAnnotationNames.Instance);

        public static IRelationalPropertyAnnotations SqlCe([NotNull] this IProperty property)
            => new RelationalPropertyAnnotations(Check.NotNull(property, nameof(property)), SqlCeFullAnnotationNames.Instance);

        public static RelationalPropertyAnnotations SqlCe([NotNull] this IMutableProperty property)
            => (RelationalPropertyAnnotations)SqlCe((IProperty)property);
    }
}
