using Microsoft.EntityFrameworkCore.FunctionalTests;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.SqlCe.FunctionalTests
{
    public class IncludeSqlServerTest : IncludeTestBase<NorthwindQuerySqlCeFixture>
    {
        private bool SupportsOffset => true;

        public IncludeSqlServerTest(NorthwindQuerySqlCeFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            //TestSqlLoggerFactory.CaptureOutput(testOutputHelper);
        }

        public override void Include_list()
        {
            base.Include_list();

            Assert.Equal(
                @"SELECT [c].[ProductID], [c].[Discontinued], [c].[ProductName], [c].[UnitsInStock]
FROM [Products] AS [c]
ORDER BY [c].[ProductID]

SELECT [o].[OrderID], [o].[ProductID], [o].[Discount], [o].[Quantity], [o].[UnitPrice], [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate]
FROM [Order Details] AS [o]
INNER JOIN (
    SELECT DISTINCT [c].[ProductID]
    FROM [Products] AS [c]
) AS [c0] ON [o].[ProductID] = [c0].[ProductID]
INNER JOIN [Orders] AS [o0] ON [o].[OrderID] = [o0].[OrderID]
ORDER BY [c0].[ProductID]",
                Sql);
        }

        public override void Include_collection()
        {
            base.Include_collection();

            Assert.Equal(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
ORDER BY [c].[CustomerID]

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
INNER JOIN (
    SELECT DISTINCT [c].[CustomerID]
    FROM [Customers] AS [c]
) AS [c0] ON [o].[CustomerID] = [c0].[CustomerID]
ORDER BY [c0].[CustomerID]",
                Sql);
        }

        public override void Include_reference_and_collection()
        {
            base.Include_reference_and_collection();

            Assert.Equal(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate], [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
ORDER BY [o].[OrderID]

SELECT [o0].[OrderID], [o0].[ProductID], [o0].[Discount], [o0].[Quantity], [o0].[UnitPrice]
FROM [Order Details] AS [o0]
INNER JOIN (
    SELECT DISTINCT [o].[OrderID]
    FROM [Orders] AS [o]
) AS [o1] ON [o0].[OrderID] = [o1].[OrderID]
ORDER BY [o1].[OrderID]",
                Sql);
        }

        public override void Include_references_multi_level()
        {
            base.Include_references_multi_level();

            Assert.Equal(
                @"SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice], [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate], [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Order Details] AS [od]
INNER JOIN [Orders] AS [o] ON [od].[OrderID] = [o].[OrderID]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]",
                Sql);
        }

        public override void Include_multiple_references_multi_level()
        {
            base.Include_multiple_references_multi_level();

            Assert.Equal(
                @"SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice], [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate], [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region], [p].[ProductID], [p].[Discontinued], [p].[ProductName], [p].[UnitsInStock]
FROM [Order Details] AS [od]
INNER JOIN [Orders] AS [o] ON [od].[OrderID] = [o].[OrderID]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
INNER JOIN [Products] AS [p] ON [od].[ProductID] = [p].[ProductID]",
                Sql);
        }

        public override void Include_multiple_references_multi_level_reverse()
        {
            base.Include_multiple_references_multi_level_reverse();

            Assert.Equal(
                @"SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice], [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate], [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region], [p].[ProductID], [p].[Discontinued], [p].[ProductName], [p].[UnitsInStock]
FROM [Order Details] AS [od]
INNER JOIN [Orders] AS [o] ON [od].[OrderID] = [o].[OrderID]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
INNER JOIN [Products] AS [p] ON [od].[ProductID] = [p].[ProductID]",
                Sql);
        }

        public override void Include_references_and_collection_multi_level()
        {
            base.Include_references_and_collection_multi_level();

            Assert.Equal(
                @"SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice], [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate], [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Order Details] AS [od]
INNER JOIN [Orders] AS [o] ON [od].[OrderID] = [o].[OrderID]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
ORDER BY [c].[CustomerID]

SELECT [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate]
FROM [Orders] AS [o0]
INNER JOIN (
    SELECT DISTINCT [c].[CustomerID]
    FROM [Order Details] AS [od]
    INNER JOIN [Orders] AS [o] ON [od].[OrderID] = [o].[OrderID]
    LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
) AS [c0] ON [o0].[CustomerID] = [c0].[CustomerID]
ORDER BY [c0].[CustomerID]",
                Sql);
        }

        public override void Include_multi_level_reference_and_collection_predicate()
        {
            base.Include_multi_level_reference_and_collection_predicate();

            Assert.Equal(
                @"SELECT TOP(2) [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate], [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
WHERE [o].[OrderID] = 10248
ORDER BY [c].[CustomerID]

SELECT [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate]
FROM [Orders] AS [o0]
INNER JOIN (
    SELECT DISTINCT TOP(2) [c].[CustomerID]
    FROM [Orders] AS [o]
    LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
    WHERE [o].[OrderID] = 10248
    ORDER BY [c].[CustomerID]
) AS [c0] ON [o0].[CustomerID] = [c0].[CustomerID]
ORDER BY [c0].[CustomerID]",
                Sql);
        }

        public override void Include_multi_level_collection_and_then_include_reference_predicate()
        {
            base.Include_multi_level_collection_and_then_include_reference_predicate();

            Assert.Equal(
                @"SELECT TOP(2) [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
WHERE [o].[OrderID] = 10248
ORDER BY [o].[OrderID]

SELECT [o0].[OrderID], [o0].[ProductID], [o0].[Discount], [o0].[Quantity], [o0].[UnitPrice], [p].[ProductID], [p].[Discontinued], [p].[ProductName], [p].[UnitsInStock]
FROM [Order Details] AS [o0]
INNER JOIN (
    SELECT DISTINCT TOP(2) [o].[OrderID]
    FROM [Orders] AS [o]
    WHERE [o].[OrderID] = 10248
    ORDER BY [o].[OrderID]
) AS [o1] ON [o0].[OrderID] = [o1].[OrderID]
INNER JOIN [Products] AS [p] ON [o0].[ProductID] = [p].[ProductID]
ORDER BY [o1].[OrderID]",
                Sql);
        }

        public override void Include_collection_alias_generation()
        {
            base.Include_collection_alias_generation();

            Assert.Equal(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
ORDER BY [o].[OrderID]

SELECT [o0].[OrderID], [o0].[ProductID], [o0].[Discount], [o0].[Quantity], [o0].[UnitPrice]
FROM [Order Details] AS [o0]
INNER JOIN (
    SELECT DISTINCT [o].[OrderID]
    FROM [Orders] AS [o]
) AS [o1] ON [o0].[OrderID] = [o1].[OrderID]
ORDER BY [o1].[OrderID]",
                Sql);
        }

        public override void Include_collection_order_by_key()
        {
            base.Include_collection_order_by_key();

            Assert.Equal(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
ORDER BY [c].[CustomerID]

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
INNER JOIN (
    SELECT DISTINCT [c].[CustomerID]
    FROM [Customers] AS [c]
) AS [c0] ON [o].[CustomerID] = [c0].[CustomerID]
ORDER BY [c0].[CustomerID]",
                Sql);
        }

        public override void Include_collection_order_by_non_key()
        {
            base.Include_collection_order_by_non_key();

            Assert.Equal(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
ORDER BY [c].[City], [c].[CustomerID]

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
INNER JOIN (
    SELECT DISTINCT [c].[City], [c].[CustomerID]
    FROM [Customers] AS [c]
) AS [c0] ON [o].[CustomerID] = [c0].[CustomerID]
ORDER BY [c0].[City], [c0].[CustomerID]",
                Sql);
        }

        public override void Include_collection_as_no_tracking()
        {
            base.Include_collection_as_no_tracking();

            Assert.Equal(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
ORDER BY [c].[CustomerID]

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
INNER JOIN (
    SELECT DISTINCT [c].[CustomerID]
    FROM [Customers] AS [c]
) AS [c0] ON [o].[CustomerID] = [c0].[CustomerID]
ORDER BY [c0].[CustomerID]",
                Sql);
        }

        public override void Include_collection_principal_already_tracked()
        {
            base.Include_collection_principal_already_tracked();

            Assert.Equal(
                @"SELECT TOP(2) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] = 'ALFKI'

SELECT TOP(2) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] = 'ALFKI'
ORDER BY [c].[CustomerID]

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
INNER JOIN (
    SELECT DISTINCT TOP(2) [c].[CustomerID]
    FROM [Customers] AS [c]
    WHERE [c].[CustomerID] = 'ALFKI'
    ORDER BY [c].[CustomerID]
) AS [c0] ON [o].[CustomerID] = [c0].[CustomerID]
ORDER BY [c0].[CustomerID]",
                Sql);
        }

        public override void Include_collection_principal_already_tracked_as_no_tracking()
        {
            base.Include_collection_principal_already_tracked_as_no_tracking();

            Assert.Equal(
                @"SELECT TOP(2) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] = 'ALFKI'

SELECT TOP(2) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] = 'ALFKI'
ORDER BY [c].[CustomerID]

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
INNER JOIN (
    SELECT DISTINCT TOP(2) [c].[CustomerID]
    FROM [Customers] AS [c]
    WHERE [c].[CustomerID] = 'ALFKI'
    ORDER BY [c].[CustomerID]
) AS [c0] ON [o].[CustomerID] = [c0].[CustomerID]
ORDER BY [c0].[CustomerID]",
                Sql);
        }

        public override void Include_collection_with_filter()
        {
            base.Include_collection_with_filter();

            Assert.Equal(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] = 'ALFKI'
ORDER BY [c].[CustomerID]

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
INNER JOIN (
    SELECT DISTINCT [c].[CustomerID]
    FROM [Customers] AS [c]
    WHERE [c].[CustomerID] = 'ALFKI'
) AS [c0] ON [o].[CustomerID] = [c0].[CustomerID]
ORDER BY [c0].[CustomerID]",
                Sql);
        }

        public override void Include_collection_with_filter_reordered()
        {
            base.Include_collection_with_filter_reordered();

            Assert.Equal(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] = 'ALFKI'
ORDER BY [c].[CustomerID]

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
INNER JOIN (
    SELECT DISTINCT [c].[CustomerID]
    FROM [Customers] AS [c]
    WHERE [c].[CustomerID] = 'ALFKI'
) AS [c0] ON [o].[CustomerID] = [c0].[CustomerID]
ORDER BY [c0].[CustomerID]",
                Sql);
        }

        public override void Include_collection_then_include_collection()
        {
            base.Include_collection_then_include_collection();

            Assert.Equal(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
ORDER BY [c].[CustomerID]

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
INNER JOIN (
    SELECT DISTINCT [c].[CustomerID]
    FROM [Customers] AS [c]
) AS [c0] ON [o].[CustomerID] = [c0].[CustomerID]
ORDER BY [c0].[CustomerID], [o].[OrderID]

SELECT [o0].[OrderID], [o0].[ProductID], [o0].[Discount], [o0].[Quantity], [o0].[UnitPrice]
FROM [Order Details] AS [o0]
INNER JOIN (
    SELECT DISTINCT [c0].[CustomerID], [o].[OrderID]
    FROM [Orders] AS [o]
    INNER JOIN (
        SELECT DISTINCT [c].[CustomerID]
        FROM [Customers] AS [c]
    ) AS [c0] ON [o].[CustomerID] = [c0].[CustomerID]
) AS [o1] ON [o0].[OrderID] = [o1].[OrderID]
ORDER BY [o1].[CustomerID], [o1].[OrderID]",
                Sql);
        }

        public override void Include_collection_when_projection()
        {
            base.Include_collection_when_projection();

            Assert.Equal(
                @"SELECT [c].[CustomerID]
FROM [Customers] AS [c]",
                Sql);
        }

        public override void Include_collection_on_join_clause_with_filter()
        {
            base.Include_collection_on_join_clause_with_filter();

            Assert.Equal(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
INNER JOIN [Orders] AS [o] ON [c].[CustomerID] = [o].[CustomerID]
WHERE [c].[CustomerID] = 'ALFKI'
ORDER BY [c].[CustomerID]

SELECT [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate]
FROM [Orders] AS [o0]
INNER JOIN (
    SELECT DISTINCT [c].[CustomerID]
    FROM [Customers] AS [c]
    INNER JOIN [Orders] AS [o] ON [c].[CustomerID] = [o].[CustomerID]
    WHERE [c].[CustomerID] = 'ALFKI'
) AS [c0] ON [o0].[CustomerID] = [c0].[CustomerID]
ORDER BY [c0].[CustomerID]",
                Sql);
        }

        public override void Include_collection_on_additional_from_clause_with_filter()
        {
            base.Include_collection_on_additional_from_clause_with_filter();

            Assert.Equal(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c1]
CROSS JOIN [Customers] AS [c]
WHERE [c].[CustomerID] = 'ALFKI'
ORDER BY [c].[CustomerID]

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
INNER JOIN (
    SELECT DISTINCT [c].[CustomerID]
    FROM [Customers] AS [c1]
    CROSS JOIN [Customers] AS [c]
    WHERE [c].[CustomerID] = 'ALFKI'
) AS [c0] ON [o].[CustomerID] = [c0].[CustomerID]
ORDER BY [c0].[CustomerID]",
                Sql);
        }

        public override void Include_collection_on_additional_from_clause()
        {
            base.Include_collection_on_additional_from_clause();

            Assert.Equal(
                @"@__p_0: 5

SELECT [c1].[CustomerID], [c1].[Address], [c1].[City], [c1].[CompanyName], [c1].[ContactName], [c1].[ContactTitle], [c1].[Country], [c1].[Fax], [c1].[Phone], [c1].[PostalCode], [c1].[Region]
FROM (
    SELECT TOP(@__p_0) [c0].*
    FROM [Customers] AS [c0]
    ORDER BY [c0].[CustomerID]
) AS [t]
CROSS JOIN [Customers] AS [c1]
ORDER BY [c1].[CustomerID]

@__p_0: 5

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
INNER JOIN (
    SELECT DISTINCT [c1].[CustomerID]
    FROM (
        SELECT TOP(@__p_0) [c0].*
        FROM [Customers] AS [c0]
        ORDER BY [c0].[CustomerID]
    ) AS [t]
    CROSS JOIN [Customers] AS [c1]
) AS [c10] ON [o].[CustomerID] = [c10].[CustomerID]
ORDER BY [c10].[CustomerID]",
                Sql);
        }

        public override void Include_duplicate_collection()
        {
            base.Include_duplicate_collection();

            if (SupportsOffset)
            {
                Assert.Equal(
                    @"@__p_0: 2

SELECT [t].[CustomerID], [t].[Address], [t].[City], [t].[CompanyName], [t].[ContactName], [t].[ContactTitle], [t].[Country], [t].[Fax], [t].[Phone], [t].[PostalCode], [t].[Region], [t0].[CustomerID], [t0].[Address], [t0].[City], [t0].[CompanyName], [t0].[ContactName], [t0].[ContactTitle], [t0].[Country], [t0].[Fax], [t0].[Phone], [t0].[PostalCode], [t0].[Region]
FROM (
    SELECT TOP(@__p_0) [c0].[CustomerID], [c0].[Address], [c0].[City], [c0].[CompanyName], [c0].[ContactName], [c0].[ContactTitle], [c0].[Country], [c0].[Fax], [c0].[Phone], [c0].[PostalCode], [c0].[Region]
    FROM [Customers] AS [c0]
    ORDER BY [c0].[CustomerID]
) AS [t]
CROSS JOIN (
    SELECT [c2].[CustomerID], [c2].[Address], [c2].[City], [c2].[CompanyName], [c2].[ContactName], [c2].[ContactTitle], [c2].[Country], [c2].[Fax], [c2].[Phone], [c2].[PostalCode], [c2].[Region]
    FROM [Customers] AS [c2]
    ORDER BY [c2].[CustomerID]
    OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY
) AS [t0]
ORDER BY [t].[CustomerID], [t0].[CustomerID]

@__p_0: 2

SELECT [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate]
FROM [Orders] AS [o0]
INNER JOIN (
    SELECT DISTINCT [t].[CustomerID], [t0].[CustomerID] AS [CustomerID0]
    FROM (
        SELECT TOP(@__p_0) [c0].[CustomerID], [c0].[Address], [c0].[City], [c0].[CompanyName], [c0].[ContactName], [c0].[ContactTitle], [c0].[Country], [c0].[Fax], [c0].[Phone], [c0].[PostalCode], [c0].[Region]
        FROM [Customers] AS [c0]
        ORDER BY [c0].[CustomerID]
    ) AS [t]
    CROSS JOIN (
        SELECT [c2].[CustomerID], [c2].[Address], [c2].[City], [c2].[CompanyName], [c2].[ContactName], [c2].[ContactTitle], [c2].[Country], [c2].[Fax], [c2].[Phone], [c2].[PostalCode], [c2].[Region]
        FROM [Customers] AS [c2]
        ORDER BY [c2].[CustomerID]
        OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY
    ) AS [t0]
) AS [t00] ON [o0].[CustomerID] = [t00].[CustomerID0]
ORDER BY [t00].[CustomerID], [t00].[CustomerID0]

@__p_0: 2

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
INNER JOIN (
    SELECT DISTINCT [t].[CustomerID]
    FROM (
        SELECT TOP(@__p_0) [c0].[CustomerID], [c0].[Address], [c0].[City], [c0].[CompanyName], [c0].[ContactName], [c0].[ContactTitle], [c0].[Country], [c0].[Fax], [c0].[Phone], [c0].[PostalCode], [c0].[Region]
        FROM [Customers] AS [c0]
        ORDER BY [c0].[CustomerID]
    ) AS [t]
    CROSS JOIN (
        SELECT [c2].[CustomerID], [c2].[Address], [c2].[City], [c2].[CompanyName], [c2].[ContactName], [c2].[ContactTitle], [c2].[Country], [c2].[Fax], [c2].[Phone], [c2].[PostalCode], [c2].[Region]
        FROM [Customers] AS [c2]
        ORDER BY [c2].[CustomerID]
        OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY
    ) AS [t0]
) AS [t1] ON [o].[CustomerID] = [t1].[CustomerID]
ORDER BY [t1].[CustomerID]",
                    Sql);
            }
        }

        public override void Include_duplicate_collection_result_operator()
        {
            base.Include_duplicate_collection_result_operator();

            if (SupportsOffset)
            {
                Assert.Equal(
                    @"@__p_1: 1
@__p_0: 2

SELECT TOP(@__p_1) [t].[CustomerID], [t].[Address], [t].[City], [t].[CompanyName], [t].[ContactName], [t].[ContactTitle], [t].[Country], [t].[Fax], [t].[Phone], [t].[PostalCode], [t].[Region], [t0].[CustomerID], [t0].[Address], [t0].[City], [t0].[CompanyName], [t0].[ContactName], [t0].[ContactTitle], [t0].[Country], [t0].[Fax], [t0].[Phone], [t0].[PostalCode], [t0].[Region]
FROM (
    SELECT TOP(@__p_0) [c0].[CustomerID], [c0].[Address], [c0].[City], [c0].[CompanyName], [c0].[ContactName], [c0].[ContactTitle], [c0].[Country], [c0].[Fax], [c0].[Phone], [c0].[PostalCode], [c0].[Region]
    FROM [Customers] AS [c0]
    ORDER BY [c0].[CustomerID]
) AS [t]
CROSS JOIN (
    SELECT [c2].[CustomerID], [c2].[Address], [c2].[City], [c2].[CompanyName], [c2].[ContactName], [c2].[ContactTitle], [c2].[Country], [c2].[Fax], [c2].[Phone], [c2].[PostalCode], [c2].[Region]
    FROM [Customers] AS [c2]
    ORDER BY [c2].[CustomerID]
    OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY
) AS [t0]
ORDER BY [t].[CustomerID], [t0].[CustomerID]

@__p_1: 1
@__p_0: 2

SELECT [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate]
FROM [Orders] AS [o0]
INNER JOIN (
    SELECT DISTINCT TOP(@__p_1) [t].[CustomerID], [t0].[CustomerID] AS [CustomerID0]
    FROM (
        SELECT TOP(@__p_0) [c0].[CustomerID], [c0].[Address], [c0].[City], [c0].[CompanyName], [c0].[ContactName], [c0].[ContactTitle], [c0].[Country], [c0].[Fax], [c0].[Phone], [c0].[PostalCode], [c0].[Region]
        FROM [Customers] AS [c0]
        ORDER BY [c0].[CustomerID]
    ) AS [t]
    CROSS JOIN (
        SELECT [c2].[CustomerID], [c2].[Address], [c2].[City], [c2].[CompanyName], [c2].[ContactName], [c2].[ContactTitle], [c2].[Country], [c2].[Fax], [c2].[Phone], [c2].[PostalCode], [c2].[Region]
        FROM [Customers] AS [c2]
        ORDER BY [c2].[CustomerID]
        OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY
    ) AS [t0]
    ORDER BY [t].[CustomerID], [t0].[CustomerID]
) AS [t00] ON [o0].[CustomerID] = [t00].[CustomerID0]
ORDER BY [t00].[CustomerID], [t00].[CustomerID0]

@__p_1: 1
@__p_0: 2

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
INNER JOIN (
    SELECT DISTINCT TOP(@__p_1) [t].[CustomerID]
    FROM (
        SELECT TOP(@__p_0) [c0].[CustomerID], [c0].[Address], [c0].[City], [c0].[CompanyName], [c0].[ContactName], [c0].[ContactTitle], [c0].[Country], [c0].[Fax], [c0].[Phone], [c0].[PostalCode], [c0].[Region]
        FROM [Customers] AS [c0]
        ORDER BY [c0].[CustomerID]
    ) AS [t]
    CROSS JOIN (
        SELECT [c2].[CustomerID], [c2].[Address], [c2].[City], [c2].[CompanyName], [c2].[ContactName], [c2].[ContactTitle], [c2].[Country], [c2].[Fax], [c2].[Phone], [c2].[PostalCode], [c2].[Region]
        FROM [Customers] AS [c2]
        ORDER BY [c2].[CustomerID]
        OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY
    ) AS [t0]
    ORDER BY [t].[CustomerID]
) AS [t1] ON [o].[CustomerID] = [t1].[CustomerID]
ORDER BY [t1].[CustomerID]",
                    Sql);
            }
        }

        public override void Include_collection_on_join_clause_with_order_by_and_filter()
        {
            base.Include_collection_on_join_clause_with_order_by_and_filter();

            Assert.Equal(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
INNER JOIN [Orders] AS [o] ON [c].[CustomerID] = [o].[CustomerID]
WHERE [c].[CustomerID] = 'ALFKI'
ORDER BY [c].[City], [c].[CustomerID]

SELECT [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate]
FROM [Orders] AS [o0]
INNER JOIN (
    SELECT DISTINCT [c].[City], [c].[CustomerID]
    FROM [Customers] AS [c]
    INNER JOIN [Orders] AS [o] ON [c].[CustomerID] = [o].[CustomerID]
    WHERE [c].[CustomerID] = 'ALFKI'
) AS [c0] ON [o0].[CustomerID] = [c0].[CustomerID]
ORDER BY [c0].[City], [c0].[CustomerID]",
                Sql);
        }

        public override void Include_collection_on_additional_from_clause2()
        {
            base.Include_collection_on_additional_from_clause2();

            Assert.Equal(
                @"@__p_0: 5

SELECT [t].[CustomerID], [t].[Address], [t].[City], [t].[CompanyName], [t].[ContactName], [t].[ContactTitle], [t].[Country], [t].[Fax], [t].[Phone], [t].[PostalCode], [t].[Region]
FROM (
    SELECT TOP(@__p_0) [c0].[CustomerID], [c0].[Address], [c0].[City], [c0].[CompanyName], [c0].[ContactName], [c0].[ContactTitle], [c0].[Country], [c0].[Fax], [c0].[Phone], [c0].[PostalCode], [c0].[Region]
    FROM [Customers] AS [c0]
    ORDER BY [c0].[CustomerID]
) AS [t]
CROSS JOIN [Customers] AS [c1]",
                Sql);
        }

        public override void Include_duplicate_collection_result_operator2()
        {
            base.Include_duplicate_collection_result_operator2();
            if (SupportsOffset)
            {
                Assert.Equal(
                    @"@__p_1: 1
@__p_0: 2

SELECT TOP(@__p_1) [t].[CustomerID], [t].[Address], [t].[City], [t].[CompanyName], [t].[ContactName], [t].[ContactTitle], [t].[Country], [t].[Fax], [t].[Phone], [t].[PostalCode], [t].[Region], [t0].[CustomerID], [t0].[Address], [t0].[City], [t0].[CompanyName], [t0].[ContactName], [t0].[ContactTitle], [t0].[Country], [t0].[Fax], [t0].[Phone], [t0].[PostalCode], [t0].[Region]
FROM (
    SELECT TOP(@__p_0) [c0].[CustomerID], [c0].[Address], [c0].[City], [c0].[CompanyName], [c0].[ContactName], [c0].[ContactTitle], [c0].[Country], [c0].[Fax], [c0].[Phone], [c0].[PostalCode], [c0].[Region]
    FROM [Customers] AS [c0]
    ORDER BY [c0].[CustomerID]
) AS [t]
CROSS JOIN (
    SELECT [c2].[CustomerID], [c2].[Address], [c2].[City], [c2].[CompanyName], [c2].[ContactName], [c2].[ContactTitle], [c2].[Country], [c2].[Fax], [c2].[Phone], [c2].[PostalCode], [c2].[Region]
    FROM [Customers] AS [c2]
    ORDER BY [c2].[CustomerID]
    OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY
) AS [t0]
ORDER BY [t].[CustomerID]

@__p_1: 1
@__p_0: 2

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
INNER JOIN (
    SELECT DISTINCT TOP(@__p_1) [t].[CustomerID]
    FROM (
        SELECT TOP(@__p_0) [c0].[CustomerID], [c0].[Address], [c0].[City], [c0].[CompanyName], [c0].[ContactName], [c0].[ContactTitle], [c0].[Country], [c0].[Fax], [c0].[Phone], [c0].[PostalCode], [c0].[Region]
        FROM [Customers] AS [c0]
        ORDER BY [c0].[CustomerID]
    ) AS [t]
    CROSS JOIN (
        SELECT [c2].[CustomerID], [c2].[Address], [c2].[City], [c2].[CompanyName], [c2].[ContactName], [c2].[ContactTitle], [c2].[Country], [c2].[Fax], [c2].[Phone], [c2].[PostalCode], [c2].[Region]
        FROM [Customers] AS [c2]
        ORDER BY [c2].[CustomerID]
        OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY
    ) AS [t0]
    ORDER BY [t].[CustomerID]
) AS [t1] ON [o].[CustomerID] = [t1].[CustomerID]
ORDER BY [t1].[CustomerID]",
                    Sql);
            }
        }

        public override void Include_reference()
        {
            base.Include_reference();

            Assert.Equal(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate], [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]",
                Sql);
        }

        public override void Include_multiple_references()
        {
            base.Include_multiple_references();

            Assert.Equal(
                @"SELECT [o].[OrderID], [o].[ProductID], [o].[Discount], [o].[Quantity], [o].[UnitPrice], [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate], [p].[ProductID], [p].[Discontinued], [p].[ProductName], [p].[UnitsInStock]
FROM [Order Details] AS [o]
INNER JOIN [Orders] AS [o0] ON [o].[OrderID] = [o0].[OrderID]
INNER JOIN [Products] AS [p] ON [o].[ProductID] = [p].[ProductID]",
                Sql);
        }

        public override void Include_reference_alias_generation()
        {
            base.Include_reference_alias_generation();

            Assert.Equal(
                @"SELECT [o].[OrderID], [o].[ProductID], [o].[Discount], [o].[Quantity], [o].[UnitPrice], [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate]
FROM [Order Details] AS [o]
INNER JOIN [Orders] AS [o0] ON [o].[OrderID] = [o0].[OrderID]",
                Sql);
        }

        public override void Include_duplicate_reference()
        {
            base.Include_duplicate_reference();

            if (SupportsOffset)
            {
                Assert.Equal(
                    @"@__p_0: 2

SELECT [t].[OrderID], [t].[CustomerID], [t].[EmployeeID], [t].[OrderDate], [t0].[OrderID], [t0].[CustomerID], [t0].[EmployeeID], [t0].[OrderDate], [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region], [c0].[CustomerID], [c0].[Address], [c0].[City], [c0].[CompanyName], [c0].[ContactName], [c0].[ContactTitle], [c0].[Country], [c0].[Fax], [c0].[Phone], [c0].[PostalCode], [c0].[Region]
FROM (
    SELECT TOP(@__p_0) [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate]
    FROM [Orders] AS [o0]
    ORDER BY [o0].[CustomerID]
) AS [t]
CROSS JOIN (
    SELECT [o2].[OrderID], [o2].[CustomerID], [o2].[EmployeeID], [o2].[OrderDate]
    FROM [Orders] AS [o2]
    ORDER BY [o2].[CustomerID]
    OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY
) AS [t0]
LEFT JOIN [Customers] AS [c] ON [t].[CustomerID] = [c].[CustomerID]
LEFT JOIN [Customers] AS [c0] ON [t0].[CustomerID] = [c0].[CustomerID]",
                    Sql);
            }
        }

        public override void Include_duplicate_reference2()
        {
            base.Include_duplicate_reference2();

            if (SupportsOffset)
            {
                Assert.Equal(
                    @"@__p_0: 2

SELECT [t].[OrderID], [t].[CustomerID], [t].[EmployeeID], [t].[OrderDate], [t0].[OrderID], [t0].[CustomerID], [t0].[EmployeeID], [t0].[OrderDate], [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM (
    SELECT TOP(@__p_0) [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate]
    FROM [Orders] AS [o0]
    ORDER BY [o0].[OrderID]
) AS [t]
CROSS JOIN (
    SELECT [o2].[OrderID], [o2].[CustomerID], [o2].[EmployeeID], [o2].[OrderDate]
    FROM [Orders] AS [o2]
    ORDER BY [o2].[OrderID]
    OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY
) AS [t0]
LEFT JOIN [Customers] AS [c] ON [t].[CustomerID] = [c].[CustomerID]",
                    Sql);
            }
        }

        public override void Include_duplicate_reference3()
        {
            base.Include_duplicate_reference3();

            if (SupportsOffset)
            {
                Assert.Equal(
                    @"@__p_0: 2

SELECT [t].[OrderID], [t].[CustomerID], [t].[EmployeeID], [t].[OrderDate], [t0].[OrderID], [t0].[CustomerID], [t0].[EmployeeID], [t0].[OrderDate], [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM (
    SELECT TOP(@__p_0) [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate]
    FROM [Orders] AS [o0]
    ORDER BY [o0].[OrderID]
) AS [t]
CROSS JOIN (
    SELECT [o2].[OrderID], [o2].[CustomerID], [o2].[EmployeeID], [o2].[OrderDate]
    FROM [Orders] AS [o2]
    ORDER BY [o2].[OrderID]
    OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY
) AS [t0]
LEFT JOIN [Customers] AS [c] ON [t0].[CustomerID] = [c].[CustomerID]",
                    Sql);
            }
        }

        public override void Include_reference_when_projection()
        {
            base.Include_reference_when_projection();

            Assert.Equal(
                @"SELECT [o].[CustomerID]
FROM [Orders] AS [o]",
                Sql);
        }

        public override void Include_reference_with_filter_reordered()
        {
            base.Include_reference_with_filter_reordered();

            Assert.Equal(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate], [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
WHERE [o].[CustomerID] = 'ALFKI'",
                Sql);
        }

        public override void Include_reference_with_filter()
        {
            base.Include_reference_with_filter();

            Assert.Equal(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate], [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
WHERE [o].[CustomerID] = 'ALFKI'",
                Sql);
        }

        public override void Include_collection_dependent_already_tracked_as_no_tracking()
        {
            base.Include_collection_dependent_already_tracked_as_no_tracking();

            Assert.Equal(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
WHERE [o].[CustomerID] = 'ALFKI'

SELECT TOP(2) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] = 'ALFKI'
ORDER BY [c].[CustomerID]

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
INNER JOIN (
    SELECT DISTINCT TOP(2) [c].[CustomerID]
    FROM [Customers] AS [c]
    WHERE [c].[CustomerID] = 'ALFKI'
    ORDER BY [c].[CustomerID]
) AS [c0] ON [o].[CustomerID] = [c0].[CustomerID]
ORDER BY [c0].[CustomerID]",
                Sql);
        }

        public override void Include_collection_dependent_already_tracked()
        {
            base.Include_collection_dependent_already_tracked();

            Assert.Equal(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
WHERE [o].[CustomerID] = 'ALFKI'

SELECT TOP(2) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] = 'ALFKI'
ORDER BY [c].[CustomerID]

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
INNER JOIN (
    SELECT DISTINCT TOP(2) [c].[CustomerID]
    FROM [Customers] AS [c]
    WHERE [c].[CustomerID] = 'ALFKI'
    ORDER BY [c].[CustomerID]
) AS [c0] ON [o].[CustomerID] = [c0].[CustomerID]
ORDER BY [c0].[CustomerID]",
                Sql);
        }

        public override void Include_reference_dependent_already_tracked()
        {
            base.Include_reference_dependent_already_tracked();

            Assert.Equal(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
WHERE [o].[CustomerID] = 'ALFKI'

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate], [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]",
                Sql);
        }

        public override void Include_reference_as_no_tracking()
        {
            base.Include_reference_as_no_tracking();

            Assert.Equal(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate], [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]",
                Sql);
        }

        public override void Include_collection_as_no_tracking2()
        {
            base.Include_collection_as_no_tracking2();

            Assert.Equal(
                @"@__p_0: 5

SELECT TOP(@__p_0) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
ORDER BY [c].[CustomerID]

@__p_0: 5

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
INNER JOIN (
    SELECT DISTINCT TOP(@__p_0) [c].[CustomerID]
    FROM [Customers] AS [c]
    ORDER BY [c].[CustomerID]
) AS [c0] ON [o].[CustomerID] = [c0].[CustomerID]
ORDER BY [c0].[CustomerID]",
                Sql);
        }

        public override void Include_with_complex_projection()
        {
            base.Include_with_complex_projection();

            Assert.Equal(
                @"SELECT [o].[CustomerID]
FROM [Orders] AS [o]",
                Sql);
        }

        public override void Include_with_take()
        {
            base.Include_with_take();

            Assert.Equal(
                @"@__p_0: 10

SELECT TOP(@__p_0) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
ORDER BY [c].[City] DESC, [c].[CustomerID]

@__p_0: 10

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
INNER JOIN (
    SELECT DISTINCT TOP(@__p_0) [c].[City], [c].[CustomerID]
    FROM [Customers] AS [c]
    ORDER BY [c].[City] DESC, [c].[CustomerID]
) AS [c0] ON [o].[CustomerID] = [c0].[CustomerID]
ORDER BY [c0].[City] DESC, [c0].[CustomerID]",
                Sql);
        }

        public override void Include_with_skip()
        {
            base.Include_with_skip();
            if (SupportsOffset)
            {
                Assert.Equal(
                    @"@__p_0: 80

SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
ORDER BY [c].[ContactName], [c].[CustomerID]
OFFSET @__p_0 ROWS

@__p_0: 80

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
INNER JOIN (
    SELECT DISTINCT [t].*
    FROM (
        SELECT [c].[ContactName], [c].[CustomerID]
        FROM [Customers] AS [c]
        ORDER BY [c].[ContactName], [c].[CustomerID]
        OFFSET @__p_0 ROWS
    ) AS [t]
) AS [c0] ON [o].[CustomerID] = [c0].[CustomerID]
ORDER BY [c0].[ContactName], [c0].[CustomerID]",
                    Sql);
            }
        }

        private static string Sql => TestSqlLoggerFactory.Sql;
    }
}
