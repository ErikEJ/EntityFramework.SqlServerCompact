// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;

namespace Microsoft.EntityFrameworkCore.SqlServer.FunctionalTests.Utilities
{
    public static class TestEnvironment
    {
        static TestEnvironment()
        {
        }

        public static bool? GetFlag(string key)
        {
            return key == nameof(SqlServerCondition.SupportsOffset);
        }
    }
}
