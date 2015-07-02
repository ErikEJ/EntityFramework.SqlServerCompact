using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Query.Expressions;
using Microsoft.Data.Entity.Query.Methods;

namespace Microsoft.Data.Entity.SqlServerCompact.Query.Methods
{
    public class StringReplaceTranslator : IMethodCallTranslator
    {
        public virtual Expression Translate([NotNull] MethodCallExpression methodCallExpression)
        {
            var methodInfo = typeof(string).GetTypeInfo()
                .GetDeclaredMethods("Replace")
                .Single(m => m.GetParameters()[0].ParameterType == typeof(string));

            if (methodInfo == methodCallExpression.Method)
            {
                var sqlArguments = new[] { methodCallExpression.Object }.Concat(methodCallExpression.Arguments);
                return new SqlFunctionExpression("REPLACE", sqlArguments, methodCallExpression.Type);
            }

            return null;
        }
    }
}
