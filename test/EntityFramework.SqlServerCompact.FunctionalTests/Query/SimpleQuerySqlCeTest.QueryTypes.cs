using Xunit;

namespace Microsoft.EntityFrameworkCore.Query
{
    public partial class SimpleQuerySqlCeTest
    {
        [Fact(Skip = "SQLCE limitation - views not supported")]
        public override void Query_backed_by_database_view()
        {
            base.Query_backed_by_database_view();

            AssertSql(
                @"SELECT [a].[CategoryName], [a].[ProductID], [a].[ProductName]
FROM [Alphabetical list of products] AS [a]");
        }

        [Fact(Skip = "SQLCE limitation")]
        public override void QueryType_with_nav_defining_query()
        {
            base.QueryType_with_nav_defining_query();

            AssertSql(
                @"@__ef_filter___searchTerm_0='A' (Size = 4000)
@__ef_filter___searchTerm_1='A' (Size = 4000)

SELECT [t].[CompanyName], [t].[OrderCount], [t].[SearchTerm]
FROM (
    SELECT [c].[CompanyName], (
        SELECT COUNT(*)
        FROM [Orders] AS [o]
        WHERE [c].[CustomerID] = [o].[CustomerID]
    ) AS [OrderCount], @__ef_filter___searchTerm_0 AS [SearchTerm]
    FROM [Customers] AS [c]
) AS [t]
WHERE (([t].[CompanyName] LIKE @__ef_filter___searchTerm_1 + N'%' AND (LEFT([t].[CompanyName], LEN(@__ef_filter___searchTerm_1)) = @__ef_filter___searchTerm_1)) OR (@__ef_filter___searchTerm_1 = N'')) AND ([t].[OrderCount] > 0)");
        }

    }
}
