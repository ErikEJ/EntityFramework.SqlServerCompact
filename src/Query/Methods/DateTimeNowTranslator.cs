using System;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Relational.Query.Expressions;
using Microsoft.Data.Entity.Relational.Query.Methods;

namespace ErikEJ.Data.Entity.SqlServerCe.Query.Methods
{
    public class DateTimeNowTranslator : IMemberTranslator
    {
        public virtual Expression Translate([NotNull] MemberExpression memberExpression)
        {
            if (memberExpression.Expression == null
                && memberExpression.Member.DeclaringType == typeof(DateTime)
                && memberExpression.Member.Name == "Now")
            {
                return new SqlFunctionExpression("GETDATE", Enumerable.Empty<Expression>(), memberExpression.Type);
            }

            return null;
        }
    }
}
