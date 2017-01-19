using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    public class SqlCeStringTrimStartTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _trimStart = typeof(string).GetTypeInfo()
            .GetDeclaredMethods(nameof(string.TrimStart))
            .Single(m => m.GetParameters().Count() == 1 && m.GetParameters()[0].ParameterType == typeof(char[]));

        public virtual Expression Translate(MethodCallExpression methodCallExpression)
        {
            if ((_trimStart == methodCallExpression.Method)
                // SqlCe LTRIM does not take arguments
                && (((methodCallExpression.Arguments[0] as ConstantExpression)?.Value as Array)?.Length == 0))
            {
                var sqlArguments = new[] { methodCallExpression.Object };
                return new SqlFunctionExpression("LTRIM", methodCallExpression.Type, sqlArguments);
            }

            return null;
        }
    }
}
