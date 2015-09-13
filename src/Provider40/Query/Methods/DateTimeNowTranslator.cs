using System;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Query.Expressions;
using Microsoft.Data.Entity.Query.ExpressionTranslators;

namespace Microsoft.Data.Entity.SqlServerCompact.Query.Methods
{
    public class DateTimeNowTranslator : IMemberTranslator
    {
        public virtual Expression Translate([NotNull] MemberExpression memberExpression)
        {
            if (memberExpression.Expression == null
                && memberExpression.Member.DeclaringType == typeof(DateTime)
                && memberExpression.Member.Name == nameof(DateTime.Now))
            {
                return new SqlFunctionExpression("GETDATE", memberExpression.Type);
            }

            return null;
        }
    }
}
