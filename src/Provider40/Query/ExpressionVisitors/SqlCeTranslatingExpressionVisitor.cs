using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Query.Expressions;

// ReSharper disable AssignNullToNotNullAttribute
namespace Microsoft.EntityFrameworkCore.Query.ExpressionVisitors
{
    public class SqlCeTranslatingExpressionVisitor : SqlTranslatingExpressionVisitor
    {
        private readonly ISqlCeOptions _sqlCeOptions;

        public override Expression Visit(Expression expression)
        {
            if (!_sqlCeOptions.ClientEvalForUnsupportedSqlConstructs)
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
            [NotNull] ISqlCeOptions sqlCeOptions,
            [NotNull] RelationalQueryModelVisitor queryModelVisitor, 
            [CanBeNull] SelectExpression targetSelectExpression = null, 
            [CanBeNull] Expression topLevelPredicate = null, 
            bool inProjection = false)
            : base(dependencies, queryModelVisitor, targetSelectExpression, topLevelPredicate, inProjection)
        {
            _sqlCeOptions = sqlCeOptions;
        }


    }
}