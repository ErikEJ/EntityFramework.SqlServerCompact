using EFCore.SqlCe.Infrastructure.Internal;
using EFCore.SqlCe.Internal;
using EFCore.SqlCe.Metadata.Conventions.Internal;
using EFCore.SqlCe.Migrations.Internal;
using EFCore.SqlCe.Query.ExpressionTranslators.Internal;
using EFCore.SqlCe.Query.ExpressionVisitors;
using EFCore.SqlCe.Query.Internal;
using EFCore.SqlCe.Query.Migrations;
using EFCore.SqlCe.Query.Sql.Internal;
using EFCore.SqlCe.Storage.Internal;
using EFCore.SqlCe.Update.Internal;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
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

            var builder = new EntityFrameworkRelationalServicesBuilder(serviceCollection)
                .TryAdd<IDatabaseProvider, DatabaseProvider<SqlCeOptionsExtension>>()
                .TryAdd<IRelationalTypeMappingSource, SqlCeTypeMappingSource>()
                .TryAdd<ISqlGenerationHelper, SqlCeSqlGenerationHelper>()
                .TryAdd<IMigrationsAnnotationProvider, SqlCeMigrationsAnnotationProvider>()
                //.TryAdd<IRelationalValueBufferFactoryFactory, UntypedRelationalValueBufferFactoryFactory>()
                .TryAdd<IModelValidator, SqlCeModelValidator>()
                .TryAdd<IConventionSetBuilder, SqlCeConventionSetBuilder>()
                .TryAdd<IUpdateSqlGenerator>(p => p.GetService<ISqlCeUpdateSqlGenerator>())
                .TryAdd<IModificationCommandBatchFactory, SqlCeModificationCommandBatchFactory>()
                .TryAdd<IRelationalConnection>(p => p.GetService<ISqlCeDatabaseConnection>())
                .TryAdd<IMigrationsSqlGenerator, SqlCeMigrationsSqlGenerator>()
                .TryAdd<IRelationalDatabaseCreator, SqlCeDatabaseCreator>()
                .TryAdd<IHistoryRepository, SqlCeHistoryRepository>()
                .TryAdd<IQueryCompilationContextFactory, SqlCeQueryCompilationContextFactory>()
                .TryAdd<IMemberTranslator, SqlCeCompositeMemberTranslator>()
                .TryAdd<ICompositeMethodCallTranslator, SqlCeCompositeMethodCallTranslator>()
                .TryAdd<IQuerySqlGeneratorFactory, SqlCeQuerySqlGeneratorFactory>()
                .TryAdd<ISqlTranslatingExpressionVisitorFactory, SqlCeTranslatingExpressionVisitorFactory>()
                .TryAdd<ISingletonOptions, ISqlCeOptions>(p => p.GetService<ISqlCeOptions>())
                .TryAddProviderSpecificServices(b => b
                    .TryAddSingleton<ISqlCeOptions, SqlCeOptions>()
                    .TryAddScoped<ISqlCeUpdateSqlGenerator, SqlCeUpdateSqlGenerator>()
                    .TryAddScoped<ISqlCeDatabaseConnection, SqlCeDatabaseConnection>());

            builder.TryAddCoreServices();

            return serviceCollection;
        }
    }
}
