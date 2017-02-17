using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.Query.Sql.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.EntityFrameworkCore.Utilities;

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
        /// <param name="serviceCollection"> The <see cref="IServiceCollection" /> to add services to. </param>
        /// <returns>
        ///     The same service collection so that multiple calls can be chained.
        /// </returns>
        public static IServiceCollection AddEntityFrameworkSqlCe([NotNull] this IServiceCollection serviceCollection)
        {
            Check.NotNull(serviceCollection, nameof(serviceCollection));

            var serviceCollectionMap = new ServiceCollectionMap(serviceCollection)
                .TryAddSingletonEnumerable<IDatabaseProvider, DatabaseProvider<SqlCeOptionsExtension>>()
                .TryAddSingleton<IRelationalTypeMapper, SqlCeTypeMapper>()
                .TryAddSingleton<ISqlGenerationHelper, SqlCeSqlGenerationHelper>()
                .TryAddSingleton<IRelationalAnnotationProvider, SqlCeAnnotationProvider>()
                .TryAddSingleton<IMigrationsAnnotationProvider, SqlCeMigrationsAnnotationProvider>()
                .TryAddScoped<IRelationalValueBufferFactoryFactory, UntypedRelationalValueBufferFactoryFactory>()
                .TryAddScoped<IConventionSetBuilder, SqlCeConventionSetBuilder>()
                .TryAddScoped<ISqlCeUpdateSqlGenerator, SqlCeUpdateSqlGenerator>()
                .TryAddScoped<IUpdateSqlGenerator>(p => p.GetService<ISqlCeUpdateSqlGenerator>())
                .TryAddScoped<IModificationCommandBatchFactory, SqlCeModificationCommandBatchFactory>()
                .TryAddScoped<ISqlCeDatabaseConnection, SqlCeDatabaseConnection>()
                .TryAddScoped<IRelationalConnection>(p => p.GetService<ISqlCeDatabaseConnection>())
                .TryAddScoped<IHistoryRepository, SqlCeHistoryRepository>()
                .TryAddScoped<IMigrationsSqlGenerator, SqlCeMigrationsSqlGenerator>()
                .TryAddScoped<IRelationalDatabaseCreator, SqlCeDatabaseCreator>()
                .TryAddScoped<IQueryCompilationContextFactory, SqlCeQueryCompilationContextFactory>()
                .TryAddScoped<IMemberTranslator, SqlCeCompositeMemberTranslator>()
                .TryAddScoped<IMethodCallTranslator, SqlCeCompositeMethodCallTranslator>()
                .TryAddScoped<ISqlTranslatingExpressionVisitorFactory, SqlCeTranslatingExpressionVisitorFactory>()
                .TryAddScoped<IQuerySqlGeneratorFactory, SqlCeQuerySqlGeneratorFactory>();

            ServiceCollectionRelationalProviderInfrastructure.TryAddDefaultRelationalServices(serviceCollectionMap);

            return serviceCollection;
        }
    }
}
