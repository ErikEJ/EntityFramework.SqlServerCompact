using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.EntityFrameworkCore
{
    public static class SqlCeReferenceReferenceBuilderExtensions
    {
        public static ReferenceReferenceBuilder ForSqlCeHasConstraintName(
            [NotNull] this ReferenceReferenceBuilder builder,
            [CanBeNull] string name)
        {
            Check.NotNull(builder, nameof(builder));
            Check.NullButNotEmpty(name, nameof(name));

            builder.Metadata.SqlCe().Name = name;

            return builder;
        }

        public static ReferenceReferenceBuilder<TEntity, TReferencedEntity> ForSqlCeHasConstraintName<TEntity, TReferencedEntity>(
            [NotNull] this ReferenceReferenceBuilder<TEntity, TReferencedEntity> builder,
            [CanBeNull] string name)
            where TEntity : class
            where TReferencedEntity : class
            => (ReferenceReferenceBuilder<TEntity, TReferencedEntity>)((ReferenceReferenceBuilder)builder).ForSqlCeHasConstraintName(name);
    }
}
