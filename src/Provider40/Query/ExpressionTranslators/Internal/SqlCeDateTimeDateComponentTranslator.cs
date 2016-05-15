using System;
using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    public class SqlCeDateTimeDateComponentTranslator : IMemberTranslator
    {
        public virtual Expression Translate(MemberExpression memberExpression)
            => (memberExpression.Expression != null)
            && (memberExpression.Expression.Type == typeof(DateTime))
            && (memberExpression.Member.Name == nameof(DateTime.Date))
            ? new SqlFunctionExpression("CONVERT",
                memberExpression.Type,
                new[]
                {
                   Expression.Constant(DbType.Date),
                   memberExpression.Expression
                })
            : null;
    }
}
