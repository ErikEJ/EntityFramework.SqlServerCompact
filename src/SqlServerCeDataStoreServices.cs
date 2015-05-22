using System;
using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using ErikEJ.Data.Entity.SqlServerCe.Migrations;
using ErikEJ.Data.Entity.SqlServerCe.Update;
using ErikEJ.Data.Entity.SqlServerCe.ValueGeneration;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Metadata;
using Microsoft.Data.Entity.Relational.Migrations.History;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using Microsoft.Data.Entity.Relational.Migrations.Sql;
using Microsoft.Data.Entity.Relational.Update;
using Microsoft.Data.Entity.Sqlite.Metadata;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.ValueGeneration;

namespace ErikEJ.Data.Entity.SqlServerCe
{
    public class SqlServerCeDataStoreServices : RelationalDataStoreServices
    {
        public SqlServerCeDataStoreServices([NotNull] IServiceProvider services)
            : base(services)
        {
        }

        public override IDataStoreConnection Connection => GetService<SqlServerCeDataStoreConnection>();
        public override IDataStoreCreator Creator => GetService<SqlServerCeDataStoreCreator>();
        public override IHistoryRepository HistoryRepository => GetService<SqlServerCeHistoryRepository>();
        public override IMigrationSqlGenerator MigrationSqlGenerator => GetService<SqlServerCeMigrationSqlGenerator>();
        public override IModelBuilderFactory ModelBuilderFactory => GetService<SqlServerCeModelBuilderFactory>();
        public override IModelDiffer ModelDiffer => GetService<SqlServerCeModelDiffer>();
        public override IModelSource ModelSource => GetService<SqlServerCeModelSource>();
        public override IRelationalConnection RelationalConnection => GetService<SqlServerCeDataStoreConnection>();
        public override ISqlGenerator SqlGenerator => GetService<SqlServerCeSqlGenerator>();
        public override IDataStore Store => GetService<SqlServerCeDataStore>();
        public override IRelationalDataStoreCreator RelationalDataStoreCreator => GetService<SqlServerCeDataStoreCreator>();
        public override IValueGeneratorCache ValueGeneratorCache => GetService<SqlServerCeValueGeneratorCache>();
        public override IModificationCommandBatchFactory ModificationCommandBatchFactory => GetService<SqlServerCeModificationCommandBatchFactory>();
        public override IRelationalMetadataExtensionProvider MetadataExtensionProvider => GetService<SqlServerCeMetadataExtensionProvider>();
    }
}
