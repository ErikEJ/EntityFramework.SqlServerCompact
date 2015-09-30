using JetBrains.Annotations;
using Microsoft.Data.Entity.MetaData.Internal;

namespace Microsoft.Data.Entity.Metadata.Internal
{
    public static class SqlCeInternalMetadataBuilderExtensions
    {
        public static RelationalModelBuilderAnnotations SqlCe(
            [NotNull] this InternalModelBuilder builder,
            ConfigurationSource configurationSource)
            => new RelationalModelBuilderAnnotations(builder, configurationSource, SqlCeAnnotationNames.Prefix);

        public static RelationalPropertyBuilderAnnotations SqlCe(
            [NotNull] this InternalPropertyBuilder builder,
            ConfigurationSource configurationSource)
            => new RelationalPropertyBuilderAnnotations(builder, configurationSource, SqlCeAnnotationNames.Prefix);

        public static RelationalEntityTypeBuilderAnnotations SqlCe(
            [NotNull] this InternalEntityTypeBuilder builder,
            ConfigurationSource configurationSource)
            => new RelationalEntityTypeBuilderAnnotations(builder, configurationSource, SqlCeAnnotationNames.Prefix);

        public static RelationalKeyBuilderAnnotations SqlCe(
            [NotNull] this InternalKeyBuilder builder,
            ConfigurationSource configurationSource)
            => new RelationalKeyBuilderAnnotations(builder, configurationSource, SqlCeAnnotationNames.Prefix);

        public static RelationalIndexBuilderAnnotations SqlCe(
            [NotNull] this InternalIndexBuilder builder,
            ConfigurationSource configurationSource)
            => new RelationalIndexBuilderAnnotations(builder, configurationSource, SqlCeAnnotationNames.Prefix);

        public static RelationalForeignKeyBuilderAnnotations SqlCe(
            [NotNull] this InternalRelationshipBuilder builder,
            ConfigurationSource configurationSource)
            => new RelationalForeignKeyBuilderAnnotations(builder, configurationSource, SqlCeAnnotationNames.Prefix);
    }
}
