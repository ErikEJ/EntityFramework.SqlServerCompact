using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Microsoft.EntityFrameworkCore
{
    public abstract class GraphUpdatesSqlCeTestBase<TFixture> : GraphUpdatesTestBase<TFixture>
        where TFixture : GraphUpdatesSqlCeTestBase<TFixture>.GraphUpdatesSqlCeFixtureBase, new()
    {
        protected GraphUpdatesSqlCeTestBase(TFixture fixture)
            : base(fixture)
        {
        }

        protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
            => facade.UseTransaction(transaction.GetDbTransaction());

        public abstract class GraphUpdatesSqlCeFixtureBase : GraphUpdatesFixtureBase
        {
            public TestSqlLoggerFactory TestSqlLoggerFactory => (TestSqlLoggerFactory)ServiceProvider.GetRequiredService<ILoggerFactory>();
            protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;
        }
    }

    public class GraphUpdatesWithIdentitySqlCeTest : GraphUpdatesSqlCeTestBase<GraphUpdatesWithIdentitySqlCeTest.GraphUpdatesWithIdentitySqlCeFixture>
    {
        public GraphUpdatesWithIdentitySqlCeTest(GraphUpdatesWithIdentitySqlCeFixture fixture)
            : base(fixture)
        {
        }

        public class GraphUpdatesWithIdentitySqlCeFixture : GraphUpdatesSqlCeFixtureBase
        {
            protected override string StoreName { get; } = "GraphIdentityUpdatesTest";
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
