using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using Microsoft.Data.Entity.Metadata.Internal;
using Microsoft.Data.Entity.Metadata.ModelConventions;

namespace ErikEJ.Data.Entity.SqlServerCe.MetaData.ModelConventions
{
    public class SqlCeIdentityConvention : IModelConvention
    {
        public virtual InternalModelBuilder Apply(InternalModelBuilder modelBuilder)
        {
            modelBuilder.Annotation(
                SqlCeAnnotationNames.Prefix + SqlCeAnnotationNames.ValueGeneration,
                SqlCeAnnotationNames.Identity,
                ConfigurationSource.Convention);

            return modelBuilder;
        }
    }
}
