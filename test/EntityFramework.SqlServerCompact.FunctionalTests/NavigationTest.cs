using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Microsoft.Data.Entity.FunctionalTests
{
    public class NavigationTest : IClassFixture<NavigationTestFixture>
    {
        [Fact]
        public void Duplicate_entries_are_not_created_for_navigations_to_principal()
        {
            using (var context = _fixture.CreateContext())
            {
                context.ConfigAction = modelBuilder =>
                {
                    modelBuilder.Entity<GoTPerson>().HasMany(p => p.Siblings).WithOne(p => p.SiblingReverse).IsRequired(false);
                    modelBuilder.Entity<GoTPerson>().HasOne(p => p.Lover).WithOne(p => p.LoverReverse).IsRequired(false);
                    return 0;
                };

                var model = context.Model;
                var entityType = model.GetEntityTypes().First();

                Assert.Equal("'GoTPerson' {'LoverId'} -> 'GoTPerson' {'Id'}", entityType.GetForeignKeys().First().ToString());
                Assert.Equal("'GoTPerson' {'SiblingReverseId'} -> 'GoTPerson' {'Id'}", entityType.GetForeignKeys().Skip(1).First().ToString());
            }
        }

        [Fact]
        public void Duplicate_entries_are_not_created_for_navigations_to_dependant()
        {
            using (var context = _fixture.CreateContext())
            {
                context.ConfigAction = modelBuilder =>
                {
                    modelBuilder.Entity<GoTPerson>().HasOne(p => p.SiblingReverse).WithMany(p => p.Siblings).IsRequired(false);
                    modelBuilder.Entity<GoTPerson>().HasOne(p => p.Lover).WithOne(p => p.LoverReverse).IsRequired(false);
                    return 0;
                };

                var model = context.Model;
                var entityType = model.GetEntityTypes().First();

                Assert.Equal("'GoTPerson' {'LoverId'} -> 'GoTPerson' {'Id'}", entityType.GetForeignKeys().First().ToString());
                Assert.Equal("'GoTPerson' {'SiblingReverseId'} -> 'GoTPerson' {'Id'}", entityType.GetForeignKeys().Skip(1).First().ToString());
            }
        }

        private readonly NavigationTestFixture _fixture;

        public NavigationTest(NavigationTestFixture fixture)
        {
            _fixture = fixture;
        }
    }

    public class GoTPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GoTPerson> Siblings { get; set; }
        public GoTPerson Lover { get; set; }
        public GoTPerson LoverReverse { get; set; }
        public GoTPerson SiblingReverse { get; set; }
    }

    public class NavigationTestFixture
    {
        private readonly DbContextOptions _options;
        private readonly IServiceProvider _serviceProvider;

        public NavigationTestFixture()
        {
            _serviceProvider = new ServiceCollection()
                .AddEntityFramework()
                .AddSqlCe()
                .ServiceCollection()
                .BuildServiceProvider();

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlCe(@"Data Source=NavigationTest.sdf");
            _options = optionsBuilder.Options;
        }

        public virtual GoTContext CreateContext() => new GoTContext(_serviceProvider, _options);

        public class GoTContext : DbContext
        {
            public GoTContext(IServiceProvider serviceProvider, DbContextOptions options)
                : base(serviceProvider, options)
            {
            }

            public DbSet<GoTPerson> People { get; set; }
            public Func<ModelBuilder, int> ConfigAction { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                ConfigAction.Invoke(modelBuilder);
            }
        }
    }
}
