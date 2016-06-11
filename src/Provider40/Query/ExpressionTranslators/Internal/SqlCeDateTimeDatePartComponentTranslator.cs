 using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    public class SqlCeDateTimeDatePartComponentTranslator : IMemberTranslator
    {
        public virtual Expression Translate(MemberExpression memberExpression)
        {
            string datePart;
            if (memberExpression.Expression != null
               && memberExpression.Expression.Type == typeof(DateTime)
               && (datePart = GetDatePart(memberExpression.Member.Name)) != null)
            {
                return new DatePartExpression(datePart,
                    memberExpression.Type,
                    memberExpression.Expression);
            }
            return null;
        }

        private static string GetDatePart(string memberName)
        {
            switch (memberName)
            {
                case nameof(DateTime.Year): return "year";
                case nameof(DateTime.Month): return "month";
                case nameof(DateTime.DayOfYear): return "dayofyear";
                case nameof(DateTime.Day): return "day";
                case nameof(DateTime.Hour): return "hour";
                case nameof(DateTime.Minute): return "minute";
                case nameof(DateTime.Second): return "second";
                case nameof(DateTime.Millisecond): return "millisecond";
                default: return null;
            }
        }
    }
}
