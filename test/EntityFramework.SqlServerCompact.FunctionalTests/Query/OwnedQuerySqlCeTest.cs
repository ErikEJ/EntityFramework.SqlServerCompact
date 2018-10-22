using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class OwnedQuerySqlCeTest : RelationalOwnedQueryTestBase<OwnedQuerySqlCeTest.OwnedQuerySqlCeFixture>
    {
        public OwnedQuerySqlCeTest(OwnedQuerySqlCeFixture fixture)
            : base(fixture)
        {
            //fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
        }

        [Fact(Skip = "SQLCE limitation")]
        public override void Navigation_rewrite_on_owned_collection()
        {
            base.Navigation_rewrite_on_owned_collection();
        }

        [Fact(Skip = "SQLCE limitation")]
        public override void Navigation_rewrite_on_owned_reference_followed_by_regular_entity_and_collection_count()
        {
            base.Navigation_rewrite_on_owned_reference_followed_by_regular_entity_and_collection_count();
        }

        private void AssertSql(params string[] expected)
            => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);

        public class OwnedQuerySqlCeFixture : RelationalOwnedQueryFixture
        {
            protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;

            protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
            {
                modelBuilder.Entity<OwnedPerson>()
                    .Property(p => p.Id)
                    .ValueGeneratedNever();

                modelBuilder.Entity<Star>()
                    .Property(p => p.Id)
                    .ValueGeneratedNever();

                modelBuilder.Entity<Element>()
                    .Property(p => p.Id)
                    .ValueGeneratedNever();

                modelBuilder.Entity<Moon>()
                    .Property(p => p.Id)
                    .ValueGeneratedNever();

                modelBuilder.Entity<Planet>()
                    .Property(p => p.Id)
                    .ValueGeneratedNever();

                modelBuilder.Entity<Order>()
                    .Property(p => p.Id)
                    .ValueGeneratedNever();

                base.OnModelCreating(modelBuilder, context);
            }
        }
    }
}
