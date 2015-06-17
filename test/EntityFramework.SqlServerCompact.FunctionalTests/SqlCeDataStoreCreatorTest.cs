using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Storage;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class SqlCeDataStoreCreatorTest
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
            using (var testDatabase = SqlCeTestStore.CreateScratch(createDatabase: false))
            {
                var creator = GetDataStoreCreator(testDatabase);

                Assert.False(async ? await creator.ExistsAsync() : creator.Exists());
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
            using (var testDatabase = SqlCeTestStore.CreateScratch(createDatabase: true))
            {
                var creator = GetDataStoreCreator(testDatabase);

                Assert.True(async ? await creator.ExistsAsync() : creator.Exists());
            }
        }

        [Fact]
        public async Task HasTables_throws_when_database_doesnt_exist()
        {
            await HasTables_throws_when_database_doesnt_exist_test(async: false);
        }

        [Fact]
        public async Task HasTablesAsync_throws_when_database_doesnt_exist()
        {
            await HasTables_throws_when_database_doesnt_exist_test(async: true);
        }

        private static async Task HasTables_throws_when_database_doesnt_exist_test(bool async)
        {
            using (var testDatabase = SqlCeTestStore.CreateScratch(createDatabase: false))
            {
                var creator = GetDataStoreCreator(testDatabase);

                var errorNumber = async
                    ? (await Assert.ThrowsAsync<SqlCeException>(() => creator.HasTablesAsync())).NativeError
                    : Assert.Throws<SqlCeException>(() => creator.HasTables()).NativeError;

                Assert.Equal(
                    25046, // The database file cannot be found. Check the path to the database.
                    errorNumber);
            }
        }

        [Fact]
        public async Task HasTables_returns_false_when_database_exists_but_has_no_tables()
        {
            await HasTables_returns_false_when_database_exists_but_has_no_tables_test(async: false);
        }

        [Fact]
        public async Task HasTablesAsync_returns_false_when_database_exists_but_has_no_tables()
        {
            await HasTables_returns_false_when_database_exists_but_has_no_tables_test(async: true);
        }

        private static async Task HasTables_returns_false_when_database_exists_but_has_no_tables_test(bool async)
        {
            using (var testDatabase = SqlCeTestStore.CreateScratch(createDatabase: true))
            {
                var creator = GetDataStoreCreator(testDatabase);

                Assert.False(async ? await creator.HasTablesAsync() : creator.HasTables());
            }
        }

        [Fact]
        public async Task HasTables_returns_true_when_database_exists_and_has_any_tables()
        {
            await HasTables_returns_true_when_database_exists_and_has_any_tables_test(async: false);
        }

        [Fact]
        public async Task HasTablesAsync_returns_true_when_database_exists_and_has_any_tables()
        {
            await HasTables_returns_true_when_database_exists_and_has_any_tables_test(async: true);
        }

        private static async Task HasTables_returns_true_when_database_exists_and_has_any_tables_test(bool async)
        {
            using (var testDatabase = SqlCeTestStore.CreateScratch(createDatabase: true))
            {
                testDatabase.ExecuteNonQuery("CREATE TABLE SomeTable (Id uniqueidentifier)");

                var creator = GetDataStoreCreator(testDatabase);

                Assert.True(async ? await creator.HasTablesAsync() : creator.HasTables());
            }
        }

        [Fact]
        public async Task Delete_will_delete_database()
        {
            await Delete_will_delete_database_test(async: false);
        }

        [Fact]
        public async Task DeleteAsync_will_delete_database()
        {
            await Delete_will_delete_database_test(async: true);
        }

        private static async Task Delete_will_delete_database_test(bool async)
        {
            using (var testDatabase = SqlCeTestStore.CreateScratch(createDatabase: true))
            {
                testDatabase.Connection.Close();

                var creator = GetDataStoreCreator(testDatabase);

                Assert.True(async ? await creator.ExistsAsync() : creator.Exists());

                if (async)
                {
                    await creator.DeleteAsync();
                }
                else
                {
                    creator.Delete();
                }

                Assert.False(async ? await creator.ExistsAsync() : creator.Exists());
            }
        }

        [Fact]
        public async Task Delete_throws_when_database_doesnt_exist()
        {
            await Delete_throws_when_database_doesnt_exist_test(async: false);
        }

        [Fact]
        public async Task DeleteAsync_throws_when_database_doesnt_exist()
        {
            await Delete_throws_when_database_doesnt_exist_test(async: true);
        }

        private static async Task Delete_throws_when_database_doesnt_exist_test(bool async)
        {
            using (var testDatabase = SqlCeTestStore.CreateScratch(createDatabase: false))
            {
                var creator = GetDataStoreCreator(testDatabase);

                if (async)
                {
                    await Assert.ThrowsAsync<SqlCeException>(() => creator.DeleteAsync());
                }
                else
                {
                    Assert.Throws<SqlCeException>(() => creator.Delete());
                }
            }
        }

        [Fact]
        public async Task CreateTables_creates_schema_in_existing_database()
        {
            await CreateTables_creates_schema_in_existing_database_test(async: false);
        }

        [Fact]
        public async Task CreateTablesAsync_creates_schema_in_existing_database()
        {
            await CreateTables_creates_schema_in_existing_database_test(async: true);
        }

        private static async Task CreateTables_creates_schema_in_existing_database_test(bool async)
        {
            using (var testDatabase = SqlCeTestStore.CreateScratch(createDatabase: true))
            {
                var serviceCollection = new ServiceCollection();
                serviceCollection
                    .AddEntityFramework()
                    .AddSqlCe();

                var serviceProvider = serviceCollection.BuildServiceProvider();

                var optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder.UseSqlCe(testDatabase.Connection.ConnectionString);

                using (var context = new BloggingContext(serviceProvider, optionsBuilder.Options))
                {
                    var contextServices = ((IAccessor<IServiceProvider>)context).Service;

                    var creator = (RelationalDataStoreCreator)contextServices.GetRequiredService<IDataStoreCreator>();

                    if (async)
                    {
                        await creator.CreateTablesAsync();
                    }
                    else
                    {
                        creator.CreateTables();
                    }

                    if (testDatabase.Connection.State != ConnectionState.Open)
                    {
                        await testDatabase.Connection.OpenAsync();
                    }

                    var tables = testDatabase.Query<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES");
                    Assert.Equal(1, tables.Count());
                    Assert.Equal("Blog", tables.Single());

                    var columns = testDatabase.Query<string>("SELECT TABLE_NAME + '.' + COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS");
                    Assert.Equal(2, columns.Count());
                    Assert.True(columns.Any(c => c == "Blog.Id"));
                    Assert.True(columns.Any(c => c == "Blog.Name"));
                }
            }
        }

        [Fact]
        public async Task CreateTables_throws_if_database_does_not_exist()
        {
            await CreateTables_throws_if_database_does_not_exist_test(async: false);
        }

        [Fact]
        public async Task CreateTablesAsync_throws_if_database_does_not_exist()
        {
            await CreateTables_throws_if_database_does_not_exist_test(async: true);
        }

        private static async Task CreateTables_throws_if_database_does_not_exist_test(bool async)
        {
            using (var testDatabase = SqlCeTestStore.CreateScratch(createDatabase: false))
            {
                var creator = GetDataStoreCreator(testDatabase);

                var errorNumber
                    = async
                        ? (await Assert.ThrowsAsync<SqlCeException>(() => creator.CreateTablesAsync())).NativeError
                        : Assert.Throws<SqlCeException>(() => creator.CreateTables()).NativeError;

                Assert.Equal(
                    25046, // The database file cannot be found. Check the path to the database.
                    errorNumber);
            }
        }

        [Fact]
        public async Task Create_creates_physical_database_but_not_tables()
        {
            await Create_creates_physical_database_but_not_tables_test(async: false);
        }

        [Fact]
        public async Task CreateAsync_creates_physical_database_but_not_tables()
        {
            await Create_creates_physical_database_but_not_tables_test(async: true);
        }

        private static async Task Create_creates_physical_database_but_not_tables_test(bool async)
        {
            using (var testDatabase = SqlCeTestStore.CreateScratch(createDatabase: false))
            {
                var creator = GetDataStoreCreator(testDatabase);

                Assert.False(creator.Exists());

                if (async)
                {
                    await creator.CreateAsync();
                }
                else
                {
                    creator.Create();
                }

                Assert.True(creator.Exists());

                if (testDatabase.Connection.State != ConnectionState.Open)
                {
                    await testDatabase.Connection.OpenAsync();
                }

                Assert.Equal(0, (testDatabase.Query<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES")).Count());

                Assert.True(testDatabase.Exists());
            }
        }

        [Fact]
        public async Task Create_throws_if_database_already_exists()
        {
            await Create_throws_if_database_already_exists_test(async: false);
        }

        [Fact]
        public async Task CreateAsync_throws_if_database_already_exists()
        {
            await Create_throws_if_database_already_exists_test(async: true);
        }

        private static async Task Create_throws_if_database_already_exists_test(bool async)
        {
            using (var testDatabase = SqlCeTestStore.CreateScratch(createDatabase: true))
            {
                var creator = GetDataStoreCreator(testDatabase);

                var errorNumber =
                        async
                        ? (await Assert.ThrowsAsync<SqlCeException>(() => creator.CreateAsync())).NativeError
                        : Assert.Throws<SqlCeException>(() => creator.Create()).NativeError;

                Assert.Equal(
                    25114, // File already exists. Try using a different database name.
                    errorNumber);
            }
        }

        private static IServiceProvider CreateContextServices(SqlCeTestStore testStore)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddEntityFramework()
                .AddSqlCe();

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlCe(testStore.Connection.ConnectionString);

            return ((IAccessor<IServiceProvider>)new DbContext(
                serviceCollection.BuildServiceProvider(),
                optionsBuilder.Options))
                .Service;
        }

        private static SqlCeDataStoreCreator GetDataStoreCreator(SqlCeTestStore testStore)
        {
            return CreateContextServices(testStore).GetService<SqlCeDataStoreCreator>();
        }

        private class BloggingContext : DbContext
        {
            public BloggingContext(IServiceProvider serviceProvider, DbContextOptions options)
                : base(serviceProvider, options)
            {
            }

            public DbSet<Blog> Blogs { get; set; }

            public class Blog
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }
        }
    }
}
