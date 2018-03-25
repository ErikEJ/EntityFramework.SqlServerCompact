using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;

namespace EFCore.SqlCe.Query.ExpressionTranslators.Internal
{
    public class SqlCeStringLengthTranslator : IMemberTranslator
    {
        public virtual Expression Translate(MemberExpression memberExpression)
        => (memberExpression.Expression != null)
           && (memberExpression.Expression.Type == typeof(string))
           && (memberExpression.Member.Name == nameof(string.Length))
            ? new SqlFunctionExpression("LEN", memberExpression.Type, new[] { memberExpression.Expression })
            : null;
    }
}
