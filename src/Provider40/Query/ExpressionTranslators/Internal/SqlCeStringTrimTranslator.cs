using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    public class SqlCeStringTrimTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _methodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.Trim), new Type[] { });

        public virtual Expression Translate(MethodCallExpression methodCallExpression)
        {
            if (_methodInfo.Equals(methodCallExpression.Method))
            {
                var sqlArguments = new[] { methodCallExpression.Object };
                return new SqlFunctionExpression(
                    "LTRIM",
                    methodCallExpression.Type,
                    new[]
                    {
                        new SqlFunctionExpression(
                            "RTRIM",
                            methodCallExpression.Type,
                            sqlArguments),
                    });
            }

            return null;
        }
    }
}
