using System;
using System.Collections.Generic;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata.Internal;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Internal;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Storage.Internal;
using Moq;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.Tests.Migrations
{
    public class SqlCeHistoryRepositoryTest
    {
        private static string EOL => Environment.NewLine;

        [Fact]
        public void GetCreateScript_works()
        {
            var sql = CreateHistoryRepository().GetCreateScript();

            Assert.Equal(
                "CREATE TABLE [__EFMigrationsHistory] (" + EOL +
                "    [MigrationId] nvarchar(150) NOT NULL," + EOL +
                "    [ProductVersion] nvarchar(32) NOT NULL," + EOL +
                "    CONSTRAINT [PK_HistoryRow] PRIMARY KEY ([MigrationId])" + EOL +
                ");"+ EOL,
                sql);
        }

        [Fact]
        public void GetCreateIfNotExistsScript_works()
        {
            var sql = CreateHistoryRepository().GetCreateIfNotExistsScript();

            Assert.Equal(
                "CREATE TABLE [__EFMigrationsHistory] (" + EOL +
                "    [MigrationId] nvarchar(150) NOT NULL," + EOL +
                "    [ProductVersion] nvarchar(32) NOT NULL," + EOL +
                "    CONSTRAINT [PK_HistoryRow] PRIMARY KEY ([MigrationId])" + EOL +
                ");" + EOL,
                sql);
        }

        [Fact]
        public void GetDeleteScript_works()
        {
            var sql = CreateHistoryRepository().GetDeleteScript("Migration1");

            Assert.Equal(
                "DELETE FROM [__EFMigrationsHistory]" + EOL +
                "WHERE [MigrationId] = 'Migration1';" + EOL,
                sql);
        }

        [Fact]
        public void GetInsertScript_works()
        {
            var sql = CreateHistoryRepository().GetInsertScript(
                new HistoryRow("Migration1", "7.0.0"));

            Assert.Equal(
                "INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])" + EOL +
                "VALUES ('Migration1', '7.0.0');" + EOL,
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
            var annotationsProvider = new SqlCeAnnotationProvider();
            var sqlGenerator = new SqlCeSqlGenerator();
            var typeMapper = new SqlCeTypeMapper();

            var commandBuilderFactory = new RelationalCommandBuilderFactory(
                typeMapper);

            return new SqlCeHistoryRepository(
                Mock.Of<IRelationalDatabaseCreator>(),
                Mock.Of<ISqlStatementExecutor>(),
                Mock.Of<ISqlCeDatabaseConnection>(),
                new DbContextOptions<DbContext>(
                    new Dictionary<Type, IDbContextOptionsExtension>
                    {
                        { typeof(SqlCeOptionsExtension), new SqlCeOptionsExtension() }
                    }),
                new MigrationsModelDiffer(
                    annotationsProvider,
                    new SqlCeMigrationsAnnotationProvider()),
                new SqlCeMigrationsSqlGenerator(
                    commandBuilderFactory,
                    new SqlCeSqlGenerator(),
                    typeMapper,
                    annotationsProvider),
                annotationsProvider,
                sqlGenerator);
        }

        private class Context : DbContext
        {
        }
    }
}
