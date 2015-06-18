namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class GraphUpdatesWithIdentitySqlCeTest : GraphUpdatesSqlServerTestBase<GraphUpdatesWithIdentitySqlCeTest.GraphUpdatesWithIdentitySqlCeFixture>
    {
        public GraphUpdatesWithIdentitySqlCeTest(GraphUpdatesWithIdentitySqlCeFixture fixture)
            : base(fixture)
        {
        }

        public class GraphUpdatesWithIdentitySqlCeFixture : GraphUpdatesSqlServerFixtureBase
        {
            protected override string DatabaseName => "GraphIdentityUpdatesTest";

            public override int IntSentinel => 0;
        }
    }
}
