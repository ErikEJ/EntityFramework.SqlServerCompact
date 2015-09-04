using System;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Operations;
using Microsoft.Data.Entity.SqlServerCompact;
using Microsoft.Data.Entity.SqlServerCompact.Metadata;
using Microsoft.Data.Entity.SqlServerCompact.Migrations;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.Tests.Migrations
{
    public class SqlCeMigrationSqlGeneratorTest : MigrationSqlGeneratorTestBase
    {
        protected override IMigrationsSqlGenerator SqlGenerator
           => new SqlCeMigrationsSqlGenerator(
               new SqlCeUpdateSqlGenerator(),
               new SqlCeTypeMapper(),
               new SqlCeMetadataExtensionProvider());

        public override void AlterSequenceOperation_without_minValue_and_maxValue()
        {
            Assert.Throws<NotSupportedException>(() => base.AlterSequenceOperation_without_minValue_and_maxValue());
        }

        public override void AlterSequenceOperation_with_minValue_and_maxValue()
        {
            Assert.Throws<NotSupportedException>(() => base.AlterSequenceOperation_with_minValue_and_maxValue());
        }

        public override void CreateSequenceOperation_without_minValue_and_maxValue()
        {
            Assert.Throws<NotSupportedException>(() => base.CreateSequenceOperation_without_minValue_and_maxValue());
        }

        public override void CreateSequenceOperation_with_minValue_and_maxValue()
        {
            Assert.Throws<NotSupportedException>(() => base.CreateSequenceOperation_with_minValue_and_maxValue());
        }

        public override void CreateSequenceOperation_with_minValue_and_maxValue_not_long()
        {
            Assert.Throws<NotSupportedException>(() => base.CreateSequenceOperation_with_minValue_and_maxValue_not_long());
        }

        public override void DropSequenceOperation()
        {
            Assert.Throws<NotSupportedException>(() => base.DropSequenceOperation());
        }

        public override void DropIndexOperation()
        {
            base.DropIndexOperation();

            Assert.Equal(
                "DROP INDEX [IX_People_Name]" + EOL,
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
                "sp_rename 'People', 'Person'" + EOL,
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
                    ClrType = typeof(int),
                    ColumnType = "int",
                    IsNullable = false,
                    [SqlCeAnnotationNames.Prefix + SqlCeAnnotationNames.ValueGeneration] =
                        SqlCeAnnotationNames.Identity
                });

            Assert.Equal(
                "ALTER TABLE [People] ADD [Id] int NOT NULL IDENTITY" + EOL,
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
                "ALTER TABLE [People] ADD PRIMARY KEY ([Id])" + EOL,
                Sql);
        }

        public override void AlterColumnOperation()
        {
            base.AlterColumnOperation();

            Assert.StartsWith(
                "ALTER TABLE [People] ALTER COLUMN [LuckyNumber] DROP DEFAULT",
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
                "CREATE INDEX [IX_People_Name] ON [People] ([Name])" + EOL,
                Sql);
        }
    }
}
