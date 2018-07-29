﻿using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Microsoft.EntityFrameworkCore
{
    public class LazyLoadProxySqlCeTest : LazyLoadProxyTestBase<LazyLoadProxySqlCeTest.LoadSqlCeFixture>
    {
        public LazyLoadProxySqlCeTest(LoadSqlCeFixture fixture)
            : base(fixture)
        {
            fixture.TestSqlLoggerFactory.Clear();
        }

        public override void Lazy_load_collection(EntityState state, bool useAttach, bool useDetach)
        {
            base.Lazy_load_collection(state, useAttach, useDetach);

            Assert.Equal(
                @"@__get_Item_0='707' (Nullable = true)

SELECT [e].[Id], [e].[ParentId]
FROM [Child] AS [e]
WHERE [e].[ParentId] = @__get_Item_0",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_many_to_one_reference_to_principal(EntityState state, bool useAttach, bool useDetach)
        {
            base.Lazy_load_many_to_one_reference_to_principal(state, useAttach, useDetach);

            Assert.Equal(
                @"@__get_Item_0='707'

SELECT [e].[Id], [e].[AlternateId]
FROM [Parent] AS [e]
WHERE [e].[Id] = @__get_Item_0",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_one_to_one_reference_to_principal(EntityState state, bool useAttach, bool useDetach)
        {
            base.Lazy_load_one_to_one_reference_to_principal(state, useAttach, useDetach);

            Assert.Equal(
                @"@__get_Item_0='707'

SELECT [e].[Id], [e].[AlternateId]
FROM [Parent] AS [e]
WHERE [e].[Id] = @__get_Item_0",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_one_to_one_reference_to_dependent(EntityState state, bool useAttach, bool useDetach)
        {
            base.Lazy_load_one_to_one_reference_to_dependent(state, useAttach, useDetach);

            Assert.Equal(
                @"@__get_Item_0='707' (Nullable = true)

SELECT [e].[Id], [e].[ParentId]
FROM [Single] AS [e]
WHERE [e].[ParentId] = @__get_Item_0",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_one_to_one_PK_to_PK_reference_to_principal(EntityState state)
        {
            base.Lazy_load_one_to_one_PK_to_PK_reference_to_principal(state);

            Assert.Equal(
                @"@__get_Item_0='707'

SELECT [e].[Id], [e].[AlternateId]
FROM [Parent] AS [e]
WHERE [e].[Id] = @__get_Item_0",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_one_to_one_PK_to_PK_reference_to_dependent(EntityState state)
        {
            base.Lazy_load_one_to_one_PK_to_PK_reference_to_dependent(state);

            Assert.Equal(
                @"@__get_Item_0='707'

SELECT [e].[Id]
FROM [SinglePkToPk] AS [e]
WHERE [e].[Id] = @__get_Item_0",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_many_to_one_reference_to_principal_null_FK(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_null_FK(state);

            Assert.Equal("", Sql);
        }

        public override void Lazy_load_one_to_one_reference_to_principal_null_FK(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_null_FK(state);

            Assert.Equal("", Sql);
        }

        public override void Lazy_load_collection_not_found(EntityState state)
        {
            base.Lazy_load_collection_not_found(state);

            Assert.Equal(
                @"@__get_Item_0='767' (Nullable = true)

SELECT [e].[Id], [e].[ParentId]
FROM [Child] AS [e]
WHERE [e].[ParentId] = @__get_Item_0",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_many_to_one_reference_to_principal_not_found(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_not_found(state);

            Assert.Equal(
                @"@__get_Item_0='787'

SELECT [e].[Id], [e].[AlternateId]
FROM [Parent] AS [e]
WHERE [e].[Id] = @__get_Item_0",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_one_to_one_reference_to_principal_not_found(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_not_found(state);

            Assert.Equal(
                @"@__get_Item_0='787'

SELECT [e].[Id], [e].[AlternateId]
FROM [Parent] AS [e]
WHERE [e].[Id] = @__get_Item_0",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_one_to_one_reference_to_dependent_not_found(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_not_found(state);

            Assert.Equal(
                @"@__get_Item_0='767' (Nullable = true)

SELECT [e].[Id], [e].[ParentId]
FROM [Single] AS [e]
WHERE [e].[ParentId] = @__get_Item_0",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_collection_already_loaded(EntityState state)
        {
            base.Lazy_load_collection_already_loaded(state);

            Assert.Equal("", Sql);
        }

        public override void Lazy_load_many_to_one_reference_to_principal_already_loaded(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_already_loaded(state);

            Assert.Equal("", Sql);
        }

        public override void Lazy_load_one_to_one_reference_to_principal_already_loaded(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_already_loaded(state);

            Assert.Equal("", Sql);
        }

        public override void Lazy_load_one_to_one_reference_to_dependent_already_loaded(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_already_loaded(state);

            Assert.Equal("", Sql);
        }

        public override void Lazy_load_one_to_one_PK_to_PK_reference_to_principal_already_loaded(EntityState state)
        {
            base.Lazy_load_one_to_one_PK_to_PK_reference_to_principal_already_loaded(state);

            Assert.Equal("", Sql);
        }

        public override void Lazy_load_one_to_one_PK_to_PK_reference_to_dependent_already_loaded(EntityState state)
        {
            base.Lazy_load_one_to_one_PK_to_PK_reference_to_dependent_already_loaded(state);

            Assert.Equal("", Sql);
        }

        public override void Lazy_load_many_to_one_reference_to_principal_alternate_key(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_alternate_key(state);

            Assert.Equal(
                @"@__get_Item_0='Root' (Size = 256)

SELECT [e].[Id], [e].[AlternateId]
FROM [Parent] AS [e]
WHERE [e].[AlternateId] = @__get_Item_0",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_one_to_one_reference_to_principal_alternate_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_alternate_key(state);

            Assert.Equal(
                @"@__get_Item_0='Root' (Size = 256)

SELECT [e].[Id], [e].[AlternateId]
FROM [Parent] AS [e]
WHERE [e].[AlternateId] = @__get_Item_0",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_one_to_one_reference_to_dependent_alternate_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_alternate_key(state);

            Assert.Equal(
                @"@__get_Item_0='Root' (Size = 256)

SELECT [e].[Id], [e].[ParentId]
FROM [SingleAk] AS [e]
WHERE [e].[ParentId] = @__get_Item_0",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_many_to_one_reference_to_principal_null_FK_alternate_key(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_null_FK_alternate_key(state);

            Assert.Equal("", Sql);
        }

        public override void Lazy_load_one_to_one_reference_to_principal_null_FK_alternate_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_null_FK_alternate_key(state);

            Assert.Equal("", Sql);
        }

        public override void Lazy_load_collection_shadow_fk(EntityState state)
        {
            base.Lazy_load_collection_shadow_fk(state);

            Assert.Equal(
                @"@__get_Item_0='707' (Nullable = true)

SELECT [e].[Id], [e].[ParentId]
FROM [ChildShadowFk] AS [e]
WHERE [e].[ParentId] = @__get_Item_0",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_many_to_one_reference_to_principal_shadow_fk(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_shadow_fk(state);

            Assert.Equal(
                @"@__get_Item_0='707'

SELECT [e].[Id], [e].[AlternateId]
FROM [Parent] AS [e]
WHERE [e].[Id] = @__get_Item_0",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_one_to_one_reference_to_principal_shadow_fk(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_shadow_fk(state);

            Assert.Equal(
                @"@__get_Item_0='707'

SELECT [e].[Id], [e].[AlternateId]
FROM [Parent] AS [e]
WHERE [e].[Id] = @__get_Item_0",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_one_to_one_reference_to_dependent_shadow_fk(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_shadow_fk(state);

            Assert.Equal(
                @"@__get_Item_0='707' (Nullable = true)

SELECT [e].[Id], [e].[ParentId]
FROM [SingleShadowFk] AS [e]
WHERE [e].[ParentId] = @__get_Item_0",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_many_to_one_reference_to_principal_null_FK_shadow_fk(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_null_FK_shadow_fk(state);

            Assert.Equal("", Sql);
        }

        public override void Lazy_load_one_to_one_reference_to_principal_null_FK_shadow_fk(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_null_FK_shadow_fk(state);

            Assert.Equal("", Sql);
        }

        public override void Lazy_load_collection_composite_key(EntityState state)
        {
            base.Lazy_load_collection_composite_key(state);

            Assert.Equal(
                @"@__get_Item_0='Root' (Size = 256)
@__get_Item_1='707' (Nullable = true)

SELECT [e].[Id], [e].[ParentAlternateId], [e].[ParentId]
FROM [ChildCompositeKey] AS [e]
WHERE ([e].[ParentAlternateId] = @__get_Item_0) AND ([e].[ParentId] = @__get_Item_1)",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_many_to_one_reference_to_principal_composite_key(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_composite_key(state);

            Assert.Equal(
                @"@__get_Item_0='Root' (Size = 256)
@__get_Item_1='707'

SELECT [e].[Id], [e].[AlternateId]
FROM [Parent] AS [e]
WHERE ([e].[AlternateId] = @__get_Item_0) AND ([e].[Id] = @__get_Item_1)",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_one_to_one_reference_to_principal_composite_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_composite_key(state);

            Assert.Equal(
                @"@__get_Item_0='Root' (Size = 256)
@__get_Item_1='707'

SELECT [e].[Id], [e].[AlternateId]
FROM [Parent] AS [e]
WHERE ([e].[AlternateId] = @__get_Item_0) AND ([e].[Id] = @__get_Item_1)",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_one_to_one_reference_to_dependent_composite_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_dependent_composite_key(state);

            Assert.Equal(
                @"@__get_Item_0='Root' (Size = 256)
@__get_Item_1='707' (Nullable = true)

SELECT [e].[Id], [e].[ParentAlternateId], [e].[ParentId]
FROM [SingleCompositeKey] AS [e]
WHERE ([e].[ParentAlternateId] = @__get_Item_0) AND ([e].[ParentId] = @__get_Item_1)",
                Sql,
                ignoreLineEndingDifferences: true);
        }

        public override void Lazy_load_many_to_one_reference_to_principal_null_FK_composite_key(EntityState state)
        {
            base.Lazy_load_many_to_one_reference_to_principal_null_FK_composite_key(state);

            Assert.Equal("", Sql);
        }

        public override void Lazy_load_one_to_one_reference_to_principal_null_FK_composite_key(EntityState state)
        {
            base.Lazy_load_one_to_one_reference_to_principal_null_FK_composite_key(state);

            Assert.Equal("", Sql);
        }

        public override async Task Load_collection(EntityState state, bool async)
        {
            await base.Load_collection(state, async);

            if (!async)
            {
                Assert.Equal(
                    @"@__get_Item_0='707' (Nullable = true)

SELECT [e].[Id], [e].[ParentId]
FROM [Child] AS [e]
WHERE [e].[ParentId] = @__get_Item_0",
                    Sql,
                    ignoreLineEndingDifferences: true);
            }
        }

        protected override void ClearLog() => Fixture.TestSqlLoggerFactory.Clear();

        protected override void RecordLog() => Sql = Fixture.TestSqlLoggerFactory.Sql;

        private string Sql { get; set; }

        public class LoadSqlCeFixture : LoadFixtureBase
        {
            public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ServiceProvider.GetRequiredService<ILoggerFactory>();
            protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;

            public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
                => base.AddOptions(builder).ConfigureWarnings(
                    c => c
                        .Log(RelationalEventId.QueryClientEvaluationWarning));
        }
    }
}
