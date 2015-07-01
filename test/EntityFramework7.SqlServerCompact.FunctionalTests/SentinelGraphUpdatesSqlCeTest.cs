using Microsoft.Data.Entity;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class SentinelGraphUpdatesSqlCeTest : GraphUpdatesSqlCeTestBase<SentinelGraphUpdatesSqlCeTest.SentinelGraphUpdatesSqlCeFixture>
    {
        public SentinelGraphUpdatesSqlCeTest(SentinelGraphUpdatesSqlCeFixture fixture)
            : base(fixture)
        {
        }

        public class SentinelGraphUpdatesSqlCeFixture : GraphUpdatesSqlCeFixtureBase
        {
            protected override string DatabaseName => "SentinelGraphUpdatesTest";

            public override int IntSentinel => -10000000;

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                SetSentinelValues(modelBuilder, IntSentinel);
            }
        }
    }
}
