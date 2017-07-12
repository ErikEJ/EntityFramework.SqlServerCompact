﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Specification.Tests.TestModels;
using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class SqlCeConfigPatternsTest
    {
        public class ImplicitServicesAndConfig
        {
            [Fact]
            public async Task Can_query_with_implicit_services_and_OnConfiguring()
            {
                using (SqlCeNorthwindContext.GetSharedStore())
                {
                    using (var context = new NorthwindContext())
                    {
                        Assert.Equal(91, await context.Customers.CountAsync());
                    }
                }
            }

            private class NorthwindContext : DbContext
            {
                public DbSet<Customer> Customers { get; set; }

                protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                    => optionsBuilder.UseSqlCe(SqlCeNorthwindContext.ConnectionString, b => b.ApplyConfiguration());

                protected override void OnModelCreating(ModelBuilder modelBuilder)
                    => ConfigureModel(modelBuilder);
            }
        }

        public class ImplicitServicesExplicitConfig
        {
            [Fact]
            public async Task Can_query_with_implicit_services_and_explicit_config()
            {
                using (SqlCeNorthwindContext.GetSharedStore())
                {
                    using (var context = new NorthwindContext(
                        new DbContextOptionsBuilder()
                            .UseSqlCe(SqlCeNorthwindContext.ConnectionString, b => b.ApplyConfiguration()).Options))
                    {
                        Assert.Equal(91, await context.Customers.CountAsync());
                    }
                }
            }

            private class NorthwindContext : DbContext
            {
                public NorthwindContext(DbContextOptions options)
                    : base(options)
                {
                }

                public DbSet<Customer> Customers { get; set; }

                protected override void OnModelCreating(ModelBuilder modelBuilder)
                    => ConfigureModel(modelBuilder);
            }
        }

        public class ExplicitServicesImplicitConfig
        {
            [Fact]
            public async Task Can_query_with_explicit_services_and_OnConfiguring()
            {
                using (SqlCeNorthwindContext.GetSharedStore())
                {
                    using (var context = new NorthwindContext(
                        new DbContextOptionsBuilder().UseInternalServiceProvider(
                            new ServiceCollection()
                                .AddEntityFrameworkSqlCe()
                                .BuildServiceProvider()).Options))
                    {
                        Assert.Equal(91, await context.Customers.CountAsync());
                    }
                }
            }

            private class NorthwindContext : DbContext
            {
                public NorthwindContext(DbContextOptions options)
                    : base(options)
                {
                }

                public DbSet<Customer> Customers { get; set; }

                protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                    => optionsBuilder.UseSqlCe(SqlCeNorthwindContext.ConnectionString, b => b.ApplyConfiguration());

                protected override void OnModelCreating(ModelBuilder modelBuilder)
                    => ConfigureModel(modelBuilder);
            }
        }

        public class ExplicitServicesAndConfig
        {
            [Fact]
            public async Task Can_query_with_explicit_services_and_explicit_config()
            {
                using (SqlCeNorthwindContext.GetSharedStore())
                {
                    using (var context = new NorthwindContext(new DbContextOptionsBuilder()
                        .UseSqlCe(SqlCeNorthwindContext.ConnectionString, b => b.ApplyConfiguration())
                        .UseInternalServiceProvider(new ServiceCollection()
                            .AddEntityFrameworkSqlCe()
                            .BuildServiceProvider()).Options))
                    {
                        Assert.Equal(91, await context.Customers.CountAsync());
                    }
                }
            }

            private class NorthwindContext : DbContext
            {
                public NorthwindContext(DbContextOptions options)
                    : base(options)
                {
                }

                public DbSet<Customer> Customers { get; set; }

                protected override void OnModelCreating(ModelBuilder modelBuilder)
                    => ConfigureModel(modelBuilder);
            }
        }

        public class ExplicitServicesAndNoConfig
        {
            [Fact]
            public void Throws_on_attempt_to_use_SQL_Server_without_providing_connection_string()
            {
                using (SqlCeNorthwindContext.GetSharedStore())
                {
                    Assert.Equal(
                        CoreStrings.NoProviderConfigured,
                        Assert.Throws<InvalidOperationException>(() =>
                        {
                            using (var context = new NorthwindContext(
                                new DbContextOptionsBuilder().UseInternalServiceProvider(new ServiceCollection()
                                    .AddEntityFrameworkSqlCe()
                                    .BuildServiceProvider()).Options))
                            {
                                Assert.Equal(91, context.Customers.Count());
                            }
                        }).Message);
                }
            }

            private class NorthwindContext : DbContext
            {
                public NorthwindContext(DbContextOptions options)
                    : base(options)
                {
                }

                public DbSet<Customer> Customers { get; set; }

                protected override void OnModelCreating(ModelBuilder modelBuilder)
                    => ConfigureModel(modelBuilder);
            }
        }

        public class NoServicesAndNoConfig
        {
            [Fact]
            public void Throws_on_attempt_to_use_context_with_no_store()
            {
                using (SqlCeNorthwindContext.GetSharedStore())
                {
                    Assert.Equal(
                        CoreStrings.NoProviderConfigured,
                        Assert.Throws<InvalidOperationException>(() =>
                        {
                            using (var context = new NorthwindContext())
                            {
                                Assert.Equal(91, context.Customers.Count());
                            }
                        }).Message);
                }
            }

            private class NorthwindContext : DbContext
            {
                public DbSet<Customer> Customers { get; set; }

                protected override void OnModelCreating(ModelBuilder modelBuilder)
                    => ConfigureModel(modelBuilder);
            }
        }

        public class ImplicitConfigButNoServices
        {
            [Fact]
            public void Throws_on_attempt_to_use_store_with_no_store_services()
            {
                var serviceCollection = new ServiceCollection();
                new EntityFrameworkServicesBuilder(serviceCollection).TryAddCoreServices();
                var serviceProvider = serviceCollection.BuildServiceProvider();

                using (SqlCeNorthwindContext.GetSharedStore())
                {
                    Assert.Equal(
                        CoreStrings.NoProviderConfigured,
                        Assert.Throws<InvalidOperationException>(() =>
                        {
                            using (var context = new NorthwindContext(
                                new DbContextOptionsBuilder()
                                    .UseInternalServiceProvider(serviceProvider).Options))
                            {
                                Assert.Equal(91, context.Customers.Count());
                            }
                        }).Message);
                }
            }

            private class NorthwindContext : DbContext
            {
                public NorthwindContext(DbContextOptions options)
                    : base(options)
                {
                }

                public DbSet<Customer> Customers { get; set; }

                protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
                    optionsBuilder.UseSqlCe(SqlCeNorthwindContext.ConnectionString, b => b.ApplyConfiguration());

                protected override void OnModelCreating(ModelBuilder modelBuilder)
                    => ConfigureModel(modelBuilder);
            }
        }

        public class InjectContext
        {
            [Fact]
            public async Task Can_register_context_with_DI_container_and_have_it_injected()
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkSqlCe()
                    .AddTransient<NorthwindContext>()
                    .AddTransient<MyController>()
                    .AddSingleton(p => new DbContextOptionsBuilder().UseInternalServiceProvider(p).Options)
                    .BuildServiceProvider();

                using (SqlCeNorthwindContext.GetSharedStore())
                {
                    await serviceProvider.GetRequiredService<MyController>().TestAsync();
                }
            }

            private class MyController
            {
                private readonly NorthwindContext _context;

                public MyController(NorthwindContext context)
                {
                    Assert.NotNull(context);

                    _context = context;
                }

                public async Task TestAsync()
                    => Assert.Equal(91, await _context.Customers.CountAsync());
            }

            private class NorthwindContext : DbContext
            {
                public NorthwindContext(DbContextOptions options)
                    : base(options)
                {
                    Assert.NotNull(options);
                }

                public DbSet<Customer> Customers { get; set; }

                protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                    => optionsBuilder.UseSqlCe(SqlCeNorthwindContext.ConnectionString, b => b.ApplyConfiguration());

                protected override void OnModelCreating(ModelBuilder modelBuilder)
                    => ConfigureModel(modelBuilder);
            }
        }

        public class InjectContextAndConfiguration
        {
            [Fact]
            public async Task Can_register_context_and_configuration_with_DI_container_and_have_both_injected()
            {
                var serviceProvider = new ServiceCollection()
                    .AddTransient<MyController>()
                    .AddTransient<NorthwindContext>()
                    .AddSingleton(new DbContextOptionsBuilder()
                        .UseSqlCe(SqlCeNorthwindContext.ConnectionString, b => b.ApplyConfiguration()).Options).BuildServiceProvider();

                using (SqlCeNorthwindContext.GetSharedStore())
                {
                    await serviceProvider.GetRequiredService<MyController>().TestAsync();
                }
            }

            private class MyController
            {
                private readonly NorthwindContext _context;

                public MyController(NorthwindContext context)
                {
                    Assert.NotNull(context);

                    _context = context;
                }

                public async Task TestAsync()
                    => Assert.Equal(91, await _context.Customers.CountAsync());
            }

            private class NorthwindContext : DbContext
            {
                public NorthwindContext(DbContextOptions options)
                    : base(options)
                {
                    Assert.NotNull(options);
                }

                public DbSet<Customer> Customers { get; set; }

                protected override void OnModelCreating(ModelBuilder modelBuilder)
                    => ConfigureModel(modelBuilder);
            }
        }

        public class ConstructorArgsToBuilder
        {
            [Fact]
            public async Task Can_pass_context_options_to_constructor_and_use_in_builder()
            {
                using (SqlCeNorthwindContext.GetSharedStore())
                {
                    using (var context = new NorthwindContext(new DbContextOptionsBuilder()
                        .UseSqlCe(SqlCeNorthwindContext.ConnectionString, b => b.ApplyConfiguration()).Options))
                    {
                        Assert.Equal(91, await context.Customers.CountAsync());
                    }
                }
            }

            private class NorthwindContext : DbContext
            {
                public NorthwindContext(DbContextOptions options)
                    : base(options)
                {
                }

                public DbSet<Customer> Customers { get; set; }

                protected override void OnModelCreating(ModelBuilder modelBuilder)
                    => ConfigureModel(modelBuilder);
            }
        }

        public class ConstructorArgsToOnConfiguring
        {
            [Fact]
            public async Task Can_pass_connection_string_to_constructor_and_use_in_OnConfiguring()
            {
                using (SqlCeNorthwindContext.GetSharedStore())
                {
                    using (var context = new NorthwindContext(SqlCeNorthwindContext.ConnectionString))
                    {
                        Assert.Equal(91, await context.Customers.CountAsync());
                    }
                }
            }

            private class NorthwindContext : DbContext
            {
                private readonly string _connectionString;

                public NorthwindContext(string connectionString)
                {
                    _connectionString = connectionString;
                }

                public DbSet<Customer> Customers { get; set; }

                protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                    => optionsBuilder.UseSqlCe(_connectionString, b => b.ApplyConfiguration());

                protected override void OnModelCreating(ModelBuilder modelBuilder)
                    => ConfigureModel(modelBuilder);
            }
        }

        public class NestedContext
        {
            [Fact]
            public async Task Can_use_one_context_nested_inside_another_of_the_same_type()
            {
                using (SqlCeNorthwindContext.GetSharedStore())
                {
                    var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkSqlCe()
                        .BuildServiceProvider();

                    using (var context1 = new NorthwindContext(serviceProvider))
                    {
                        var customers1 = await context1.Customers.ToListAsync();
                        Assert.Equal(91, customers1.Count);
                        Assert.Equal(91, context1.ChangeTracker.Entries().Count());

                        using (var context2 = new NorthwindContext(serviceProvider))
                        {
                            Assert.Equal(0, context2.ChangeTracker.Entries().Count());

                            var customers2 = await context2.Customers.ToListAsync();
                            Assert.Equal(91, customers2.Count);
                            Assert.Equal(91, context2.ChangeTracker.Entries().Count());

                            Assert.Equal(customers1[0].CustomerID, customers2[0].CustomerID);
                            Assert.NotSame(customers1[0], customers2[0]);
                        }
                    }
                }
            }

            private class NorthwindContext : DbContext
            {
                private readonly IServiceProvider _serviceProvider;

                public NorthwindContext(IServiceProvider serviceProvider)
                {
                    _serviceProvider = serviceProvider;
                }

                public DbSet<Customer> Customers { get; set; }

                protected override void OnModelCreating(ModelBuilder modelBuilder)
                    => ConfigureModel(modelBuilder);

                protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
                    .UseInternalServiceProvider(_serviceProvider)
                    .UseSqlCe(SqlCeNorthwindContext.ConnectionString, b => b.ApplyConfiguration());
            }
        }

        private class Customer
        {
            public string CustomerID { get; set; }
            public string CompanyName { get; set; }
            public string Fax { get; set; }
        }

        private static void ConfigureModel(ModelBuilder builder)
            => builder.Entity<Customer>(b =>
            {
                b.HasKey(c => c.CustomerID);
                b.ToTable("Customers");
            });
    }
}