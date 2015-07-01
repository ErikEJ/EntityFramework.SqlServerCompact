﻿using System;
using System.Collections.Generic;
using ErikEJ.Data.Entity.SqlServerCe.Migrations;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.History;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using Microsoft.Data.Entity.Relational.Migrations.Sql;
using Microsoft.Data.Entity.Relational.Tests;
using Microsoft.Data.Entity.Relational.Update;
using Microsoft.Data.Entity.SqlServerCompact;
using Microsoft.Data.Entity.SqlServerCompact.Migrations;
using Microsoft.Data.Entity.SqlServerCompact.Update;
using Microsoft.Data.Entity.SqlServerCompact.ValueGeneration;
using Microsoft.Data.Entity.Tests;
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
            VerifySingleton<SqlCeSqlGenerator>();
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
            VerifyScoped<SqlCeHistoryRepository>();

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
                .AddSqlCe()
                .ServiceCollection();
        }

        protected override DbContext CreateContext(IServiceProvider serviceProvider)
        {
            return SqlCeTestHelpers.Instance.CreateContext(serviceProvider);
        }
    }
}