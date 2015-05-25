using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using Microsoft.Data.Entity.Metadata.Internal;
using Microsoft.Data.Entity.Metadata.ModelConventions;

namespace ErikEJ.Data.Entity.SqlServerCe.MetaData.ModelConventions
{
    public class SqlServerCeIdentityConvention : IModelConvention
    {
        public virtual InternalModelBuilder Apply(InternalModelBuilder modelBuilder)
        {
            modelBuilder.Annotation(
                SqlServerCeAnnotationNames.Prefix + SqlServerCeAnnotationNames.ValueGeneration,
                SqlServerCeAnnotationNames.Identity,
                ConfigurationSource.Convention);

            return modelBuilder;
        }
    }
}
