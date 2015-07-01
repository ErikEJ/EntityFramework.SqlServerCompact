using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Migrations.History;
using Microsoft.Data.Entity.Relational.Migrations.Operations;
using Microsoft.Data.Entity.SqlServerCompact;
using Microsoft.Data.Entity.SqlServerCompact.Migrations;
using Moq;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.Migrations
{
    public class SqlCeHistoryRepositoryTest
    {
        private static string EOL => Environment.NewLine;

        [Fact]
        public void GetCreateOperation_works_when_ifNotExists_false()
        {
            var sql = CreateHistoryRepository().Create(ifNotExists: false);

            Assert.Equal(
                "CREATE TABLE [__MigrationHistory] (" + EOL +
                "    [MigrationId] nvarchar(150) NOT NULL," + EOL +
                "    [ContextKey] nvarchar(300) NOT NULL," + EOL +
                "    [ProductVersion] nvarchar(32) NOT NULL," + EOL +
                "    CONSTRAINT [PK_MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])" + EOL +
                ");",
                sql);
        }

        [Fact]
        public void GetDeleteOperation_works()
        {
            var sqlOperation = (SqlOperation)CreateHistoryRepository().GetDeleteOperation("Migration1");

            Assert.Equal(
                "DELETE FROM [__MigrationHistory]" + EOL +
                "WHERE [MigrationId] = 'Migration1' AND [ContextKey] = '" + typeof(Context).FullName + "';" + EOL,
                sqlOperation.Sql);
        }

        [Fact]
        public void GetInsertOperation_works()
        {
            var sqlOperation = (SqlOperation)CreateHistoryRepository().GetInsertOperation(
                new HistoryRow("Migration1", "7.0.0"));

            Assert.Equal(
                "INSERT INTO [__MigrationHistory] ([MigrationId], [ContextKey], [ProductVersion])" + EOL +
                "VALUES ('Migration1', '" + typeof(Context).FullName + "', '7.0.0');" + EOL,
                sqlOperation.Sql);
        }

        [Fact]
        public void BeginIfNotExists_works()
        {
            var sql = CreateHistoryRepository().BeginIfNotExists("Migration1");

            Assert.Equal(string.Empty, sql);
        }

        [Fact]
        public void BeginIfExists_works()
        {
            var sql = CreateHistoryRepository().BeginIfExists("Migration1");

            Assert.Equal(string.Empty, sql);
        }

        [Fact]
        public void EndIf_works()
        {
            var sql = CreateHistoryRepository().EndIf();

            Assert.Equal(string.Empty, sql);
        }

        private static IHistoryRepository CreateHistoryRepository()
        {
            return new SqlCeHistoryRepository(
                Mock.Of<IRelationalConnection>(),
                Mock.Of<IRelationalDatabaseCreator>(),
                new Context(),
                new SqlCeSqlGenerator());
        }

        private class Context : DbContext
        {
        }
    }
}
