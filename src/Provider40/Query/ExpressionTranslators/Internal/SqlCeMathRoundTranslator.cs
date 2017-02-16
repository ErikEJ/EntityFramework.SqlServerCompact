using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    public class SqlCeMathRoundTranslator : IMethodCallTranslator
    {
        private static readonly IEnumerable<MethodInfo> _methodInfos = typeof(Math).GetTypeInfo().GetDeclaredMethods(nameof(Math.Round))
            .Where(m => (m.GetParameters().Length == 1)
                        || ((m.GetParameters().Length == 2) && (m.GetParameters()[1].ParameterType == typeof(int))));

        public virtual Expression Translate(MethodCallExpression methodCallExpression)
        {            
            if (_methodInfos.Contains(methodCallExpression.Method))
            {
                var arguments = methodCallExpression.Arguments.Count == 1
                    ? new[] { methodCallExpression.Arguments[0], Expression.Constant(0) }
                    : new[] { methodCallExpression.Arguments[0], methodCallExpression.Arguments[1] };

                return new SqlFunctionExpression("ROUND", methodCallExpression.Type, arguments);
            }

            return null;
        }
    }
}
