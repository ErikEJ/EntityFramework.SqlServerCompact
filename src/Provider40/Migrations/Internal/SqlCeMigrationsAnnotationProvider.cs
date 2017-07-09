using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore.Migrations.Internal
{
    public class SqlCeMigrationsAnnotationProvider : MigrationsAnnotationProvider
    {
        public SqlCeMigrationsAnnotationProvider([NotNull] MigrationsAnnotationProviderDependencies dependencies)
            : base(dependencies)
        {
        }

        public override IEnumerable<IAnnotation> For(IProperty property)
        {
            var generateIdentityKey = (property.ValueGenerated == ValueGenerated.OnAdd)
                && property.ClrType.IsIntegerForIdentity();

            if (generateIdentityKey)
            {
                yield return new Annotation(
                    SqlCeAnnotationNames.ValueGeneration,
                    SqlCeAnnotationNames.Identity);
            }
        }
    }
}
