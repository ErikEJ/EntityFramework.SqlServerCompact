using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;

namespace EFCore.SqlCe.Query.ExpressionTranslators.Internal
{
    public class SqlCeStringIsNullOrWhiteSpaceTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _methodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.IsNullOrWhiteSpace), new[] { typeof(string) });

        public virtual Expression Translate(MethodCallExpression methodCallExpression)
        {
            if (methodCallExpression.Method.Equals(_methodInfo))
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
