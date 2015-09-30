using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Data.Entity.Query.Expressions;

namespace Microsoft.Data.Entity.Query.ExpressionTranslators.Internal
{
    public class SqlCeStringReplaceTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _methodInfo = typeof(string).GetTypeInfo()
            .GetDeclaredMethods(nameof(string.Replace))
            .Single(m => m.GetParameters()[0].ParameterType == typeof(string));

        public virtual Expression Translate(MethodCallExpression methodCallExpression)
            => methodCallExpression.Method == _methodInfo
                ? new SqlFunctionExpression(
                    "REPLACE",
                    methodCallExpression.Type,
                    new[] { methodCallExpression.Object }.Concat(methodCallExpression.Arguments))
                : null;
    }
}
