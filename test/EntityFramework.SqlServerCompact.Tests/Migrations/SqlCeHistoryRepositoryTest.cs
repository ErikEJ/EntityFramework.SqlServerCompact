using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Data.SqlServerCe;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Tests.Migrations
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
                "    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])" + EOL 
                + ")" + EOL + EOL,
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
                "    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])" + EOL + 
                ")" + EOL + EOL,
                sql);
        }

        [Fact]
        public void GetDeleteScript_works()
        {
            var sql = CreateHistoryRepository().GetDeleteScript("Migration1");

            Assert.Equal(
                "DELETE FROM [__EFMigrationsHistory]" + EOL +
                "WHERE [MigrationId] = N'Migration1'" + EOL + EOL,
                sql);
        }

        [Fact]
        public void GetInsertScript_works()
        {
            var sql = CreateHistoryRepository().GetInsertScript(
                new HistoryRow("Migration1", "1.0.0"));

            Assert.Equal(
                "INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])" + EOL +
                "VALUES (N'Migration1', N'1.0.0')" + EOL + EOL,
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

        private static IHistoryRepository CreateHistoryRepository(string schema = null)
            => new DbContext(
                    new DbContextOptionsBuilder()
                        .UseInternalServiceProvider(SqlCeTestHelpers.Instance.CreateServiceProvider())
                        .UseSqlCe(
                            new SqlCeConnection("Data Source=DummyDatabase"),
                            b => b.MigrationsHistoryTable(HistoryRepository.DefaultTableName, schema))
                        .Options)
                .GetService<IHistoryRepository>();

        private class Context : DbContext
        {
        }
    }
}
