using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    public class SqlCeStringReplaceTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _methodInfo = typeof(string).GetTypeInfo()
            .GetDeclaredMethods(nameof(string.Replace))
            .Single(m => m.GetParameters()[0].ParameterType == typeof(string));

        public virtual Expression Translate(MethodCallExpression methodCallExpression)
            => _methodInfo.Equals(methodCallExpression.Method)
                ? new SqlFunctionExpression(
                    "REPLACE",
                    methodCallExpression.Type,
                    new[] { methodCallExpression.Object }.Concat(methodCallExpression.Arguments))
                : null;
    }
}
