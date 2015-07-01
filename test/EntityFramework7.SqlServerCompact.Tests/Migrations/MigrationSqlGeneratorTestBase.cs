// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.Data.Entity.Relational.Migrations.Operations;
using Xunit;

namespace Microsoft.Data.Entity.Relational.Migrations.Sql
{
    public abstract class MigrationSqlGeneratorTestBase
    {
        protected static string EOL => Environment.NewLine;

        protected abstract IMigrationSqlGenerator SqlGenerator { get; }

        protected virtual string Sql { get; set; }

        [Fact]
        public virtual void AddColumnOperation_with_defaultValue()
        {
            Generate(
                new AddColumnOperation
                {
                    Table = "People",
                    Schema = "dbo",
                    Name = "Name",
                    Type = "varchar(30)",
                    IsNullable = false,
                    DefaultValue = "John Doe"
                });
        }

        [Fact]
        public virtual void AddColumnOperation_with_defaultValueSql()
        {
            Generate(
                new AddColumnOperation
                {
                    Table = "People",
                    Name = "Birthday",
                    Type = "date",
                    IsNullable = true,
                    DefaultExpression = "CURRENT_TIMESTAMP"
                });
        }

        [Fact]
        public virtual void AddForeignKeyOperation_with_name()
        {
            Generate(
                new AddForeignKeyOperation
                {
                    Table = "People",
                    Schema = "dbo",
                    Name = "FK_People_Companies",
                    Columns = new[] { "EmployerId1", "EmployerId2" },
                    ReferencedTable = "Companies",
                    ReferencedSchema = "hr",
                    ReferencedColumns = new[] { "Id1", "Id2" },
                    OnDelete = ReferentialAction.Cascade
                });
        }

        [Fact]
        public virtual void AddForeignKeyOperation_without_name()
        {
            Generate(
                new AddForeignKeyOperation
                {
                    Table = "People",
                    Columns = new[] { "SpouseId" },
                    ReferencedTable = "People",
                    ReferencedColumns = new[] { "Id" }
                });
        }

        [Fact]
        public virtual void AddPrimaryKeyOperation_with_name()
        {
            Generate(
                new AddPrimaryKeyOperation
                {
                    Table = "People",
                    Schema = "dbo",
                    Name = "PK_People",
                    Columns = new[] { "Id1", "Id2" }
                });
        }

        [Fact]
        public virtual void AddPrimaryKeyOperation_without_name()
        {
            Generate(
                new AddPrimaryKeyOperation
                {
                    Table = "People",
                    Columns = new[] { "Id" }
                });
        }

        [Fact]
        public virtual void AddUniqueConstraintOperation_with_name()
        {
            Generate(
                new AddUniqueConstraintOperation
                {
                    Table = "People",
                    Schema = "dbo",
                    Name = "AK_People_DriverLicense",
                    Columns = new[] { "DriverLicense_State", "DriverLicense_Number" }
                });
        }

        [Fact]
        public virtual void AddUniqueConstraintOperation_without_name()
        {
            Generate(
                new AddUniqueConstraintOperation
                {
                    Table = "People",
                    Columns = new[] { "SSN" }
                });
        }

        [Fact]
        public virtual void AlterColumnOperation()
        {
            Generate(
                new AlterColumnOperation
                {
                    Table = "People",
                    Schema = "dbo",
                    Name = "LuckyNumber",
                    Type = "int",
                    IsNullable = false,
                    DefaultValue = 7
                });
        }

        [Fact]
        public virtual void AlterSequenceOperation_with_minValue_and_maxValue()
        {
            Generate(
                new AlterSequenceOperation
                {
                    Name = "DefaultSequence",
                    Schema = "dbo",
                    IncrementBy = 1,
                    MinValue = 2,
                    MaxValue = 816,
                    Cycle = true
                });
        }

        [Fact]
        public virtual void AlterSequenceOperation_without_minValue_and_maxValue()
        {
            Generate(
                new AlterSequenceOperation
                {
                    Name = "DefaultSequence",
                    IncrementBy = 1
                });
        }

        [Fact]
        public virtual void AlterTableOperation()
        {
            Generate(
                new RenameTableOperation
                {
                    Name = "People",
                    Schema = "dbo"
                });
        }

        [Fact]
        public virtual void CreateIndexOperation_unique()
        {
            Generate(
                new CreateIndexOperation
                {
                    Name = "IX_People_Name",
                    Table = "People",
                    Schema = "dbo",
                    Columns = new[] { "FirstName", "LastName" },
                    IsUnique = true
                });
        }

        [Fact]
        public virtual void CreateIndexOperation_nonunique()
        {
            Generate(
                new CreateIndexOperation
                {
                    Name = "IX_People_Name",
                    Table = "People",
                    Columns = new[] { "Name" },
                    IsUnique = false
                });
        }

        [Fact]
        public virtual void CreateSequenceOperation_with_minValue_and_maxValue()
        {
            Generate(
                new CreateSequenceOperation
                {
                    Name = "DefaultSequence",
                    Schema = "dbo",
                    StartWith = 3,
                    IncrementBy = 1,
                    MinValue = 2,
                    MaxValue = 816,
                    Type = "bigint",
                    Cycle = true
                });
        }

        [Fact]
        public virtual void CreateSequenceOperation_without_minValue_and_maxValue()
        {
            Generate(
                new CreateSequenceOperation
                {
                    Name = "DefaultSequence",
                    StartWith = 3,
                    IncrementBy = 1
                });
        }

        [Fact]
        public virtual void CreateTableOperation()
        {
            Generate(
                new CreateTableOperation
                {
                    Name = "People",
                    Schema = "dbo",
                    Columns =
                    {
                        new AddColumnOperation
                        {
                            Name = "Id",
                            Type = "int",
                            IsNullable = false
                        },
                        new AddColumnOperation
                        {
                            Name = "EmployerId",
                            Type = "int",
                            IsNullable = true
                        },
                         new AddColumnOperation
                        {
                            Name = "SSN",
                            Type = "char(11)",
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
                            ReferencedTable = "Companies"
                        }
                    }
                });
        }

        [Fact]
        public virtual void DropColumnOperation()
        {
            Generate(
                new DropColumnOperation
                {
                    Table = "People",
                    Schema = "dbo",
                    Name = "LuckyNumber"
                });
        }

        [Fact]
        public virtual void DropForeignKeyOperation()
        {
            Generate(
                new DropForeignKeyOperation
                {
                    Table = "People",
                    Schema = "dbo",
                    Name = "FK_People_Companies"
                });
        }

        [Fact]
        public virtual void DropIndexOperation()
        {
            Generate(
                new DropIndexOperation
                {
                    Name = "IX_People_Name",
                    Table = "People",
                    Schema = "dbo"
                });
        }

        [Fact]
        public virtual void DropPrimaryKeyOperation()
        {
            Generate(
                new DropPrimaryKeyOperation
                {
                    Table = "People",
                    Schema = "dbo",
                    Name = "PK_People"
                });
        }

        [Fact]
        public virtual void DropSequenceOperation()
        {
            Generate(
                new DropSequenceOperation
                {
                    Name = "DefaultSequence",
                    Schema = "dbo"
                });
        }

        [Fact]
        public virtual void DropTableOperation()
        {
            Generate(
                new DropTableOperation
                {
                    Name = "People",
                    Schema = "dbo"
                });
        }

        [Fact]
        public virtual void DropUniqueConstraintOperation()
        {
            Generate(
                new DropUniqueConstraintOperation
                {
                    Table = "People",
                    Schema = "dbo",
                    Name = "AK_People_SSN"
                });
        }

        [Fact]
        public virtual void SqlOperation()
        {
            Generate(
                new SqlOperation
                {
                    Sql = "-- I <3 DDL"
                });
        }

        protected virtual void Generate(MigrationOperation operation)
        {
            var batch = SqlGenerator.Generate(new[] { operation });

            Sql = string.Join(
                EOL + "GO" + EOL + EOL,
                batch.Select(b => b.Sql));
        }
    }
}
