using JetBrains.Annotations;
using Microsoft.Data.Entity.ChangeTracking.Internal;
using Microsoft.Data.Entity.Metadata.Internal;
using Microsoft.Data.Entity.Relational.Query;
using Microsoft.Framework.Logging;

namespace ErikEJ.Data.Entity.SqlServerCe.Query
{
    public class SqlServerCeQueryContextFactory : RelationalQueryContextFactory, ISqlServerCeQueryContextFactory
    {
        public SqlServerCeQueryContextFactory(
            [NotNull] IStateManager stateManager,
            [NotNull] IEntityKeyFactorySource entityKeyFactorySource,
            [NotNull] IClrCollectionAccessorSource collectionAccessorSource,
            [NotNull] IClrAccessorSource<IClrPropertySetter> propertySetterSource,
            [NotNull] ISqlServerCeConnection connection,
            [NotNull] ILoggerFactory loggerFactory)
            : base(
                stateManager,
                entityKeyFactorySource,
                collectionAccessorSource,
                propertySetterSource,
                connection,
                loggerFactory)
        {
        }
    }
}
