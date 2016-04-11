using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    public class SqlCeStringSubstringTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _methodInfo = typeof(string).GetTypeInfo()
           .GetDeclaredMethods(nameof(string.Substring))
           .Single(m => m.GetParameters().Length == 2);

        public virtual Expression Translate(MethodCallExpression methodCallExpression)
            => methodCallExpression.Method == _methodInfo
                ? new SqlFunctionExpression(
                    "SUBSTRING",
                    methodCallExpression.Type,
                    new[] {
                        methodCallExpression.Object,
                        // Accomodate for SQL Server Compact assumption of 1-based string indexes
                        methodCallExpression.Arguments[0].NodeType == ExpressionType.Constant
                            ? (Expression)Expression.Constant(
                                (int)((ConstantExpression) methodCallExpression.Arguments[0]).Value + 1)
                            : Expression.Add(
                                methodCallExpression.Arguments[0],
                                Expression.Constant(1)),
                        methodCallExpression.Arguments[1] })
                : null;

    }
}
