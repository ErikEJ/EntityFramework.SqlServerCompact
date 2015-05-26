using System;
using JetBrains.Annotations;
using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Data.Entity.Metadata.Builders;

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class SqlCeBuilderExtensions
    {
        public static SqlCePropertyBuilder ForSqlCe(
            [NotNull] this PropertyBuilder propertyBuilder)
            => new SqlCePropertyBuilder(Check.NotNull(propertyBuilder, nameof(propertyBuilder)).Metadata);

        public static PropertyBuilder ForSqlCe(
            [NotNull] this PropertyBuilder propertyBuilder,
            [NotNull] Action<SqlCePropertyBuilder> builderAction)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));
            Check.NotNull(builderAction, nameof(builderAction));

            builderAction(ForSqlCe(propertyBuilder));

            return propertyBuilder;
        }

        public static PropertyBuilder<TProperty> ForSqlCe<TProperty>(
            [NotNull] this PropertyBuilder<TProperty> propertyBuilder,
            [NotNull] Action<SqlCePropertyBuilder> builderAction)
        {
            Check.NotNull(propertyBuilder, nameof(propertyBuilder));
            Check.NotNull(builderAction, nameof(builderAction));

            builderAction(ForSqlCe(propertyBuilder));

            return propertyBuilder;
        }

        public static SqlCeModelBuilder ForSqlCe(
            [NotNull] this ModelBuilder modelBuilder)
            => new SqlCeModelBuilder(Check.NotNull(modelBuilder, nameof(modelBuilder)).Model);

        public static ModelBuilder ForSqlCe(
            [NotNull] this ModelBuilder modelBuilder,
            [NotNull] Action<SqlCeModelBuilder> builderAction)
        {
            Check.NotNull(modelBuilder, nameof(modelBuilder));
            Check.NotNull(builderAction, nameof(builderAction));

            builderAction(ForSqlCe(modelBuilder));

            return modelBuilder;
        }
    }
}
