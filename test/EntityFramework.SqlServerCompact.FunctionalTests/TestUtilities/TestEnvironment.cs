﻿namespace Microsoft.EntityFrameworkCore.TestUtilities
{
    public static class TestEnvironment
    {
        static TestEnvironment()
        {
        }

        public static bool? GetFlag(string key) => key == nameof(SqlServerCondition.SupportsOffset);
    }
}
