using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Internal;

namespace Microsoft.Data.Entity.Migrations.Internal
{
    public class SqlCeMigrationsAnnotationProvider : MigrationsAnnotationProvider
    {        
        public override IEnumerable<IAnnotation> For(IProperty property)
        {
            var generateIdentityKey = property.ValueGenerated == ValueGenerated.OnAdd
                && property.ClrType.IsIntegerForIdentity();

            if (generateIdentityKey)
            {
                yield return new Annotation(
                    SqlCeAnnotationNames.Prefix + SqlCeAnnotationNames.ValueGeneration,
                    SqlCeAnnotationNames.Identity);
            }
        }
    }
}

