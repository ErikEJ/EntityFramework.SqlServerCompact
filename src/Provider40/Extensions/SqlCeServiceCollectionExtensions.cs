using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
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
    public static class SqlCeServiceCollectionExtensions
    {
        /// <summary>
        ///     <para>
        ///         Adds the services required by the Microsoft SQL Server Compact database provider for Entity Framework
        ///         to an <see cref="IServiceCollection" />. You use this method when using dependency injection
        ///         in your application, such as with ASP.NET. For more information on setting up dependency
        ///         injection, see http://go.microsoft.com/fwlink/?LinkId=526890.
        ///     </para>
        ///     <para>
        ///         You only need to use this functionality when you want Entity Framework to resolve the services it uses
        ///         from an external <see cref="IServiceCollection" />. If you are not using an external
        ///         <see cref="IServiceCollection" /> Entity Framework will take care of creating the services it requires.
        ///     </para>
        /// </summary>
        /// <example>
        ///     <code>
        ///         public void ConfigureServices(IServiceCollection services) 
        ///         {
        ///             var connectionString = "connection string to database";
        /// 
        ///             services
        ///                 .AddEntityFrameworkSqlCe() 
        ///                 .AddDbContext&lt;MyContext&gt;(options => options.UseSqlCe(connectionString)); 
        ///         }
        ///     </code>
        /// </example>
        /// <param name="services"> The <see cref="IServiceCollection" /> to add services to. </param>
        /// <returns>
        ///     A builder that allows further Entity Framework specific setup of the <see cref="IServiceCollection" />.
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
                .AddScoped<SqlCeQuerySqlGeneratorFactory>();
    }
}
