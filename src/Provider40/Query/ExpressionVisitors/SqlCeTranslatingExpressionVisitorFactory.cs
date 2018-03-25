using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Utilities;
using EFCore.SqlCe.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using Microsoft.EntityFrameworkCore.Query;

namespace EFCore.SqlCe.Query.ExpressionVisitors
{
    /// <summary>
    ///     A factory for creating instances of <see cref="SqlCeTranslatingExpressionVisitor" />.
    /// </summary>
    public class SqlCeTranslatingExpressionVisitorFactory : ISqlTranslatingExpressionVisitorFactory
    {
        private readonly SqlTranslatingExpressionVisitorDependencies _dependencies;
        private readonly ISqlCeOptions _sqlCeOptions;

        /// <summary>
        ///     Creates a new instance of <see cref="SqlTranslatingExpressionVisitorFactory" />.
        /// </summary>
        /// <param name="dependencies"> The relational annotation provider. </param>
        /// <param name="sqlCeOptions">SqlCe options</param>
        public SqlCeTranslatingExpressionVisitorFactory(
            [NotNull] SqlTranslatingExpressionVisitorDependencies dependencies,
            [NotNull] ISqlCeOptions sqlCeOptions)
        {
            Check.NotNull(dependencies, nameof(dependencies));
            Check.NotNull(sqlCeOptions, nameof(sqlCeOptions));

            _dependencies = dependencies;
            _sqlCeOptions = sqlCeOptions;
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
                _sqlCeOptions,
                Check.NotNull(queryModelVisitor, nameof(queryModelVisitor)),
                targetSelectExpression,
                topLevelPredicate,
                inProjection);
    }
}