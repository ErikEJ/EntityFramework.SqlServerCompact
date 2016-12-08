using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Query.Sql.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable CheckNamespace

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     SQL Server Compact specific extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class SqlCeServiceCollectionExtensions
    {
        /// <summary>
        ///     <para>
        ///         Adds the services required by the SQL Server Compact database provider for Entity Framework
        ///         to an <see cref="IServiceCollection" />. You use this method when using dependency injection
        ///         in your application, such as with ASP.NET. For more information on setting up dependency
        ///         injection, see http://go.microsoft.com/fwlink/?LinkId=526890.
        ///     </para>
        ///     <para>
        ///         You only need to use this functionality when you want Entity Framework to resolve the services it uses
        ///         from an external dependency injection container. If you are not using an external
        ///         dependency injection container, Entity Framework will take care of creating the services it requires.
        ///     </para>
        /// </summary>
        /// <example>
        ///     <code>
        ///         public void ConfigureServices(IServiceCollection services)
        ///         {
        ///             var connectionString = "connection string to database";
        ///
        ///             services
        ///                 .AddEntityFrameworkSqlServer()
        ///                 .AddDbContext&lt;MyContext&gt;((serviceProvider, options) =>
        ///                     options.UseSqlServer(connectionString)
        ///                            .UseInternalServiceProvider(serviceProvider));
        ///         }
        ///     </code>
        /// </example>
        /// <param name="services"> The <see cref="IServiceCollection" /> to add services to. </param>
        /// <returns>
        ///     The same service collection so that multiple calls can be chained.
        /// </returns>
        public static IServiceCollection AddEntityFrameworkSqlCe([NotNull] this IServiceCollection services)
        {
            Check.NotNull(services, nameof(services));

            services.AddRelational();

            services.TryAddEnumerable(ServiceDescriptor
                .Singleton<IDatabaseProvider, DatabaseProvider<SqlCeDatabaseProviderServices, SqlCeOptionsExtension>>());

            services.TryAdd(new ServiceCollection()
                .AddSingleton<SqlCeValueGeneratorCache>()                
                .AddSingleton<SqlCeTypeMapper>()
                .AddSingleton<SqlCeSqlGenerationHelper>()
                .AddSingleton<SqlCeModelSource>()
                .AddSingleton<SqlCeAnnotationProvider>()
                .AddSingleton<SqlCeMigrationsAnnotationProvider>()
                .AddScoped<SqlCeConventionSetBuilder>()
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
            => serviceCollection
                .AddScoped<SqlCeQueryCompilationContextFactory>()
                .AddScoped<SqlCeCompositeMemberTranslator>()
                .AddScoped<SqlCeCompositeMethodCallTranslator>()
                .AddScoped<SqlCeTranslatingExpressionVisitorFactory>()
                .AddScoped<SqlCeQuerySqlGeneratorFactory>();
    }
}
