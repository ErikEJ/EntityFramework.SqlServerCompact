using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering.Configuration;

namespace Microsoft.Data.Entity.SqlServerCompact.Design.ReverseEngineering
{
    public class SqlCeEntityTypeCodeGeneratorHelper : EntityTypeCodeGeneratorHelper
    {
        public SqlCeEntityTypeCodeGeneratorHelper(
            [NotNull] EntityTypeGeneratorModel generatorModel)
            : base(generatorModel)
        {
        }

        public virtual string ErrorMessageAnnotation
        {
            get
            {
                return (string)GeneratorModel
                    .EntityType[SqlCeMetadataModelProvider.AnnotationNameEntityTypeError];
            }
        }

        public virtual IEnumerable<NavigationPropertyInitializerConfiguration> NavPropInitializers
        {
            get
            {
                var navPropInitializers = new List<NavigationPropertyInitializerConfiguration>();

                foreach (var otherEntityType in GeneratorModel.EntityType.Model
                    .EntityTypes.Where(et => et != GeneratorModel.EntityType))
                {
                    // find navigation properties for foreign keys from another EntityType which reference this EntityType
                    foreach (var foreignKey in otherEntityType
                        .GetForeignKeys().Where(fk => fk.PrincipalEntityType == GeneratorModel.EntityType))
                    {
                        var navigationPropertyName =
                            (string)foreignKey[SqlCeMetadataModelProvider.AnnotationNamePrincipalEndNavPropName];
                        if (((EntityType)otherEntityType)
                            .FindAnnotation(SqlCeMetadataModelProvider.AnnotationNameEntityTypeError) == null)
                        {
                            if (!foreignKey.IsUnique)
                            {
                                navPropInitializers.Add(
                                    new NavigationPropertyInitializerConfiguration(
                                        navigationPropertyName, otherEntityType.Name));
                            }
                        }
                    }
                }

                return navPropInitializers;
            }
        }

        public virtual IEnumerable<NavigationPropertyConfiguration> NavigationProperties
        {
            get
            {
                var navProps = new List<NavigationPropertyConfiguration>();

                foreach (var otherEntityType in GeneratorModel.EntityType.Model
                    .EntityTypes.Where(et => et != GeneratorModel.EntityType))
                {
                    // set up the navigation properties for foreign keys from
                    // another EntityType which reference this EntityType
                    foreach (var foreignKey in otherEntityType
                        .GetForeignKeys().Where(fk => fk.PrincipalEntityType == GeneratorModel.EntityType))
                    {
                        if (((EntityType)otherEntityType)
                            .FindAnnotation(SqlCeMetadataModelProvider.AnnotationNameEntityTypeError) != null)
                        {
                            navProps.Add(new NavigationPropertyConfiguration(
                                Strings.UnableToAddNavigationProperty(otherEntityType.Name)));
                        }
                        else
                        {
                            var referencedType = foreignKey.IsUnique
                                ? otherEntityType.Name
                                : "ICollection<" + otherEntityType.Name + ">";
                            navProps.Add(new NavigationPropertyConfiguration(
                                referencedType,
                                (string)foreignKey[SqlCeMetadataModelProvider.AnnotationNamePrincipalEndNavPropName]));
                        }
                    }
                }

                foreach (var foreignKey in GeneratorModel.EntityType.GetForeignKeys())
                {
                    // set up the navigation property on this end of foreign keys owned by this EntityType
                    navProps.Add(new NavigationPropertyConfiguration(
                        foreignKey.PrincipalEntityType.Name,
                        (string)foreignKey[SqlCeMetadataModelProvider.AnnotationNameDependentEndNavPropName]));

                    // set up the other navigation property for self-referencing foreign keys owned by this EntityType
                    if (((ForeignKey)foreignKey).IsSelfReferencing())
                    {
                        var referencedType = foreignKey.IsUnique
                            ? foreignKey.DeclaringEntityType.Name
                            : "ICollection<" + foreignKey.DeclaringEntityType.Name + ">";
                        navProps.Add(new NavigationPropertyConfiguration(
                            referencedType,
                            (string)foreignKey[SqlCeMetadataModelProvider.AnnotationNamePrincipalEndNavPropName]));
                    }
                }

                return navProps;
            }
        }
    }
}
