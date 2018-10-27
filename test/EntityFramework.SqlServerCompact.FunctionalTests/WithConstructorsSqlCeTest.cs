using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;

namespace Microsoft.EntityFrameworkCore
{
    public class WithConstructorsSqlCeTest : WithConstructorsTestBase<WithConstructorsSqlCeTest.WithConstructorsSqlCeFixture>
    {
        public WithConstructorsSqlCeTest(WithConstructorsSqlCeFixture fixture)
            : base(fixture)
        {
        }

        [Fact(Skip="SQLCE does not support views")]
        public override void Query_with_query_type()
        {
            base.Query_with_query_type();
        }

        protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
            => facade.UseTransaction(transaction.GetDbTransaction());

        public class WithConstructorsSqlCeFixture : WithConstructorsFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;
        }
    }
}
