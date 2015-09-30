using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity
{
    public static class SqlCeIndexBuilderExtensions
    {
        public static IndexBuilder SqlCeIndexName([NotNull] this IndexBuilder builder, [CanBeNull] string name)
        {
            Check.NotNull(builder, nameof(builder));
            Check.NullButNotEmpty(name, nameof(name));

            builder.Metadata.SqlCe().Name = name;

            return builder;
        }
    }
}
