using System.Collections.Generic;
using System.Linq;
using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using JetBrains.Annotations;
using Microsoft.Data.Entity;
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

            var generateIdentityKey = GetIdentityKeyGeneration(target);

            if (generateIdentityKey.HasValue && generateIdentityKey.Value)
            {
                operation[SqlCeAnnotationNames.Prefix + SqlCeAnnotationNames.ValueGeneration] =
                    SqlCeAnnotationNames.Identity;
            }
            yield return operation;
        }

        private bool? GetIdentityKeyGeneration(IProperty property) => property.SqlCe().IdentityKeyGeneration;
    }
}
