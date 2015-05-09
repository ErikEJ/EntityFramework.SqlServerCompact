using System;
using ErikEJ.Data.Entity.SqlServerCe.Metadata;
using Microsoft.Data.Entity.Relational.Migrations.Operations;
using Microsoft.Data.Entity.Relational.Migrations.Sql;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.Tests.Migrations
{
    public class SqliteMigrationSqlGeneratorTest : MigrationSqlGeneratorTestBase
    {
        protected override IMigrationSqlGenerator SqlGenerator => new SqlServerCeMigrationSqlGenerator(new SqlServerCeSqlGenerator());

        public override void DropIndexOperation()
        {
            base.DropIndexOperation();

            Assert.Equal(
                "DROP INDEX [IX_People_Name];" + EOL,
                Sql);
        }

        [Fact]
        public virtual void RenameTableOperation()
        {
            Generate(
                new RenameTableOperation
                {
                    Name = "People",
                    NewName = "Person"
                });

            Assert.Equal(
                "sp_rename 'People', 'Person';" + EOL,
                Sql);
        }

        [Fact]
        public virtual void AddColumnOperation_identity()
        {
            Generate(
                new AddColumnOperation
                {
                    Table = "People",
                    Name = "Id",
                    Type = "int",
                    IsNullable = false,
                    [SqlServerCeAnnotationNames.Prefix + SqlServerCeAnnotationNames.ValueGeneration] =
                        SqlServerCeAnnotationNames.Strategy
                });

            Assert.Equal(
                "ALTER TABLE [People] ADD [Id] int NOT NULL IDENTITY;" + EOL,
                Sql);
        }

        [Fact]
        public virtual void AddPrimaryKeyOperation_nonclustered()
        {
            Generate(
                new AddPrimaryKeyOperation
                {
                    Table = "People",
                    Columns = new[] { "Id" }
                });

            Assert.Equal(
                "ALTER TABLE [People] ADD PRIMARY KEY ([Id]);" + EOL,
                Sql);
        }

        public override void AlterColumnOperation()
        {
            base.AlterColumnOperation();

            Assert.Equal(
                "ALTER TABLE [People] ALTER COLUMN [LuckyNumber] int NOT NULL DEFAULT 7;" + EOL,
                Sql);
        }

        [Fact]
        public virtual void CreateIndexOperation()
        {
            Generate(
                new CreateIndexOperation
                {
                    Name = "IX_People_Name",
                    Table = "People",
                    Columns = new[] { "Name" }
                });

            Assert.Equal(
                "CREATE INDEX [IX_People_Name] ON [People] ([Name]);" + EOL,
                Sql);
        }
    }
}
