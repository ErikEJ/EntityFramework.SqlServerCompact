using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Storage;

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
            [NotNull] IRelationalAnnotationProvider relationalAnnotationProvider, 
            [NotNull] IExpressionFragmentTranslator compositeExpressionFragmentTranslator, 
            [NotNull] IMethodCallTranslator methodCallTranslator, 
            [NotNull] IMemberTranslator memberTranslator, 
            [NotNull] IRelationalTypeMapper relationalTypeMapper, 
            [NotNull] IDbContextOptions contextOptions,
            [NotNull] RelationalQueryModelVisitor queryModelVisitor, 
            [CanBeNull] SelectExpression targetSelectExpression = null, 
            [CanBeNull] Expression topLevelPredicate = null, 
            bool bindParentQueries = false, 
            bool inProjection = false)
            : base(relationalAnnotationProvider, compositeExpressionFragmentTranslator, methodCallTranslator, memberTranslator, relationalTypeMapper, queryModelVisitor, targetSelectExpression, topLevelPredicate, bindParentQueries, inProjection)
        {
            _contextOptions = contextOptions;
        }
    }
}