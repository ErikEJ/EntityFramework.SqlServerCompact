using System.Linq;
using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using Microsoft.Data.Entity.Metadata;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.Tests.Metadata.ModelConventions
{
    public class SqlCeValueGenerationStrategyConventionTest
    {
        [Fact(Skip = "Awaits API design decisions")]
        public void Annotations_are_added_when_conventional_model_builder_is_used()
        {
            var model = new SqlCeModelBuilderFactory().CreateConventionBuilder(new Model()).Model;

            Assert.Equal(1, model.Annotations.Count());

            Assert.Equal(SqlCeAnnotationNames.Prefix + SqlCeAnnotationNames.ValueGeneration, model.Annotations.ElementAt(0).Name);
            Assert.Equal(SqlCeAnnotationNames.Identity, model.Annotations.ElementAt(0).Value);
        }
    }
}
