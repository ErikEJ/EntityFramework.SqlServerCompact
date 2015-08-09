using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Internal;
using Microsoft.Data.Entity.SqlServerCompact.Metadata;

namespace Microsoft.Data.Entity.SqlServerCompact.Metadata.Internal
{
    public static class SqlCeInternalMetadataBuilderExtensions
    {
        public static RelationalModelAnnotations SqlCe(
            [NotNull] this InternalModelBuilder builder,
            ConfigurationSource configurationSource)
            => new RelationalModelAnnotations(builder, configurationSource, SqlCeAnnotationNames.Prefix);

        public static RelationalPropertyAnnotations SqlCe(
            [NotNull] this InternalPropertyBuilder builder,
            ConfigurationSource configurationSource)
            => new RelationalPropertyAnnotations(builder, configurationSource, SqlCeAnnotationNames.Prefix);

        public static RelationalEntityTypeAnnotations SqlCe(
            [NotNull] this InternalEntityTypeBuilder builder,
            ConfigurationSource configurationSource)
            => new RelationalEntityTypeAnnotations(builder, configurationSource, SqlCeAnnotationNames.Prefix);

        public static RelationalKeyAnnotations SqlCe(
            [NotNull] this InternalKeyBuilder builder,
            ConfigurationSource configurationSource)
            => new RelationalKeyAnnotations(builder, configurationSource, SqlCeAnnotationNames.Prefix);

        public static RelationalIndexAnnotations SqlCe(
            [NotNull] this InternalIndexBuilder builder,
            ConfigurationSource configurationSource)
            => new RelationalIndexAnnotations(builder, configurationSource, SqlCeAnnotationNames.Prefix);

        public static RelationalForeignKeyAnnotations SqlCe(
            [NotNull] this InternalRelationshipBuilder builder,
            ConfigurationSource configurationSource)
            => new RelationalForeignKeyAnnotations(builder, configurationSource, SqlCeAnnotationNames.Prefix);
    }
}
