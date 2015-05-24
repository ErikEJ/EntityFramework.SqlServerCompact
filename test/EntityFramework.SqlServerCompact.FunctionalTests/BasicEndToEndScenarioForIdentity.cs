using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class BasicEndToEndScenarioForIdentity
    {
        [Fact]
        public void Can_run_end_to_end_scenario()
        {
            using (var db = new BloggingContextForIdentity())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Blogs.Add(new BlogWithIdentity { Url = "http://erikej.blogspot.com" });
                db.SaveChanges();

                var blogs = db.Blogs.ToList();

                Assert.Equal(blogs.Count, 1);
                Assert.Equal(blogs[0].Url, "http://erikej.blogspot.com");
            }
        }
    }

    public class BloggingContextForIdentity : DbContext
    {
        public DbSet<BlogWithIdentity> Blogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServerCe(@"Data Source=BloggingIdentity.sdf");
        }
    }

    public class BlogWithIdentity
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
}
