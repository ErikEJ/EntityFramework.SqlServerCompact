// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Specification.Tests;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Relational.Specification.Tests
{
    public abstract class MigrationSqlGeneratorTestBase
    {
        protected static string EOL => Environment.NewLine;

        protected virtual string Sql { get; set; }

        [Fact]
        public virtual void CreateIndexOperation_with_filter_where_clause()
            => Generate(
                modelBuilder => modelBuilder.Entity("People").Property<string>("Name").IsRequired(),
                new CreateIndexOperation
                {
                    Name = "IX_People_Name",
                    Table = "People",
                    Columns = new[] { "Name" },
                    Filter = "[Name] IS NOT NULL"
                });

        [Fact]
        public virtual void CreateIndexOperation_with_filter_where_clause_and_is_unique()
            => Generate(
                modelBuilder => modelBuilder.Entity("People").Property<string>("Name"),
                new CreateIndexOperation
                {
                    Name = "IX_People_Name",
                    Table = "People",
                    Columns = new[] { "Name" },
                    IsUnique = true,
                    Filter = "[Name] IS NOT NULL AND <> ''"
                });

        [Fact]
        public virtual void AddColumnOperation_with_defaultValue()
            => Generate(
                new AddColumnOperation
                {
                    Table = "People",
                    Schema = "dbo",
                    Name = "Name",
                    ClrType = typeof(string),
                    ColumnType = "varchar(30)",
                    IsNullable = false,
                    DefaultValue = "John Doe"
                });

        [Fact]
        public virtual void AddColumnOperation_with_defaultValueSql()
            => Generate(
                new AddColumnOperation
                {
                    Table = "People",
                    Name = "Birthday",
                    ClrType = typeof(DateTime),
                    ColumnType = "date",
                    IsNullable = true,
                    DefaultValueSql = "CURRENT_TIMESTAMP"
                });

        [Fact]
        public virtual void AddColumnOperation_with_computed_column_SQL()
            => Generate(
                new AddColumnOperation
                {
                    Table = "People",
                    Name = "Birthday",
                    ClrType = typeof(DateTime),
                    ColumnType = "date",
                    IsNullable = true,
                    ComputedColumnSql = "CURRENT_TIMESTAMP"
                });

        [Fact]
        public virtual void AddColumnOperation_without_column_type()
            => Generate(
                new AddColumnOperation
                {
                    Table = "People",
                    Name = "Alias",
                    ClrType = typeof(string)
                });

        [Fact]
        public virtual void AddColumnOperation_with_ansi()
            => Generate(
                modelBuilder => modelBuilder.Entity("Person").Property<string>("Name").IsUnicode(false),
                new AddColumnOperation
                {
                    Table = "Person",
                    Name = "Name",
                    ClrType = typeof(string),
                    IsUnicode = false,
                    IsNullable = true
                });

        [Fact]
        public virtual void AddColumnOperation_with_unicode_overridden()
            => Generate(
                modelBuilder => modelBuilder.Entity("Person").Property<string>("Name").IsUnicode(false),
                new AddColumnOperation
                {
                    Table = "Person",
                    Name = "Name",
                    ClrType = typeof(string),
                    IsUnicode = true,
                    IsNullable = true
                });

        [Fact]
        public virtual void AddColumnOperation_with_unicode_no_model()
            => Generate(
                new AddColumnOperation
                {
                    Table = "Person",
                    Name = "Name",
                    ClrType = typeof(string),
                    IsUnicode = false,
                    IsNullable = true
                });

        [Fact]
        public virtual void AddColumnOperation_with_maxLength()
            => Generate(
                modelBuilder => modelBuilder.Entity("Person").Property<string>("Name").HasMaxLength(30),
                new AddColumnOperation
                {
                    Table = "Person",
                    Name = "Name",
                    ClrType = typeof(string),
                    MaxLength = 30,
                    IsNullable = true
                });

        [Fact]
        public virtual void AddColumnOperation_with_maxLength_overridden()
            => Generate(
                modelBuilder => modelBuilder.Entity("Person").Property<string>("Name").HasMaxLength(30),
                new AddColumnOperation
                {
                    Table = "Person",
                    Name = "Name",
                    ClrType = typeof(string),
                    MaxLength = 32,
                    IsNullable = true
                });

        [Fact]
        public virtual void AddColumnOperation_with_maxLength_no_model()
            => Generate(
                new AddColumnOperation
                {
                    Table = "Person",
                    Name = "Name",
                    ClrType = typeof(string),
                    MaxLength = 30,
                    IsNullable = true
                });

        [Fact]
        public virtual void AddColumnOperation_with_maxLength_on_derived()
            => Generate(
                modelBuilder =>
                {
                    modelBuilder.Entity("Person");
                    modelBuilder.Entity("SpecialPerson", b =>
                    {
                        b.HasBaseType("Person");
                        b.Property<string>("Name").HasMaxLength(30);
                    });

                    modelBuilder.Entity("MoreSpecialPerson").HasBaseType("SpecialPerson");
                },
                new AddColumnOperation
                {
                    Table = "Person",
                    Name = "Name",
                    ClrType = typeof(string),
                    MaxLength = 30,
                    IsNullable = true
                });

        [Fact]
        public virtual void AddColumnOperation_with_shared_column()
            => Generate(
                modelBuilder =>
                {
                    modelBuilder.Entity<Base>();
                    modelBuilder.Entity<Derived1>();
                    modelBuilder.Entity<Derived2>();
                },
                new AddColumnOperation
                {
                    Table = "Base",
                    Name = "Foo",
                    ClrType = typeof(string),
                    IsNullable = true
                });

        private class Base
        {
            public int Id { get; set; }
        }

        private class Derived1 : Base
        {
            public string Foo { get; set; }
        }

        private class Derived2 : Base
        {
            public string Foo { get; set; }
        }

        [Fact]
        public virtual void AddForeignKeyOperation_with_name()
            => Generate(
                new AddForeignKeyOperation
                {
                    Table = "People",
                    Schema = "dbo",
                    Name = "FK_People_Companies",
                    Columns = new[] { "EmployerId1", "EmployerId2" },
                    PrincipalTable = "Companies",
                    PrincipalSchema = "hr",
                    PrincipalColumns = new[] { "Id1", "Id2" },
                    OnDelete = ReferentialAction.Cascade
                });

        [Fact]
        public virtual void AddForeignKeyOperation_without_name()
            => Generate(
                new AddForeignKeyOperation
                {
                    Table = "People",
                    Columns = new[] { "SpouseId" },
                    PrincipalTable = "People",
                    PrincipalColumns = new[] { "Id" }
                });

        [Fact]
        public virtual void AddForeignKeyOperation_without_principal_columns()
            => Generate(
                new AddForeignKeyOperation
                {
                    Table = "People",
                    Columns = new[] { "SpouseId" },
                    PrincipalTable = "People"
                });

        [Fact]
        public virtual void AddPrimaryKeyOperation_with_name()
            => Generate(
                new AddPrimaryKeyOperation
                {
                    Table = "People",
                    Schema = "dbo",
                    Name = "PK_People",
                    Columns = new[] { "Id1", "Id2" }
                });

        [Fact]
        public virtual void AddPrimaryKeyOperation_without_name()
            => Generate(
                new AddPrimaryKeyOperation
                {
                    Table = "People",
                    Columns = new[] { "Id" }
                });

        [Fact]
        public virtual void AddUniqueConstraintOperation_with_name()
            => Generate(
                new AddUniqueConstraintOperation
                {
                    Table = "People",
                    Schema = "dbo",
                    Name = "AK_People_DriverLicense",
                    Columns = new[] { "DriverLicense_State", "DriverLicense_Number" }
                });

        [Fact]
        public virtual void AddUniqueConstraintOperation_without_name()
            => Generate(
                new AddUniqueConstraintOperation
                {
                    Table = "People",
                    Columns = new[] { "SSN" }
                });

        [Fact]
        public virtual void AlterColumnOperation()
            => Generate(
                new AlterColumnOperation
                {
                    Table = "People",
                    Schema = "dbo",
                    Name = "LuckyNumber",
                    ClrType = typeof(int),
                    ColumnType = "int",
                    IsNullable = false,
                    DefaultValue = 7
                });

        [Fact]
        public virtual void AlterColumnOperation_without_column_type()
            => Generate(
                new AlterColumnOperation
                {
                    Table = "People",
                    Name = "LuckyNumber",
                    ClrType = typeof(int)
                });

        [Fact]
        public virtual void AlterSequenceOperation_with_minValue_and_maxValue()
            => Generate(
                new AlterSequenceOperation
                {
                    Name = "EntityFrameworkHiLoSequence",
                    Schema = "dbo",
                    IncrementBy = 1,
                    MinValue = 2,
                    MaxValue = 816,
                    IsCyclic = true
                });

        [Fact]
        public virtual void AlterSequenceOperation_without_minValue_and_maxValue()
            => Generate(
                new AlterSequenceOperation
                {
                    Name = "EntityFrameworkHiLoSequence",
                    IncrementBy = 1
                });

        [Fact]
        public virtual void RenameTableOperation_within_schema()
            => Generate(
                new RenameTableOperation
                {
                    Name = "People",
                    Schema = "dbo",
                    NewName = "Personas",
                    NewSchema = "dbo"
                });

        [Fact]
        public virtual void CreateIndexOperation_unique()
            => Generate(
                new CreateIndexOperation
                {
                    Name = "IX_People_Name",
                    Table = "People",
                    Schema = "dbo",
                    Columns = new[] { "FirstName", "LastName" },
                    IsUnique = true
                });

        [Fact]
        public virtual void CreateIndexOperation_nonunique()
            => Generate(
                new CreateIndexOperation
                {
                    Name = "IX_People_Name",
                    Table = "People",
                    Columns = new[] { "Name" },
                    IsUnique = false
                });

        [Fact]
        public virtual void CreateIndexOperation_with_where_clauses()
            => Generate(
                new CreateIndexOperation
                {
                    Name = "IX_People_Name",
                    Table = "People",
                    Columns = new[] { "Name" },
                    IsUnique = false,
                    Filter = "[Id] > 2"
                });

        [Fact]
        public virtual void CreateSequenceOperation_with_minValue_and_maxValue()
            => Generate(
                new CreateSequenceOperation
                {
                    Name = "EntityFrameworkHiLoSequence",
                    Schema = "dbo",
                    StartValue = 3,
                    IncrementBy = 1,
                    MinValue = 2,
                    MaxValue = 816,
                    ClrType = typeof(long),
                    IsCyclic = true
                });

        [Fact]
        public virtual void CreateSequenceOperation_with_minValue_and_maxValue_not_long()
            => Generate(
                new CreateSequenceOperation
                {
                    Name = "EntityFrameworkHiLoSequence",
                    Schema = "dbo",
                    StartValue = 3,
                    IncrementBy = 1,
                    MinValue = 2,
                    MaxValue = 816,
                    ClrType = typeof(int),
                    IsCyclic = true
                });

        [Fact]
        public virtual void CreateSequenceOperation_without_minValue_and_maxValue()
            => Generate(
                new CreateSequenceOperation
                {
                    Name = "EntityFrameworkHiLoSequence",
                    ClrType = typeof(long),
                    StartValue = 3,
                    IncrementBy = 1
                });

        [Fact]
        public virtual void CreateTableOperation()
            => Generate(
                new CreateTableOperation
                {
                    Name = "People",
                    Schema = "dbo",
                    Columns =
                    {
                        new AddColumnOperation
                        {
                            Name = "Id",
                            Table = "People",
                            ClrType = typeof(int),
                            IsNullable = false
                        },
                        new AddColumnOperation
                        {
                            Name = "EmployerId",
                            Table = "People",
                            ClrType = typeof(int),
                            IsNullable = true
                        },
                        new AddColumnOperation
                        {
                            Name = "SSN",
                            Table = "People",
                            ClrType = typeof(string),
                            ColumnType = "char(11)",
                            IsNullable = true
                        }
                    },
                    PrimaryKey = new AddPrimaryKeyOperation
                    {
                        Columns = new[] { "Id" }
                    },
                    UniqueConstraints =
                    {
                        new AddUniqueConstraintOperation
                        {
                            Columns = new[] { "SSN" }
                        }
                    },
                    ForeignKeys =
                    {
                        new AddForeignKeyOperation
                        {
                            Columns = new[] { "EmployerId" },
                            PrincipalTable = "Companies",
                            PrincipalColumns = new[] { "Id" }
                        }
                    }
                });

        [Fact]
        public virtual void DropColumnOperation()
            => Generate(
                new DropColumnOperation
                {
                    Table = "People",
                    Schema = "dbo",
                    Name = "LuckyNumber"
                });

        [Fact]
        public virtual void DropForeignKeyOperation()
            => Generate(
                new DropForeignKeyOperation
                {
                    Table = "People",
                    Schema = "dbo",
                    Name = "FK_People_Companies"
                });

        [Fact]
        public virtual void DropIndexOperation()
            => Generate(
                new DropIndexOperation
                {
                    Name = "IX_People_Name",
                    Table = "People",
                    Schema = "dbo"
                });

        [Fact]
        public virtual void DropPrimaryKeyOperation()
            => Generate(
                new DropPrimaryKeyOperation
                {
                    Table = "People",
                    Schema = "dbo",
                    Name = "PK_People"
                });

        [Fact]
        public virtual void DropSequenceOperation()
            => Generate(
                new DropSequenceOperation
                {
                    Name = "EntityFrameworkHiLoSequence",
                    Schema = "dbo"
                });

        [Fact]
        public virtual void DropTableOperation()
            => Generate(
                new DropTableOperation
                {
                    Name = "People",
                    Schema = "dbo"
                });

        [Fact]
        public virtual void DropUniqueConstraintOperation()
            => Generate(
                new DropUniqueConstraintOperation
                {
                    Table = "People",
                    Schema = "dbo",
                    Name = "AK_People_SSN"
                });

        [Fact]
        public virtual void SqlOperation()
            => Generate(
                new SqlOperation
                {
                    Sql = "-- I <3 DDL"
                });

        [Fact]
        public virtual void InsertRowsOperation()
            => Generate(
                new InsertOperation
                {
                    Table = "People",
                    Columns = new[] { "Id", "Full Name" },
                    Values = new object[,]
                    {
                        { 0, null },
                        { 1, "Daenerys Targaryen" },
                        { 2, "John Snow" },
                        { 3, "Arya Stark" },
                        { 4, "Harry Strickland" },
                    }
                });

        [Fact]
        public virtual void DeleteRowsOperation_simple_key()
            => Generate(
                new DeleteOperation
                {
                    Table = "People",
                    KeyColumns = new[] { "Id" },
                    KeyValues = new object[,]
                    {
                        { 2 },
                        { 4 }
                    }
                });

        [Fact]
        public virtual void DeleteRowsOperation_composite_key()
            => Generate(
                new DeleteOperation
                {
                    Table = "People",
                    KeyColumns = new[] { "First Name", "Last Name" },
                    KeyValues = new object[,]
                    {
                        { "Hodor", null },
                        { "Daenerys", "Targaryen" }
                    }
                });

        [Fact]
        public virtual void UpdateRowsOperation_simple_key()
            => Generate(
                new UpdateOperation
                {
                    Table = "People",
                    KeyColumns = new[] { "Id" },
                    KeyValues = new object[,]
                    {
                        { 1 },
                        { 4 }
                    },
                    Columns = new[] { "Full Name" },
                    Values = new object[,]
                    {
                        { "Daenerys Stormborn" },
                        { "Homeless Harry Strickland" }
                    }
                });

        [Fact]
        public virtual void UpdateRowsOperation_composite_key()
            => Generate(
                new UpdateOperation
                {
                    Table = "People",
                    KeyColumns = new[] { "Id", "Last Name" },
                    KeyValues = new object[,]
                    {
                        { 0, null },
                        { 4, "Strickland" }
                    },
                    Columns = new[] { "First Name" },
                    Values = new object[,]
                    {
                        { "Hodor" },
                        { "Homeless Harry" }
                    }
                });

        [Fact]
        public virtual void UpdateRowsOperation_multiple_columns()
            => Generate(
                new UpdateOperation
                {
                    Table = "People",
                    KeyColumns = new[] { "Id" },
                    KeyValues = new object[,]
                    {
                        { 1 },
                        { 4 }
                    },
                    Columns = new[] { "First Name", "Nickname" },
                    Values = new object[,]
                    {
                        { "Daenerys", "Dany" },
                        { "Harry", "Homeless" }
                    }
                });

        private readonly TestHelpers _testHelpers;

        protected MigrationSqlGeneratorTestBase(TestHelpers testHelpers)
        {
            _testHelpers = testHelpers;
        }

        protected virtual void Generate(params MigrationOperation[] operation)
            => Generate(_ => { }, operation);

        protected virtual void Generate(Action<ModelBuilder> buildAction, params MigrationOperation[] operation)
        {
            var modelBuilder = _testHelpers.CreateConventionBuilder();
            buildAction(modelBuilder);

            var batch = _testHelpers.CreateContextServices().GetRequiredService<IMigrationsSqlGenerator>()
                .Generate(operation, modelBuilder.Model);

            Sql = string.Join(
                "GO" + EOL + EOL,
                batch.Select(b => b.CommandText));
        }
    }
}