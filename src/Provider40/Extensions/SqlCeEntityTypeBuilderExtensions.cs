using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    ///     SQL Server Compact specific extension methods for <see cref="EntityTypeBuilder"/>.
    /// </summary>
    public static class SqlCeEntityTypeBuilderExtensions
    {
        /// <summary>
        ///     Configures the table that the entity maps to when targeting SQL Server Compact.
        /// </summary>
        /// <param name="entityTypeBuilder"> The builder for the entity type being configured. </param>
        /// <param name="name"> The name of the table. </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
        public static EntityTypeBuilder ForSqlCeToTable([NotNull] this EntityTypeBuilder entityTypeBuilder, [CanBeNull] string name)
        {
            Check.NotNull(entityTypeBuilder, nameof(entityTypeBuilder));
            Check.NullButNotEmpty(name, nameof(name));

            entityTypeBuilder.Metadata.SqlCe().TableName = name;

            return entityTypeBuilder;
        }

        /// <summary>
        ///     Configures the table that the entity maps to when targeting SQL Server Compact.
        /// </summary>
        /// <typeparam name="TEntity"> The entity type being configured. </typeparam>
        /// <param name="entityTypeBuilder"> The builder for the entity type being configured. </param>
        /// <param name="name"> The name of the table. </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
        public static EntityTypeBuilder<TEntity> ForSqlCeToTable<TEntity>(
            [NotNull] this EntityTypeBuilder<TEntity> entityTypeBuilder,
            [CanBeNull] string name)
            where TEntity : class
            => (EntityTypeBuilder<TEntity>)((EntityTypeBuilder)entityTypeBuilder).ForSqlCeToTable(name);
    }
}
