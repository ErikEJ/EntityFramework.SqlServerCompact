using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;

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
                model,
                logger,
                entityQueryModelVisitorFactory,
                requiresMaterializationExpressionVisitorFactory,
                linqOperatorProvider,
                queryMethodProvider,
                contextType,
                trackQueryResults)
        {
        }
        public override bool IsLateralJoinSupported => true;
    }
}
