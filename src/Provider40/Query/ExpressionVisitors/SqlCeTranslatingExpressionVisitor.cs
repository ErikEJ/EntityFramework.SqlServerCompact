using System;
using System.Linq.Expressions;
using EFCore.SqlCe.Infrastructure.Internal;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using Microsoft.EntityFrameworkCore.Storage;

// ReSharper disable AssignNullToNotNullAttribute
namespace EFCore.SqlCe.Query.ExpressionVisitors
{
    public class SqlCeTranslatingExpressionVisitor : SqlTranslatingExpressionVisitor
    {
        private readonly ISqlCeOptions _sqlCeOptions;

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override Expression Visit(Expression expression)
        {
            if (!_sqlCeOptions.ClientEvalForUnsupportedSqlConstructs)
            {
                return base.Visit(expression);
            }

            var evaluatedExpression = base.Visit(expression);
            if (evaluatedExpression is SelectExpression)
            {
                return null;
            }
            return evaluatedExpression;
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected override Expression VisitBinary(BinaryExpression binaryExpression)
        {
            var visitedExpression = base.VisitBinary(binaryExpression);

            if (visitedExpression == null)
            {
                return null;
            }

            switch (visitedExpression.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.Subtract:
                case ExpressionType.Multiply:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                    return IsDateTimeBasedOperation(visitedExpression)
                        ? null
                        : visitedExpression;
            }

            return visitedExpression;
        }

        private static bool IsDateTimeBasedOperation(Expression expression)
        {
            if (expression is BinaryExpression binaryExpression)
            {
                var typeMapping = InferTypeMappingFromColumn(binaryExpression.Left)
                    ?? InferTypeMappingFromColumn(binaryExpression.Right);

                if (typeMapping != null
                    && typeMapping.StoreType == "datetime")
                {
                    return true;
                }
            }

            return false;
        }

        private static RelationalTypeMapping InferTypeMappingFromColumn(Expression expression)
            => expression.FindProperty(expression.Type)?.FindRelationalMapping();

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
