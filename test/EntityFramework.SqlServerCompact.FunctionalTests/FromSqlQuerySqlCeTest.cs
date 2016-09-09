using System.Data.Common;
using System.Data.SqlServerCe;
using System.Linq;
using Microsoft.EntityFrameworkCore.Specification.Tests.TestModels.Northwind;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class FromSqlQuerySqlCeTest : FromSqlQueryTestBase<NorthwindQuerySqlCeFixture>
    {
        //disabled - see https://github.com/aspnet/EntityFramework/issues/6471
        public override void Bad_data_error_handling_invalid_cast()
        {
            //base.Bad_data_error_handling_invalid_cast();
        }

        public override void Bad_data_error_handling_invalid_cast_key()
        {
            //base.Bad_data_error_handling_invalid_cast_key();
        }

        public override void Bad_data_error_handling_invalid_cast_no_tracking()
        {
            //base.Bad_data_error_handling_invalid_cast_no_tracking();
        }

        public override void Bad_data_error_handling_invalid_cast_projection()
        {
            //base.Bad_data_error_handling_invalid_cast_projection();
        }

        public override void Bad_data_error_handling_null()
        {
            //base.Bad_data_error_handling_null();
        }

        public override void Bad_data_error_handling_null_no_tracking()
        {
            //base.Bad_data_error_handling_null_no_tracking();
        }

        public override void Bad_data_error_handling_null_projection()
        {
            //base.Bad_data_error_handling_null_projection();
        }

        public override void From_sql_queryable_simple()
        {
            base.From_sql_queryable_simple();

            Assert.Equal(
                @"SELECT * FROM ""Customers"" WHERE ""ContactName"" LIKE '%z%'",
                Sql);
        }

        public override void From_sql_queryable_simple_columns_out_of_order()
        {
            base.From_sql_queryable_simple_columns_out_of_order();

            Assert.Equal(
                @"SELECT ""Region"", ""PostalCode"", ""Phone"", ""Fax"", ""CustomerID"", ""Country"", ""ContactTitle"", ""ContactName"", ""CompanyName"", ""City"", ""Address"" FROM ""Customers""",
                Sql);
        }

        public override void From_sql_queryable_simple_columns_out_of_order_and_extra_columns()
        {
            base.From_sql_queryable_simple_columns_out_of_order_and_extra_columns();

            Assert.Equal(
                @"SELECT ""Region"", ""PostalCode"", ""PostalCode"" AS ""Foo"", ""Phone"", ""Fax"", ""CustomerID"", ""Country"", ""ContactTitle"", ""ContactName"", ""CompanyName"", ""City"", ""Address"" FROM ""Customers""",
                Sql);
        }

        public override void From_sql_queryable_composed()
        {
            base.From_sql_queryable_composed();

            Assert.Equal(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM (
    SELECT * FROM ""Customers""
) AS [c]
WHERE CHARINDEX(N'z', [c].[ContactName]) > 0",
                Sql);
        }

        public override void From_sql_queryable_multiple_composed()
        {
            base.From_sql_queryable_multiple_composed();

            Assert.Equal(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region], [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM (
    SELECT * FROM ""Customers""
) AS [c]
CROSS JOIN (
    SELECT * FROM ""Orders""
) AS [o]
WHERE [c].[CustomerID] = [o].[CustomerID]",
                Sql);
        }

        public override void From_sql_queryable_multiple_composed_with_closure_parameters()
        {
            base.From_sql_queryable_multiple_composed_with_closure_parameters();

            Assert.Equal(
                @"@__8__locals1_startDate_1: 01/01/1997 00:00:00 (DbType = DateTime)
@__8__locals1_endDate_2: 01/01/1998 00:00:00 (DbType = DateTime)

SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region], [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM (
    SELECT * FROM ""Customers""
) AS [c]
CROSS JOIN (
    SELECT * FROM ""Orders"" WHERE ""OrderDate"" BETWEEN @__8__locals1_startDate_1 AND @__8__locals1_endDate_2
) AS [o]
WHERE [c].[CustomerID] = [o].[CustomerID]",
                Sql);
        }

        public override void From_sql_queryable_multiple_composed_with_parameters_and_closure_parameters()
        {
            base.From_sql_queryable_multiple_composed_with_parameters_and_closure_parameters();

            Assert.Equal(
                @"@p0: London (Size = 4000)
@__8__locals1_startDate_1: 01/01/1997 00:00:00 (DbType = DateTime)
@__8__locals1_endDate_2: 01/01/1998 00:00:00 (DbType = DateTime)

SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region], [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM (
    SELECT * FROM ""Customers"" WHERE ""City"" = @p0
) AS [c]
CROSS JOIN (
    SELECT * FROM ""Orders"" WHERE ""OrderDate"" BETWEEN @__8__locals1_startDate_1 AND @__8__locals1_endDate_2
) AS [o]
WHERE [c].[CustomerID] = [o].[CustomerID]",
                Sql);
        }

        public override void From_sql_queryable_multiple_line_query()
        {
            base.From_sql_queryable_multiple_line_query();

            Assert.Equal(
                @"SELECT *
FROM ""Customers""
WHERE ""City"" = 'London'",
                Sql);
        }

        public override void From_sql_queryable_composed_multiple_line_query()
        {
            base.From_sql_queryable_composed_multiple_line_query();

            Assert.Equal(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM (
    SELECT *
    FROM ""Customers""
) AS [c]
WHERE [c].[City] = N'London'",
                Sql);
        }

        public override void From_sql_queryable_with_parameters()
        {
            base.From_sql_queryable_with_parameters();

            Assert.Equal(
                @"@p0: London (Size = 4000)
@p1: Sales Representative (Size = 4000)

SELECT * FROM ""Customers"" WHERE ""City"" = @p0 AND ""ContactTitle"" = @p1",
                Sql);
        }

        public override void From_sql_queryable_with_parameters_inline()
        {
            base.From_sql_queryable_with_parameters_inline();

            Assert.Equal(
                @"@p0: London (Size = 4000)
@p1: Sales Representative (Size = 4000)

SELECT * FROM ""Customers"" WHERE ""City"" = @p0 AND ""ContactTitle"" = @p1",
                Sql);
        }

        public override void From_sql_queryable_with_null_parameter()
        {
            // http://entityframework.codeplex.com/workitem/287
            int? reportsTo = null;

            using (var context = CreateContext())
            {
                var actual = context.Set<Employee>()
                    .FromSql(@"SELECT * FROM ""Employees"" WHERE ""ReportsTo"" = {0} OR (""ReportsTo"" IS NULL AND  cast({0} AS nvarchar(4000)) IS NULL)",
                        reportsTo)
                    .ToArray();

                Assert.Equal(1, actual.Length);
            }
        }

        public override void From_sql_queryable_with_parameters_and_closure()
        {
            base.From_sql_queryable_with_parameters_and_closure();

            Assert.Equal(
                @"@p0: London (Size = 4000)
@__contactTitle_1: Sales Representative (Size = 4000)

SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM (
    SELECT * FROM ""Customers"" WHERE ""City"" = @p0
) AS [c]
WHERE [c].[ContactTitle] = @__contactTitle_1",
                Sql);
        }

        public override void From_sql_queryable_simple_cache_key_includes_query_string()
        {
            base.From_sql_queryable_simple_cache_key_includes_query_string();

            Assert.Equal(
                @"SELECT * FROM ""Customers"" WHERE ""City"" = 'London'

SELECT * FROM ""Customers"" WHERE ""City"" = 'Seattle'",
                Sql);
        }

        public override void From_sql_queryable_with_parameters_cache_key_includes_parameters()
        {
            base.From_sql_queryable_with_parameters_cache_key_includes_parameters();

            Assert.Equal(
                @"@p0: London (Size = 4000)
@p1: Sales Representative (Size = 4000)

SELECT * FROM ""Customers"" WHERE ""City"" = @p0 AND ""ContactTitle"" = @p1

@p0: Madrid (Size = 4000)
@p1: Accounting Manager (Size = 4000)

SELECT * FROM ""Customers"" WHERE ""City"" = @p0 AND ""ContactTitle"" = @p1",
                Sql);
        }

        public override void From_sql_queryable_simple_as_no_tracking_not_composed()
        {
            base.From_sql_queryable_simple_as_no_tracking_not_composed();

            Assert.Equal(
                @"SELECT * FROM ""Customers""",
                Sql);
        }

        public override void From_sql_queryable_simple_projection_composed()
        {
            base.From_sql_queryable_simple_projection_composed();

            Assert.Equal(
                @"SELECT [p].[ProductName]
FROM (
    SELECT *
    FROM Products
    WHERE Discontinued <> 1
    AND ((UnitsInStock + UnitsOnOrder) < ReorderLevel)
) AS [p]",
                Sql);
        }

        public override void From_sql_queryable_simple_include()
        {
            base.From_sql_queryable_simple_include();

            Assert.Equal(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM (
    SELECT * FROM ""Customers""
) AS [c]
ORDER BY [c].[CustomerID]

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
WHERE EXISTS (
    SELECT 1
    FROM (
        SELECT * FROM ""Customers""
    ) AS [c]
    WHERE [o].[CustomerID] = [c].[CustomerID])
ORDER BY [o].[CustomerID]",
                Sql);
        }

        public override void From_sql_queryable_simple_composed_include()
        {
            base.From_sql_queryable_simple_composed_include();

            Assert.Equal(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM (
    SELECT * FROM ""Customers""
) AS [c]
WHERE [c].[City] = N'London'
ORDER BY [c].[CustomerID]

SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
WHERE EXISTS (
    SELECT 1
    FROM (
        SELECT * FROM ""Customers""
    ) AS [c]
    WHERE ([c].[City] = N'London') AND ([o].[CustomerID] = [c].[CustomerID]))
ORDER BY [o].[CustomerID]",
                Sql);
        }

        public override void From_sql_annotations_do_not_affect_successive_calls()
        {
            base.From_sql_annotations_do_not_affect_successive_calls();

            Assert.Equal(
                @"SELECT * FROM ""Customers"" WHERE ""ContactName"" LIKE '%z%'

SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]",
                Sql);
        }

        public override void From_sql_composed_with_nullable_predicate()
        {
            base.From_sql_composed_with_nullable_predicate();

            Assert.Equal(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM (
    SELECT * FROM ""Customers""
) AS [c]
WHERE ([c].[ContactName] = [c].[CompanyName]) OR ([c].[ContactName] IS NULL AND [c].[CompanyName] IS NULL)",
                Sql);
        }

        public FromSqlQuerySqlCeTest(NorthwindQuerySqlCeFixture fixture)
            : base(fixture)
        {
        }

        protected override DbParameter CreateDbParameter(string name, object value)
            => new SqlCeParameter
            {
                ParameterName = name,
                Value = value
            };

        private static string Sql => TestSqlLoggerFactory.Sql;
    }
}
