using System;
using System.Linq;
using Microsoft.Data.Entity.Internal;
using Xunit;
using Microsoft.Data.Entity;

namespace Microsoft.Data.Entity.FunctionalTests
{
    public class QueryNoClientEvalSqlCeTest : QueryNoClientEvalTestBase<QueryNoClientEvalSqlCeFixture>
    {
        public QueryNoClientEvalSqlCeTest(QueryNoClientEvalSqlCeFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public virtual void Throws_when_where2()
        {
            using (var context = CreateContext())
            {
                
                Assert.Equal(RelationalStrings.ClientEvalDisabled("[c].IsLondon"),
                    Assert.Throws<InvalidOperationException>(
                        () => context.Customers.Where(c => c.IsLondon).ToList()).Message);
            }
        }
    }
}