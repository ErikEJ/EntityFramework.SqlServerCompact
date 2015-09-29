using System.Collections.Generic;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.MetaData.Conventions.Internal;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Internal;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Storage.Internal;
using Microsoft.Data.Entity.Tests;
using Microsoft.Data.Entity.Update;
using Microsoft.Data.Entity.Update.Internal;
using Microsoft.Data.Entity.ValueGeneration;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.Tests.Extensions
{
    public class SqlCeEntityFrameworkServicesBuilderExtensionsTest : RelationalEntityFrameworkServicesBuilderExtensionsTest
    {
        [Fact]
        public override void Services_wire_up_correctly()
        {
            base.Services_wire_up_correctly();

            // Relational
            VerifySingleton<IComparer<ModificationCommand>>();

            // SQL Server Ce dingletones
            VerifySingleton<SqlCeConventionSetBuilder>();
            VerifySingleton<SqlCeValueGeneratorCache>();
            
            VerifySingleton<SqlCeTypeMapper>();            
            VerifySingleton<SqlCeModelSource>();

            // SQL Server Ce scoped
            VerifyScoped<ISqlCeUpdateSqlGenerator>();
            VerifyScoped<SqlCeModificationCommandBatchFactory>();
            VerifyScoped<SqlCeDatabaseProviderServices>();
            VerifyScoped<ISqlCeDatabaseConnection>();
            VerifyScoped<SqlCeMigrationsAnnotationProvider>();
            VerifyScoped<SqlCeMigrationsSqlGenerator>();
            VerifyScoped<SqlCeDatabaseCreator>();

            // Migrations
            VerifyScoped<IMigrationsAssembly>();
            VerifyScoped<SqlCeHistoryRepository>();
            VerifyScoped<IMigrator>();
            VerifySingleton<IMigrationsIdGenerator>();
            VerifyScoped<IMigrationsModelDiffer>();
            VerifyScoped<SqlCeMigrationsSqlGenerator>();
        }

        public SqlCeEntityFrameworkServicesBuilderExtensionsTest()
            : base(SqlCeTestHelpers.Instance)
        {
        }
    }
}
