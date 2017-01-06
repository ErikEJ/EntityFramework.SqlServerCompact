﻿using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionVisitors
{
    /// <summary>
    ///     A factory for creating instances of <see cref="SqlCeTranslatingExpressionVisitor" />.
    /// </summary>
    public class SqlCeTranslatingExpressionVisitorFactory : ISqlTranslatingExpressionVisitorFactory
    {
        private readonly IRelationalAnnotationProvider _relationalAnnotationProvider;
        private readonly IExpressionFragmentTranslator _compositeExpressionFragmentTranslator;
        private readonly IMethodCallTranslator _methodCallTranslator;
        private readonly IMemberTranslator _memberTranslator;
        private readonly IDbContextOptions _contextOptions;
        private readonly IRelationalTypeMapper _relationalTypeMapper;

        /// <summary>
        ///     Creates a new instance of <see cref="SqlTranslatingExpressionVisitorFactory" />.
        /// </summary>
        /// <param name="relationalAnnotationProvider"> The relational annotation provider. </param>
        /// <param name="compositeExpressionFragmentTranslator"> The composite expression fragment translator. </param>
        /// <param name="methodCallTranslator"> The method call translator. </param>
        /// <param name="memberTranslator"> The member translator. </param>
        /// <param name="relationalTypeMapper"> The relational type mapper. </param>
        public SqlCeTranslatingExpressionVisitorFactory(
            [NotNull] IRelationalAnnotationProvider relationalAnnotationProvider,
            [NotNull] IExpressionFragmentTranslator compositeExpressionFragmentTranslator,
            [NotNull] IMethodCallTranslator methodCallTranslator,
            [NotNull] IMemberTranslator memberTranslator,
            [NotNull] IDbContextOptions contextOptions,
            [NotNull] IRelationalTypeMapper relationalTypeMapper)
        {
            Check.NotNull(relationalAnnotationProvider, nameof(relationalAnnotationProvider));
            Check.NotNull(compositeExpressionFragmentTranslator, nameof(compositeExpressionFragmentTranslator));
            Check.NotNull(methodCallTranslator, nameof(methodCallTranslator));
            Check.NotNull(memberTranslator, nameof(memberTranslator));
            Check.NotNull(contextOptions, nameof(contextOptions));
            Check.NotNull(relationalTypeMapper, nameof(relationalTypeMapper));

            _relationalAnnotationProvider = relationalAnnotationProvider;
            _compositeExpressionFragmentTranslator = compositeExpressionFragmentTranslator;
            _methodCallTranslator = methodCallTranslator;
            _memberTranslator = memberTranslator;
            _contextOptions = contextOptions;
            _relationalTypeMapper = relationalTypeMapper;
        }

        /// <summary>
        ///     Creates a new SqlTranslatingExpressionVisitor.
        /// </summary>
        /// <param name="queryModelVisitor"> The query model visitor. </param>
        /// <param name="targetSelectExpression"> The target select expression. </param>
        /// <param name="topLevelPredicate"> The top level predicate. </param>
        /// <param name="bindParentQueries"> true to bind parent queries. </param>
        /// <param name="inProjection"> true if we are translating a projection. </param>
        /// <returns>
        ///     A SqlTranslatingExpressionVisitor.
        /// </returns>
        public virtual SqlTranslatingExpressionVisitor Create(
            RelationalQueryModelVisitor queryModelVisitor,
            SelectExpression targetSelectExpression = null,
            Expression topLevelPredicate = null,
            bool bindParentQueries = false,
            bool inProjection = false)
            => new SqlCeTranslatingExpressionVisitor(
                _relationalAnnotationProvider,
                _compositeExpressionFragmentTranslator,
                _methodCallTranslator,
                _memberTranslator,
                _relationalTypeMapper,
                _contextOptions,
                Check.NotNull(queryModelVisitor, nameof(queryModelVisitor)),
                targetSelectExpression,
                topLevelPredicate,
                bindParentQueries,
                inProjection);
    }
}