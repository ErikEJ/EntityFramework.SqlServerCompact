﻿using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class SqlCeContainsOptimizedTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _methodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.Contains), new[] { typeof(string) });

        public virtual Expression Translate(MethodCallExpression methodCallExpression)
        {
            if (Equals(methodCallExpression.Method, _methodInfo))
            {
                var patternExpression = methodCallExpression.Arguments[0];
                var patternConstantExpression = patternExpression as ConstantExpression;

                var charIndexExpression = Expression.GreaterThan(
                    new SqlFunctionExpression("CHARINDEX", typeof(int), new[] { patternExpression, methodCallExpression.Object }),
                    Expression.Constant(0));

                return
                    patternConstantExpression != null
                    ? (string)patternConstantExpression.Value == string.Empty
                        ? (Expression)Expression.Constant(true)
                        : charIndexExpression
                    : Expression.OrElse(
                        charIndexExpression,
                        Expression.Equal(patternExpression, Expression.Constant(string.Empty)));
            }

            return null;
        }
    }
}
