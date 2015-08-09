using System;
using System.Collections.Generic;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Migrations.History;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using Microsoft.Data.Entity.SqlServerCompact;
using Microsoft.Data.Entity.SqlServerCompact.Metadata;
using Microsoft.Data.Entity.SqlServerCompact.Migrations;
using Microsoft.Data.Entity.Storage;
using Moq;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.Migrations
{
    public class SqlCeHistoryRepositoryTest
    {
        private static string EOL => Environment.NewLine;

        [Fact]
        public void GetCreateScript_works()
        {
            var sql = CreateHistoryRepository().GetCreateScript();

            Assert.Equal(
                "CREATE TABLE [__MigrationHistory] (" + EOL +
                "    [MigrationId] nvarchar(150) NOT NULL," + EOL +
                "    [ProductVersion] nvarchar(32)," + EOL +
                "    CONSTRAINT [PK_HistoryRow] PRIMARY KEY ([MigrationId])" + EOL +
                ")",
                sql);
        }

        [Fact]
        public void GetCreateIfNotExistsScript_works()
        {
            Assert.Throws<NotSupportedException>(() => CreateHistoryRepository().GetCreateIfNotExistsScript());
        }

        [Fact]
        public void GetDeleteScript_works()
        {
            var sql = CreateHistoryRepository().GetDeleteScript("Migration1");

            Assert.Equal(
                "DELETE FROM [__MigrationHistory]" + EOL +
                "WHERE [MigrationId] = 'Migration1';",
                sql);
        }

        [Fact]
        public void GetInsertScript_works()
        {
            var sql = CreateHistoryRepository().GetInsertScript(
                new HistoryRow("Migration1", "7.0.0"));

            Assert.Equal(
                "INSERT INTO [__MigrationHistory] ([MigrationId], [ProductVersion])" + EOL +
                "VALUES ('Migration1', '7.0.0');",
                sql);
        }

        [Fact]
        public void GetBeginIfNotExistsScript_works()
        {
            Assert.Throws<NotSupportedException>(() => CreateHistoryRepository().GetBeginIfNotExistsScript("Migration1"));
        }

        [Fact]
        public void GetBeginIfExistsScript_works()
        {
            Assert.Throws<NotSupportedException>(() => CreateHistoryRepository().GetBeginIfExistsScript("Migration1"));
        }

        [Fact]
        public void GetEndIfScript_works()
        {
            Assert.Throws<NotSupportedException>(() => CreateHistoryRepository().GetEndIfScript());
        }

        private static IHistoryRepository CreateHistoryRepository()
        {
            var annotationsProvider = new SqlCeMetadataExtensionProvider();
            var updateSqlGenerator = new SqlCeUpdateSqlGenerator();

            return new SqlCeHistoryRepository(
                Mock.Of<IRelationalDatabaseCreator>(),
                Mock.Of<ISqlStatementExecutor>(),
                Mock.Of<IRelationalConnection>(),
                new MigrationModelFactory(),
                new DbContextOptions<DbContext>(
                    new Dictionary<Type, IDbContextOptionsExtension>
                    {
                        { typeof(SqlCeOptionsExtension), new SqlCeOptionsExtension() }
                    }),
                new ModelDiffer(
                    annotationsProvider,
                    new SqlCeMigrationAnnotationProvider()),
                new SqlCeMigrationSqlGenerator(
                    updateSqlGenerator,
                    new SqlCeTypeMapper(),
                    annotationsProvider),
                annotationsProvider,
                updateSqlGenerator,
                Mock.Of<IServiceProvider>());
        }

        private class Context : DbContext
        {
        }
    }
}
