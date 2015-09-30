using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class SqlCeEntityTypeBuilderExtensions
    {
        public static EntityTypeBuilder ToSqlCeTable([NotNull] this EntityTypeBuilder builder, [CanBeNull] string name)
        {
            Check.NotNull(builder, nameof(builder));
            Check.NullButNotEmpty(name, nameof(name));

            builder.Metadata.SqlCe().TableName = name;

            return builder;
        }

        public static EntityTypeBuilder<TEntity> ToSqlCeTable<TEntity>(
            [NotNull] this EntityTypeBuilder<TEntity> builder,
            [CanBeNull] string name)
            where TEntity : class
            => (EntityTypeBuilder<TEntity>)((EntityTypeBuilder)builder).ToSqlCeTable(name);
    }
}
