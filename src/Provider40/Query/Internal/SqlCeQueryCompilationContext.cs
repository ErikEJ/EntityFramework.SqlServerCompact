using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Query.Internal
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
