using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Storage;

// ReSharper disable AssignNullToNotNullAttribute
namespace Microsoft.EntityFrameworkCore.Query.ExpressionVisitors
{
    public class SqlCeTranslatingExpressionVisitor : SqlTranslatingExpressionVisitor
    {
        private readonly IExpressionFragmentTranslator _compositeExpressionFragmentTranslator;

        public override Expression Visit(Expression expression)
        {
            return base.Visit(expression);
        }

        protected override Exception CreateUnhandledItemException<T>(T unhandledItem, string visitMethod)
            => null; // Never called

        public SqlCeTranslatingExpressionVisitor(
            [NotNull] IRelationalAnnotationProvider relationalAnnotationProvider, 
            [NotNull] IExpressionFragmentTranslator compositeExpressionFragmentTranslator, 
            [NotNull] IMethodCallTranslator methodCallTranslator, 
            [NotNull] IMemberTranslator memberTranslator, 
            [NotNull] IRelationalTypeMapper relationalTypeMapper, 
            [NotNull] RelationalQueryModelVisitor queryModelVisitor, 
            [CanBeNull] SelectExpression targetSelectExpression = null, 
            [CanBeNull] Expression topLevelPredicate = null, 
            bool bindParentQueries = false, 
            bool inProjection = false)
            : base(relationalAnnotationProvider, compositeExpressionFragmentTranslator, methodCallTranslator, memberTranslator, relationalTypeMapper, queryModelVisitor, targetSelectExpression, topLevelPredicate, bindParentQueries, inProjection)
        {
            _compositeExpressionFragmentTranslator = compositeExpressionFragmentTranslator;
        }
    }
}