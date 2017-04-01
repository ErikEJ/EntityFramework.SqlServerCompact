using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Query.Expressions;

// ReSharper disable AssignNullToNotNullAttribute
namespace Microsoft.EntityFrameworkCore.Query.ExpressionVisitors
{
    public class SqlCeTranslatingExpressionVisitor : SqlTranslatingExpressionVisitor
    {
        private readonly IDbContextOptions _contextOptions;

        public override Expression Visit(Expression expression)
        {
            if (_contextOptions.FindExtension<SqlCeOptionsExtension>()?.ClientEvalForUnsupportedSqlConstructs != true)
                return base.Visit(expression);

            var evaluatedExpression = base.Visit(expression);
            if (evaluatedExpression is SelectExpression)
            {
                return null;
            }
            return evaluatedExpression;
        }

        protected override Exception CreateUnhandledItemException<T>(T unhandledItem, string visitMethod)
            => null; // Never called

        public SqlCeTranslatingExpressionVisitor(
            [NotNull] SqlTranslatingExpressionVisitorDependencies dependencies,
            [NotNull] IDbContextOptions contextOptions,
            [NotNull] RelationalQueryModelVisitor queryModelVisitor, 
            [CanBeNull] SelectExpression targetSelectExpression = null, 
            [CanBeNull] Expression topLevelPredicate = null, 
            bool inProjection = false)
            : base(dependencies, queryModelVisitor, targetSelectExpression, topLevelPredicate, inProjection)
        {
            _contextOptions = contextOptions;
        }


    }
}