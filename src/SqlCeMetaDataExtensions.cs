using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class SqlCeMetadataExtensions
    {
        public static SqlCePropertyExtensions SqlCe([NotNull] this Property property)
            => new SqlCePropertyExtensions(Check.NotNull(property, nameof(property)));

        public static ISqlServerCePropertyExtensions SqlCe([NotNull] this IProperty property)
            => new ReadOnlySqlCePropertyExtensions(Check.NotNull(property, nameof(property)));

        public static SqlCeModelExtensions SqlCe([NotNull] this Model model)
            => new SqlCeModelExtensions(Check.NotNull(model, nameof(model)));

        public static ISqlCeModelExtensions SqlCe([NotNull] this IModel model)
            => new ReadOnlySqlCeModelExtensions(Check.NotNull(model, nameof(model)));
    }
}
