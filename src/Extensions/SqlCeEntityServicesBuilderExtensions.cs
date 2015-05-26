using ErikEJ.Data.Entity.SqlServerCe;
using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using ErikEJ.Data.Entity.SqlServerCe.Migrations;
using ErikEJ.Data.Entity.SqlServerCe.Update;
using ErikEJ.Data.Entity.SqlServerCe.ValueGeneration;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Framework.DependencyInjection
{
    public static class SqlCeEntityFrameworkServicesBuilderExtensions
    {
        public static EntityFrameworkServicesBuilder AddSqlCe([NotNull] this EntityFrameworkServicesBuilder services)
        {
            Check.NotNull(services, nameof(services));

            ((IAccessor<IServiceCollection>)services.AddRelational()).Service
                .AddSingleton<IDataStoreSource, SqlCeDataStoreSource>()
                .TryAdd(new ServiceCollection()
                    .AddSingleton<SqlCeModelBuilderFactory>()
                    .AddSingleton<SqlCeValueGeneratorCache>()
                    .AddSingleton<SqlCeSqlGenerator>()
                    .AddSingleton<SqlCeMetadataExtensionProvider>()
                    .AddSingleton<SqlCeTypeMapper>()
                    .AddSingleton<SqlCeModelSource>()
                    .AddScoped<SqlCeModificationCommandBatchFactory>()
                    .AddScoped<SqlCeDataStoreServices>()
                    .AddScoped<SqlCeDataStore>()
                    .AddScoped<SqlCeDataStoreConnection>()
                    .AddScoped<SqlCeModelDiffer>()
                    .AddScoped<SqlCeMigrationSqlGenerator>()
                    .AddScoped<SqlCeDataStoreCreator>()
                    .AddScoped<SqlCeHistoryRepository>()
                    );

            return services;
        }
    }
}
