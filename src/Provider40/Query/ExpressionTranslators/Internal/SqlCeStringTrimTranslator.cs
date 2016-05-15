using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    public class SqlCeStringTrimTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _trim = typeof(string).GetTypeInfo()
            .GetDeclaredMethods(nameof(string.Trim))
            .SingleOrDefault(m => !m.GetParameters().Any());

        public virtual Expression Translate(MethodCallExpression methodCallExpression)
        {
            if (_trim == methodCallExpression.Method)
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
