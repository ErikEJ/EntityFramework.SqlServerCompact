using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    ///     SQL Server Compact specific extension methods for <see cref="PropertyBuilder"/>.
    /// </summary>
    public static class SqlCePropertyBuilderExtensions
    {
        /// <summary>
        ///     Configures the column that the property maps to when targeting SQL Server Compact.
        /// </summary>
        /// <param name="propertyBuilder"> The builder for the property being configured. </param>
        /// <param name="name"> The name of the column. </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
        public static PropertyBuilder ForSqlCeHasColumnName(
            [NotNull] this PropertyBuilder propertyBuilder,
            [CanBeNull] string name)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));
            Check.NullButNotEmpty(name, nameof(name));

            propertyBuilder.Metadata.SqlCe().ColumnName = name;

            return propertyBuilder;
        }

        /// <summary>
        ///     Configures the column that the property maps to when targeting SQL Server Compact.
        /// </summary>
        /// <typeparam name="TProperty"> The type of the property being configured. </typeparam>
        /// <param name="propertyBuilder"> The builder for the property being configured. </param>
        /// <param name="name"> The name of the column. </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
        public static PropertyBuilder<TProperty> ForSqlCeHasColumnName<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder,
            [CanBeNull] string name)
            => (PropertyBuilder<TProperty>)ForSqlCeHasColumnName((PropertyBuilder)propertyBuilder, name);

        /// <summary>
        ///     Configures the data type of the column that the property maps to when targeting SQL Server Compact.
        ///     This should be the complete type name, including precision, scale, length, etc. 
        /// </summary>
        /// <param name="propertyBuilder"> The builder for the property being configured. </param>
        /// <param name="typeName"> The name of the data type of the column. </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
        public static PropertyBuilder ForSqlCeHasColumnType(
            [NotNull] this PropertyBuilder propertyBuilder,
            [CanBeNull] string typeName)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));
            Check.NullButNotEmpty(typeName, nameof(typeName));

            propertyBuilder.Metadata.SqlCe().ColumnType = typeName;

            return propertyBuilder;
        }

        /// <summary>
        ///     Configures the data type of the column that the property maps to when targeting SQL Server Compact.
        ///     This should be the complete type name, including precision, scale, length, etc. 
        /// </summary>
        /// <param name="propertyBuilder"> The builder for the property being configured. </param>
        /// <param name="typeName"> The name of the data type of the column. </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
        public static PropertyBuilder<TProperty> ForSqlCeHasColumnType<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder,
            [CanBeNull] string typeName)
            => (PropertyBuilder<TProperty>)ForSqlCeHasColumnType((PropertyBuilder)propertyBuilder, typeName);

        /// <summary>
        ///     Configures the default value expression for the column that the property maps to when targeting SQL Server Compact.
        /// </summary>
        /// <param name="propertyBuilder"> The builder for the property being configured. </param>
        /// <param name="sql"> The SQL expression for the default value of the column. </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
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

        /// <summary>
        ///     Configures the default value expression for the column that the property maps to when targeting SQL Server Compact.
        /// </summary>
        /// <param name="propertyBuilder"> The builder for the property being configured. </param>
        /// <param name="sql"> The SQL expression for the default value of the column. </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
        public static PropertyBuilder<TProperty> ForSqlCeHasDefaultValueSql<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder,
            [CanBeNull] string sql)
            => (PropertyBuilder<TProperty>)ForSqlCeHasDefaultValueSql((PropertyBuilder)propertyBuilder, sql);

        /// <summary>
        ///     Configures the default value for the column that the property maps to when targeting SQL Server Compact.
        /// </summary>
        /// <param name="propertyBuilder"> The builder for the property being configured. </param>
        /// <param name="value"> The default value of the column. </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
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

        /// <summary>
        ///     Configures the default value for the column that the property maps to when targeting SQL Server Compact.
        /// </summary>
        /// <param name="propertyBuilder"> The builder for the property being configured. </param>
        /// <param name="value"> The default value of the column. </param>
        /// <returns> The same builder instance so that multiple calls can be chained. </returns>
        public static PropertyBuilder<TProperty> ForSqlCeHasDefaultValue<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder,
            [CanBeNull] object value)
            => (PropertyBuilder<TProperty>)ForSqlCeHasDefaultValue((PropertyBuilder)propertyBuilder, value);
    }
}
