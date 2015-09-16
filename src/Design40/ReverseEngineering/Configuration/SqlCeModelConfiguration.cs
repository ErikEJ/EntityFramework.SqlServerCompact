using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering.Configuration;
using Microsoft.Data.Entity.Relational.Design.Utilities;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.SqlServerCompact.Design.ReverseEngineering.Configuration
{
    public class SqlCeModelConfiguration : ModelConfiguration
    {
        private const string _dbContextSuffix = "Context";

        public SqlCeModelConfiguration(
            [NotNull] IModel model,
            [NotNull] CustomConfiguration customConfiguration,
            [NotNull] IRelationalMetadataExtensionProvider extensionsProvider,
            [NotNull] CSharpUtilities cSharpUtilities,
            [NotNull] ModelUtilities modelUtilities)
            : base(model, customConfiguration, extensionsProvider, cSharpUtilities, modelUtilities)
        {
        }

        public override string DefaultSchemaName => null;
        public override string UseMethodName => nameof(SqlCeDbContextOptionsExtensions.UseSqlCe);

        public override string ClassName()
        {
            if (CustomConfiguration.ContextClassName != null)
            {
                return CustomConfiguration.ContextClassName;
            }

            var path = SqlCeHelper.PathFromConnectionString(CustomConfiguration.ConnectionString);
            if (File.Exists(path))
            {
                return CSharpUtilities.Instance.GenerateCSharpIdentifier(
                    Path.GetFileNameWithoutExtension(path) + _dbContextSuffix, null);
            }

            return base.ClassName();
        }

        public override void AddValueGeneratedConfiguration(
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
                && _keyConvention.FindValueGeneratedOnAddProperty(
                    new List<Property> { (Property)propertyConfiguration.Property },
                    (EntityType)propertyConfiguration.EntityConfiguration.EntityType) != null)
            {
                propertyConfiguration.FluentApiConfigurations.Add(
                    new FluentApiConfiguration(nameof(PropertyBuilder.ValueGeneratedNever)));
            }
            else
            {
                base.AddValueGeneratedConfiguration(propertyConfiguration);
            }
        }
    }
}
