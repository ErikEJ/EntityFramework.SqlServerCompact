using System;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Query.ExpressionVisitors;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Query.Internal
{
    public class SqlCeQueryCompilationContext : RelationalQueryCompilationContext
    {
        public SqlCeQueryCompilationContext(
           [NotNull] IModel model,
           [NotNull] ISensitiveDataLogger logger,
           [NotNull] IEntityQueryModelVisitorFactory entityQueryModelVisitorFactory,
           [NotNull] IRequiresMaterializationExpressionVisitorFactory requiresMaterializationExpressionVisitorFactory,
           [NotNull] ILinqOperatorProvider linqOperatorProvider,
           [NotNull] IQueryMethodProvider queryMethodProvider,
           [NotNull] Type contextType,
           bool trackQueryResults)
            : base(
                Check.NotNull(model, nameof(model)),
                Check.NotNull(logger, nameof(logger)),
                Check.NotNull(entityQueryModelVisitorFactory, nameof(entityQueryModelVisitorFactory)),
                Check.NotNull(requiresMaterializationExpressionVisitorFactory, nameof(requiresMaterializationExpressionVisitorFactory)),
                Check.NotNull(linqOperatorProvider, nameof(linqOperatorProvider)),
                Check.NotNull(queryMethodProvider, nameof(queryMethodProvider)),
                Check.NotNull(contextType, nameof(contextType)),
                trackQueryResults)
        {
        }
        public override bool IsLateralJoinSupported => true;
    }
}
