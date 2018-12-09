using System;
using Xunit;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore.TestUtilities;
using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore.Query
{

    public class QueryNavigationsWithForcedClientEvalSqlCeTest : QueryNavigationsTestBase<NorthwindQueryWithForcedClientEvalSqlCeFixture>
    {
        public QueryNavigationsWithForcedClientEvalSqlCeTest(
            // ReSharper disable once UnusedParameter.Local
            NorthwindQueryWithForcedClientEvalSqlCeFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            fixture.TestSqlLoggerFactory.Clear();
            //TestSqlLoggerFactory.CaptureOutput(testOutputHelper);
        }

        //TODO ErikEJ Investigate fails

        [Theory(Skip ="Investigate fail")]
        public override Task Where_subquery_on_navigation(bool isAsync)
        {
            return base.Where_subquery_on_navigation(isAsync);
        }

        [Theory(Skip = "Investigate fail")]
        public override Task Where_subquery_on_navigation2(bool isAsync)
        {
            return base.Where_subquery_on_navigation2(isAsync);
        }

        [Theory(Skip = "Investigate fail")]
        public override Task Project_single_scalar_value_subquery_is_properly_inlined(bool isAsync)
        {
            return base.Project_single_scalar_value_subquery_is_properly_inlined(isAsync);
        }
    }
}
