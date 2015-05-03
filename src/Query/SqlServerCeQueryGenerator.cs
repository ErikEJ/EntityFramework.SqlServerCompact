using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Relational.Query.Expressions;
using Microsoft.Data.Entity.Relational.Query.Sql;
using Microsoft.Data.Entity.Utilities;

namespace ErikEJ.Data.Entity.SqlServerCe.Query
{
    public class SqlServerCeQueryGenerator : DefaultSqlQueryGenerator
    {
        public SqlServerCeQueryGenerator([NotNull] SelectExpression selectExpression)
            : base(Check.NotNull(selectExpression, nameof(selectExpression)))
        {
        }

        protected override string DelimitIdentifier(string identifier)
            => "[" + identifier.Replace("]", "]]") + "]";

        //public override Expression VisitCountExpression(CountExpression countExpression)
        //{
        //    Check.NotNull(countExpression, nameof(countExpression));

        //    if (countExpression.Type == typeof(long))
        //    {
        //        Sql.Append("COUNT_BIG(*)");
        //        return countExpression;
        //    }

        //    return base.VisitCountExpression(countExpression);
        //}
    }
}
