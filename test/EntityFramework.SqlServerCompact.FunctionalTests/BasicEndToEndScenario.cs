using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class BasicEndToEndScenario
    {
        private string _log;
        [Fact]
        public void Can_run_end_to_end_scenario()
        {
            //Demo of logging using Log
            using (var db = new BloggingContext())
            {
                db.Database.Log(AppendLog);
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Blogs.Add(new Blog { BlogId = new Guid("b3279372-78f5-4c13-93c9-e9b281a5ed5b"), Url = "http://erikej.blogspot.com" });
                db.SaveChanges();

                var blogs = db.Blogs.ToList();

                Assert.True(_log.Contains(@"[Parameters=[@p0: b3279372-78f5-4c13-93c9-e9b281a5ed5b, @p1: http://erikej.blogspot.com (Size = 4000)], CommandType='Text', CommandTimeout='0']
INSERT INTO [Blogs] ([BlogId], [Url])
VALUES (@p0, @p1)"));

                Assert.True(_log.Contains(@"[Parameters=[], CommandType='Text', CommandTimeout='0']
SELECT [b].[BlogId], [b].[Url]
FROM [Blogs] AS [b]"));
                
                Assert.Equal(1, blogs.Count);
                Assert.Equal("http://erikej.blogspot.com", blogs[0].Url);
            }
        }

        private void AppendLog(string s)
        {
            _log += s;
        }

        public class BloggingContext : DbContext
        {
            public DbSet<Blog> Blogs { get; set; }
            public DbSet<Post> Posts { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
                optionsBuilder
                    //Log parameter values
                    .EnableSensitiveDataLogging()
                    .UseSqlCe(@"Data Source=Blogging.sdf");

            protected override void OnModelCreating(ModelBuilder builder)
            {
                builder.Entity<Blog>()
                    .HasMany(b => b.Posts)
                    .WithOne(p => p.Blog)
                    .HasForeignKey(p => p.BlogId);
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
