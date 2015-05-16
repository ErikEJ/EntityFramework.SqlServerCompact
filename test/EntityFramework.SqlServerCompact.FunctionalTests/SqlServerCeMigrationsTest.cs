using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class SqlServerMigrationsTest
    {
        [Fact]
        public void Empty_Migration_Creates_Database()
        {
            using (var testDatabase = SqlServerCeTestStore.CreateScratch(createDatabase: false))
            {
                
                using (var context = CreateContext(testDatabase))
                {
                    context.Database.AsRelational().ApplyMigrations();

                    Assert.True(context.Database.AsRelational().Exists());
                }
            }
        }

        private static BloggingContext CreateContext(SqlServerCeTestStore testStore)
        {
            var serviceProvider =
                new ServiceCollection()
                    .AddEntityFramework()
                    .AddSqlServerCe()
                    .ServiceCollection()
                    .BuildServiceProvider();

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServerCe(testStore.Connection.ConnectionString);

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

            public override IModel Target
            {
                get { return new Model(); }
            }
        }
    }
}
