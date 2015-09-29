using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Update.Internal
{
    public class SqlCeModificationCommandBatchFactory : ModificationCommandBatchFactory
    {
        public SqlCeModificationCommandBatchFactory(
                   [NotNull] IRelationalCommandBuilderFactory commandBuilderFactory,
                   [NotNull] ISqlCeUpdateSqlGenerator sqlGenerator)
            : base(commandBuilderFactory, sqlGenerator)
        { }

        public override ModificationCommandBatch Create(
             IDbContextOptions options,
             IRelationalAnnotationProvider annotationProvider)
        {
            Check.NotNull(options, nameof(options));
            Check.NotNull(annotationProvider, nameof(annotationProvider));

            return new SqlCeModificationCommandBatch(
                CommandBuilderFactory,
                (ISqlCeUpdateSqlGenerator)UpdateSqlGenerator);
        }
    }
}
