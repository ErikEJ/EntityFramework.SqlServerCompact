using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.SqlServerCompact;
using Microsoft.Data.Entity.SqlServerCompact.MetaData;
using Microsoft.Data.Entity.SqlServerCompact.Migrations;
using Microsoft.Data.Entity.SqlServerCompact.Update;
using Microsoft.Data.Entity.SqlServerCompact.ValueGeneration;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;

// ReSharper disable CheckNamespace

namespace Microsoft.Framework.DependencyInjection
{
    public static class SqlCeEntityFrameworkServicesBuilderExtensions
    {
        public static EntityFrameworkServicesBuilder AddSqlCe([NotNull] this EntityFrameworkServicesBuilder services)
        {
            Check.NotNull(services, nameof(services));

            ((IAccessor<IServiceCollection>)services.AddRelational()).Service
                .AddSingleton<IDatabaseProvider, DatabaseProvider<SqlCeDatabaseProviderServices, SqlCeOptionsExtension>>()
                .TryAdd(new ServiceCollection()
                    .AddSingleton<SqlCeValueGeneratorCache>()
                    .AddSingleton<SqlCeUpdateSqlGenerator>()
                    .AddSingleton<SqlCeMetadataExtensionProvider>()
                    .AddSingleton<SqlCeTypeMapper>()
                    .AddSingleton<SqlCeModelSource>()
                    .AddSingleton<SqlCeMigrationAnnotationProvider>()
                    .AddScoped<SqlCeModificationCommandBatchFactory>()
                    .AddScoped<SqlCeDatabaseProviderServices>()
                    .AddScoped<SqlCeDatabase>()
                    .AddScoped<SqlCeDatabaseConnection>()
                    .AddScoped<SqlCeMigrationSqlGenerator>()
                    .AddScoped<SqlCeDatabaseCreator>()
                    .AddScoped<SqlCeHistoryRepository>()
                    .AddScoped<SqlCeCompositeMethodCallTranslator>()
                    .AddScoped<SqlCeCompositeMemberTranslator>()
                    );

            return services;
        }
    }
}
