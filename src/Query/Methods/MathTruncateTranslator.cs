using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Relational.Query.Expressions;
using Microsoft.Data.Entity.Relational.Query.Methods;

namespace ErikEJ.Data.Entity.SqlServerCe.Query.Methods
{
    public class MathTruncateTranslator : IMethodCallTranslator
    {
        public virtual Expression Translate([NotNull] MethodCallExpression methodCallExpression)
        {
            var methodInfos = typeof(Math).GetTypeInfo().GetDeclaredMethods("Truncate");
            if (methodInfos.Contains(methodCallExpression.Method))
            {
                var arguments = new[] { methodCallExpression.Arguments[0], Expression.Constant(0), Expression.Constant(1) };
                return new SqlFunctionExpression("ROUND", arguments, methodCallExpression.Type);
            }

            return null;
        }
    }
}
