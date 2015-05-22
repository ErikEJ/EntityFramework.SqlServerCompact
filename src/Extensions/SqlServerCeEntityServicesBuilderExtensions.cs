using ErikEJ.Data.Entity.SqlServerCe;
using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using ErikEJ.Data.Entity.SqlServerCe.Migrations;
using ErikEJ.Data.Entity.SqlServerCe.Query;
using ErikEJ.Data.Entity.SqlServerCe.Update;
using ErikEJ.Data.Entity.SqlServerCe.ValueGeneration;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Framework.DependencyInjection
{
    public static class SqlServerCeEntityFrameworkServicesBuilderExtensions
    {
        public static EntityFrameworkServicesBuilder AddSqlServerCe([NotNull] this EntityFrameworkServicesBuilder services)
        {
            Check.NotNull(services, nameof(services));

            ((IAccessor<IServiceCollection>)services.AddRelational()).Service
                .AddSingleton<IDataStoreSource, SqlServerCeDataStoreSource>()
                .TryAdd(new ServiceCollection()
                    .AddSingleton<SqlServerCeModelBuilderFactory>()
                    .AddSingleton<SqlServerCeValueGeneratorCache>()
                    .AddSingleton<SqlServerCeSqlGenerator>()
                    .AddSingleton<SqlStatementExecutor>()
                    .AddSingleton<SqlServerCeTypeMapper>()
                    .AddSingleton<SqlServerCeModificationCommandBatchFactory>()
                    .AddSingleton<SqlServerCeModelSource>()
                    .AddScoped<SqlServerCeDataStoreServices>()
                    .AddScoped<SqlServerCeDataStore>()
                    .AddScoped<SqlServerCeDataStoreConnection>()
                    .AddScoped<SqlServerCeModelDiffer>()
                    .AddScoped<SqlServerCeMigrationSqlGenerator>()
                    .AddScoped<SqlServerCeDataStoreCreator>()
                    .AddScoped<SqlServerCeHistoryRepository>()
                    );

            return services;
        }
    }
}
