using JetBrains.Annotations;
using Microsoft.Data.Entity.Query.ExpressionVisitors;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.Logging;

namespace Microsoft.Data.Entity.Query
{
    public class SqlCeQueryCompilationContextFactory : IQueryCompilationContextFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IEntityQueryModelVisitorFactory _entityQueryModelVisitorFactory;
        private readonly IRequiresMaterializationExpressionVisitorFactory _requiresMaterializationExpressionVisitorFactory;

        public SqlCeQueryCompilationContextFactory(
            [NotNull] ILoggerFactory loggerFactory,
            [NotNull] IEntityQueryModelVisitorFactory entityQueryModelVisitorFactory,
            [NotNull] IRequiresMaterializationExpressionVisitorFactory requiresMaterializationExpressionVisitorFactory)
        {
            Check.NotNull(loggerFactory, nameof(loggerFactory));
            Check.NotNull(entityQueryModelVisitorFactory, nameof(entityQueryModelVisitorFactory));
            Check.NotNull(requiresMaterializationExpressionVisitorFactory, nameof(requiresMaterializationExpressionVisitorFactory));

            _loggerFactory = loggerFactory;
            _entityQueryModelVisitorFactory = entityQueryModelVisitorFactory;
            _requiresMaterializationExpressionVisitorFactory = requiresMaterializationExpressionVisitorFactory;
        }

        public virtual QueryCompilationContext Create(bool async)
            => async
                ? new SqlCeQueryCompilationContext(
                    _loggerFactory,
                    _entityQueryModelVisitorFactory,
                    _requiresMaterializationExpressionVisitorFactory,
                    new AsyncLinqOperatorProvider(),
                    new AsyncQueryMethodProvider())
                : new SqlCeQueryCompilationContext(
                    _loggerFactory,
                    _entityQueryModelVisitorFactory,
                    _requiresMaterializationExpressionVisitorFactory,
                    new LinqOperatorProvider(),
                    new QueryMethodProvider());
    }
}
