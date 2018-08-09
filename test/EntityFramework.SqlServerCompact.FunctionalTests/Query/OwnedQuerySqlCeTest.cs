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

        [Fact(Skip = "ErikEJ Investigate fail")]
        public override void Query_when_group_by()
        {
            base.Query_when_group_by();
        }

        [Fact(Skip = "ErikEJ Investigate fail")]
        public override void Query_with_owned_entity_equality_operator()
        {
            base.Query_with_owned_entity_equality_operator();
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

                base.OnModelCreating(modelBuilder, context);
            }
        }
    }
}
