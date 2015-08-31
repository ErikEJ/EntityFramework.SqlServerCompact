using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering.Configuration;
using Microsoft.Data.Entity.Relational.Design.Templating;
using Microsoft.Data.Entity.Relational.Design.Utilities;
using Microsoft.Data.Entity.SqlServerCompact.Metadata;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.SqlServerCompact.Design.ReverseEngineering
{
    public class SqlCeDbContextCodeGeneratorHelper : DbContextCodeGeneratorHelper
    {
        private const string _dbContextSuffix = "Context";

        public SqlCeDbContextCodeGeneratorHelper(
            [NotNull] DbContextGeneratorModel generatorModel,
            [NotNull] IRelationalMetadataExtensionProvider extensionsProvider,
            [NotNull] ModelUtilities modelUtilities)
            : base(generatorModel, extensionsProvider, modelUtilities)
        {
        }

        public override string ClassName([NotNull] string connectionString)
        {
            Check.NotEmpty(connectionString, nameof(connectionString));

            var path = SqlCeHelper.PathFromConnectionString(connectionString);
            if (File.Exists(path))
            {
                return CSharpUtilities.Instance.GenerateCSharpIdentifier(
                    Path.GetFileNameWithoutExtension(path) + _dbContextSuffix, null);
            }

            return base.ClassName(connectionString);
        }

        public override string UseMethodName => nameof(SqlCeDbContextOptionsExtensions.UseSqlCe);

        public override void AddValueGeneratedFacetConfiguration(
            [NotNull] PropertyConfiguration propertyConfiguration)
        {
            Check.NotNull(propertyConfiguration, nameof(propertyConfiguration));

            var annotation =
                propertyConfiguration.Property.Annotations.FirstOrDefault(
                    a => a.Name == SqlCeAnnotationNames.Prefix + SqlCeAnnotationNames.ValueGeneration);

            // If this property is the single integer primary key on the EntityType then
            // KeyConvention assumes ValueGeneratedOnAdd(). If the underlying column does
            // not have Identity set then we need to set to ValueGeneratedNever() to
            // override this behavior.
            if (annotation == null
                && _keyConvention.ValueGeneratedOnAddProperty(
                    new List<Property> { (Property)propertyConfiguration.Property },
                    (EntityType)propertyConfiguration.EntityConfiguration.EntityType) != null)
            {
                propertyConfiguration.AddFacetConfiguration(
                    new FacetConfiguration(nameof(PropertyBuilder.ValueGeneratedNever) + "()"));
            }
            else
            {
                base.AddValueGeneratedFacetConfiguration(propertyConfiguration);
            }
        }
    }
}
