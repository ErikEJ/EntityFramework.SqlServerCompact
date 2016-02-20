using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Query.Sql.Internal;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Tests.Extensions
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
            VerifySingleton<SqlCeValueGeneratorCache>();           
            VerifySingleton<SqlCeTypeMapper>();            
            VerifySingleton<SqlCeModelSource>();
            VerifySingleton<SqlCeSqlGenerationHelper>();
            VerifySingleton<SqlCeAnnotationProvider>();
            VerifySingleton<SqlCeMigrationsAnnotationProvider>();

            // SQL Server Ce scoped
            VerifyScoped<SqlCeConventionSetBuilder>();
            VerifyScoped<ISqlCeUpdateSqlGenerator>();
            VerifyScoped<SqlCeModificationCommandBatchFactory>();
            VerifyScoped<SqlCeDatabaseProviderServices>();
            VerifyScoped<ISqlCeDatabaseConnection>();
            VerifyScoped<SqlCeDatabaseCreator>();

            //Query
            VerifyScoped<SqlCeQueryCompilationContextFactory>();
            VerifyScoped<SqlCeCompositeMemberTranslator>();
            VerifyScoped<SqlCeCompositeMethodCallTranslator>();
            VerifyScoped<SqlCeQuerySqlGeneratorFactory>();

            // Migrations
            VerifyScoped<IMigrationsAssembly>();
            VerifyScoped<SqlCeMigrationsAnnotationProvider>();
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
