using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.EntityFrameworkCore
{
    public static class SqlCePropertyBuilderExtensions
    {
        public static PropertyBuilder ForSqlCeHasColumnName(
            [NotNull] this PropertyBuilder propertyBuilder,
            [CanBeNull] string name)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));
            Check.NullButNotEmpty(name, nameof(name));

            propertyBuilder.Metadata.SqlCe().ColumnName = name;

            return propertyBuilder;
        }

        public static PropertyBuilder<TProperty> ForSqlCeHasColumnName<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder,
            [CanBeNull] string name)
            => (PropertyBuilder<TProperty>)ForSqlCeHasColumnName((PropertyBuilder)propertyBuilder, name);

        public static PropertyBuilder ForSqlCeHasColumnType(
            [NotNull] this PropertyBuilder propertyBuilder,
            [CanBeNull] string typeName)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));
            Check.NullButNotEmpty(typeName, nameof(typeName));

            propertyBuilder.Metadata.SqlCe().ColumnType = typeName;

            return propertyBuilder;
        }

        public static PropertyBuilder<TProperty> ForSqlCeHasColumnType<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder,
            [CanBeNull] string typeName)
            => (PropertyBuilder<TProperty>)ForSqlCeHasColumnType((PropertyBuilder)propertyBuilder, typeName);

        public static PropertyBuilder ForSqlCeHasDefaultValueSql(
            [NotNull] this PropertyBuilder propertyBuilder,
            [CanBeNull] string sql)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));
            Check.NullButNotEmpty(sql, nameof(sql));

            var property = (Property)propertyBuilder.Metadata;
            if (ConfigurationSource.Convention.Overrides(property.GetValueGeneratedConfigurationSource()))
            {
                property.SetValueGenerated(ValueGenerated.OnAdd, ConfigurationSource.Convention);
            }

            propertyBuilder.Metadata.SqlCe().DefaultValueSql = sql;

            return propertyBuilder;
        }

        public static PropertyBuilder<TProperty> ForSqlCeHasDefaultValueSql<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder,
            [CanBeNull] string sql)
            => (PropertyBuilder<TProperty>)ForSqlCeHasDefaultValueSql((PropertyBuilder)propertyBuilder, sql);

        public static PropertyBuilder ForSqlCeHasDefaultValue(
            [NotNull] this PropertyBuilder propertyBuilder,
            [CanBeNull] object value)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));

            var property = (Property)propertyBuilder.Metadata;
            if (ConfigurationSource.Convention.Overrides(property.GetValueGeneratedConfigurationSource()))
            {
                property.SetValueGenerated(ValueGenerated.OnAdd, ConfigurationSource.Convention);
            }

            propertyBuilder.Metadata.SqlCe().DefaultValue = value;

            return propertyBuilder;
        }

        public static PropertyBuilder<TProperty> ForSqlCeHasDefaultValue<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder,
            [CanBeNull] object value)
            => (PropertyBuilder<TProperty>)ForSqlCeHasDefaultValue((PropertyBuilder)propertyBuilder, value);
    }
}
