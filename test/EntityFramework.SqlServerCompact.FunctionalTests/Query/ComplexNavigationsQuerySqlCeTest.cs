using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore.Query
{
    public class ComplexNavigationsQuerySqlCeTest : ComplexNavigationsQueryTestBase<ComplexNavigationsQuerySqlCeFixture>
    {
        public ComplexNavigationsQuerySqlCeTest(
            ComplexNavigationsQuerySqlCeFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            Fixture.TestSqlLoggerFactory.Clear();
            //Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
        }

        private bool SupportsOffset => TestEnvironment.GetFlag(nameof(SqlServerCondition.SupportsOffset)) ?? true;
    }
}
