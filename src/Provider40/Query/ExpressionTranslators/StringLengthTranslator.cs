using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Query.Expressions;

namespace Microsoft.Data.Entity.Query.ExpressionTranslators
{
    public class StringLengthTranslator : IMemberTranslator
    {
        public virtual Expression Translate([NotNull] MemberExpression memberExpression)
        {
            if (memberExpression.Expression != null
                && memberExpression.Expression.Type == typeof(string)
                && memberExpression.Member.Name == nameof(string.Length))
            {
                return new SqlFunctionExpression("LEN", memberExpression.Type, new[] { memberExpression.Expression });
            }

            return null;
        }
    }
}
