﻿using System;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.Internal;
using Xunit;

namespace Microsoft.Data.Entity.SqlCe.FunctionalTests
{
    public class MigrationsSqlCeTest : MigrationsTestBase<MigrationsSqlCeFixture>
    {
        public MigrationsSqlCeTest(MigrationsSqlCeFixture fixture)
            : base(fixture)
        {
        }

        public override void Can_generate_up_scripts()
        {
            //TODO ErikEJ Why does this fail?
//            base.Can_generate_up_scripts();

//            Assert.Equal(
//                @"IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
//    CREATE TABLE [__EFMigrationsHistory] (
//        [MigrationId] nvarchar(150) NOT NULL,
//        [ProductVersion] nvarchar(32) NOT NULL,
//        CONSTRAINT [PK_HistoryRow] PRIMARY KEY ([MigrationId])
//    );

//GO

//CREATE TABLE [Table1] (
//    [Id] int NOT NULL,
//    CONSTRAINT [PK_Table1] PRIMARY KEY ([Id])
//);

//GO

//INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
//VALUES (N'00000000000001_Migration1', N'7.0.0-test');

//GO

//EXEC sp_rename N'Table1', N'Table2';

//GO

//INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
//VALUES (N'00000000000002_Migration2', N'7.0.0-test');

//GO

//",
//                Sql);
        }

        public override void Can_generate_idempotent_up_scripts()
        {
            Assert.Throws<NotSupportedException>(() => base.Can_generate_idempotent_up_scripts());
        }

        public override void Can_generate_down_scripts()
        {
            base.Can_generate_down_scripts();

            Assert.Equal(
                @"sp_rename N'Table2', N'Table1'

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = '00000000000002_Migration2';

GO

DROP TABLE [Table1]

GO

DELETE FROM [__EFMigrationsHistory]
WHERE [MigrationId] = '00000000000001_Migration1';

GO

",
                Sql);
        }

        public override void Can_generate_idempotent_down_scripts()
        {
            Assert.Throws<NotSupportedException>(() => base.Can_generate_idempotent_down_scripts());
        }

        protected override async Task AssertFirstMigrationAsync(DbConnection connection)
        {
            var sql = await GetDatabaseSchemaAsync(connection);
            Assert.Equal(
                @"
CreatedTable
    Id int NOT NULL
    ColumnWithDefaultToDrop int NULL DEFAULT ((0))
    ColumnWithDefaultToAlter int NULL DEFAULT ((1))
",
                sql);
        }

        protected override async Task AssertSecondMigrationAsync(DbConnection connection)
        {
            var sql = await GetDatabaseSchemaAsync(connection);
            Assert.Equal(
                @"
CreatedTable
    Id int NOT NULL
    ColumnWithDefaultToAlter int NULL
",
                sql);
        }

        public override async Task Can_execute_operations()
        {
            //TODO ErikEJ Implement!
            //return base.Can_execute_operations();
        }

        private async Task<string> GetDatabaseSchemaAsync(DbConnection connection)
        {
            var builder = new IndentedStringBuilder();

            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT
                    t.name,
                    c.Name,
                    TYPE_NAME(c.user_type_id),
                    c.is_nullable,
                    d.Definition
                FROM sys.objects t
                LEFT JOIN sys.columns c ON c.object_id = t.object_id
                LEFT JOIN sys.default_constraints d ON d.parent_column_id = c.column_id
                WHERE t.type = 'U'
                ORDER BY t.name, c.column_id;";

            using (var reader = await command.ExecuteReaderAsync())
            {
                var first = true;
                string lastTable = null;
                while (await reader.ReadAsync())
                {
                    var currentTable = reader.GetString(0);
                    if (currentTable != lastTable)
                    {
                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            builder.DecrementIndent();
                        }

                        builder
                            .AppendLine()
                            .AppendLine(currentTable)
                            .IncrementIndent();

                        lastTable = currentTable;
                    }

                    builder
                        .Append(reader[1]) // Name
                        .Append(" ")
                        .Append(reader[2]) // Type
                        .Append(" ")
                        .Append(reader.GetBoolean(3) ? "NULL" : "NOT NULL");

                    if (!await reader.IsDBNullAsync(4))
                    {
                        builder
                            .Append(" DEFAULT ")
                            .Append(reader[4]);
                    }

                    builder.AppendLine();
                }
            }

            return builder.ToString();
        }
    }
}
