﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;
using System;
using System.Linq;
using Xunit.Sdk;

namespace Microsoft.EntityFrameworkCore.Specification.Tests.Utilities
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    [TraitDiscoverer("Microsoft.EntityFrameworkCore.SqlServer.FunctionalTests.Utilities.SqlServerConditionTraitDiscoverer", "Microsoft.EntityFrameworkCore.SqlServer.FunctionalTests")]
    public class SqlServerConditionAttribute : Attribute, ITestCondition, ITraitAttribute
    {
        public SqlServerCondition Conditions { get; set; }

        public SqlServerConditionAttribute(SqlServerCondition conditions)
        {
            Conditions = conditions;
        }

        public bool IsMet
        {
            get
            {
                var isMet = true;
                if (Conditions.HasFlag(SqlServerCondition.SupportsSequences))
                {
                    return false;
                }
                if (Conditions.HasFlag(SqlServerCondition.SupportsOffset))
                {
                    return true;
                }
                if (Conditions.HasFlag(SqlServerCondition.IsSqlAzure))
                {
                    isMet = false;
                }
                return isMet;
            }
        }

        public string SkipReason =>
            string.Format("The test SQL Server does not meet these conditions: '{0}'"
                , string.Join(", ", Enum.GetValues(typeof(SqlServerCondition))
                    .Cast<Enum>()
                    .Where(f => Conditions.HasFlag(f))
                    .Select(f => Enum.GetName(typeof(SqlServerCondition), f))));
    }

    [Flags]
    public enum SqlServerCondition
    {
        SupportsSequences = 1 << 0,
        SupportsOffset = 1 << 1,
        IsSqlAzure = 1 << 2,
        IsNotSqlAzure = 1 << 3,
        SupportsMemoryOptimized = 1 << 4,
        SupportsAttach = 1 << 5
    }
}
