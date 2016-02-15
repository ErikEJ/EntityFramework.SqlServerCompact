using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    public class SqlCeDateTimeNowTranslator : IMemberTranslator
    {
        public virtual Expression Translate(MemberExpression memberExpression)
        {
            if ((memberExpression.Expression == null)
                && (memberExpression.Member.DeclaringType == typeof(DateTime))
                && (memberExpression.Member.Name == nameof(DateTime.Now)))
            {
                return new SqlFunctionExpression("GETDATE", memberExpression.Type);
            }

            return null;
        }
    }
}
