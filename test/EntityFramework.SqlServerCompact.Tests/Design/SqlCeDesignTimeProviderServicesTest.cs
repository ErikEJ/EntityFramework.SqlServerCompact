using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Design;
using EFCore.SqlCe.Scaffolding.Internal;
using EFCore.SqlCe.Storage.Internal;

namespace EntityFramework.SqlServerCompact40.Design.FunctionalTest
{
    public class SqlCeDesignTimeProviderServicesTest : DesignTimeProviderServicesTest
    {
        protected override Assembly GetRuntimeAssembly()
            => typeof(SqlCeDatabaseConnection).GetTypeInfo().Assembly;

        protected override Type GetDesignTimeServicesType()
            => typeof(SqlCeDesignTimeServices);
    }
}