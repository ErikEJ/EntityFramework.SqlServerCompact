using JetBrains.Annotations;
using Microsoft.Data.Entity.Relational.Query.Expressions;
using Microsoft.Data.Entity.Relational.Query.Sql;
using Microsoft.Data.Entity.Utilities;

namespace ErikEJ.Data.Entity.SqlServerCe.Query
{
    public class SqlCeQueryGenerator : DefaultSqlQueryGenerator
    {
        public SqlCeQueryGenerator([NotNull] SelectExpression selectExpression)
            : base(Check.NotNull(selectExpression, nameof(selectExpression)))
        {
        }

        protected override string DelimitIdentifier(string identifier)
            => "[" + identifier.Replace("]", "]]") + "]";

    }
}
