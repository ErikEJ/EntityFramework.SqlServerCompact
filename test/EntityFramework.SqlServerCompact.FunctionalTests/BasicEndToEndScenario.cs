using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Data.Entity.Infrastructure;
using Xunit;

namespace Microsoft.Data.Entity.FunctionalTests
{
    public class BasicEndToEndScenario
    {
        [Fact]
        public void Can_run_end_to_end_scenario()
        {
            //Demo of logging both using LogToConsole and an IObserver
            using (var db = new BloggingContext())
            {
                var diag = db.GetService<DiagnosticListener>();
                var result = new List<KeyValuePair<string, object>>();
                var observer = new ObserverToList<KeyValuePair<string, object>>(result);

                using (diag.Subscribe(observer))
                {
                    db.LogToConsole();
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                    db.Blogs.Add(new Blog { BlogId = Guid.NewGuid(), Url = "http://erikej.blogspot.com" });
                    db.SaveChanges();
                }
                var blogs = db.Blogs.ToList();

                Assert.Equal(result.Count, 8);

                Assert.Equal(blogs.Count, 1);
                Assert.Equal(blogs[0].Url, "http://erikej.blogspot.com");
            }
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

    internal class ObserverToList<T> : IObserver<T>
    {
        public ObserverToList(List<T> output, Predicate<T> filter = null, string name = null)
        {
            _output = output;
            _output.Clear();
            _filter = filter;
            _name = name;
        }

        public bool Completed { get; private set; }

        #region private

        public void OnCompleted()
        {
            Completed = true;
        }

        public void OnError(Exception error)
        {
            Assert.True(false, "Error happened on IObserver");
        }

        public void OnNext(T value)
        {
            Assert.False(Completed);
            if (_filter == null || _filter(value))
                _output.Add(value);
        }

        private List<T> _output;
        private Predicate<T> _filter;
        private string _name;  // for debugging 
        #endregion
    }
}
