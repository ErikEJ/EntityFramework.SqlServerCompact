using System;
using System.Reflection;
using EFCore.SqlCe.Storage.Internal;
using EFCore.SqlCe.Design.Internal;

namespace Microsoft.EntityFrameworkCore.Design
{
    public class SqlCeDesignTimeProviderServicesTest : DesignTimeProviderServicesTest
    {
        protected override Assembly GetRuntimeAssembly()
            => typeof(SqlCeDatabaseConnection).GetTypeInfo().Assembly;

        protected override Type GetDesignTimeServicesType()
            => typeof(SqlCeDesignTimeServices);
    }
}
