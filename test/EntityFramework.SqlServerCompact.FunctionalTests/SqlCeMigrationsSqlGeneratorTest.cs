using System;
using EFCore.SqlCe.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;

// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore
{
    public class SqlCeMigrationSqlGeneratorTest : MigrationSqlGeneratorTestBase
    {
        private const string NotSupported = "SQL Server Compact does not support this migration operation";

        public override void DropSequenceOperation()
        {
            var ex = Assert.Throws<NotSupportedException>(() => base.DropSequenceOperation());
            Assert.StartsWith(NotSupported, ex.Message);
        }

        public override void AlterSequenceOperation_without_minValue_and_maxValue()
        {
            var ex = Assert.Throws<NotSupportedException>(() => base.AlterSequenceOperation_without_minValue_and_maxValue());
            Assert.StartsWith(NotSupported, ex.Message);
        }

        public override void AlterSequenceOperation_with_minValue_and_maxValue()
        {
            var ex = Assert.Throws<NotSupportedException>(() => base.AlterSequenceOperation_with_minValue_and_maxValue());
            Assert.StartsWith(NotSupported, ex.Message);
        }

        public override void CreateSequenceOperation_without_minValue_and_maxValue()
        {
            var ex = Assert.Throws<NotSupportedException>(() => base.CreateSequenceOperation_without_minValue_and_maxValue());
            Assert.StartsWith(NotSupported, ex.Message);
        }

        public override void CreateSequenceOperation_with_minValue_and_maxValue()
        {
            var ex = Assert.Throws<NotSupportedException>(() => base.CreateSequenceOperation_with_minValue_and_maxValue());
            Assert.StartsWith(NotSupported, ex.Message);
        }

        public override void CreateSequenceOperation_with_minValue_and_maxValue_not_long()
        {
            var ex = Assert.Throws<NotSupportedException>(() => base.CreateSequenceOperation_with_minValue_and_maxValue_not_long());
            Assert.StartsWith(NotSupported, ex.Message);
        }

        public override void CreateIndexOperation_with_filter_where_clause()
        {
            base.CreateIndexOperation_with_filter_where_clause();

            Assert.Equal(
                "CREATE INDEX [IX_People_Name] ON [People] ([Name])" + EOL + EOL,
                Sql);
        }

        public override void CreateIndexOperation_with_filter_where_clause_and_is_unique()
        {
            base.CreateIndexOperation_with_filter_where_clause_and_is_unique();

            Assert.Equal(
                "CREATE UNIQUE INDEX [IX_People_Name] ON [People] ([Name])" + EOL + EOL,
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
                    DefaultValue = 0,
                    IsNullable = false,
                    [SqlCeAnnotationNames.ValueGeneration] =
                    SqlCeAnnotationNames.Identity
                });

            Assert.Equal(
                "ALTER TABLE [People] ADD [Id] int NOT NULL IDENTITY" + EOL + EOL,
                Sql);
        }

        public override void AddColumnOperation_without_column_type()
        {
            base.AddColumnOperation_without_column_type();

            Assert.Equal(
                "ALTER TABLE [People] ADD [Alias] nvarchar(4000) NOT NULL" + EOL + EOL,
                Sql);
        }

        public override void AddColumnOperation_with_unicode_no_model()
        {
            base.AddColumnOperation_with_unicode_no_model();

            Assert.Equal(
                "ALTER TABLE [Person] ADD [Name] nvarchar(4000) NULL" + EOL + EOL,
                Sql);
        }

        public override void AddColumnOperation_with_maxLength()
        {
            base.AddColumnOperation_with_maxLength();

            Assert.Equal(
                "ALTER TABLE [Person] ADD [Name] nvarchar(30) NULL" + EOL + EOL,
                Sql);
        }

        public override void AddColumnOperation_with_maxLength_overridden()
        {
            base.AddColumnOperation_with_maxLength_overridden();

            Assert.Equal(
                "ALTER TABLE [Person] ADD [Name] nvarchar(32) NULL" + EOL + EOL,
                Sql);
        }

        public override void AddColumnOperation_with_maxLength_on_derived()
        {
            base.AddColumnOperation_with_maxLength_on_derived();

            Assert.Equal(
                "ALTER TABLE [Person] ADD [Name] nvarchar(30) NULL" + EOL + EOL,
                Sql);
        }

        public override void AddColumnOperation_with_ansi()
        {
            base.AddColumnOperation_with_ansi();

            Assert.Equal(
                "ALTER TABLE [Person] ADD [Name] nvarchar(4000) NULL" + EOL + EOL,
                Sql);
        }

        public override void AddColumnOperation_with_unicode_overridden()
        {
            base.AddColumnOperation_with_unicode_overridden();

            Assert.Equal(
                "ALTER TABLE [Person] ADD [Name] nvarchar(4000) NULL" + EOL + EOL,
                Sql);
        }

        public override void AddColumnOperation_with_shared_column()
        {
            base.AddColumnOperation_with_shared_column();

            Assert.Equal(
                "ALTER TABLE [Base] ADD [Foo] nvarchar(4000) NULL" + EOL + EOL,
                Sql);
        }

        [Fact]
        public virtual void AddColumnOperation_with_rowversion_overridden()
        {
            Generate(
                modelBuilder => modelBuilder.Entity("Person").Property<byte[]>("RowVersion"),
                new AddColumnOperation
                {
                    Table = "Person",
                    Name = "RowVersion",
                    ClrType = typeof(byte[]),
                    IsRowVersion = true,
                    IsNullable = true
                });

            Assert.Equal(
                "ALTER TABLE [Person] ADD [RowVersion] rowversion NULL" + EOL + EOL,
                Sql);
        }

        [Fact]
        public virtual void AddColumnOperation_with_rowversion_no_model()
        {
            Generate(
                new AddColumnOperation
                {
                    Table = "Person",
                    Name = "RowVersion",
                    ClrType = typeof(byte[]),
                    IsRowVersion = true,
                    IsNullable = true
                });

            Assert.Equal(
                "ALTER TABLE [Person] ADD [RowVersion] rowversion NULL" + EOL + EOL,
                Sql);
        }

        public override void AlterColumnOperation()
        {
            base.AlterColumnOperation();

            Assert.Equal(
                "ALTER TABLE [People] ALTER COLUMN [LuckyNumber] DROP DEFAULT" + EOL +
                "GO" + EOL + EOL +
                "ALTER TABLE [People] ALTER COLUMN [LuckyNumber] int NOT NULL" + EOL +
                "GO" + EOL + EOL +
                "ALTER TABLE [People] ALTER COLUMN [LuckyNumber] SET  DEFAULT 7",
                Sql);
        }

        public override void AlterColumnOperation_without_column_type()
        {
            base.AlterColumnOperation_without_column_type();

            Assert.Equal(
                "ALTER TABLE [People] ALTER COLUMN [LuckyNumber] DROP DEFAULT" + EOL +
                "GO" + EOL + EOL +
                "ALTER TABLE [People] ALTER COLUMN [LuckyNumber] int NOT NULL" + EOL,
                Sql);
        }

        public override void CreateIndexOperation_nonunique()
        {
            base.CreateIndexOperation_nonunique();

            Assert.Equal(
                "CREATE INDEX [IX_People_Name] ON [People] ([Name])" + EOL + EOL,
                Sql);
        }

        public override void CreateIndexOperation_unique()
        {
            base.CreateIndexOperation_unique();

            Assert.Equal(
                "CREATE UNIQUE INDEX [IX_People_Name] ON [dbo].[People] ([FirstName], [LastName])" + EOL + EOL,
                Sql);
        }

        [Fact]
        public virtual void CreateIndexOperation_unique_non_legacy()
        {
            Generate(
                modelBuilder => modelBuilder.HasAnnotation(CoreAnnotationNames.ProductVersionAnnotation, "2.0.0"),
                   new CreateIndexOperation
                   {
                       Name = "IX_People_Name",
                       Table = "People",
                       Schema = null,
                       Columns = new[] { "FirstName", "LastName" },
                       IsUnique = true
                   });

            Assert.Equal(
                "CREATE UNIQUE INDEX [IX_People_Name] ON [People] ([FirstName], [LastName])" + EOL + EOL,
                Sql);
        }

        public override void DropColumnOperation()
        {
            base.DropColumnOperation();

            Assert.Equal(
                "ALTER TABLE [dbo].[People] DROP COLUMN [LuckyNumber]" + EOL + EOL,
                Sql);
        }

        public override void DropIndexOperation()
        {
            base.DropIndexOperation();

            Assert.Equal(
                "DROP INDEX [People].[IX_People_Name]",
                Sql);
        }

        [Fact]
        public virtual void SqlOperation_handles_backslash()
        {
            Generate(
                new SqlOperation
                {
                    Sql = @"-- Multiline \" + EOL +
                          "comment"
                });

            Assert.Equal(
                "-- Multiline comment" + EOL,
                Sql);
        }

        [Fact]
        public virtual void SqlOperation_ignores_sequential_gos()
        {
            Generate(
                new SqlOperation
                {
                    Sql = "-- Ready set" + EOL +
                          "GO" + EOL +
                          "GO"
                });

            Assert.Equal(
                "-- Ready set" + EOL,
                Sql);
        }

        [Fact]
        public virtual void SqlOperation_handles_go()
        {
            Generate(
                new SqlOperation
                {
                    Sql = "-- I" + EOL +
                          "go" + EOL +
                          "-- Too"
                });

            Assert.Equal(
                "-- I" + EOL +
                "GO" + EOL +
                EOL +
                "-- Too" + EOL,
                Sql);
        }

        [Fact]
        public virtual void SqlOperation_handles_go_with_count()
        {
            Generate(
                new SqlOperation
                {
                    Sql = "-- I" + EOL +
                          "GO 2"
                });

            Assert.Equal(
                "-- I" + EOL +
                "GO" + EOL +
                EOL +
                "-- I" + EOL,
                Sql);
        }

        [Fact]
        public virtual void SqlOperation_ignores_non_go()
        {
            Generate(
                new SqlOperation
                {
                    Sql = "-- I GO 2"
                });

            Assert.Equal(
                "-- I GO 2" + EOL,
                Sql);
        }

        public override void DeleteDataOperation_simple_key()
        {
            base.DeleteDataOperation_simple_key();

            Assert.Equal(
                "DELETE FROM [People]" + EOL +
                "WHERE [Id] = 2" + EOL + EOL +
                "DELETE FROM [People]" + EOL +
                "WHERE [Id] = 4" + EOL + EOL,
                Sql);
        }

        public override void DeleteDataOperation_composite_key()
        {
            base.DeleteDataOperation_composite_key();

            Assert.Equal(
                "DELETE FROM [People]" + EOL +
                "WHERE [First Name] = N'Hodor' AND [Last Name] IS NULL" + EOL + EOL +
                "DELETE FROM [People]" + EOL +
                "WHERE [First Name] = N'Daenerys' AND [Last Name] = N'Targaryen'" + EOL + EOL,
                Sql);
        }

        public override void UpdateDataOperation_simple_key()
        {
            base.UpdateDataOperation_simple_key();

            Assert.Equal(
                "UPDATE [People] SET [Full Name] = N'Daenerys Stormborn'" + EOL +
                "WHERE [Id] = 1" + EOL + EOL +
                "UPDATE [People] SET [Full Name] = N'Homeless Harry Strickland'" + EOL +
                "WHERE [Id] = 4" + EOL + EOL,
                Sql);
        }

        public override void UpdateDataOperation_composite_key()
        {
            base.UpdateDataOperation_composite_key();

            Assert.Equal(
                "UPDATE [People] SET [First Name] = N'Hodor'" + EOL +
                "WHERE [Id] = 0 AND [Last Name] IS NULL" + EOL + EOL +
                "UPDATE [People] SET [First Name] = N'Harry'" + EOL +
                "WHERE [Id] = 4 AND [Last Name] = N'Strickland'" + EOL + EOL,
                Sql);
        }

        public override void UpdateDataOperation_multiple_columns()
        {
            base.UpdateDataOperation_multiple_columns();

            Assert.Equal(
                "UPDATE [People] SET [First Name] = N'Daenerys', [Nickname] = N'Dany'" + EOL +
                "WHERE [Id] = 1" + EOL + EOL +
                "UPDATE [People] SET [First Name] = N'Harry', [Nickname] = N'Homeless'" + EOL +
                "WHERE [Id] = 4" + EOL + EOL,
                Sql);
        }

        public SqlCeMigrationSqlGeneratorTest()
            : base(SqlCeTestHelpers.Instance)
        {
        }
    }
}
