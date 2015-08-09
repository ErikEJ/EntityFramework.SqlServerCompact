using System;
using System.Linq;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Conventions;
using Microsoft.Data.Entity.Metadata.Internal;
using Microsoft.Data.Entity.SqlServerCompact.Metadata.Internal;
using Xunit;

namespace Microsoft.Data.Entity.SqlServerCompact.Metadata
{
    public class InternalSqlCeMetadataBuilderExtensionsTest
    {
        private InternalModelBuilder CreateBuilder()
            => new InternalModelBuilder(new Model(), new ConventionSet());

        [Fact]
        public void Can_access_model()
        {
            var builder = CreateBuilder();

            builder.SqlCe(ConfigurationSource.Convention).GetOrAddSequence("Mine").IncrementBy = 77;

            Assert.Equal(77, builder.Metadata.SqlCe().FindSequence("Mine").IncrementBy);

            Assert.Equal(1, builder.Metadata.Annotations.Count(
                a => a.Name.StartsWith(SqlCeAnnotationNames.Prefix, StringComparison.Ordinal)));
        }

        [Fact]
        public void Can_access_entity_type()
        {
            var typeBuilder = CreateBuilder().Entity(typeof(Splot), ConfigurationSource.Convention);

            typeBuilder.SqlCe(ConfigurationSource.Convention).TableName = "Splew";
            Assert.Equal("Splew", typeBuilder.Metadata.SqlCe().TableName);

            typeBuilder.SqlCe(ConfigurationSource.DataAnnotation).TableName = "Splow";
            Assert.Equal("Splow", typeBuilder.Metadata.SqlCe().TableName);

            typeBuilder.SqlCe(ConfigurationSource.Convention).TableName = "Splod";
            Assert.Equal("Splow", typeBuilder.Metadata.SqlCe().TableName);

            Assert.Equal(1, typeBuilder.Metadata.Annotations.Count(
                a => a.Name.StartsWith(SqlCeAnnotationNames.Prefix, StringComparison.Ordinal)));
        }

        [Fact]
        public void Can_access_property()
        {
            var propertyBuilder = CreateBuilder()
                .Entity(typeof(Splot), ConfigurationSource.Convention)
                .Property("Id", typeof(int), ConfigurationSource.Convention);

            propertyBuilder.SqlCe(ConfigurationSource.Convention).ColumnName = "Splew";
            Assert.Equal("Splew", propertyBuilder.Metadata.SqlCe().ColumnName);

            propertyBuilder.SqlCe(ConfigurationSource.DataAnnotation).ColumnName = "Splow";
            Assert.Equal("Splow", propertyBuilder.Metadata.SqlCe().ColumnName);

            propertyBuilder.SqlCe(ConfigurationSource.Convention).ColumnName = "Splod";
            Assert.Equal("Splow", propertyBuilder.Metadata.SqlCe().ColumnName);

            Assert.Equal(1, propertyBuilder.Metadata.Annotations.Count(
                a => a.Name.StartsWith(SqlCeAnnotationNames.Prefix, StringComparison.Ordinal)));
        }

        [Fact]
        public void Can_access_key()
        {
            var modelBuilder = CreateBuilder();
            var entityTypeBuilder = modelBuilder.Entity(typeof(Splot), ConfigurationSource.Convention);
            var property = entityTypeBuilder.Property("Id", typeof(int), ConfigurationSource.Convention).Metadata;
            var keyBuilder = entityTypeBuilder.Key(new[] { property }, ConfigurationSource.Convention);

            keyBuilder.SqlCe(ConfigurationSource.Convention).Name = "Splew";
            Assert.Equal("Splew", keyBuilder.Metadata.SqlCe().Name);

            keyBuilder.SqlCe(ConfigurationSource.DataAnnotation).Name = "Splow";
            Assert.Equal("Splow", keyBuilder.Metadata.SqlCe().Name);

            keyBuilder.SqlCe(ConfigurationSource.Convention).Name = "Splod";
            Assert.Equal("Splow", keyBuilder.Metadata.SqlCe().Name);

            Assert.Equal(1, keyBuilder.Metadata.Annotations.Count(
                a => a.Name.StartsWith(SqlCeAnnotationNames.Prefix, StringComparison.Ordinal)));
        }

        [Fact]
        public void Can_access_index()
        {
            var modelBuilder = CreateBuilder();
            var entityTypeBuilder = modelBuilder.Entity(typeof(Splot), ConfigurationSource.Convention);
            entityTypeBuilder.Property("Id", typeof(int), ConfigurationSource.Convention);
            var indexBuilder = entityTypeBuilder.Index(new[] { "Id" }, ConfigurationSource.Convention);

            indexBuilder.SqlCe(ConfigurationSource.Convention).Name = "Splew";
            Assert.Equal("Splew", indexBuilder.Metadata.SqlCe().Name);

            indexBuilder.SqlCe(ConfigurationSource.DataAnnotation).Name = "Splow";
            Assert.Equal("Splow", indexBuilder.Metadata.SqlCe().Name);

            indexBuilder.SqlCe(ConfigurationSource.Convention).Name = "Splod";
            Assert.Equal("Splow", indexBuilder.Metadata.SqlCe().Name);

            Assert.Equal(1, indexBuilder.Metadata.Annotations.Count(
                a => a.Name.StartsWith(SqlCeAnnotationNames.Prefix, StringComparison.Ordinal)));
        }

        [Fact]
        public void Can_access_relationship()
        {
            var modelBuilder = CreateBuilder();
            var entityTypeBuilder = modelBuilder.Entity(typeof(Splot), ConfigurationSource.Convention);
            entityTypeBuilder.Property("Id", typeof(int), ConfigurationSource.Convention);
            var relationshipBuilder = entityTypeBuilder.ForeignKey("Splot", new[] { "Id" }, ConfigurationSource.Convention);

            relationshipBuilder.SqlCe(ConfigurationSource.Convention).Name = "Splew";
            Assert.Equal("Splew", relationshipBuilder.Metadata.SqlCe().Name);

            relationshipBuilder.SqlCe(ConfigurationSource.DataAnnotation).Name = "Splow";
            Assert.Equal("Splow", relationshipBuilder.Metadata.SqlCe().Name);

            relationshipBuilder.SqlCe(ConfigurationSource.Convention).Name = "Splod";
            Assert.Equal("Splow", relationshipBuilder.Metadata.SqlCe().Name);

            Assert.Equal(1, relationshipBuilder.Metadata.Annotations.Count(
                a => a.Name.StartsWith(SqlCeAnnotationNames.Prefix, StringComparison.Ordinal)));
        }

        private class Splot
        {
        }
    }
}
