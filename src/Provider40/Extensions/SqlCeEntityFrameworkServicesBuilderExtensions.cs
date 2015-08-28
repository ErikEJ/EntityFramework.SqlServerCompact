using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.SqlServerCompact;
using Microsoft.Data.Entity.SqlServerCompact.Metadata;
using Microsoft.Data.Entity.SqlServerCompact.Migrations;
using Microsoft.Data.Entity.SqlServerCompact.Query.ExpressionTranslators;
using Microsoft.Data.Entity.SqlServerCompact.Update;
using Microsoft.Data.Entity.SqlServerCompact.ValueGeneration;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.DependencyInjection.Extensions;

// ReSharper disable CheckNamespace

namespace Microsoft.Framework.DependencyInjection
{
    public static class SqlCeEntityFrameworkServicesBuilderExtensions
    {
        public static EntityFrameworkServicesBuilder AddSqlCe([NotNull] this EntityFrameworkServicesBuilder services)
        {
            Check.NotNull(services, nameof(services));

            var service = services.AddRelational().GetService();

            service.TryAddEnumerable(ServiceDescriptor
                .Singleton<IDatabaseProvider, DatabaseProvider<SqlCeDatabaseProviderServices, SqlCeOptionsExtension>>());

            service.TryAdd(new ServiceCollection()
                .AddSingleton<SqlCeValueGeneratorCache>()
                .AddSingleton<SqlCeUpdateSqlGenerator>()
                .AddSingleton<SqlCeMetadataExtensionProvider>()
                .AddSingleton<SqlCeTypeMapper>()
                .AddSingleton<SqlCeModelSource>()
                .AddSingleton<SqlCeMigrationsAnnotationProvider>()
                .AddSingleton<SqlCeConventionSetBuilder>()
                .AddScoped<SqlCeModificationCommandBatchFactory>()
                .AddScoped<SqlCeDatabaseProviderServices>()
                .AddScoped<SqlCeDatabase>()
                .AddScoped<SqlCeDatabaseConnection>()
                .AddScoped<SqlCeMigrationsSqlGenerator>()
                .AddScoped<SqlCeDatabaseCreator>()
                .AddScoped<SqlCeHistoryRepository>()
                .AddScoped<SqlCeCompositeMethodCallTranslator>()
                .AddScoped<SqlCeCompositeMemberTranslator>()
                .AddScoped<SqlCeCompositeExpressionFragmentTranslator>()
                );

            return services;
        }
    }
}
