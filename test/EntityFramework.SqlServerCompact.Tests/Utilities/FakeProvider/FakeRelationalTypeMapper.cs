// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore.TestUtilities.FakeProvider
{
    public class FakeRelationalTypeMapper : RelationalTypeMapper
    {
        private static readonly RelationalTypeMapping _int = new IntTypeMapping("int", DbType.Int32);
        private static readonly RelationalTypeMapping _long = new LongTypeMapping("DefaultLong", DbType.Int64);
        private static readonly RelationalTypeMapping _string = new StringTypeMapping("DefaultString", DbType.String);

        public FakeRelationalTypeMapper(
            CoreTypeMapperDependencies coreDependencies,
            RelationalTypeMapperDependencies dependencies)
            : base(coreDependencies, dependencies)
        {
        }

        private readonly IReadOnlyDictionary<Type, RelationalTypeMapping> _simpleMappings
            = new Dictionary<Type, RelationalTypeMapping>
            {
                { typeof(int), _int },
                { typeof(long), _long },
                { typeof(string), _string }
            };

        private readonly IReadOnlyDictionary<string, IList<RelationalTypeMapping>> _simpleNameMappings
            = new Dictionary<string, IList<RelationalTypeMapping>>
            {
                { "DefaultInt", new List<RelationalTypeMapping> { _int } },
                { "DefaultLong", new List<RelationalTypeMapping> { _long } },
                { "DefaultString", new List<RelationalTypeMapping> { _string } }
            };

        protected override IReadOnlyDictionary<Type, RelationalTypeMapping> GetClrTypeMappings()
            => _simpleMappings;

        protected override IReadOnlyDictionary<string, IList<RelationalTypeMapping>> GetMultipleStoreTypeMappings()
            => _simpleNameMappings;
    }
}
