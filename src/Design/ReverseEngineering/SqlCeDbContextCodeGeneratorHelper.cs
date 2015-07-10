using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational.Design.CodeGeneration;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering.Configuration;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.SqlServerCompact.Design.ReverseEngineering
{
    public class SqlCeDbContextCodeGeneratorHelper : DbContextCodeGeneratorHelper
    {
        private const string _dbContextSuffix = "Context";

        public SqlCeDbContextCodeGeneratorHelper(
            [NotNull] DbContextGeneratorModel generatorModel)
            : base(generatorModel)
        {
        }

        public override IEnumerable<IEntityType> OrderedEntityTypes()
        {
            // do not configure EntityTypes for which we had an error when generating
            return GeneratorModel.MetadataModel.EntityTypes.OrderBy(e => e.Name)
                .Where(e => ((EntityType)e).FindAnnotation(
                    SqlCeMetadataModelProvider.AnnotationNameEntityTypeError) == null);
        }

        public override string ClassName([NotNull] string connectionString)
        {
            Check.NotEmpty(connectionString, nameof(connectionString));

            var builder = new SqlCeConnectionStringBuilder(connectionString);
            if (builder.DataSource != null)
            {
                return CSharpUtilities.Instance.GenerateCSharpIdentifier(
                    System.IO.Path.GetFileNameWithoutExtension(builder.DataSource) + _dbContextSuffix, null);
            }

            return base.ClassName(connectionString);
        }

        public override void AddNavigationsConfiguration(EntityConfiguration entityConfiguration)
        {
            Check.NotNull(entityConfiguration, nameof(entityConfiguration));

            foreach (var foreignKey in entityConfiguration.EntityType.GetForeignKeys())
            {
                var dependentEndNavigationPropertyName =
                    (string)foreignKey[SqlCeMetadataModelProvider.AnnotationNameDependentEndNavPropName];
                var principalEndNavigationPropertyName =
                    (string)foreignKey[SqlCeMetadataModelProvider.AnnotationNamePrincipalEndNavPropName];

                entityConfiguration.RelationshipConfigurations.Add(
                    new RelationshipConfiguration(entityConfiguration, foreignKey,
                        dependentEndNavigationPropertyName, principalEndNavigationPropertyName));
            }
        }
    }
}
