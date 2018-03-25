using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Microsoft.EntityFrameworkCore
{
    public class SqlCeMigrationsTest
    {
        [Fact]
        public void Empty_Migration_Creates_Database()
        {
            using (var testDatabase = SqlCeTestStore.CreateScratch(createDatabase: false))
            {
                
                using (var context = CreateContext(testDatabase))
                {
                    context.Database.Migrate();

                    Assert.True(context.GetService<IRelationalDatabaseCreator>().Exists());
                }
            }
        }

        private static BloggingContext CreateContext(SqlCeTestStore testStore)
        {
            var serviceProvider =
                new ServiceCollection()
                    .AddEntityFrameworkSqlCe()
                    .BuildServiceProvider();

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder
                .UseSqlCe(testStore.ConnectionString)
                .UseInternalServiceProvider(serviceProvider);

            return new BloggingContext(serviceProvider, optionsBuilder.Options);
        }

        private class BloggingContext : DbContext
        {
            public BloggingContext(IServiceProvider serviceProvider, DbContextOptions options)
                : base(options)
            {
            }

            public DbSet<Blog> Blogs { get; set; }

            public class Blog
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }
        }

        [DbContext(typeof(BloggingContext))]
        [Migration("00000000000000_Empty")]
        public class EmptyMigration : Migration
        {
            protected override void Up(MigrationBuilder migrationBuilder)
            {
            }
        }
    }
}
