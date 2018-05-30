using System;
using System.Collections.Generic;
using System.Reflection;
using EFCore.SqlCe.Storage.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore
{
    public class ApiConsistencyTest : ApiConsistencyTestBase
    {
        private static readonly Type[] _fluentApiTypes =
        {
            typeof(SqlCeDbContextOptionsBuilder),
            typeof(SqlCeDbContextOptionsExtensions),
            typeof(SqlCeMetadataExtensions),
            typeof(SqlCeServiceCollectionExtensions)
        };

        protected override IEnumerable<Type> FluentApiTypes => _fluentApiTypes;

        protected override void AddServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddEntityFrameworkSqlCe();
        }

        protected override Assembly TargetAssembly => typeof(SqlCeDatabaseConnection).GetTypeInfo().Assembly;
    }
}
