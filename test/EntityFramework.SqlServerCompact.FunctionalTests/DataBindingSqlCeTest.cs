using Microsoft.EntityFrameworkCore.Specification.Tests;

namespace Microsoft.EntityFrameworkCore.SqlCe.FunctionalTests
{
    public class DatabindingSqlCeTest : DatabindingTestBase<SqlCeTestStore, F1SqlCeFixture>
    {
        public DatabindingSqlCeTest(F1SqlCeFixture fixture)
            : base(fixture)
        {
        }
    }
}