using System;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Query.Expressions;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Query.Sql.Internal
{
    public class SqlCeQuerySqlGenerator : DefaultQuerySqlGenerator
    {
        public SqlCeQuerySqlGenerator(
            [NotNull] IRelationalCommandBuilderFactory commandBuilderFactory,
            [NotNull] ISqlGenerator sqlGenerator,
            [NotNull] IParameterNameGeneratorFactory parameterNameGeneratorFactory,
            [NotNull] SelectExpression selectExpression)
            : base(commandBuilderFactory, sqlGenerator, parameterNameGeneratorFactory, selectExpression)
        {
        }

        public override Expression VisitCount(CountExpression countExpression)
        {
            Check.NotNull(countExpression, nameof(countExpression));

            if (countExpression.Type == typeof(long))
            {
                Sql.Append("CAST(COUNT(*) AS bigint)");

                return countExpression;
            }
            return base.VisitCount(countExpression);
        }

        protected override void GenerateLimitOffset(SelectExpression selectExpression)
        {
#if SQLCE35
            if (selectExpression.Offset != null)
            {
                throw new NotSupportedException("SKIP clause is not supported by SQL Server Compact 3.5");
            }
#endif
            if (selectExpression.Offset != null
                && !selectExpression.OrderBy.Any())
            {
                Sql.AppendLine().Append("ORDER BY GETDATE()");
            }

            base.GenerateLimitOffset(selectExpression);
        }
    }
}
