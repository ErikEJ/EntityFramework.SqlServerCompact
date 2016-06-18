using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    ///     SQL Server Compact specific extension methods for <see cref="ReferenceCollectionBuilder"/>.
    /// </summary>
    public static class SqlCeReferenceCollectionBuilderExtensions
    {
        /// <summary>
        ///     Configures the foreign key constraint name for this relationship when targeting SQL Server Compact.
        /// </summary>
        /// <param name="referenceCollectionBuilder"> The builder being used to configure the relationship. </param>
        /// <param name="name"> The name of the foreign key constraint. </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
        public static ReferenceCollectionBuilder ForSqlCeHasConstraintName(
            [NotNull] this ReferenceCollectionBuilder referenceCollectionBuilder,
            [CanBeNull] string name)
        {
            Check.NotNull(referenceCollectionBuilder, nameof(referenceCollectionBuilder));
            Check.NullButNotEmpty(name, nameof(name));

            referenceCollectionBuilder.Metadata.SqlCe().Name = name;

            return referenceCollectionBuilder;
        }

        /// <summary>
        ///     Configures the foreign key constraint name for this relationship when targeting SQL Server Compact.
        /// </summary>
        /// <param name="referenceCollectionBuilder"> The builder being used to configure the relationship. </param>
        /// <param name="name"> The name of the foreign key constraint. </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
        /// <typeparam name="TEntity"> The principal entity type in this relationship. </typeparam>
        /// <typeparam name="TRelatedEntity"> The dependent entity type in this relationship. </typeparam>
        public static ReferenceCollectionBuilder<TEntity, TRelatedEntity> ForSqlCeHasConstraintName<TEntity, TRelatedEntity>(
            [NotNull] this ReferenceCollectionBuilder<TEntity, TRelatedEntity> referenceCollectionBuilder,
            [CanBeNull] string name)
            where TEntity : class
            where TRelatedEntity : class
            => (ReferenceCollectionBuilder<TEntity, TRelatedEntity>)((ReferenceCollectionBuilder)referenceCollectionBuilder).ForSqlCeHasConstraintName(name);
    }
}
