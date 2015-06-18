using System;
using System.Collections.Generic;
using System.Linq;
using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using Microsoft.Data.Entity.Relational.Migrations.Operations;
using Microsoft.Data.Entity.Relational.Metadata;

namespace ErikEJ.Data.Entity.SqlServerCe.Migrations
{
    public class SqlCeModelDiffer : ModelDiffer
    {
        public SqlCeModelDiffer(
            [NotNull] IRelationalTypeMapper typeMapper,
            [NotNull] IRelationalMetadataExtensionProvider metadataExtensions)
            : base(typeMapper, metadataExtensions)
        {
        }

        protected override IEnumerable<MigrationOperation> Add(IProperty target)
        {
            var operation = base.Add(target).Cast<AddColumnOperation>().Single();

            var generateIdentityKey = target.StoreGeneratedPattern == StoreGeneratedPattern.Identity
                                      && target.ClrType.IsIntegerForIdentity();

            if (generateIdentityKey && !target.ClrType.IsIntegerForIdentity())
            {
                throw new ArgumentException(string.Format(
                    Strings.IdentityBadType,
                    target.Name, target.EntityType.Name, target.ClrType.Name));
            }

            if (generateIdentityKey)
            {
                operation[SqlCeAnnotationNames.Prefix + SqlCeAnnotationNames.ValueGeneration] =
                    SqlCeAnnotationNames.Identity;
            }
            yield return operation;
        }
    }
}
