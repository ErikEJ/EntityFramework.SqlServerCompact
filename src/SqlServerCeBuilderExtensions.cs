using System;
using JetBrains.Annotations;
using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class SqlServerCeBuilderExtensions
    {
        public static SqlServerCePropertyBuilder ForSqlCe(
            [NotNull] this PropertyBuilder propertyBuilder)
            => new SqlServerCePropertyBuilder(Check.NotNull(propertyBuilder, nameof(propertyBuilder)).Metadata);

        public static PropertyBuilder ForSqlCe(
            [NotNull] this PropertyBuilder propertyBuilder,
            [NotNull] Action<SqlServerCePropertyBuilder> builderAction)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));
            Check.NotNull(builderAction, nameof(builderAction));

            builderAction(ForSqlCe(propertyBuilder));

            return propertyBuilder;
        }

        public static PropertyBuilder<TProperty> ForSqlCe<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder,
            [NotNull] Action<SqlServerCePropertyBuilder> builderAction)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));
            Check.NotNull(builderAction, nameof(builderAction));

            builderAction(ForSqlCe(propertyBuilder));

            return propertyBuilder;
        }

        public static SqlServerCeModelBuilder ForSqlCe(
            [NotNull] this ModelBuilder modelBuilder)
            => new SqlServerCeModelBuilder(Check.NotNull(modelBuilder, nameof(modelBuilder)).Model);

        public static ModelBuilder ForSqlCe(
            [NotNull] this ModelBuilder modelBuilder,
            [NotNull] Action<SqlServerCeModelBuilder> builderAction)
        {
            Check.NotNull(modelBuilder, nameof(modelBuilder));
            Check.NotNull(builderAction, nameof(builderAction));

            builderAction(ForSqlCe(modelBuilder));

            return modelBuilder;
        }
    }
}
