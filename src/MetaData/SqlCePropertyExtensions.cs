using System;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Utilities;

namespace ErikEJ.Data.Entity.SqlServerCe.Metadata
{
    public class SqlCePropertyExtensions : ReadOnlySqlCePropertyExtensions
    {
        public SqlCePropertyExtensions([NotNull] Property property)
            : base(property)
        {
        }

        [CanBeNull]
        public new virtual bool? IdentityKeyGeneration
        {
            get { return base.IdentityKeyGeneration; }
            [param: CanBeNull]
            set
            {
                var property = ((Property)Property);

                if (value == null)
                {
                    property[SqlServerCeValueGenerationAnnotation] = null;
                    property.GenerateValueOnAdd = null;
                }
                else
                {
                    var propertyType = Property.ClrType;

                    if (value == true
                        && (!propertyType.IsIntegerForIdentity()))
                    {
                        throw new ArgumentException(string.Format(
                            Strings.IdentityBadType,
                            property.Name, property.EntityType.Name, propertyType.Name));
                    }

                    // TODO: Issue #777: Non-string annotations
                    property[SqlServerCeValueGenerationAnnotation] = SqlCeAnnotationNames.Identity;
                    property.GenerateValueOnAdd = true;
                }
            }
        }
    }
}
