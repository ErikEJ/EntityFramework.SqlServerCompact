using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    ///     SQL Server Compact specific extension methods for metadata.
    /// </summary>
    public static class SqlCeMetadataExtensions
    {
        /// <summary>
        ///     Gets the SQL Server Compact specific metadata for an entity.
        /// </summary>
        /// <param name="entityType"> The entity to get metadata for. </param>
        /// <returns> The SQL Server Compact specific metadata for the entity. </returns>
        public static IRelationalEntityTypeAnnotations SqlCe([NotNull] this IEntityType entityType)
            => new RelationalEntityTypeAnnotations(Check.NotNull(entityType, nameof(entityType)));

        /// <summary>
        ///     Gets the SQL Server Compact specific metadata for an entity.
        /// </summary>
        /// <param name="entityType"> The entity to get metadata for. </param>
        /// <returns> The SQL Server Compact specific metadata for the entity. </returns>
        public static RelationalEntityTypeAnnotations SqlCe([NotNull] this IMutableEntityType entityType)
            => (RelationalEntityTypeAnnotations)SqlCe((IEntityType)entityType);

        /// <summary>
        ///     Gets the SQL Server Compact specific metadata for a foreign key.
        /// </summary>
        /// <param name="foreignKey"> The foreign key to get metadata for. </param>
        /// <returns> The SQL Server Compact specific metadata for the foreign key. </returns>
        public static IRelationalForeignKeyAnnotations SqlCe([NotNull] this IForeignKey foreignKey)
            => new RelationalForeignKeyAnnotations(Check.NotNull(foreignKey, nameof(foreignKey)));

        /// <summary>
        ///     Gets the SQL Server Compact specific metadata for a foreign key.
        /// </summary>
        /// <param name="foreignKey"> The foreign key to get metadata for. </param>
        /// <returns> The SQL Server Compact specific metadata for the foreign key. </returns>
        public static RelationalForeignKeyAnnotations SqlCe([NotNull] this IMutableForeignKey foreignKey)
            => (RelationalForeignKeyAnnotations)SqlCe((IForeignKey)foreignKey);

        /// <summary>
        ///     Gets the SQL Server Compact specific metadata for an index.
        /// </summary>
        /// <param name="index"> The index to get metadata for. </param>
        /// <returns> The SQL Server Compact specific metadata for the index. </returns>
        public static IRelationalIndexAnnotations SqlCe([NotNull] this IIndex index)
            => new RelationalIndexAnnotations(Check.NotNull(index, nameof(index)));

        /// <summary>
        ///     Gets the SQL Server Compact specific metadata for an index.
        /// </summary>
        /// <param name="index"> The index to get metadata for. </param>
        /// <returns> The SQL Server Compact specific metadata for the index. </returns>
        public static RelationalIndexAnnotations SqlCe([NotNull] this IMutableIndex index)
            => (RelationalIndexAnnotations)SqlCe((IIndex)index);

        /// <summary>
        ///     Gets the SQL Server Compact specific metadata for a key.
        /// </summary>
        /// <param name="key"> The key to get metadata for. </param>
        /// <returns> The SQL Server Compact specific metadata for the key. </returns>
        public static IRelationalKeyAnnotations SqlCe([NotNull] this IKey key)
            => new RelationalKeyAnnotations(Check.NotNull(key, nameof(key)));

        /// <summary>
        ///     Gets the SQL Server Compact specific metadata for a key.
        /// </summary>
        /// <param name="key"> The key to get metadata for. </param>
        /// <returns> The SQL Server Compact specific metadata for the key. </returns>
        public static RelationalKeyAnnotations SqlCe([NotNull] this IMutableKey key)
            => (RelationalKeyAnnotations)SqlCe((IKey)key);

        /// <summary>
        ///     Gets the SQL Server Compact specific metadata for a model.
        /// </summary>
        /// <param name="model"> The model to get metadata for. </param>
        /// <returns> The SQL Server Compact specific metadata for the model. </returns>
        public static RelationalModelAnnotations SqlCe([NotNull] this IMutableModel model)
            => (RelationalModelAnnotations)SqlCe((IModel)model);

        /// <summary>
        ///     Gets the SQL Server Compact specific metadata for a model.
        /// </summary>
        /// <param name="model"> The model to get metadata for. </param>
        /// <returns> The SQL Server Compact specific metadata for the model. </returns>
        public static IRelationalModelAnnotations SqlCe([NotNull] this IModel model)
            => new RelationalModelAnnotations(Check.NotNull(model, nameof(model)));

        /// <summary>
        ///     Gets the SQL Server Compact specific metadata for a property.
        /// </summary>
        /// <param name="property"> The property to get metadata for. </param>
        /// <returns> The SQL Server Compact specific metadata for the property. </returns>
        public static IRelationalPropertyAnnotations SqlCe([NotNull] this IProperty property)
            => new RelationalPropertyAnnotations(Check.NotNull(property, nameof(property)));

        /// <summary>
        ///     Gets the SQL Server Compact specific metadata for a property.
        /// </summary>
        /// <param name="property"> The property to get metadata for. </param>
        /// <returns> The SQL Server Compact specific metadata for the property. </returns>
        public static RelationalPropertyAnnotations SqlCe([NotNull] this IMutableProperty property)
            => (RelationalPropertyAnnotations)SqlCe((IProperty)property);
    }
}
