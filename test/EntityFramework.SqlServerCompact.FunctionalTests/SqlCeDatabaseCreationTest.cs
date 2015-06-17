﻿using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class SqlCeDatabaseCreationTest
    {
        [Fact]
        public async Task Exists_returns_false_when_database_doesnt_exist()
        {
            await Exists_returns_false_when_database_doesnt_exist_test(async: false);
        }

        [Fact]
        public async Task ExistsAsync_returns_false_when_database_doesnt_exist()
        {
            await Exists_returns_false_when_database_doesnt_exist_test(async: true);
        }

        private static async Task Exists_returns_false_when_database_doesnt_exist_test(bool async)
        {
            using (var testDatabase = await SqlCeTestStore.CreateScratchAsync(createDatabase: false))
            {
                using (var context = new BloggingContext(testDatabase))
                {
                    Assert.False(async ? await context.Database.AsRelational().ExistsAsync() : context.Database.AsRelational().Exists());

                    Assert.Equal(ConnectionState.Closed, context.Database.AsRelational().Connection.DbConnection.State);
                }
            }
        }

        [Fact]
        public async Task Exists_returns_true_when_database_exists()
        {
            await Exists_returns_true_when_database_exists_test(async: false);
        }

        [Fact]
        public async Task ExistsAsync_returns_true_when_database_exists()
        {
            await Exists_returns_true_when_database_exists_test(async: true);
        }

        private static async Task Exists_returns_true_when_database_exists_test(bool async)
        {
            using (var testDatabase = await SqlCeTestStore.CreateScratchAsync(createDatabase: true))
            {
                using (var context = new BloggingContext(testDatabase))
                {
                    Assert.True(async ? await context.Database.AsRelational().ExistsAsync() : context.Database.AsRelational().Exists());

                    Assert.Equal(ConnectionState.Closed, context.Database.AsRelational().Connection.DbConnection.State);
                }
            }
        }

        [Fact]
        public async Task EnsureDeleted_will_delete_database()
        {
            await EnsureDeleted_will_delete_database_test(async: false, openConnection: false);
        }

        [Fact]
        public async Task EnsureDeletedAsync_will_delete_database()
        {
            await EnsureDeleted_will_delete_database_test(async: true, openConnection: false);
        }

        [Fact]
        public async Task EnsureDeleted_will_not_delete_database_with_opened_connections()
        {
            await EnsureDeleted_will_delete_database_test(async: false, openConnection: true);
        }

        [Fact]
        public async Task EnsureDeletedAsync_will_not_delete_database_with_opened_connections()
        {
            await EnsureDeleted_will_delete_database_test(async: true, openConnection: true);
        }

        private static async Task EnsureDeleted_will_delete_database_test(bool async, bool openConnection)
        {
            using (var testDatabase = await SqlCeTestStore.CreateScratchAsync(createDatabase: true))
            {
                if (!openConnection)
                {
                    testDatabase.Connection.Close();
                }

                using (var context = new BloggingContext(testDatabase))
                {
                    Assert.True(async ? await context.Database.AsRelational().ExistsAsync() : context.Database.AsRelational().Exists());

                    if (openConnection)
                    {

                        Assert.Throws<IOException>(() => context.Database.EnsureDeleted());
                    }
                    else
                    {
                        if (async)
                        {
                            Assert.True(await context.Database.EnsureDeletedAsync());
                        }
                        else
                        {
                            Assert.True(context.Database.EnsureDeleted());
                        }

                        Assert.Equal(ConnectionState.Closed, context.Database.AsRelational().Connection.DbConnection.State);

                        Assert.False(async ? await context.Database.AsRelational().ExistsAsync() : context.Database.AsRelational().Exists());

                        Assert.Equal(ConnectionState.Closed, context.Database.AsRelational().Connection.DbConnection.State);
                    }
                }
            }
        }

        [Fact]
        public async Task EnsuredDeleted_noop_when_database_doesnt_exist()
        {
            await EnsuredDeleted_noop_when_database_doesnt_exist_test(async: false);
        }

        [Fact]
        public async Task EnsuredDeletedAsync_noop_when_database_doesnt_exist()
        {
            await EnsuredDeleted_noop_when_database_doesnt_exist_test(async: true);
        }

        private static async Task EnsuredDeleted_noop_when_database_doesnt_exist_test(bool async)
        {
            using (var testDatabase = await SqlCeTestStore.CreateScratchAsync(createDatabase: false))
            {
                using (var context = new BloggingContext(testDatabase))
                {
                    Assert.False(async ? await context.Database.AsRelational().ExistsAsync() : context.Database.AsRelational().Exists());

                    if (async)
                    {
                        Assert.False(await context.Database.AsRelational().EnsureDeletedAsync());
                    }
                    else
                    {
                        Assert.False(context.Database.AsRelational().EnsureDeleted());
                    }

                    Assert.Equal(ConnectionState.Closed, context.Database.AsRelational().Connection.DbConnection.State);

                    Assert.False(async ? await context.Database.AsRelational().ExistsAsync() : context.Database.AsRelational().Exists());

                    Assert.Equal(ConnectionState.Closed, context.Database.AsRelational().Connection.DbConnection.State);
                }
            }
        }

        [Fact]
        public async Task EnsureCreated_can_create_schema_in_existing_database()
        {
            await EnsureCreated_can_create_schema_in_existing_database_test(async: false);
        }

        [Fact]
        public async Task EnsureCreatedAsync_can_create_schema_in_existing_database()
        {
            await EnsureCreated_can_create_schema_in_existing_database_test(async: true);
        }

        private static async Task EnsureCreated_can_create_schema_in_existing_database_test(bool async)
        {
            using (var testDatabase = await SqlCeTestStore.CreateScratchAsync())
            {
                await RunDatabaseCreationTest(testDatabase, async);
            }
        }

        [Fact]
        public async Task EnsureCreated_can_create_physical_database_and_schema()
        {
            await EnsureCreated_can_create_physical_database_and_schema_test(async: false);
        }

        [Fact]
        public async Task EnsureCreatedAsync_can_create_physical_database_and_schema()
        {
            await EnsureCreated_can_create_physical_database_and_schema_test(async: true);
        }

        private static async Task EnsureCreated_can_create_physical_database_and_schema_test(bool async)
        {
            using (var testDatabase = await SqlCeTestStore.CreateScratchAsync(createDatabase: false))
            {
                await RunDatabaseCreationTest(testDatabase, async);
            }
        }

        private static async Task RunDatabaseCreationTest(SqlCeTestStore testStore, bool async)
        {
            using (var context = new BloggingContext(testStore))
            {
                Assert.Equal(ConnectionState.Closed, context.Database.AsRelational().Connection.DbConnection.State);

                if (async)
                {
                    Assert.True(await context.Database.AsRelational().EnsureCreatedAsync());
                }
                else
                {
                    Assert.True(context.Database.AsRelational().EnsureCreated());
                }

                Assert.Equal(ConnectionState.Closed, context.Database.AsRelational().Connection.DbConnection.State);

                if (testStore.Connection.State != ConnectionState.Open)
                {
                    await testStore.Connection.OpenAsync();
                }

                var tables = testStore.Query<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES ");
                Assert.Equal(1, tables.Count());
                Assert.Equal("Blog", tables.Single());

                var columns = (testStore.Query<string>(
                    "SELECT TABLE_NAME + '.' + COLUMN_NAME + ' (' + DATA_TYPE + ')' FROM INFORMATION_SCHEMA.COLUMNS ORDER BY TABLE_NAME, COLUMN_NAME")).ToArray();
                Assert.Equal(19, columns.Length);

                Assert.Equal(
                    new[]
                        {
                            "Blog.AndChew (image)",
                            "Blog.AndRow (rowversion)",
                            "Blog.Cheese (nvarchar)",
                            "Blog.CupOfChar (int)",
                            "Blog.ErMilan (int)",
                            "Blog.Fuse (smallint)",
                            "Blog.George (bit)",
                            "Blog.Key1 (nvarchar)",
                            "Blog.Key2 (varbinary)",
                            "Blog.NotFigTime (datetime)",
                            "Blog.NotToEat (smallint)",
                            "Blog.On (real)",
                            "Blog.OrNothing (float)",
                            "Blog.OrULong (int)",
                            "Blog.OrUShort (numeric)",
                            "Blog.OrUSkint (bigint)",
                            "Blog.TheGu (uniqueidentifier)",
                            "Blog.ToEat (tinyint)",
                            "Blog.WayRound (bigint)"
                        },
                    columns);
            }
        }

        [Fact]
        public async Task EnsuredCreated_is_noop_when_database_exists_and_has_schema()
        {
            await EnsuredCreated_is_noop_when_database_exists_and_has_schema_test(async: false);
        }

        [Fact]
        public async Task EnsuredCreatedAsync_is_noop_when_database_exists_and_has_schema()
        {
            await EnsuredCreated_is_noop_when_database_exists_and_has_schema_test(async: true);
        }

        private static async Task EnsuredCreated_is_noop_when_database_exists_and_has_schema_test(bool async)
        {
            using (var testDatabase = await SqlCeTestStore.CreateScratchAsync(createDatabase: false))
            {
                using (var context = new BloggingContext(testDatabase))
                {
                    context.Database.EnsureCreated();

                    if (async)
                    {
                        Assert.False(await context.Database.EnsureCreatedAsync());
                    }
                    else
                    {
                        Assert.False(context.Database.EnsureCreated());
                    }

                    Assert.Equal(ConnectionState.Closed, context.Database.AsRelational().Connection.DbConnection.State);
                }
            }
        }

        private static IServiceProvider CreateServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddEntityFramework()
                .AddSqlCe();

            return serviceCollection.BuildServiceProvider();
        }

        private class BloggingContext : DbContext
        {
            private readonly SqlCeTestStore _testStore;

            public BloggingContext(SqlCeTestStore testStore)
                : base(CreateServiceProvider())
            {
                _testStore = testStore;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlCe(_testStore.Connection.ConnectionString);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Blog>(b =>
                {
                    b.Key(e => new { e.Key1, e.Key2 });
                    b.Property(e => e.AndRow).ConcurrencyToken();
                });
            }

            public DbSet<Blog> Blogs { get; set; }
        }

        public class Blog
        {
            public string Key1 { get; set; }
            public byte[] Key2 { get; set; }
            public string Cheese { get; set; }
            public int ErMilan { get; set; }
            public bool George { get; set; }
            public Guid TheGu { get; set; }
            public DateTime NotFigTime { get; set; }
            public byte ToEat { get; set; }
            public char CupOfChar { get; set; }
            public double OrNothing { get; set; }
            public short Fuse { get; set; }
            public long WayRound { get; set; }
            public sbyte NotToEat { get; set; }
            public float On { get; set; }
            public ushort OrULong { get; set; }
            public uint OrUSkint { get; set; }
            public ulong OrUShort { get; set; }
            public byte[] AndChew { get; set; }
            public byte[] AndRow { get; set; }
        }
    }
}
