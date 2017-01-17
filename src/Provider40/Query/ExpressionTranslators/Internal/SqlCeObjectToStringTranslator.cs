using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class SqlCeObjectToStringTranslator : IMethodCallTranslator
    {
        private const int DefaultLength = 100;

        private static readonly Dictionary<Type, string> _typeMapping
            = new Dictionary<Type, string>
            {
                { typeof(int), "NVARCHAR(11)" },
                { typeof(long), "NVARCHAR(20)" },
                { typeof(DateTime), $"NVARCHAR({DefaultLength})" },
                { typeof(Guid), "NVARCHAR(36)" },
                { typeof(bool), "NVARCHAR(5)" },
                { typeof(byte), "NVARCHAR(3)" },
                { typeof(byte[]), $"NVARCHAR({DefaultLength})" },
                { typeof(double), $"NVARCHAR({DefaultLength})" },
                { typeof(DateTimeOffset), $"NVARCHAR({DefaultLength})" },
                { typeof(char), "NVARCHAR(1)" },
                { typeof(short), "NVARCHAR(6)" },
                { typeof(float), $"NVARCHAR({DefaultLength})" },
                { typeof(decimal), $"NVARCHAR({DefaultLength})" },
                { typeof(TimeSpan), $"NVARCHAR({DefaultLength})" },
                { typeof(uint), "NVARCHAR(10)" },
                { typeof(ushort), "NVARCHAR(5)" },
                { typeof(ulong), "NVARCHAR(19)" },
                { typeof(sbyte), "NVARCHAR(4)" }
            };

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual Expression Translate(MethodCallExpression methodCallExpression)
        {
            string storeType;

            if (methodCallExpression.Method.Name == nameof(ToString)
                && methodCallExpression.Arguments.Count == 0
                && methodCallExpression.Object != null
                && _typeMapping.TryGetValue(
                    methodCallExpression.Object.Type
                        .UnwrapNullableType()
                        .UnwrapEnumType(),
                    out storeType))
            {
                return new SqlFunctionExpression(
                    functionName: "CONVERT",
                    returnType: methodCallExpression.Type,
                    arguments: new[]
                    {
                        new SqlFragmentExpression(storeType),
                        methodCallExpression.Object
                    });
            }

            return null;
        }
    }
}
