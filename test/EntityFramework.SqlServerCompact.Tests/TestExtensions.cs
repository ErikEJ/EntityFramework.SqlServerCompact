// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Data.Entity.Tests
{
    public static class TestExtensions
    {
        public static IServiceCollection ServiceCollection(this EntityFrameworkServicesBuilder builder)
            => builder.GetInfrastructure();
    }
}
