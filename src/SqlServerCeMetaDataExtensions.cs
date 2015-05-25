using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class SqlServerCeMetadataExtensions
    {
        public static SqlServerCePropertyExtensions SqlCe([NotNull] this Property property)
            => new SqlServerCePropertyExtensions(Check.NotNull(property, nameof(property)));

        public static ISqlServerCePropertyExtensions SqlCe([NotNull] this IProperty property)
            => new ReadOnlySqlServerCePropertyExtensions(Check.NotNull(property, nameof(property)));

        public static SqlServerCeModelExtensions SqlCe([NotNull] this Model model)
            => new SqlServerCeModelExtensions(Check.NotNull(model, nameof(model)));

        public static ISqlServerCeModelExtensions SqlCe([NotNull] this IModel model)
            => new ReadOnlySqlServerCeModelExtensions(Check.NotNull(model, nameof(model)));
    }
}
