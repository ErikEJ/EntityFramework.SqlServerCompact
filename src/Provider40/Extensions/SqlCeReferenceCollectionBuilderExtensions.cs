using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class SqlCeReferenceCollectionBuilderExtensions
    {
        public static ReferenceCollectionBuilder ForSqlCeHasConstraintName(
            [NotNull] this ReferenceCollectionBuilder builder,
            [CanBeNull] string name)
        {
            Check.NotNull(builder, nameof(builder));
            Check.NullButNotEmpty(name, nameof(name));

            builder.Metadata.SqlCe().Name = name;

            return builder;
        }

        public static ReferenceCollectionBuilder<TEntity, TReferencedEntity> ForSqlCeHasConstraintName<TEntity, TReferencedEntity>(
            [NotNull] this ReferenceCollectionBuilder<TEntity, TReferencedEntity> builder,
            [CanBeNull] string name)
            where TEntity : class
            where TReferencedEntity : class
            => (ReferenceCollectionBuilder<TEntity, TReferencedEntity>)((ReferenceCollectionBuilder)builder).ForSqlCeHasConstraintName(name);
    }
}
