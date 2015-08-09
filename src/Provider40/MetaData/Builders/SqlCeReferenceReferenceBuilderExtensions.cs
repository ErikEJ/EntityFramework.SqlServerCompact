using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class SqlCeReferenceReferenceBuilderExtensions
    {
        public static ReferenceReferenceBuilder SqlCeConstraintName(
            [NotNull] this ReferenceReferenceBuilder builder,
            [CanBeNull] string name)
        {
            Check.NotNull(builder, nameof(builder));
            Check.NullButNotEmpty(name, nameof(name));

            builder.Metadata.SqlCe().Name = name;

            return builder;
        }

        public static ReferenceReferenceBuilder<TEntity, TReferencedEntity> SqlCeConstraintName<TEntity, TReferencedEntity>(
            [NotNull] this ReferenceReferenceBuilder<TEntity, TReferencedEntity> builder,
            [CanBeNull] string name)
            where TEntity : class
            where TReferencedEntity : class
            => (ReferenceReferenceBuilder<TEntity, TReferencedEntity>)((ReferenceReferenceBuilder)builder).SqlCeConstraintName(name);
    }
}
