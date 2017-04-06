﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class SqlCeDateAddTranslator : IMethodCallTranslator
    {
        /// <summary>
        ///     Translates the given method call expression.
        /// </summary>
        /// <param name="methodCallExpression">The method call expression.</param>
        /// <returns>
        ///     A SQL expression representing the translated MethodCallExpression.
        /// </returns>
        public virtual Expression Translate(MethodCallExpression methodCallExpression)
        {
            string datePart;
            if (methodCallExpression.Method.DeclaringType == typeof(DateTime)
                && (datePart = GetDatePart(methodCallExpression.Method.Name)) != null)
            {
                var amountToAdd = methodCallExpression.Arguments.First();

                if (!datePart.Equals("year")
                    && !datePart.Equals("month")
                    && amountToAdd is ConstantExpression constantExpression
                    && ((double)constantExpression.Value >= int.MaxValue
                        || (double)constantExpression.Value <= int.MinValue))
                {
                    return null;
                }

                return new SqlFunctionExpression(
                    functionName: "DATEADD",
                    returnType: methodCallExpression.Type,
                    arguments: new[]
                    {
                        new SqlFragmentExpression(datePart),
                        amountToAdd,
                        methodCallExpression.Object
                    });
            }
            return null;
        }

        private static string GetDatePart(string memberName)
        {
            switch (memberName)
            {
                case nameof(DateTime.AddYears):
                    return "year";
                case nameof(DateTime.AddMonths):
                    return "month";
                case nameof(DateTime.AddDays):
                    return "day";
                case nameof(DateTime.AddHours):
                    return "hour";
                case nameof(DateTime.AddMinutes):
                    return "minute";
                case nameof(DateTime.AddSeconds):
                    return "second";
                case nameof(DateTime.AddMilliseconds):
                    return "millisecond";
                default:
                    return null;
            }
        }
    }
}