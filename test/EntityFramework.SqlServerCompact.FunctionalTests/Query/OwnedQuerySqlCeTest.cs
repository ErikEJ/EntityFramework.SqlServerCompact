﻿using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class OwnedQuerySqlCeTest : OwnedQueryTestBase<OwnedQuerySqlCeTest.OwnedQuerySqlCeFixture>
    {
        public OwnedQuerySqlCeTest(OwnedQuerySqlCeFixture fixture)
            : base(fixture)
        {
            fixture.TestSqlLoggerFactory.Clear();
        }


        [Fact(Skip = "#8973")]
        public override void No_ignored_include_warning_when_implicit_load()
        {
            base.No_ignored_include_warning_when_implicit_load();
        }

        [Fact(Skip = "#8973")]
        public override void Query_for_base_type_loads_all_owned_navs()
        {
            base.Query_for_base_type_loads_all_owned_navs();

            AssertSql("");
        }

        [Fact(Skip = "#8973")]
        public override void Query_for_branch_type_loads_all_owned_navs()
        {
            base.Query_for_branch_type_loads_all_owned_navs();

            AssertSql("");
        }

        [Fact(Skip = "#8973")]
        public override void Query_for_leaf_type_loads_all_owned_navs()
        {
            base.Query_for_leaf_type_loads_all_owned_navs();

            AssertSql(
                @"SELECT [o].[Id], [o].[Discriminator], [o].[Id], [o].[Id], [o].[LeafAAddress_Country_Name], [o].[Id], [o].[Id], [o].[BranchAddress_Country_Name], [o].[Id], [o].[Id], [o].[PersonAddress_Country_Name]
FROM [OwnedPerson] AS [o]
WHERE [o].[Discriminator] = N'LeafA'");
        }

        [Fact(Skip = "#8973")]
        public override void Query_when_group_by()
        {
            base.Query_when_group_by();
        }

        [Fact(Skip = "#8973")]
        public override void Query_when_subquery()
        {
            base.Query_when_subquery();
        }

        [Fact(Skip = "ErikEJ Investigate fail")]
        public override void Query_with_owned_entity_equality_operator()
        {
            base.Query_with_owned_entity_equality_operator();
        }

        private void AssertSql(params string[] expected)
            => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);

        public class OwnedQuerySqlCeFixture : OwnedQueryFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;
            public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ServiceProvider.GetRequiredService<ILoggerFactory>();

            protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
            {
                modelBuilder.Entity<OwnedPerson>()
                    .Property(p => p.Id)
                    .ValueGeneratedNever();

                base.OnModelCreating(modelBuilder, context);
            }
        }
    }
}
