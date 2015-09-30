using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Data.Entity.Query.Expressions;

namespace Microsoft.Data.Entity.Query.ExpressionTranslators.Internal
{
    public class SqlCeStringSubstringTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _methodInfo = typeof(string).GetTypeInfo()
            .GetDeclaredMethods(nameof(string.Substring))
            .Single(m => m.GetParameters().Count() == 2);

        public virtual Expression Translate(MethodCallExpression methodCallExpression)
            => methodCallExpression.Method == _methodInfo
                ? new SqlFunctionExpression(
                    "SUBSTRING",
                    methodCallExpression.Type,
                    new[] { methodCallExpression.Object }.Concat(methodCallExpression.Arguments))
                : null;
    }
}
