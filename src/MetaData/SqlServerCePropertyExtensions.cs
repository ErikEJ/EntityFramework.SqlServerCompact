using System;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Utilities;

namespace ErikEJ.Data.Entity.SqlServerCe.Metadata
{
    public class SqlServerCePropertyExtensions : ReadOnlySqlServerCePropertyExtensions
    {
        public SqlServerCePropertyExtensions([NotNull] Property property)
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
                            "Identity value generation cannot be used for the property '{0}' on entity type '{1}' because the property type is '{2}'.Identity value generation can only be used with signed integer properties.",
                            property.Name, property.EntityType.Name, propertyType.Name));
                    }

                    // TODO: Issue #777: Non-string annotations
                    property[SqlServerCeValueGenerationAnnotation] = SqlServerCeAnnotationNames.Identity;
                    property.GenerateValueOnAdd = true;
                }
            }
        }
    }
}
