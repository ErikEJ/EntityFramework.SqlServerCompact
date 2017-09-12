using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class BasicEndToEndScenarioForNoIdentity
    {
        [Fact]
        public void Can_run_end_to_end_scenario()
        {
            using (var db = new BloggingContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                var logo = File.ReadAllBytes("EFCore.png");
                var logoSize = logo.Length;
                db.Blogs.Add(new Blog { Id = 99, Url = "http://erikej.blogspot.com", Logo = logo });
                db.SaveChanges();


                var blogs = db.Blogs.ToList();

                Assert.Equal(blogs.Count, 1);
                Assert.Equal(blogs[0].Url, "http://erikej.blogspot.com");
                Assert.Equal(blogs[0].Id, 99);
                Assert.Equal(logoSize, blogs[0].Logo.Length);
                Assert.True(ByteArrayCompare(logo, blogs[0].Logo));
            }
        }

        private static bool ByteArrayCompare(byte[] a1, byte[] a2)
        {
            if (a1.Length != a2.Length)
                return false;

            for (int i = 0; i < a1.Length; i++)
                if (a1[i] != a2[i])
                    return false;

            return true;
        }

        private class BloggingContext : DbContext
        {
            public DbSet<Blog> Blogs { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlCe(@"Data Source=BloggingNoIdentity.sdf");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Blog>()
                    .Property(e => e.Id)
                    .ValueGeneratedNever();
            }
        }

        private class Blog
        {
            public int Id { get; set; }
            public string Url { get; set; }

            [Column(TypeName = "image")]
            public byte[] Logo { get; set; }
        }
    }
}
