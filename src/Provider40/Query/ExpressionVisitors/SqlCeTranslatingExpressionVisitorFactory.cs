using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionVisitors
{
    /// <summary>
    ///     A factory for creating instances of <see cref="SqlCeTranslatingExpressionVisitor" />.
    /// </summary>
    public class SqlCeTranslatingExpressionVisitorFactory : ISqlTranslatingExpressionVisitorFactory
    {
        private readonly SqlTranslatingExpressionVisitorDependencies _dependencies;
        private readonly IDbContextOptions _contextOptions;

        /// <summary>
        ///     Creates a new instance of <see cref="SqlTranslatingExpressionVisitorFactory" />.
        /// </summary>
        /// <param name="dependencies"> The relational annotation provider. </param>
        /// <param name="contextOptions">DbContext options</param>
        public SqlCeTranslatingExpressionVisitorFactory(
            [NotNull] SqlTranslatingExpressionVisitorDependencies dependencies,
            [NotNull] IDbContextOptions contextOptions)
        {
            Check.NotNull(dependencies, nameof(dependencies));
            Check.NotNull(contextOptions, nameof(contextOptions));

            _dependencies = dependencies;
            _contextOptions = contextOptions;
        }

        /// <summary>
        ///     Creates a new SqlTranslatingExpressionVisitor.
        /// </summary>
        /// <param name="queryModelVisitor"> The query model visitor. </param>
        /// <param name="targetSelectExpression"> The target select expression. </param>
        /// <param name="topLevelPredicate"> The top level predicate. </param>
        /// <param name="inProjection"> true if we are translating a projection. </param>
        /// <returns>
        ///     A SqlTranslatingExpressionVisitor.
        /// </returns>
        public virtual SqlTranslatingExpressionVisitor Create(
            RelationalQueryModelVisitor queryModelVisitor,
            SelectExpression targetSelectExpression = null,
            Expression topLevelPredicate = null,
            bool inProjection = false)
            => new SqlCeTranslatingExpressionVisitor(
                _dependencies,
                _contextOptions,
                Check.NotNull(queryModelVisitor, nameof(queryModelVisitor)),
                targetSelectExpression,
                topLevelPredicate,
                inProjection);
    }
}