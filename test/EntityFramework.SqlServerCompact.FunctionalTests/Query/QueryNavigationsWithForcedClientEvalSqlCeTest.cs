using System;
using Xunit;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{

    //TODO ErikEJ Investiaget fails

//    public class QueryNavigationsWithForcedClientEvalSqlCeTest : QueryNavigationsTestBase<NorthwindQueryWithForcedClientEvalSqlCeFixture>
//    {
//        public QueryNavigationsWithForcedClientEvalSqlCeTest(
//            // ReSharper disable once UnusedParameter.Local
//            NorthwindQueryWithForcedClientEvalSqlCeFixture fixture, ITestOutputHelper testOutputHelper)
//            : base(fixture)
//        {
//            //TestSqlLoggerFactory.CaptureOutput(testOutputHelper);
//        }

//        [Fact(Skip = "Investigate why this fails with forced client eval - 2.1")]
//        public override void Where_subquery_on_navigation()
//        {
//            base.Where_subquery_on_navigation();
//        }

//        [Fact(Skip = "Investigate why this fails with forced client eval - 2.1")]
//        public override void Where_subquery_on_navigation2()
//        {
//            base.Where_subquery_on_navigation2();
//        }

//        protected override void ClearLog() => Fixture.TestSqlLoggerFactory.Clear();

//        private const string FileLineEnding = @"
//";

//        private string Sql => Fixture.TestSqlLoggerFactory.Sql.Replace(Environment.NewLine, FileLineEnding);
//    }
}