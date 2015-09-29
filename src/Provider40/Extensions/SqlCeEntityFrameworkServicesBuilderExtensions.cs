using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata.Internal;
using Microsoft.Data.Entity.MetaData.Conventions.Internal;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Internal;
using Microsoft.Data.Entity.Query.ExpressionTranslators;
using Microsoft.Data.Entity.Query.Internal;
using Microsoft.Data.Entity.Query.Sql;
using Microsoft.Data.Entity.Query.Sql.Internal;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Storage.Internal;
using Microsoft.Data.Entity.Update.Internal;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Data.Entity.ValueGeneration;
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
                .AddSingleton<SqlCeConventionSetBuilder>()
                .AddSingleton<SqlCeValueGeneratorCache>()                
                .AddSingleton<SqlCeTypeMapper>()
                .AddSingleton<SqlCeSqlGenerator>()
                .AddSingleton<SqlCeModelSource>()
                .AddSingleton<SqlCeAnnotationProvider>()
                .AddSingleton<SqlCeMigrationsAnnotationProvider>()                
                .AddScoped<ISqlCeUpdateSqlGenerator, SqlCeUpdateSqlGenerator>()
                .AddScoped<SqlCeModificationCommandBatchFactory>()
                .AddScoped<SqlCeDatabaseProviderServices>()
                .AddScoped<ISqlCeDatabaseConnection, SqlCeDatabaseConnection>()
                .AddScoped<SqlCeMigrationsSqlGenerator>()
                .AddScoped<SqlCeDatabaseCreator>()                
                .AddScoped<SqlCeHistoryRepository>()
                .AddQuery()
                );

            return services;
        }

        private static IServiceCollection AddQuery(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddScoped<SqlCeQueryCompilationContextFactory>()
                .AddScoped<SqlCeCompositeExpressionFragmentTranslator>()
                .AddScoped<SqlCeCompositeMemberTranslator>()
                .AddScoped<SqlCeCompositeMethodCallTranslator>()
                .AddScoped<SqlCeQuerySqlGeneratorFactory>();
        }
    }
}
