using System.Collections.Generic;
using ErikEJ.Data.Entity.SqlServerCe.Query;
using JetBrains.Annotations;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational.Metadata;
using Microsoft.Data.Entity.Relational.Update;

namespace ErikEJ.Data.Entity.SqlServerCe.Update
{
    public class SqlServerCeCommandBatchPreparer : CommandBatchPreparer, ISqlServerCeCommandBatchPreparer
    {
        public SqlServerCeCommandBatchPreparer(
            [NotNull] ISqlServerCeModificationCommandBatchFactory modificationCommandBatchFactory,
            [NotNull] IParameterNameGeneratorFactory parameterNameGeneratorFactory,
            [NotNull] IComparer<ModificationCommand> modificationCommandComparer,
            [NotNull] ISqlServerCeValueBufferFactoryFactory valueBufferFactoryFactory)
            : base(
                modificationCommandBatchFactory,
                parameterNameGeneratorFactory,
                modificationCommandComparer,
                valueBufferFactoryFactory)
        {
        }

        public override IRelationalEntityTypeExtensions GetEntityTypeExtensions(IEntityType entityType) => 
            entityType.Relational();

        public override IRelationalPropertyExtensions GetPropertyExtensions(IProperty property) => 
            property.Relational();
    }
}
