using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore
{
    public class FieldMappingSqlCeTest : FieldMappingTestBase<FieldMappingSqlCeTest.FieldMappingSqlCeFixture>
    {
        public FieldMappingSqlCeTest(FieldMappingSqlCeFixture fixture)
            : base(fixture)
        {
        }

        protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
            => facade.UseTransaction(transaction.GetDbTransaction());

        public class FieldMappingSqlCeFixture : FieldMappingFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;
        }
    }
}
