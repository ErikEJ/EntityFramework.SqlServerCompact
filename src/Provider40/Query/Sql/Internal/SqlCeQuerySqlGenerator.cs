#if SQLCE35
using System;
#endif
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;
using Remotion.Linq.Clauses;
using Remotion.Linq.Parsing;

namespace Microsoft.EntityFrameworkCore.Query.Sql.Internal
{
    public class SqlCeQuerySqlGenerator : DefaultQuerySqlGenerator, ISqlCeExpressionVisitor
    {
        public SqlCeQuerySqlGenerator(
            [NotNull] IRelationalCommandBuilderFactory commandBuilderFactory,
            [NotNull] ISqlGenerationHelper sqlGenerationHelper,
            [NotNull] IParameterNameGeneratorFactory parameterNameGeneratorFactory,
            [NotNull] IRelationalTypeMapper relationalTypeMapper,
            [NotNull] SelectExpression selectExpression)
            : base(commandBuilderFactory, sqlGenerationHelper, parameterNameGeneratorFactory, relationalTypeMapper, selectExpression)
        {
        }

        public override Expression VisitLateralJoin(LateralJoinExpression lateralJoinExpression)
        {
            Check.NotNull(lateralJoinExpression, nameof(lateralJoinExpression));

            Sql.Append("CROSS APPLY ");

            Visit(lateralJoinExpression.TableExpression);

            return lateralJoinExpression;
        }

        public override Expression VisitCount(CountExpression countExpression)
        {
            Check.NotNull(countExpression, nameof(countExpression));

            if (countExpression.Type != typeof (long))
            {
                return base.VisitCount(countExpression);
            }
            Sql.Append("CAST(COUNT(*) AS bigint)");

            return countExpression;
        }

        public virtual Expression VisitDatePartExpression(DatePartExpression datePartExpression)
        {
            Check.NotNull(datePartExpression, nameof(datePartExpression));

            Sql.Append("DATEPART(")
                .Append(datePartExpression.DatePart)
                .Append(", ");
            Visit(datePartExpression.Argument);
            Sql.Append(")");
            return datePartExpression;
        }

        protected override void GenerateLimitOffset(SelectExpression selectExpression)
        {
#if SQLCE35
            if (selectExpression.Offset != null)
            {
                throw new NotSupportedException("SKIP clause is not supported by SQL Server Compact 3.5");
            }
#endif
            if ((selectExpression.Offset != null)
                && !selectExpression.OrderBy.Any())
            {
                Sql.AppendLine().Append("ORDER BY GETDATE()");
            }

            base.GenerateLimitOffset(selectExpression);
        }

        protected override void GenerateOrdering(Ordering ordering)
        {
            if (ordering.Expression is ParameterExpression
                || ordering.Expression is ConstantExpression)
            {
                Sql.Append("GETDATE()");
            }
            else
            {
                base.GenerateOrdering(ordering);
            }
        }

        protected override void VisitProjection(IReadOnlyList<Expression> projections)
        {
            var comparisonTransformer = new ProjectionComparisonTransformingVisitor();
            var transformedProjections = projections.Select(comparisonTransformer.Visit).ToList();

            base.VisitProjection(transformedProjections);
        }

        private class ProjectionComparisonTransformingVisitor : RelinqExpressionVisitor
        {
            private bool _insideConditionalTest;

            protected override Expression VisitUnary(UnaryExpression node)
            {
                if (!_insideConditionalTest
                    && (node.NodeType == ExpressionType.Not)
                    && node.Operand is AliasExpression)
                {
                    return Expression.Condition(
                        node,
                        Expression.Constant(true, typeof(bool)),
                        Expression.Constant(false, typeof(bool)));
                }

                return base.VisitUnary(node);
            }

            protected override Expression VisitBinary(BinaryExpression node)
            {
                if (!_insideConditionalTest
                    && (node.IsComparisonOperation()
                        || node.IsLogicalOperation()))
                {
                    return Expression.Condition(
                        node,
                        Expression.Constant(true, typeof(bool)),
                        Expression.Constant(false, typeof(bool)));
                }

                return base.VisitBinary(node);
            }

            protected override Expression VisitConditional(ConditionalExpression node)
            {
                _insideConditionalTest = true;
                var test = Visit(node.Test);
                _insideConditionalTest = false;
                if (test is AliasExpression)
                {
                    return Expression.Condition(
                        Expression.Equal(test, Expression.Constant(true, typeof(bool))),
                        Visit(node.IfTrue),
                        Visit(node.IfFalse));
                }

                var condition = test as ConditionalExpression;
                if (condition != null)
                {
                    return Expression.Condition(
                        condition.Test,
                        Visit(node.IfTrue),
                        Visit(node.IfFalse));
                }
                return Expression.Condition(test,
                    Visit(node.IfTrue),
                    Visit(node.IfFalse));
            }
        }
    }
}
