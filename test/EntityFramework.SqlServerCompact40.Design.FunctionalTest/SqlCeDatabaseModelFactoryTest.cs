using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Specification.Tests;
using Microsoft.Extensions.Logging;
using Xunit;

namespace EntityFramework.SqlServerCompact40.Design.FunctionalTest
{
    public class SqlCeDatabaseModelFactoryTest : IClassFixture<SqlCeDatabaseModelFixture>
    {
        [Fact]
        public void It_reads_tables()
        {
            var sql = new List<string>
            {
                "CREATE TABLE [Everest] ( id int );",
                "CREATE TABLE [Denali] ( id int );"
            };
            var dbInfo = CreateModel(sql, new TableSelectionSet(new List<string> { "Everest", "Denali" }));

            Assert.Collection(dbInfo.Tables.OrderBy(t => t.Name),
                d =>
                {
                    Assert.Equal(null, d.SchemaName);
                    Assert.Equal("Denali", d.Name);
                },
                e =>
                {
                    Assert.Equal(null, e.SchemaName);
                    Assert.Equal("Everest", e.Name);
                });
        }

        [Fact]
        public void It_reads_foreign_keys()
        {
            var sql = new List<string> {
                "CREATE TABLE Ranges ( Id INT IDENTITY (1,1) PRIMARY KEY);",
                "CREATE TABLE Mountains ( RangeId INT NOT NULL, FOREIGN KEY (RangeId) REFERENCES Ranges(Id) ON DELETE CASCADE)"
            };
            var dbInfo = CreateModel(sql, new TableSelectionSet(new List<string> { "Ranges", "Mountains" }));

            var fk = Assert.Single(dbInfo.Tables.Single(t => t.ForeignKeys.Count > 0).ForeignKeys);

            Assert.Equal(null, fk.Table.SchemaName);
            Assert.Equal("Mountains", fk.Table.Name);
            Assert.Equal(null, fk.PrincipalTable.SchemaName);
            Assert.Equal("Ranges", fk.PrincipalTable.Name);
            Assert.Equal("RangeId", fk.Columns.Single().Column.Name);
            Assert.Equal("Id", fk.Columns.Single().PrincipalColumn.Name);
            Assert.Equal(ReferentialAction.Cascade, fk.OnDelete);
        }

        [Fact]
        public void It_reads_composite_foreign_keys()
        {
            var sql = new List<string> {
                "CREATE TABLE Ranges1 ( Id INT IDENTITY (1,1), AltId INT, PRIMARY KEY(Id, AltId));",
                "CREATE TABLE Mountains1 ( RangeId INT NOT NULL, RangeAltId INT NOT NULL, FOREIGN KEY (RangeId, RangeAltId) REFERENCES Ranges1(Id, AltId) ON DELETE NO ACTION)"
            };
            var dbInfo = CreateModel(sql, new TableSelectionSet(new List<string> { "Ranges1", "Mountains1" }));

            var fk = Assert.Single(dbInfo.Tables.Single(t => t.ForeignKeys.Count > 0).ForeignKeys);

            Assert.Equal(null, fk.Table.SchemaName);
            Assert.Equal("Mountains1", fk.Table.Name);
            Assert.Equal(null, fk.PrincipalTable.SchemaName);
            Assert.Equal("Ranges1", fk.PrincipalTable.Name);
            Assert.Equal(new[] { "RangeId", "RangeAltId" }, fk.Columns.Select(c => c.Column.Name).ToArray());
            Assert.Equal(new[] { "Id", "AltId" }, fk.Columns.Select(c => c.PrincipalColumn.Name).ToArray());
            Assert.Equal(ReferentialAction.NoAction, fk.OnDelete);
        }

        [Fact]
        public void It_reads_indexes()
        {
            var sql = new List<string>
            {
                "CREATE TABLE Place ( Id int PRIMARY KEY NONCLUSTERED, Name int UNIQUE, Location int );",
                "CREATE NONCLUSTERED INDEX IX_Location ON Place (Location);"
            };
            var dbInfo = CreateModel(sql, new TableSelectionSet(new List<string> { "Place" }));

            var indexes = dbInfo.Tables.Single().Indexes;

            Assert.All(indexes, c =>
            {
                Assert.Equal(null, c.Table.SchemaName);
                Assert.Equal("Place", c.Table.Name);
            });

            Assert.Collection(indexes,
                nonClustered =>
                {
                    Assert.Equal("IX_Location", nonClustered.Name);
                    Assert.Equal("Location", nonClustered.IndexColumns.Select(c => c.Column.Name).Single());
                },
                unique =>
                {
                    Assert.True(unique.IsUnique);
                    Assert.Equal("Name", unique.IndexColumns.Single().Column.Name);
                });
        }

        [Fact]
        public void It_reads_columns()
        {
            var sql = new List<string>
            { @"
                CREATE TABLE [MountainsColumns] (
                    Id int,
                    Name nvarchar(100) NOT NULL,
                    Latitude decimal( 5, 2 ) DEFAULT 0.0,
                    Created datetime DEFAULT('October 20, 2015 11am'),
                    Modified rowversion,
                    Primary Key (Name, Id)
                );"
            };
            var dbInfo = CreateModel(sql, new TableSelectionSet(new List<string> { "MountainsColumns" }));

            var columns = dbInfo.Tables.Single().Columns.OrderBy(c => c.Ordinal);

            Assert.All(columns, c =>
            {
                Assert.Equal(null, c.Table.SchemaName);
                Assert.Equal("MountainsColumns", c.Table.Name);
            });

            Assert.Collection(columns,
                id =>
                {
                    Assert.Equal("Id", id.Name);
                    Assert.Equal("int", id.DataType);
                    Assert.Equal(2, id.PrimaryKeyOrdinal);
                    Assert.False(id.IsNullable);
                    Assert.Equal(0, id.Ordinal);
                    Assert.Null(id.DefaultValue);
                },
                name =>
                {
                    Assert.Equal("Name", name.Name);
                    Assert.Equal("nvarchar", name.DataType);
                    Assert.Equal(1, name.PrimaryKeyOrdinal);
                    Assert.False(name.IsNullable);
                    Assert.Equal(1, name.Ordinal);
                    Assert.Null(name.DefaultValue);
                    Assert.Equal(100, name.MaxLength);
                },
                lat =>
                {
                    Assert.Equal("Latitude", lat.Name);
                    Assert.Equal("numeric", lat.DataType);
                    Assert.Null(lat.PrimaryKeyOrdinal);
                    Assert.True(lat.IsNullable);
                    Assert.Equal(2, lat.Ordinal);
                    Assert.Equal("0.0", lat.DefaultValue);
                    Assert.Equal(5, lat.Precision);
                    Assert.Equal(2, lat.Scale);
                    Assert.Null(lat.MaxLength);
                },
                created =>
                {
                    Assert.Equal("Created", created.Name);
                    Assert.Equal(null, created.Scale);
                    Assert.Equal("('October 20, 2015 11am')", created.DefaultValue);
                },
                modified =>
                {
                    Assert.Equal("Modified", modified.Name);
                    Assert.Equal(ValueGenerated.OnAddOrUpdate, modified.ValueGenerated);
                    Assert.Equal("rowversion", modified.DataType); // intentional - testing the alias
                        });
        }

        [Theory]
        [InlineData("nvarchar(55)", 55)]
        [InlineData("nchar(14)", 14)]
        [InlineData("ntext", null)]
        public void It_reads_max_length(string type, int? length)
        {
            var tables = _fixture.Query<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Strings';");
            if (tables.Count() > 0)
            {
                _fixture.ExecuteNonQuery("DROP TABLE [Strings];");
            }

            var sql = new List<string>
            {
                "CREATE TABLE [Strings] ( CharColumn " + type + ");"
            };
            var db = CreateModel(sql, new TableSelectionSet(new List<string> { "Strings" }));

            Assert.Equal(length, db.Tables.Single().Columns.Single().MaxLength);
        }


        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void It_reads_identity(bool isIdentity)
        {
            var tables = _fixture.Query<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Identities';");
            if (tables.Count() > 0)
            {
                _fixture.ExecuteNonQuery("DROP TABLE [Identities];");
            }

            var sql = new List<string>
            {
                "CREATE TABLE [Identities] ( Id INT " + (isIdentity ? "IDENTITY(1,1)" : "") + ")"
            };

            var dbInfo = CreateModel(sql, new TableSelectionSet(new List<string> { "Identities" }));

            var column = Assert.IsType<ColumnModel>(Assert.Single(dbInfo.Tables.Single().Columns));
            Assert.Equal(isIdentity, column.SqlCe().IsIdentity);
            Assert.Equal(isIdentity ? ValueGenerated.OnAdd : default(ValueGenerated?), column.ValueGenerated);
        }

        [Fact]
        public void It_filters_tables()
        {
            var sql = new List<string>
            {
                "CREATE TABLE [K2] ( Id int, A nvarchar, UNIQUE (A) );",
                "CREATE TABLE [Kilimanjaro] ( Id int,B nvarchar, UNIQUE (B ), FOREIGN KEY (B) REFERENCES K2 (A) );"
            };
            var selectionSet = new TableSelectionSet(new List<string> { "K2" });

            var dbInfo = CreateModel(sql, selectionSet);
            var table = Assert.Single(dbInfo.Tables);
            Assert.Equal("K2", table.Name);
            Assert.Equal(2, table.Columns.Count);
            Assert.Equal(1, table.Indexes.Count);
            Assert.Empty(table.ForeignKeys);
        }

        private readonly SqlCeDatabaseModelFixture _fixture;

        public DatabaseModel CreateModel(List<string> createSql, TableSelectionSet selection = null)
            => _fixture.CreateModel(createSql, selection);

        public SqlCeDatabaseModelFactoryTest(SqlCeDatabaseModelFixture fixture)
        {
            _fixture = fixture;
        }
    }

    public class SqlCeDatabaseModelFixture : IDisposable
    {
        private readonly SqlCeTestStore _testStore;

        public SqlCeDatabaseModelFixture()
        {
            _testStore = SqlCeTestStore.CreateScratch(true);
        }

        public DatabaseModel CreateModel(List<string> createSql, TableSelectionSet selection = null)
        {
            foreach (var sql in createSql)
            {
                _testStore.ExecuteNonQuery(sql);
            }

            var reader = new SqlCeDatabaseModelFactory(new LoggerFactory());

            return reader.Create(_testStore.Connection.ConnectionString, selection ?? TableSelectionSet.All);
        }

        public int ExecuteNonQuery(string sql) => _testStore.ExecuteNonQuery(sql);

        public IEnumerable<T> Query<T>(string sql, params object[] parameters) => _testStore.Query<T>(sql, parameters);

        public void Dispose()
        {
            _testStore.Dispose();
        }
    }
}
