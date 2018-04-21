﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Xunit;

// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
namespace Microsoft.EntityFrameworkCore.Storage
{
    public abstract class SqlGenerationHelperTestBase
    {
        [Fact]
        public virtual void GenerateParameterName_returns_parameter_name()
        {
            var name = CreateSqlGenerationHelper().GenerateParameterName("name");
            Assert.Equal("@name", name);
        }

        [Fact]
        public void Default_BatchCommandSeparator_is_semicolon()
        {
            Assert.Equal(Environment.NewLine, CreateSqlGenerationHelper().StatementTerminator);
        }

        [Fact]
        public virtual void BatchSeparator_returns_separator()
        {
            Assert.Equal(string.Empty, CreateSqlGenerationHelper().BatchTerminator);
        }

        protected abstract ISqlGenerationHelper CreateSqlGenerationHelper();
    }
}
