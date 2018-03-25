using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;

namespace EFCore.SqlCe.Query.ExpressionTranslators.Internal
{
    public class SqlCeStringReplaceTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _methodInfo
                    = typeof(string).GetRuntimeMethod(nameof(string.Replace), new[] { typeof(string), typeof(string) });

        public virtual Expression Translate(MethodCallExpression methodCallExpression)
            => _methodInfo.Equals(methodCallExpression.Method)
                ? new SqlFunctionExpression(
                    "REPLACE",
                    methodCallExpression.Type,
                    new[] { methodCallExpression.Object }.Concat(methodCallExpression.Arguments))
                : null;
    }
}
