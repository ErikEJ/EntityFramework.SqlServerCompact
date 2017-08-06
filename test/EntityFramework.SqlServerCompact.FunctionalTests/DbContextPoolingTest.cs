﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;
using Microsoft.EntityFrameworkCore.Specification.Tests;

// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable UnusedMember.Local
// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable VirtualMemberCallInConstructor
namespace Microsoft.EntityFrameworkCore.SqlServer.FunctionalTests
{
    public class DbContextPoolingTest
    {
        private static IServiceProvider BuildServiceProvider<TContext>(int poolSize = 32)
            where TContext : DbContext
        => new ServiceCollection()
            .AddEntityFrameworkSqlCe()
            .AddDbContextPool<TContext>(
                ob => ob.UseSqlCe(SqlCeTestStore.NorthwindConnectionString),
                poolSize)
            .BuildServiceProvider();

        private class PooledContext : DbContext
        {
            public static int InstanceCount;

            public static bool ModifyOptions;

            public PooledContext(DbContextOptions options)
                : base(options)
            {
                Interlocked.Increment(ref InstanceCount);

                ChangeTracker.AutoDetectChangesEnabled = false;
                Database.AutoTransactionsEnabled = false;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (ModifyOptions)
                {
                    optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                }
            }

            public DbSet<Customer> Customers { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
                => modelBuilder.Entity<Customer>().ToTable("Customers");

            public class Customer
            {
                public string CustomerId { get; set; }
                public string CompanyName { get; set; }
            }
        }

        [Fact]
        public void Invalid_pool_size()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => BuildServiceProvider<PooledContext>(0));

            Assert.Throws<ArgumentOutOfRangeException>(
                () => BuildServiceProvider<PooledContext>(-1));
        }

        [Fact]
        public void Options_modified_in_on_configuring()
        {
            var serviceProvider = BuildServiceProvider<PooledContext>();

            var serviceScope1 = serviceProvider.CreateScope();

            PooledContext.ModifyOptions = true;

            try
            {
                Assert.Throws<InvalidOperationException>(
                    () => serviceScope1.ServiceProvider.GetService<PooledContext>());
            }
            finally
            {
                PooledContext.ModifyOptions = false;
            }
        }

        private class BadCtorContext : DbContext
        {
        }

        [Fact]
        public void Throws_when_used_with_parameterless_constructor_context()
        {
            var serviceCollection = new ServiceCollection();

            Assert.Equal(CoreStrings.DbContextMissingConstructor(nameof(BadCtorContext)),
                Assert.Throws<ArgumentException>(
                    () => serviceCollection.AddDbContextPool<BadCtorContext>(
                        _ => { })).Message);

            Assert.Equal(CoreStrings.DbContextMissingConstructor(nameof(BadCtorContext)),
                Assert.Throws<ArgumentException>(
                    () => serviceCollection.AddDbContextPool<BadCtorContext>(
                        (_, __) => { })).Message);
        }

        [Fact]
        public void Can_pool_non_derived_context()
        {
            var serviceProvider = BuildServiceProvider<DbContext>();

            var serviceScope1 = serviceProvider.CreateScope();

            var context1 = serviceScope1.ServiceProvider.GetService<DbContext>();

            var serviceScope2 = serviceProvider.CreateScope();

            var context2 = serviceScope2.ServiceProvider.GetService<DbContext>();

            Assert.NotSame(context1, context2);

            serviceScope1.Dispose();
            serviceScope2.Dispose();

            var serviceScope3 = serviceProvider.CreateScope();

            var context3 = serviceScope3.ServiceProvider.GetService<DbContext>();

            Assert.Same(context1, context3);

            var serviceScope4 = serviceProvider.CreateScope();

            var context4 = serviceScope4.ServiceProvider.GetService<DbContext>();

            Assert.Same(context2, context4);
        }

        [Fact]
        public void Contexts_are_pooled()
        {
            var serviceProvider = BuildServiceProvider<PooledContext>();

            var serviceScope1 = serviceProvider.CreateScope();

            var context1 = serviceScope1.ServiceProvider.GetService<PooledContext>();

            var serviceScope2 = serviceProvider.CreateScope();

            var context2 = serviceScope2.ServiceProvider.GetService<PooledContext>();

            Assert.NotSame(context1, context2);

            serviceScope1.Dispose();
            serviceScope2.Dispose();

            var serviceScope3 = serviceProvider.CreateScope();

            var context3 = serviceScope3.ServiceProvider.GetService<PooledContext>();

            Assert.Same(context1, context3);

            var serviceScope4 = serviceProvider.CreateScope();

            var context4 = serviceScope4.ServiceProvider.GetService<PooledContext>();

            Assert.Same(context2, context4);
        }

        [Fact]
        public void Context_configuration_is_reset()
        {
            var serviceProvider = BuildServiceProvider<PooledContext>();

            var serviceScope = serviceProvider.CreateScope();

            var context1 = serviceScope.ServiceProvider.GetService<PooledContext>();

            context1.ChangeTracker.AutoDetectChangesEnabled = true;
            context1.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            context1.Database.AutoTransactionsEnabled = true;

            serviceScope.Dispose();

            serviceScope = serviceProvider.CreateScope();

            var context2 = serviceScope.ServiceProvider.GetService<PooledContext>();

            Assert.Same(context1, context2);

            Assert.False(context2.ChangeTracker.AutoDetectChangesEnabled);
            Assert.Equal(QueryTrackingBehavior.TrackAll, context2.ChangeTracker.QueryTrackingBehavior);
            Assert.False(context2.Database.AutoTransactionsEnabled);
        }

        [ConditionalFact]
        [FrameworkSkipCondition(RuntimeFrameworks.CoreCLR, SkipReason = "Failing after netcoreapp2.0 upgrade")]
        public void State_manager_is_reset()
        {
            var serviceProvider = BuildServiceProvider<PooledContext>();

            var serviceScope = serviceProvider.CreateScope();

            var context1 = serviceScope.ServiceProvider.GetService<PooledContext>();

            var weakRef = new WeakReference(context1.Customers.First(c => c.CustomerId == "ALFKI"));

            Assert.Equal(1, context1.ChangeTracker.Entries().Count());

            serviceScope.Dispose();

            serviceScope = serviceProvider.CreateScope();

            var context2 = serviceScope.ServiceProvider.GetService<PooledContext>();

            Assert.Same(context1, context2);
            Assert.Empty(context2.ChangeTracker.Entries());

            GC.Collect();

            Assert.False(weakRef.IsAlive);
        }

        [Fact]
        public void Pool_disposes_context_when_context_not_pooled()
        {
            var serviceProvider = BuildServiceProvider<PooledContext>(poolSize: 1);

            var serviceScope1 = serviceProvider.CreateScope();

            serviceScope1.ServiceProvider.GetService<PooledContext>();

            var serviceScope2 = serviceProvider.CreateScope();

            var context = serviceScope2.ServiceProvider.GetService<PooledContext>();

            serviceScope1.Dispose();
            serviceScope2.Dispose();

            Assert.Throws<ObjectDisposedException>(() => context.Customers.ToList());
        }

        [Fact]
        public void Pool_disposes_contexts_when_disposed()
        {
            var serviceProvider = BuildServiceProvider<PooledContext>();

            var serviceScope = serviceProvider.CreateScope();

            var context = serviceScope.ServiceProvider.GetService<PooledContext>();

            serviceScope.Dispose();

            ((IDisposable)serviceProvider).Dispose();

            Assert.Throws<ObjectDisposedException>(() => context.Customers.ToList());
        }

        [Fact]
        public void Object_in_pool_is_disposed()
        {
            var serviceProvider = BuildServiceProvider<PooledContext>();

            var serviceScope = serviceProvider.CreateScope();

            var context = serviceScope.ServiceProvider.GetService<PooledContext>();

            serviceScope.Dispose();

            Assert.Throws<ObjectDisposedException>(() => context.Customers.ToList());
        }

        [Fact]
        public void Provider_services_are_reset()
        {
            var serviceProvider = BuildServiceProvider<PooledContext>();

            var serviceScope = serviceProvider.CreateScope();

            var context1 = serviceScope.ServiceProvider.GetService<PooledContext>();

            context1.Database.BeginTransaction();

            Assert.NotNull(context1.Database.CurrentTransaction);

            serviceScope.Dispose();

            serviceScope = serviceProvider.CreateScope();

            var context2 = serviceScope.ServiceProvider.GetService<PooledContext>();

            Assert.Same(context1, context2);
            Assert.Null(context2.Database.CurrentTransaction);

            context2.Database.BeginTransaction();

            Assert.NotNull(context2.Database.CurrentTransaction);

            serviceScope.Dispose();

            serviceScope = serviceProvider.CreateScope();

            var context3 = serviceScope.ServiceProvider.GetService<PooledContext>();

            Assert.Same(context2, context3);
            Assert.Null(context3.Database.CurrentTransaction);
        }

        [ConditionalFact(Skip = "ErikEJ investigate fail")]
        public async Task Concurrency_test()
        {
            // This test is for measuring different pooling approaches.

            WriteResults();

            var serviceProvider = BuildServiceProvider<PooledContext>();

            Func<Task> work = async () =>
            {
                while (_stopwatch.IsRunning)
                {
                    using (var serviceScope = serviceProvider.CreateScope())
                    {
                        var context = serviceScope.ServiceProvider.GetService<PooledContext>();

                        await context.Customers.AsNoTracking().FirstAsync(c => c.CustomerId == "ALFKI");

                        Interlocked.Increment(ref _requests);
                    }
                }
            };

            var tasks = new Task[32];

            for (var i = 0; i < 32; i++)
            {
                tasks[i] = work();
            }

            await Task.WhenAll(tasks);

            Assert.InRange(PooledContext.InstanceCount, 30, 50);
        }

        private readonly TimeSpan _duration = TimeSpan.FromSeconds(10);

        private int _stopwatchStarted;

        private readonly Stopwatch _stopwatch = new Stopwatch();

        private long _requests;

        private async void WriteResults()
        {
            if (Interlocked.Exchange(ref _stopwatchStarted, 1) == 0)
            {
                _stopwatch.Start();
            }

            var lastRequests = (long)0;
            var lastElapsed = TimeSpan.Zero;

            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));

                var currentRequests = _requests - lastRequests;
                lastRequests = _requests;

                var elapsed = _stopwatch.Elapsed;
                var currentElapsed = elapsed - lastElapsed;
                lastElapsed = elapsed;

                _testOutputHelper?
                    .WriteLine(
                        $"[{DateTime.Now:HH:mm:ss.fff}] Requests: {_requests}, "
                        + $"RPS: {Math.Round(currentRequests / currentElapsed.TotalSeconds)}");

                if (elapsed > _duration)
                {
                    _testOutputHelper?.WriteLine("");
                    _testOutputHelper?.WriteLine($"Average RPS: {Math.Round(_requests / elapsed.TotalSeconds)}");

                    _stopwatch.Stop();
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private readonly ITestOutputHelper _testOutputHelper = null;

        // ReSharper disable once UnusedParameter.Local
        public DbContextPoolingTest(ITestOutputHelper testOutputHelper)
        {
            //_testOutputHelper = testOutputHelper;
        }
    }
}