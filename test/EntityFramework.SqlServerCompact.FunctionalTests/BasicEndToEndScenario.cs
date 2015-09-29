using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class BasicEndToEndScenario
    {
        [Fact]
        public void Can_run_end_to_end_scenario()
        {
            using (var db = new BloggingContext())
            {
                db.LogToConsole();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Blogs.Add(new Blog { BlogId = Guid.NewGuid(), Url = "http://erikej.blogspot.com" });
                db.SaveChanges();

                var blogs = db.Blogs.ToList();

                Assert.Equal(blogs.Count, 1);
                Assert.Equal(blogs[0].Url, "http://erikej.blogspot.com");
            }
        }

        public class BloggingContext : DbContext
        {
            public DbSet<Blog> Blogs { get; set; }
            public DbSet<Post> Posts { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlCe(@"Data Source=Blogging.sdf");
            }

            protected override void OnModelCreating(ModelBuilder builder)
            {
                builder.Entity<Blog>()
                    .HasMany(b => b.Posts)
                    .WithOne(p => p.Blog)
                    .ForeignKey(p => p.BlogId);
            }
        }

        public class Blog
        {
            public Guid BlogId { get; set; }
            public string Url { get; set; }

            public List<Post> Posts { get; set; }
        }

        public class Post
        {
            public Guid PostId { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }

            public Guid BlogId { get; set; }
            public Blog Blog { get; set; }
        }
    }
}
