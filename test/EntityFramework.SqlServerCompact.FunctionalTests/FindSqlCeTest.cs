using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Specification.Tests.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public abstract class FindSqlCeTest :  FindTestBase<SqlCeTestStore, FindSqlCeTest.FindSqlCeFixture>
    {
        protected FindSqlCeTest(FindSqlCeFixture fixture)
            : base(fixture)
        {
            fixture.TestSqlLoggerFactory.Clear();
        }

        public class FindSqlCeTestSet : FindSqlCeTest
        {
            public FindSqlCeTestSet(FindSqlCeFixture fixture)
                : base(fixture)
            {
            }

            protected override TEntity Find<TEntity>(DbContext context, params object[] keyValues)
                => context.Set<TEntity>().Find(keyValues);

            protected override Task<TEntity> FindAsync<TEntity>(DbContext context, params object[] keyValues)
                => context.Set<TEntity>().FindAsync(keyValues);
        }

        public class FindSqlCeTestContext : FindSqlCeTest
        {
            public FindSqlCeTestContext(FindSqlCeFixture fixture)
                : base(fixture)
            {
            }

            protected override TEntity Find<TEntity>(DbContext context, params object[] keyValues)
                => context.Find<TEntity>(keyValues);

            protected override Task<TEntity> FindAsync<TEntity>(DbContext context, params object[] keyValues)
                => context.FindAsync<TEntity>(keyValues);
        }

        public class FindSqlCeTestNonGeneric : FindSqlCeTest
        {
            public FindSqlCeTestNonGeneric(FindSqlCeFixture fixture)
                : base(fixture)
            {
            }

            protected override TEntity Find<TEntity>(DbContext context, params object[] keyValues)
                => (TEntity)context.Find(typeof(TEntity), keyValues);

            protected override async Task<TEntity> FindAsync<TEntity>(DbContext context, params object[] keyValues)
                => (TEntity)await context.FindAsync(typeof(TEntity), keyValues);
        }

        [Fact]
        public override void Find_int_key_tracked()
        {
            base.Find_int_key_tracked();

            Assert.Equal("", Sql);
        }

        [Fact]
        public override void Find_int_key_from_store()
        {
            base.Find_int_key_from_store();

            Assert.Equal(
                @"@__get_Item_0='77'

SELECT TOP(1) [e].[Id], [e].[Foo]
FROM [IntKey] AS [e]
WHERE [e].[Id] = @__get_Item_0", Sql);
        }

        [Fact]
        public override void Returns_null_for_int_key_not_in_store()
        {
            base.Returns_null_for_int_key_not_in_store();

            Assert.Equal(
                @"@__get_Item_0='99'

SELECT TOP(1) [e].[Id], [e].[Foo]
FROM [IntKey] AS [e]
WHERE [e].[Id] = @__get_Item_0", Sql);
        }

        [Fact]
        public override void Find_string_key_tracked()
        {
            base.Find_string_key_tracked();

            Assert.Equal("", Sql);
        }

        [Fact]
        public override void Find_string_key_from_store()
        {
            base.Find_string_key_from_store();

            Assert.Equal(
                @"@__get_Item_0='Cat' (Size = 256)

SELECT TOP(1) [e].[Id], [e].[Foo]
FROM [StringKey] AS [e]
WHERE [e].[Id] = @__get_Item_0", Sql);
        }

        [Fact]
        public override void Returns_null_for_string_key_not_in_store()
        {
            base.Returns_null_for_string_key_not_in_store();

            Assert.Equal(
                @"@__get_Item_0='Fox' (Size = 256)

SELECT TOP(1) [e].[Id], [e].[Foo]
FROM [StringKey] AS [e]
WHERE [e].[Id] = @__get_Item_0", Sql);
        }

        [Fact]
        public override void Find_composite_key_tracked()
        {
            base.Find_composite_key_tracked();

            Assert.Equal("", Sql);
        }

        [Fact]
        public override void Find_composite_key_from_store()
        {
            base.Find_composite_key_from_store();

            Assert.Equal(
                @"@__get_Item_0='77'
@__get_Item_1='Dog' (Size = 256)

SELECT TOP(1) [e].[Id1], [e].[Id2], [e].[Foo]
FROM [CompositeKey] AS [e]
WHERE ([e].[Id1] = @__get_Item_0) AND ([e].[Id2] = @__get_Item_1)", Sql);
        }

        [Fact]
        public override void Returns_null_for_composite_key_not_in_store()
        {
            base.Returns_null_for_composite_key_not_in_store();

            Assert.Equal(
                @"@__get_Item_0='77'
@__get_Item_1='Fox' (Size = 256)

SELECT TOP(1) [e].[Id1], [e].[Id2], [e].[Foo]
FROM [CompositeKey] AS [e]
WHERE ([e].[Id1] = @__get_Item_0) AND ([e].[Id2] = @__get_Item_1)", Sql);
        }

        [Fact]
        public override void Find_base_type_tracked()
        {
            base.Find_base_type_tracked();

            Assert.Equal("", Sql);
        }

        [Fact]
        public override void Find_base_type_from_store()
        {
            base.Find_base_type_from_store();

            Assert.Equal(
                @"@__get_Item_0='77'

SELECT TOP(1) [e].[Id], [e].[Discriminator], [e].[Foo], [e].[Boo]
FROM [BaseType] AS [e]
WHERE [e].[Discriminator] IN (N'DerivedType', N'BaseType') AND ([e].[Id] = @__get_Item_0)", Sql);
        }

        [Fact]
        public override void Returns_null_for_base_type_not_in_store()
        {
            base.Returns_null_for_base_type_not_in_store();

            Assert.Equal(
                @"@__get_Item_0='99'

SELECT TOP(1) [e].[Id], [e].[Discriminator], [e].[Foo], [e].[Boo]
FROM [BaseType] AS [e]
WHERE [e].[Discriminator] IN (N'DerivedType', N'BaseType') AND ([e].[Id] = @__get_Item_0)", Sql);
        }

        [Fact]
        public override void Find_derived_type_tracked()
        {
            base.Find_derived_type_tracked();

            Assert.Equal("", Sql);
        }

        [Fact]
        public override void Find_derived_type_from_store()
        {
            base.Find_derived_type_from_store();

            Assert.Equal(
                @"@__get_Item_0='78'

SELECT TOP(1) [e].[Id], [e].[Discriminator], [e].[Foo], [e].[Boo]
FROM [BaseType] AS [e]
WHERE ([e].[Discriminator] = N'DerivedType') AND ([e].[Id] = @__get_Item_0)", Sql);
        }

        [Fact]
        public override void Returns_null_for_derived_type_not_in_store()
        {
            base.Returns_null_for_derived_type_not_in_store();

            Assert.Equal(
                @"@__get_Item_0='99'

SELECT TOP(1) [e].[Id], [e].[Discriminator], [e].[Foo], [e].[Boo]
FROM [BaseType] AS [e]
WHERE ([e].[Discriminator] = N'DerivedType') AND ([e].[Id] = @__get_Item_0)", Sql);
        }

        [Fact]
        public override void Find_base_type_using_derived_set_tracked()
        {
            base.Find_base_type_using_derived_set_tracked();

            Assert.Equal(
                @"@__get_Item_0='88'

SELECT TOP(1) [e].[Id], [e].[Discriminator], [e].[Foo], [e].[Boo]
FROM [BaseType] AS [e]
WHERE ([e].[Discriminator] = N'DerivedType') AND ([e].[Id] = @__get_Item_0)", Sql);
        }

        [Fact]
        public override void Find_base_type_using_derived_set_from_store()
        {
            base.Find_base_type_using_derived_set_from_store();

            Assert.Equal(
                @"@__get_Item_0='77'

SELECT TOP(1) [e].[Id], [e].[Discriminator], [e].[Foo], [e].[Boo]
FROM [BaseType] AS [e]
WHERE ([e].[Discriminator] = N'DerivedType') AND ([e].[Id] = @__get_Item_0)", Sql);
        }

        [Fact]
        public override void Find_derived_type_using_base_set_tracked()
        {
            base.Find_derived_type_using_base_set_tracked();

            Assert.Equal("", Sql);
        }

        [Fact]
        public override void Find_derived_using_base_set_type_from_store()
        {
            base.Find_derived_using_base_set_type_from_store();

            Assert.Equal(
                @"@__get_Item_0='78'

SELECT TOP(1) [e].[Id], [e].[Discriminator], [e].[Foo], [e].[Boo]
FROM [BaseType] AS [e]
WHERE [e].[Discriminator] IN (N'DerivedType', N'BaseType') AND ([e].[Id] = @__get_Item_0)", Sql);
        }

        [Fact]
        public override void Find_shadow_key_tracked()
        {
            base.Find_shadow_key_tracked();

            Assert.Equal("", Sql);
        }

        [Fact]
        public override void Find_shadow_key_from_store()
        {
            base.Find_shadow_key_from_store();

            Assert.Equal(
                @"@__get_Item_0='77'

SELECT TOP(1) [e].[Id], [e].[Foo]
FROM [ShadowKey] AS [e]
WHERE [e].[Id] = @__get_Item_0", Sql);
        }

        [Fact]
        public override void Returns_null_for_shadow_key_not_in_store()
        {
            base.Returns_null_for_shadow_key_not_in_store();

            Assert.Equal(
                @"@__get_Item_0='99'

SELECT TOP(1) [e].[Id], [e].[Foo]
FROM [ShadowKey] AS [e]
WHERE [e].[Id] = @__get_Item_0", Sql);
        }

        private const string FileLineEnding = @"
";

        private string Sql => Fixture.TestSqlLoggerFactory.Sql.Replace(Environment.NewLine, FileLineEnding);

        public class FindSqlCeFixture : FindFixtureBase
        {
            private const string DatabaseName = "FindTest";
            private readonly DbContextOptions _options;

            public TestSqlLoggerFactory TestSqlLoggerFactory { get; } = new TestSqlLoggerFactory();

            public FindSqlCeFixture()
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkSqlCe()
                    .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                    .AddSingleton<ILoggerFactory>(TestSqlLoggerFactory)
                    .BuildServiceProvider();
                    
                _options = new DbContextOptionsBuilder()
                    .UseSqlCe(SqlCeTestStore.CreateConnectionString(DatabaseName), b => b.ApplyConfiguration())
                    .UseInternalServiceProvider(serviceProvider)
                    .EnableSensitiveDataLogging()
                    .Options;
            }

            public override SqlCeTestStore CreateTestStore()
            {
                return SqlCeTestStore.GetOrCreateShared(DatabaseName, () =>
                {
                    using (var context = new FindContext(_options))
                    {
                        context.Database.EnsureClean();
                        Seed(context);
                    }
                });
            }

            public override DbContext CreateContext(SqlCeTestStore testStore)
                => new FindContext(_options);
        }
    }
}
