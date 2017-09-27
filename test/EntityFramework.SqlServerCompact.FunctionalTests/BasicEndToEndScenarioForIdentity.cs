using System;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class BasicEndToEndScenarioForIdentity
    {
        [Fact]
        public void Can_run_end_to_end_scenario()
        {
            using (var db = new BloggingContext())
            {
                db.ConfigureLogging(x => Debug.WriteLine(x));
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Blogs.Add(new Blog { Url = "http://erikej.blogspot.com" });
                db.SaveChanges();

                foreach (var entity in db.Model.GetEntityTypes())
                {
                    Console.WriteLine($" {entity.ClrType.Name} => {entity.SqlCe().TableName}");
                }


                var blogs = db.Blogs.ToList();

                Assert.Equal(blogs.Count, 1);
                Assert.Equal(blogs[0].Url, "http://erikej.blogspot.com");
            }
        }

        public class BloggingContext : DbContext
        {
            public DbSet<Blog> Blogs { get; set; }
#if SQLCE35
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlCe(@"Data Source=BloggingIdentity.sdf");
            }
#else
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlCe(
                new SqlCeConnectionStringBuilder
                {
                    DataSource = "BloggingIdentity.sdf"
                }
                    .ConnectionString);
#endif
        }

        public class Blog
        {
            public int Id { get; set; }
            public string Url { get; set; }
        }
    }
}
