using JetBrains.Annotations;
using Microsoft.Data.Entity.Relational.Query.Expressions;
using Microsoft.Data.Entity.Relational.Query.Sql;
using Microsoft.Data.Entity.Utilities;
using System.Linq;

namespace ErikEJ.Data.Entity.SqlServerCe.Query
{
    public class SqlCeQuerySqlGenerator : DefaultQuerySqlGenerator
    {
        public SqlCeQuerySqlGenerator([NotNull] SelectExpression selectExpression)
            : base(Check.NotNull(selectExpression, nameof(selectExpression)))
        {
        }

        protected override string DelimitIdentifier(string identifier)
            => "[" + identifier.Replace("]", "]]") + "]";

        protected override void GenerateLimitOffset(SelectExpression selectExpression)
        {
            if (selectExpression.Offset != null
                && !selectExpression.OrderBy.Any())
            {
                Sql.AppendLine().Append("ORDER BY GETDATE()");
            }

            base.GenerateLimitOffset(selectExpression);
        }
    }
}
