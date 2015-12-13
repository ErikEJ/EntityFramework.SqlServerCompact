using System;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Update;
using Microsoft.Data.Entity.ValueGeneration;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Internal;
using Microsoft.Data.Entity.Query;
using Microsoft.Data.Entity.Query.ExpressionTranslators;
using Microsoft.Data.Entity.Query.ExpressionTranslators.Internal;
using Microsoft.Data.Entity.Query.Internal;
using Microsoft.Data.Entity.Query.Sql;
using Microsoft.Data.Entity.Query.Sql.Internal;
using Microsoft.Data.Entity.Update.Internal;
using Microsoft.Data.Entity.ValueGeneration.Internal;

namespace Microsoft.Data.Entity.Storage.Internal
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
        public override IQuerySqlGeneratorFactory QuerySqlGeneratorFactory => GetService<SqlCeQuerySqlGeneratorFactory>();
    }
}
