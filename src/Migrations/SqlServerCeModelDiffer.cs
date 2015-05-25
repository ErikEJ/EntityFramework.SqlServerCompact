using System.Collections.Generic;
using System.Linq;
using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using JetBrains.Annotations;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace ErikEJ.Data.Entity.SqlServerCe.Migrations
{
    public class SqlServerCeModelDiffer : ModelDiffer
    {
        public SqlServerCeModelDiffer([NotNull] IRelationalTypeMapper typeMapper)
            : base(typeMapper)
        {
        }

        protected override IEnumerable<MigrationOperation> Add(IProperty target)
        {
            var operation = base.Add(target).Cast<AddColumnOperation>().Single();

            var generateIdentityKey = GetIdentityKeyGeneration(target);

            if (generateIdentityKey.HasValue && generateIdentityKey.Value)
            {
                operation[SqlServerCeAnnotationNames.Prefix + SqlServerCeAnnotationNames.ValueGeneration] =
                    SqlServerCeAnnotationNames.Identity;
            }
            yield return operation;
        }

        private bool? GetIdentityKeyGeneration(IProperty property) => property.SqlCe().IdentityKeyGeneration;
    }
}
