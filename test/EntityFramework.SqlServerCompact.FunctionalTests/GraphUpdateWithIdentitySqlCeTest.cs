using Xunit;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class GraphUpdatesWithIdentitySqlCeTest : GraphUpdatesSqlCeTestBase<GraphUpdatesWithIdentitySqlCeTest.GraphUpdatesWithIdentitySqlCeFixture>
    {
        public GraphUpdatesWithIdentitySqlCeTest(GraphUpdatesWithIdentitySqlCeFixture fixture)
            : base(fixture)
        {
        }

        public class GraphUpdatesWithIdentitySqlCeFixture : GraphUpdatesSqlCeFixtureBase
        {
            protected override string DatabaseName => "GraphIdentityUpdatesTest";
        }

        [Fact(Skip = "SQL CE limitation: Unique keys not enforced for nullable FKs")]
        public override void Optional_One_to_one_with_AK_relationships_are_one_to_one()
        {
            base.Optional_One_to_one_with_AK_relationships_are_one_to_one();
        }

        [Fact(Skip = "SQL CE limitation: Unique keys not enforced for nullable FKs")]
        public override void Optional_One_to_one_relationships_are_one_to_one()
        {
            base.Optional_One_to_one_relationships_are_one_to_one();
        }
    }
}
