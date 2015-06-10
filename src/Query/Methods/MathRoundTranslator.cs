using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Relational.Query.Expressions;
using Microsoft.Data.Entity.Relational.Query.Methods;

namespace ErikEJ.Data.Entity.SqlServerCe.Query.Methods
{
    public class MathRoundTranslator : IMethodCallTranslator
    {
        public virtual Expression Translate([NotNull] MethodCallExpression methodCallExpression)
        {
            var methodInfos = typeof(Math).GetTypeInfo().GetDeclaredMethods("Round").Where(m => 
                m.GetParameters().Count() == 1 
                || (m.GetParameters().Count() == 2 && m.GetParameters()[1].ParameterType == typeof(int)));

            if (methodInfos.Contains(methodCallExpression.Method))
            {
                var arguments = new[] { methodCallExpression.Arguments[0], Expression.Constant(0) };

                return new SqlFunctionExpression("ROUND", arguments, methodCallExpression.Type);
            }

            return null;
        }
    }
}
