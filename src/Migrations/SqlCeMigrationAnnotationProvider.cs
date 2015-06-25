using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using System;
using System.Collections.Generic;

namespace ErikEJ.Data.Entity.SqlServerCe.Migrations
{
    public class SqlCeMigrationAnnotationProvider : MigrationAnnotationProvider
    {        
        public override IEnumerable<IAnnotation> For(IProperty property)
        {
            var generateIdentityKey = property.StoreGeneratedPattern == StoreGeneratedPattern.Identity;

            if (generateIdentityKey && !property.ClrType.IsIntegerForIdentity())
            {
                throw new ArgumentException(string.Format(
                    Strings.IdentityBadType,
                    property.Name, property.EntityType.Name, property.ClrType.Name));
            }

            if (generateIdentityKey)
            {
                yield return new Annotation(
                    SqlCeAnnotationNames.Prefix + SqlCeAnnotationNames.ValueGeneration,
                    SqlCeAnnotationNames.Identity);
            }
        }
    }
}

