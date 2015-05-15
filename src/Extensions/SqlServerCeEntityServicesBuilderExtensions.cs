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
                    .AddSingleton<ISqlServerCeModelBuilderFactory, SqlServerCeModelBuilderFactory>()
                    .AddSingleton<ISqlServerCeValueGeneratorCache, SqlServerCeValueGeneratorCache>()
                    .AddSingleton<ISqlServerCeSqlGenerator, SqlServerCeSqlGenerator>()
                    .AddSingleton<ISqlStatementExecutor, SqlStatementExecutor>()
                    .AddSingleton<ISqlServerCeTypeMapper, SqlServerCeTypeMapper>()
                    .AddSingleton<ISqlServerCeModificationCommandBatchFactory, SqlServerCeModificationCommandBatchFactory>()
                    .AddSingleton<ISqlServerCeCommandBatchPreparer, SqlServerCeCommandBatchPreparer>()
                    .AddSingleton<ISqlServerCeModelSource, SqlServerCeModelSource>()
                    .AddSingleton<ISqlServerCeValueBufferFactoryFactory, SqlServerCeValueBufferFactoryFactory>()
                    .AddScoped<ISqlServerCeQueryContextFactory, SqlServerCeQueryContextFactory>()
                    .AddScoped<ISqlServerCeValueGeneratorSelector, SqlServerCeValueGeneratorSelector>()
                    .AddScoped<ISqlServerCeBatchExecutor, SqlServerCeBatchExecutor>()
                    .AddScoped<ISqlServerCeDataStoreServices, SqlServerCeDataStoreServices>()
                    .AddScoped<ISqlServerCeDataStore, SqlServerCeDataStore>()
                    .AddScoped<ISqlServerCeConnection, SqlServerCeDataStoreConnection>()
                    .AddScoped<ISqlServerCeModelDiffer, SqlServerCeModelDiffer>()
                    .AddScoped<ISqlServerCeDatabaseFactory, SqlServerCeDatabaseFactory>()
                    .AddScoped<ISqlServerCeMigrationSqlGenerator, SqlServerCeMigrationSqlGenerator>()
                    .AddScoped<ISqlServerCeDataStoreCreator, SqlServerCeDataStoreCreator>()
                    .AddScoped<ISqlServerCeHistoryRepository, SqlServerCeHistoryRepository>()
                    );

            return services;
        }
    }
}
