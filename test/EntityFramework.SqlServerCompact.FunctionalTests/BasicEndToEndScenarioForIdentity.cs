using System.Diagnostics;
using System.Linq;
using Xunit;
#if SQLCE35
#else
using System.Data.SqlServerCe;
#endif
namespace Microsoft.EntityFrameworkCore.FunctionalTests
{
    public class BasicEndToEndScenarioForIdentity
    {
        [Fact]
        public void Can_run_end_to_end_scenario()
        {
            using (var db = new BloggingContext())
            {
                db.Database.Log(x => Debug.WriteLine(x));
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Blogs.Add(new Blog { Url = "http://erikej.blogspot.com" });
                db.SaveChanges();

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
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlCe(
                    new SqlCeConnectionStringBuilder
                    {
                        DataSource = "BloggingIdentity.sdf"
                    }
                    .ConnectionString);
            }
#endif
        }

        public class Blog
        {
            public int Id { get; set; }
            public string Url { get; set; }
        }
    }
}
