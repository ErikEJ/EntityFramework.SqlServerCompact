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

            if (countExpression.Type != typeof(long))
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

        protected override Expression VisitBinary(BinaryExpression expression)
        {
            if (expression.NodeType == ExpressionType.Equal
                || expression.NodeType == ExpressionType.NotEqual)
            {
                Expression replacedExpression = null;
                var leftSelect = expression.Left as SelectExpression;
                var rightAlias = expression.Right as AliasExpression;
                if (leftSelect != null && rightAlias != null)
                {
                    replacedExpression = new InExpression(rightAlias, leftSelect);
                }

                var rightSelect = expression.Right as SelectExpression;
                var leftAlias = expression.Left as AliasExpression;
                if (rightSelect != null && leftAlias != null)
                {
                    replacedExpression = new InExpression(leftAlias, rightSelect);
                }

                if (replacedExpression != null)
                {
                    replacedExpression = expression.NodeType == ExpressionType.Equal
                        ? replacedExpression
                        : Expression.Not(replacedExpression);
                    return Visit(replacedExpression);
                }
            }
            return base.VisitBinary(expression);
        }
    }
}
