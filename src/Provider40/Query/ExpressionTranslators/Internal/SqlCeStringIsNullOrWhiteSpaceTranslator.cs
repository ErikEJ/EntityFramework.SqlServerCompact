﻿using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    public class SqlCeStringIsNullOrWhiteSpaceTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _methodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.IsNullOrWhiteSpace), new[] { typeof(string) });

        public virtual Expression Translate(MethodCallExpression methodCallExpression)
        {
            if (_methodInfo == methodCallExpression.Method)
            {
                var argument = methodCallExpression.Arguments[0];

                return Expression.MakeBinary(
                    ExpressionType.OrElse,
                    new IsNullExpression(argument),
                    Expression.Equal(
                        new SqlFunctionExpression(
                            "LTRIM",
                            typeof(string),
                            new[]
                            {
                                new SqlFunctionExpression(
                                    "RTRIM",
                                    typeof(string),
                                    new [] { argument})
                            }),
                        Expression.Constant("", typeof(string))));
            }

            return null;
        }
    }
}
