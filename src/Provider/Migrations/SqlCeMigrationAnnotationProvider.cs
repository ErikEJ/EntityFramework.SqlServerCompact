using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using Microsoft.Data.Entity.SqlServerCompact.MetaData;

namespace Microsoft.Data.Entity.SqlServerCompact.Migrations
{
    public class SqlCeMigrationAnnotationProvider : MigrationAnnotationProvider
    {        
        public override IEnumerable<IAnnotation> For(IProperty property)
        {
            var generateIdentityKey = property.StoreGeneratedPattern == StoreGeneratedPattern.Identity
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

