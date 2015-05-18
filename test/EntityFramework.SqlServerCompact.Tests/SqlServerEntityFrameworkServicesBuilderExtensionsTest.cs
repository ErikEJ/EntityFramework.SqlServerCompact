using System;
using System.Collections.Generic;
using ErikEJ.Data.Entity.SqlServerCe;
using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using ErikEJ.Data.Entity.SqlServerCe.Migrations;
using ErikEJ.Data.Entity.SqlServerCe.Query;
using ErikEJ.Data.Entity.SqlServerCe.Update;
using ErikEJ.Data.Entity.SqlServerCe.ValueGeneration;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.History;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using Microsoft.Data.Entity.Relational.Migrations.Sql;
using Microsoft.Data.Entity.Relational.Update;
using Microsoft.Data.Entity.Tests;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace Microsoft.Data.Entity.SqlServer.Tests
{
    public class SqlServerCeEntityFrameworkServicesBuilderExtensionsTest : EntityFrameworkServiceCollectionExtensionsTest
    {
        [Fact]
        public override void Services_wire_up_correctly()
        {
            base.Services_wire_up_correctly();

            // Relational
            VerifySingleton<IComparer<ModificationCommand>>();

            // SQL Server Ce dingletones
            VerifySingleton<ISqlServerCeModelBuilderFactory>();
            VerifySingleton<ISqlServerCeValueGeneratorCache>();
            VerifySingleton<ISqlServerCeSqlGenerator>();
            VerifySingleton<ISqlStatementExecutor>();
            VerifySingleton<ISqlServerCeTypeMapper>();
            VerifySingleton<ISqlServerCeModificationCommandBatchFactory>();
            VerifySingleton<ISqlServerCeCommandBatchPreparer>();
            VerifySingleton<ISqlServerCeModelSource>();

            // SQL Server Ce scoped
            VerifyScoped<ISqlServerCeQueryContextFactory>();
            VerifyScoped<ISqlServerCeValueGeneratorSelector>();
            VerifyScoped<ISqlServerCeBatchExecutor>();
            VerifyScoped<ISqlServerCeDataStoreServices>();
            VerifyScoped<ISqlServerCeDataStore>();
            VerifyScoped<ISqlServerCeConnection>();
            VerifyScoped<ISqlServerCeModelDiffer>();
            VerifyScoped<ISqlServerCeDatabaseFactory>();
            VerifyScoped<ISqlServerCeMigrationSqlGenerator>();
            VerifyScoped<ISqlServerCeDataStoreCreator>();
            VerifyScoped<ISqlServerCeHistoryRepository>();

            VerifyCommonDataStoreServices();

            // Migrations
            VerifyScoped<IMigrationAssembly>();
            VerifyScoped<IHistoryRepository>();
            VerifyScoped<IMigrator>();
            VerifySingleton<IMigrationIdGenerator>();
            VerifyScoped<IModelDiffer>();
            VerifyScoped<IMigrationSqlGenerator>();
        }

        protected override IServiceCollection GetServices(IServiceCollection services = null)
        {
            return (services ?? new ServiceCollection())
                .AddEntityFramework()
                .AddSqlServerCe()
                .ServiceCollection();
        }

        protected override DbContextOptions GetOptions()
        {
            return SqlServerCeTestHelpers.Instance.CreateOptions();
        }

        protected override DbContext CreateContext(IServiceProvider serviceProvider)
        {
            return SqlServerCeTestHelpers.Instance.CreateContext(serviceProvider);
        }
    }
}
