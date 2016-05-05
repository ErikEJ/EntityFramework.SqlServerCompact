using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
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