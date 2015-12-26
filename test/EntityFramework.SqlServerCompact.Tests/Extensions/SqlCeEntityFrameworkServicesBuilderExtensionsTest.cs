using System.Collections.Generic;
using Microsoft.Data.Entity.Infrastructure.Internal;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Internal;
using Microsoft.Data.Entity.Query.ExpressionTranslators.Internal;
using Microsoft.Data.Entity.Query.Internal;
using Microsoft.Data.Entity.Query.Sql.Internal;
using Microsoft.Data.Entity.Storage.Internal;
using Microsoft.Data.Entity.Update;
using Microsoft.Data.Entity.Update.Internal;
using Microsoft.Data.Entity.ValueGeneration.Internal;
using Xunit;

namespace Microsoft.Data.Entity.Tests.Extensions
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
            VerifyScoped<SqlCeModelValidator>();

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
