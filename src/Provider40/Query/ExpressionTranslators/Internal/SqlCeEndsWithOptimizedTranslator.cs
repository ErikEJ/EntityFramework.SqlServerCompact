using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class SqlCeEndsWithOptimizedTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _methodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.EndsWith), new[] { typeof(string) });

        public virtual Expression Translate(MethodCallExpression methodCallExpression)
        {
            if (ReferenceEquals(methodCallExpression.Method, _methodInfo))
            {
                var patternExpression = methodCallExpression.Arguments[0];
                var patternConstantExpression = patternExpression as ConstantExpression;
                //SUBSTRING(a, LEN(a) - LEN(b) + 1, LEN(b))
                //RIGHT(a, LEN(b)) = b
                var endsWithExpression = Expression.Equal(
                    new SqlFunctionExpression(
                        "SUBSTRING",
                        typeof(string),
                        new[]
                        {
                            methodCallExpression.Object,
                            Expression.Subtract( 
                                Expression.Add(
                                    new SqlFunctionExpression("LEN", typeof(int), 
                                        new[] { methodCallExpression.Object }),
                                    Expression.Constant(1)),
                                new SqlFunctionExpression("LEN", typeof(int), new[] { patternExpression })),
                            new SqlFunctionExpression("LEN", typeof(int), new[] { patternExpression })
                        }),
                        patternExpression);

                return new NotNullableExpression(
                        patternConstantExpression != null
                        ? (string)patternConstantExpression.Value == string.Empty
                            ? (Expression)Expression.Constant(true)
                            : endsWithExpression
                        : Expression.OrElse(
                            endsWithExpression,
                            Expression.Equal(patternExpression, Expression.Constant(string.Empty))));
            }

            return null;
        }
    }
}
