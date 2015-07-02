using System;
using System.Collections.Generic;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.History;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using Microsoft.Data.Entity.Migrations.Sql;
using Microsoft.Data.Entity.SqlServerCompact;
using Microsoft.Data.Entity.SqlServerCompact.Migrations;
using Microsoft.Data.Entity.SqlServerCompact.Update;
using Microsoft.Data.Entity.SqlServerCompact.ValueGeneration;
using Microsoft.Data.Entity.Tests;
using Microsoft.Data.Entity.Update;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.Tests.Extensions
{
    public class SqlCeEntityFrameworkServicesBuilderExtensionsTest : RelationalEntityServicesBuilderExtensionsTest
    {
        [Fact]
        public override void Services_wire_up_correctly()
        {
            base.Services_wire_up_correctly();

            // Relational
            VerifySingleton<IComparer<ModificationCommand>>();

            // SQL Server Ce dingletones
            VerifySingleton<SqlCeValueGeneratorCache>();
            VerifySingleton<SqlCeUpdateSqlGenerator>();
            VerifySingleton<ISqlStatementExecutor>();
            VerifySingleton<SqlCeTypeMapper>();
            
            VerifySingleton<SqlCeModelSource>();

            // SQL Server Ce scoped
            VerifyScoped<SqlCeModificationCommandBatchFactory>();
            VerifyScoped<SqlCeDatabaseProviderServices>();
            VerifyScoped<SqlCeDatabase>();
            VerifyScoped<SqlCeDatabaseConnection>();
            VerifyScoped<SqlCeMigrationAnnotationProvider>();
            VerifyScoped<SqlCeMigrationSqlGenerator>();
            VerifyScoped<SqlCeDatabaseCreator>();

            // Migrations
            VerifyScoped<IMigrationAssembly>();
            VerifyScoped<SqlCeHistoryRepository>();
            VerifyScoped<IMigrator>();
            VerifySingleton<IMigrationIdGenerator>();
            VerifyScoped<IModelDiffer>();
            VerifyScoped<SqlCeMigrationSqlGenerator>();
        }

        protected override IServiceCollection GetServices(IServiceCollection services = null)
        {
            return (services ?? new ServiceCollection())
                .AddEntityFramework()
                .AddSqlCe()
                .ServiceCollection();
        }

        protected override DbContext CreateContext(IServiceProvider serviceProvider)
        {
            return SqlCeTestHelpers.Instance.CreateContext(serviceProvider);
        }
    }
}
