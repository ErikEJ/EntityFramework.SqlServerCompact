using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace ErikEJ.Data.Entity.SqlServerCe.Migrations
{
    public class SqlServerCeModelDiffer : ModelDiffer, ISqlServerCeModelDiffer
    {
        public SqlServerCeModelDiffer([NotNull] ISqlServerCeTypeMapper typeMapper)
            : base(typeMapper)
        {
        }

        protected override IEnumerable<MigrationOperation> Add(IProperty target)
        {
            var operation = base.Add(target).Cast<AddColumnOperation>().Single();

            if (UseIdentityStrategy(target))
            {
                operation[SqlServerCeAnnotationNames.Prefix + SqlServerCeAnnotationNames.ValueGeneration] =
                        SqlServerCeAnnotationNames.Strategy;
            }
            yield return operation;
        }

        private bool UseIdentityStrategy(IProperty target)
        {
            var property = ((Property)target);
            var propertyType = target.ClrType;

            if (property.ClrType == typeof(Guid) && property.IsKey())
            {
                property.GenerateValueOnAdd = null;
                return false;
            }

            if (property.GenerateValueOnAdd.HasValue && property.GenerateValueOnAdd.Value)
            {
                if (!propertyType.IsIntegerForIdentity())
                {
                    //TODO Localize?
                    throw new ArgumentException(string.Format(
                        "Identity value generation cannot be used for the property '{0}' on entity type '{1}' because the property type is '{2}'.Identity value generation can only be used with signed integer properties.",
                        target.Name, target.EntityType.Name, propertyType.Name));
                }

                property[SqlServerCeAnnotationNames.Prefix + SqlServerCeAnnotationNames.ValueGeneration] =
                    SqlServerCeAnnotationNames.Strategy;
                return true;
            }
            return false;
        }
    }
}
