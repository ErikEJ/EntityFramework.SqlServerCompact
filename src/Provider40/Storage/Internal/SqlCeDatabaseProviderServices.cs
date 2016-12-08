using System;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.ValueGeneration;
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
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace Microsoft.EntityFrameworkCore.Storage.Internal
{
    public class SqlCeDatabaseProviderServices : RelationalDatabaseProviderServices
    {
        public SqlCeDatabaseProviderServices([NotNull] IServiceProvider services)
            : base(services)
        {
        }

        public override string InvariantName => GetType().GetTypeInfo().Assembly.GetName().Name;
        public override IDatabaseCreator Creator => GetService<SqlCeDatabaseCreator>();
        public override IHistoryRepository HistoryRepository => GetService<SqlCeHistoryRepository>();
        public override ISqlGenerationHelper SqlGenerationHelper => GetService<SqlCeSqlGenerationHelper>();
        public override IMigrationsSqlGenerator MigrationsSqlGenerator => GetService<SqlCeMigrationsSqlGenerator>();
        public override IMigrationsAnnotationProvider MigrationsAnnotationProvider => GetService<SqlCeMigrationsAnnotationProvider>();
        public override IModelSource ModelSource => GetService<SqlCeModelSource>();
        public override IRelationalConnection RelationalConnection => GetService<ISqlCeDatabaseConnection>();
        public override IUpdateSqlGenerator UpdateSqlGenerator => GetService<ISqlCeUpdateSqlGenerator>();
        public override IValueGeneratorCache ValueGeneratorCache => GetService<SqlCeValueGeneratorCache>();
        public override IRelationalTypeMapper TypeMapper => GetService<SqlCeTypeMapper>();
        public override IModificationCommandBatchFactory ModificationCommandBatchFactory => GetService<SqlCeModificationCommandBatchFactory>();
        public override IRelationalDatabaseCreator RelationalDatabaseCreator => GetService<SqlCeDatabaseCreator>();
        public override IConventionSetBuilder ConventionSetBuilder => GetService<SqlCeConventionSetBuilder>();
        public override IRelationalAnnotationProvider AnnotationProvider => GetService<SqlCeAnnotationProvider>();
        public override IMethodCallTranslator CompositeMethodCallTranslator => GetService<SqlCeCompositeMethodCallTranslator>();
        public override IMemberTranslator CompositeMemberTranslator => GetService<SqlCeCompositeMemberTranslator>();
        public override IQueryCompilationContextFactory QueryCompilationContextFactory => GetService<SqlCeQueryCompilationContextFactory>();
        public override IQuerySqlGeneratorFactory QuerySqlGeneratorFactory => GetService<SqlCeQuerySqlGeneratorFactory>();
        public override ISqlTranslatingExpressionVisitorFactory SqlTranslatingExpressionVisitorFactory => GetService<SqlCeTranslatingExpressionVisitorFactory>();
    }
}
