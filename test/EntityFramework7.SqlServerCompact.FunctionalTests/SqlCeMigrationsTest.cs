using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using Microsoft.Data.Entity.Storage;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
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
                    context.Database.ApplyMigrations();

                    Assert.True(context.GetService<IRelationalDatabaseCreator>().Exists());
                }
            }
        }

        private static BloggingContext CreateContext(SqlCeTestStore testStore)
        {
            var serviceProvider =
                new ServiceCollection()
                    .AddEntityFramework()
                    .AddSqlCe()
                    .ServiceCollection()
                    .BuildServiceProvider();

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlCe(testStore.Connection.ConnectionString);

            return new BloggingContext(serviceProvider, optionsBuilder.Options);
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

        [ContextType(typeof(BloggingContext))]
        public class EmptyMigration : Migration
        {
            public override void Up(MigrationBuilder migrationBuilder)
            {
            }

            public override void Down(MigrationBuilder migrationBuilder)
            {
            }

            public override string Id
            {
                get { return "Empty"; }
            }

            public override string ProductVersion
            {
                get { return "EF7"; }
            }

            public override void BuildTargetModel(ModelBuilder modelBuilder)
            {
            }
        }
    }
}
