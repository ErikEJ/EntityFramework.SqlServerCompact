﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Migrations.Internal
{
    public abstract class MigrationsModelDifferTestBase
    {
        protected void Execute(
            Action<ModelBuilder> buildSourceAction,
            Action<ModelBuilder> buildTargetAction,
            Action<IReadOnlyList<MigrationOperation>> assertAction)
            => Execute(m => { }, buildSourceAction, buildTargetAction, assertAction);

        protected void Execute(
            Action<ModelBuilder> buildCommonAction,
            Action<ModelBuilder> buildSourceAction,
            Action<ModelBuilder> buildTargetAction,
            Action<IReadOnlyList<MigrationOperation>> assertAction)
        {
            var sourceModelBuilder = CreateModelBuilder();
            buildCommonAction(sourceModelBuilder);
            buildSourceAction(sourceModelBuilder);

            var targetModelBuilder = CreateModelBuilder();
            buildCommonAction(targetModelBuilder);
            buildTargetAction(targetModelBuilder);

            var modelDiffer = CreateModelDiffer();

            var operations = modelDiffer.GetDifferences(sourceModelBuilder.Model, targetModelBuilder.Model);

            assertAction(operations);
        }

        protected abstract ModelBuilder CreateModelBuilder();

        protected virtual MigrationsModelDiffer CreateModelDiffer()
            => new MigrationsModelDiffer(
                new ConcreteTypeMapper(new RelationalTypeMapperDependencies()),
                new MigrationsAnnotationProvider(new MigrationsAnnotationProviderDependencies()));

        private class ConcreteTypeMapper : RelationalTypeMapper
        {
            public ConcreteTypeMapper(RelationalTypeMapperDependencies dependencies)
                : base(dependencies)
            {
            }

            protected override string GetColumnType(IProperty property) => property.TestProvider().ColumnType;

            public override RelationalTypeMapping FindMapping(Type clrType)
                => clrType == typeof(string)
                    ? new StringTypeMapping("varchar(4000)", dbType: null, unicode: false, size: 4000)
                    : base.FindMapping(clrType);

            protected override RelationalTypeMapping FindCustomMapping(IProperty property)
                => property.ClrType == typeof(string) && (property.GetMaxLength().HasValue || property.IsUnicode().HasValue)
                    ? new StringTypeMapping(((property.IsUnicode() ?? true) ? "n" : "") + "varchar(" + (property.GetMaxLength() ?? 767) + ")", dbType: null, unicode: false, size: property.GetMaxLength())
                    : base.FindCustomMapping(property);

            private readonly IReadOnlyDictionary<Type, RelationalTypeMapping> _simpleMappings
                = new Dictionary<Type, RelationalTypeMapping>
                {
                    { typeof(int), new IntTypeMapping("int") },
                    { typeof(bool), new BoolTypeMapping("boolean") }
                };

            private readonly IReadOnlyDictionary<string, RelationalTypeMapping> _simpleNameMappings
                = new Dictionary<string, RelationalTypeMapping>
                {
                    { "varchar", new StringTypeMapping("varchar", dbType: null, unicode: false, size: null) },
                    { "bigint", new LongTypeMapping("bigint") }
                };

            protected override IReadOnlyDictionary<Type, RelationalTypeMapping> GetClrTypeMappings()
                => _simpleMappings;

            protected override IReadOnlyDictionary<string, RelationalTypeMapping> GetStoreTypeMappings()
                => _simpleNameMappings;
        }
    }
}
