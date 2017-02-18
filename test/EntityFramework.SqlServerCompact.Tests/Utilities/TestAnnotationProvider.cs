﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Specification.Tests;

namespace Microsoft.EntityFrameworkCore.Relational.Tests.TestUtilities
{
    public class TestAnnotationProvider : IRelationalAnnotationProvider
    {
        public virtual IRelationalEntityTypeAnnotations For(IEntityType entityType) => entityType.TestProvider();
        public virtual IRelationalForeignKeyAnnotations For(IForeignKey foreignKey) => foreignKey.TestProvider();
        public virtual IRelationalIndexAnnotations For(IIndex index) => index.TestProvider();
        public virtual IRelationalKeyAnnotations For(IKey key) => key.TestProvider();
        public virtual IRelationalModelAnnotations For(IModel model) => model.TestProvider();
        public virtual IRelationalPropertyAnnotations For(IProperty property) => property.TestProvider();
    }
}