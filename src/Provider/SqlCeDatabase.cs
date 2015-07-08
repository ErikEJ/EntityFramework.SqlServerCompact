using JetBrains.Annotations;
using Microsoft.Data.Entity.ChangeTracking.Internal;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Internal;
using Microsoft.Data.Entity.Query;
using Microsoft.Data.Entity.SqlServerCompact.Query;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.Logging;
using Microsoft.Data.Entity.Query.Methods;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Update;

namespace Microsoft.Data.Entity.SqlServerCompact
{
    public class SqlCeDatabase : RelationalDatabase
    {
        public SqlCeDatabase(
            [NotNull] IModel model,
            [NotNull] IEntityKeyFactorySource entityKeyFactorySource,
            [NotNull] IEntityMaterializerSource entityMaterializerSource,
            [NotNull] IClrAccessorSource<IClrPropertyGetter> clrPropertyGetterSource,
            [NotNull] IRelationalConnection connection,
            [NotNull] ICommandBatchPreparer batchPreparer,
            [NotNull] IBatchExecutor batchExecutor,
            [NotNull] IDbContextOptions options,
            [NotNull] ILoggerFactory loggerFactory,
            [NotNull] IRelationalValueBufferFactoryFactory valueBufferFactoryFactory,
            [NotNull] IMethodCallTranslator compositeMethodCallTranslator,
            [NotNull] IMemberTranslator compositeMemberTranslator,
            [NotNull] IRelationalTypeMapper typeMapper)
             : base(
                model,
                entityKeyFactorySource,
                entityMaterializerSource,
                clrPropertyGetterSource,
                connection,
                batchPreparer,
                batchExecutor,
                options,
                loggerFactory,
                valueBufferFactoryFactory,
                compositeMethodCallTranslator,
                compositeMemberTranslator,
                typeMapper)
        {
        }

        protected override RelationalQueryCompilationContext CreateQueryCompilationContext(
            ILinqOperatorProvider linqOperatorProvider,
            IResultOperatorHandler resultOperatorHandler,
            IQueryMethodProvider enumerableMethodProvider,
            IMethodCallTranslator compositeMethodCallTranslator,
            IMemberTranslator compositeMemberTranslator)
        {
            Check.NotNull(linqOperatorProvider, nameof(linqOperatorProvider));
            Check.NotNull(resultOperatorHandler, nameof(resultOperatorHandler));
            Check.NotNull(enumerableMethodProvider, nameof(enumerableMethodProvider));
            Check.NotNull(compositeMethodCallTranslator, nameof(compositeMethodCallTranslator));

            return new SqlCeQueryCompilationContext(
                Model,
                Logger,
                linqOperatorProvider,
                resultOperatorHandler,
                EntityMaterializerSource,
                EntityKeyFactorySource,
                ClrPropertyGetterSource,
                enumerableMethodProvider,
                compositeMethodCallTranslator,
                compositeMemberTranslator,
                ValueBufferFactoryFactory,
                TypeMapper);
        }
    }
}
