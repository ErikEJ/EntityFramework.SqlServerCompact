using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class SqlCePropertyBuilderExtensions
    {
        public static PropertyBuilder ForSqlCeHasColumnName([NotNull] this PropertyBuilder builder, [CanBeNull] string name)
        {
            Check.NotNull(builder, nameof(builder));
            Check.NullButNotEmpty(name, nameof(name));

            builder.Metadata.SqlCe().ColumnName = name;

            return builder;
        }

        public static PropertyBuilder<TEntity> ForSqlCeHasColumnName<TEntity>(
            [NotNull] this PropertyBuilder<TEntity> builder,
            [CanBeNull] string name)
            where TEntity : class
            => (PropertyBuilder<TEntity>)((PropertyBuilder)builder).ForSqlCeHasColumnName(name);

        public static PropertyBuilder HasSqlCeColumnType([NotNull] this PropertyBuilder builder, [CanBeNull] string type)
        {
            Check.NotNull(builder, nameof(builder));
            Check.NullButNotEmpty(type, nameof(type));

            builder.Metadata.SqlCe().ColumnType = type;

            return builder;
        }

        public static PropertyBuilder<TEntity> ForSqlCeHasColumnType<TEntity>(
            [NotNull] this PropertyBuilder<TEntity> builder,
            [CanBeNull] string type)
            where TEntity : class
            => (PropertyBuilder<TEntity>)builder.HasSqlCeColumnType(type);

        public static PropertyBuilder ForSqlCeHasDefaultValueSql(
            [NotNull] this PropertyBuilder builder,
            [CanBeNull] string sql)
        {
            Check.NotNull(builder, nameof(builder));
            Check.NullButNotEmpty(sql, nameof(sql));

            builder.ValueGeneratedOnAdd();
            builder.Metadata.SqlCe().GeneratedValueSql = sql;

            return builder;
        }

        public static PropertyBuilder<TEntity> ForSqlCeHasDefaultValueSql<TEntity>(
            [NotNull] this PropertyBuilder<TEntity> builder,
            [CanBeNull] string sql)
            where TEntity : class
            => (PropertyBuilder<TEntity>)((PropertyBuilder)builder).ForSqlCeHasDefaultValueSql(sql);
    }
}
