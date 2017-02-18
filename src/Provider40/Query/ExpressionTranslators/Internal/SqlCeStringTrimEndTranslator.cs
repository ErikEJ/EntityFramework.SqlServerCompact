using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    public class SqlCeStringTrimEndTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _trimEnd = typeof(string).GetTypeInfo()
            .GetDeclaredMethods(nameof(string.TrimEnd))
            .Single(m => m.GetParameters().Count() == 1 && m.GetParameters()[0].ParameterType == typeof(char[]));

        public virtual Expression Translate(MethodCallExpression methodCallExpression)
        {
            if (_trimEnd.Equals(methodCallExpression.Method)
                // SqlCe RTRIM does not take arguments
                && ((methodCallExpression.Arguments[0] as ConstantExpression)?.Value as Array)?.Length == 0)
            {
                var sqlArguments = new[] { methodCallExpression.Object };
                return new SqlFunctionExpression("RTRIM", methodCallExpression.Type, sqlArguments);
            }

            return null;
        }
    }
}
