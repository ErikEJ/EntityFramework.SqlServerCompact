using System;
using System.Linq;
using System.Linq.Expressions;
using EFCore.SqlCe.Query.Expressions.Internal;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;
using Remotion.Linq.Clauses;

namespace EFCore.SqlCe.Query.Sql.Internal
{
    public class SqlCeQuerySqlGenerator : DefaultQuerySqlGenerator, ISqlCeExpressionVisitor
    {
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public SqlCeQuerySqlGenerator(
            [NotNull] QuerySqlGeneratorDependencies dependencies,
            [NotNull] SelectExpression selectExpression)
            : base(dependencies, selectExpression)
        {
        }

        public override Expression VisitCrossJoinLateral(CrossJoinLateralExpression crossJoinLateralExpression)
        {
            Check.NotNull(crossJoinLateralExpression, nameof(crossJoinLateralExpression));

            Sql.Append("CROSS APPLY ");

            Visit(crossJoinLateralExpression.TableExpression);

            return crossJoinLateralExpression;
        }

        public override Expression VisitSqlFunction(SqlFunctionExpression sqlFunctionExpression)
        {
            if (sqlFunctionExpression.FunctionName.StartsWith("@@", StringComparison.Ordinal))
            {
                Sql.Append(sqlFunctionExpression.FunctionName);

                return sqlFunctionExpression;
            }

            if ((sqlFunctionExpression.FunctionName == "COUNT")
                && (sqlFunctionExpression.Type == typeof(long)))
            {
                Sql.Append("CAST(COUNT(*) AS bigint)");

                return sqlFunctionExpression;
            }

            return base.VisitSqlFunction(sqlFunctionExpression);
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
            if ((expression.NodeType == ExpressionType.Equal)
                || (expression.NodeType == ExpressionType.NotEqual))
            {
                var left = expression.Left.RemoveConvert();
                var right = expression.Right.RemoveConvert();
                Expression replacedExpression = null;
                var leftSelect = left as SelectExpression;
                var rightSelect = right as SelectExpression;
                if ((leftSelect != null) && (rightSelect == null))
                {
                    replacedExpression = new InExpression(right, leftSelect);
                }

                if ((rightSelect != null) && (leftSelect == null))
                {
                    replacedExpression = new InExpression(left, rightSelect);
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

        protected override void GenerateProjection(Expression projection)
        {
            var aliasedProjection = projection as AliasExpression;
            var expressionToProcess = aliasedProjection?.Expression ?? projection;
            var updatedExperssion = ExplicitCastToBool(expressionToProcess);

            expressionToProcess = aliasedProjection != null
                ? new AliasExpression(aliasedProjection.Alias, updatedExperssion)
                : updatedExperssion;

            base.GenerateProjection(expressionToProcess);
        }

        private Expression ExplicitCastToBool(Expression expression)
        {
            return (expression as BinaryExpression)?.NodeType == ExpressionType.Coalesce
                   && expression.Type.UnwrapNullableType() == typeof(bool)
                ? new ExplicitCastExpression(expression, expression.Type)
                : expression;
        }
    }
}
