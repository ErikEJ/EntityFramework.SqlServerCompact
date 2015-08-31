﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Query.Expressions;
using Microsoft.Data.Entity.Query.ExpressionTranslators;

namespace Microsoft.Data.Entity.SqlServerCompact.Query.Methods
{
    public class ConvertTranslator : IMethodCallTranslator
    {
        private static readonly Dictionary<string, DbType> _typeMapping = new Dictionary<string, DbType>
        {
            [nameof(Convert.ToByte)] = DbType.Byte,
            [nameof(Convert.ToDecimal)] = DbType.Decimal,
            [nameof(Convert.ToDouble)] = DbType.Double,
            [nameof(Convert.ToInt16)] = DbType.Int16,
            [nameof(Convert.ToInt32)] = DbType.Int32,
            [nameof(Convert.ToInt64)] = DbType.Int64,
            [nameof(Convert.ToString)] = DbType.String,
        };

        private static readonly List<Type> _supportedTypes = new List<Type>
        {
            typeof(bool),
            typeof(byte),
            typeof(decimal),
            typeof(double),
            typeof(float),
            typeof(int),
            typeof(long),
            typeof(short),
            typeof(string),
        };

        private static readonly IEnumerable<MethodInfo> _supportedMethods;

        static ConvertTranslator()
        {
            _supportedMethods = _typeMapping.Keys
                .SelectMany(t => typeof(Convert).GetTypeInfo().GetDeclaredMethods(t)
                    .Where(m => m.GetParameters().Count() == 1
                        && _supportedTypes.Contains(m.GetParameters().First().ParameterType)));
        }

        public virtual Expression Translate([NotNull] MethodCallExpression methodCallExpression)
        {
            if (_supportedMethods.Contains(methodCallExpression.Method))
            {
                var arguments = new[] { Expression.Constant(_typeMapping[methodCallExpression.Method.Name]), methodCallExpression.Arguments[0] };

                return new SqlFunctionExpression("CONVERT", arguments, methodCallExpression.Type);
            }

            return null;
        }
    }
}
