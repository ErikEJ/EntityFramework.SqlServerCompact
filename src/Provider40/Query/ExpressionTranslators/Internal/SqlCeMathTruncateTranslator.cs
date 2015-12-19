using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Query.Expressions;

namespace Microsoft.Data.Entity.Query.ExpressionTranslators.Internal
{
    public class SqlCeMathTruncateTranslator : IMethodCallTranslator
    {
        private static readonly IEnumerable<MethodInfo> _methodInfos = typeof(Math).GetTypeInfo().GetDeclaredMethods(nameof(Math.Truncate));

        public virtual Expression Translate(MethodCallExpression methodCallExpression)
        {
            if (_methodInfos.Contains(methodCallExpression.Method))
            {
                var arguments = new[] { methodCallExpression.Arguments[0], Expression.Constant(0), Expression.Constant(1) };
                return new SqlFunctionExpression("ROUND", methodCallExpression.Type, arguments);
            }

            return null;
        }
    }
}
