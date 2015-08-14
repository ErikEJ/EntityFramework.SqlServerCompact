﻿using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.FunctionalTests.TestModels.Northwind;
using Microsoft.Framework.Logging;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class QueryLoggingSqlCeTest : IClassFixture<NorthwindQuerySqlCeFixture>
    {
        [Fact]
        public virtual void Queryable_simple()
        {
            using (var context = CreateContext())
            {
                var customers
                    = context.Set<Customer>()
                        .ToList();

                Assert.NotNull(customers);
                Assert.StartsWith(Microsoft.Data.Entity.Internal.Strings.DebugLogWarning(nameof(LogLevel.Debug), nameof(ILoggerFactory) + "." + nameof(ILoggerFactory.MinimumLevel), nameof(LogLevel) + "." + nameof(LogLevel.Verbose)) + @"
    Compiling query model: 'value(Microsoft.Data.Entity.Query.EntityQueryable`1[Microsoft.Data.Entity.FunctionalTests.TestModels.Northwind.Customer])'
    Optimized query model: 'value(Microsoft.Data.Entity.Query.EntityQueryable`1[Microsoft.Data.Entity.FunctionalTests.TestModels.Northwind.Customer])'
    Tracking query sources: [<generated>_0]
    TRACKED: True
(QueryContext prm0, QueryResultScope prm1) => () => IEnumerable<Customer> _Select(
    source: IEnumerable<QueryResultScope> _SelectMany(
        source: IEnumerable<QueryResultScope> _ToSequence(
            element: prm1
        )
        , 
        selector: (QueryResultScope prm1) => IEnumerable<QueryResultScope<Customer>> _ShapedQuery(
            queryContext: prm0, 
            commandBuilder: SelectExpression: 
                SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                FROM ""Customers"" AS ""c""
            , 
            shaper: (ValueBuffer prm2) => QueryResultScope<Customer> CreateEntity(
                querySource: from Customer <generated>_0 in value(EntityQueryable`1[FunctionalTests.TestModels.Northwind.Customer]), 
                queryContext: prm0, 
                parentQueryResultScope: prm1, 
                valueBuffer: prm2, 
                valueBufferOffset: 0, 
                entityType: FunctionalTests.TestModels.Northwind.Customer, 
                queryStateManager: True, 
                entityKeyFactory: SimpleNullSentinelEntityKeyFactory`1, 
                keyProperties: List<Property> { Customer.CustomerID, }, 
                materializer: (ValueBuffer prm3) => 
                {
                    var var4
                    var4 = new Customer()
                    var4.CustomerID = (string) object prm3.get_Item(0)
                    var4.Address = (string) object prm3.get_Item(1)
                    var4.City = (string) object prm3.get_Item(2)
                    var4.CompanyName = (string) object prm3.get_Item(3)
                    var4.ContactName = (string) object prm3.get_Item(4)
                    var4.ContactTitle = (string) object prm3.get_Item(5)
                    var4.Country = (string) object prm3.get_Item(6)
                    var4.Fax = (string) object prm3.get_Item(7)
                    var4.Phone = (string) object prm3.get_Item(8)
                    var4.PostalCode = (string) object prm3.get_Item(9)
                    var4.Region = (string) object prm3.get_Item(10)
                    var4
                }
            )
        )
    )
    , 
    selector: (QueryResultScope prm1) => Customer prm1._GetResult(
        querySource: from Customer <generated>_0 in value(EntityQueryable`1[FunctionalTests.TestModels.Northwind.Customer])
    )
)", TestSqlLoggerFactory.Log);
            }
        }

        [Fact]
        public virtual void Queryable_with_parameter()
        {
            using (var context = CreateContext())
            {
                var cities = new[] { "Seattle", "Redmond" };

                foreach (var city in cities)
                {
                    var customers
                        = context.Customers.Where(c => c.City == city).ToList();
                }
                Assert.Contains(Microsoft.Data.Entity.Internal.Strings.DebugLogWarning(nameof(LogLevel.Debug), nameof(ILoggerFactory) + "." + nameof(ILoggerFactory.MinimumLevel), nameof(LogLevel) + "." + nameof(LogLevel.Verbose)), TestSqlLoggerFactory.Log);
                Assert.Equal(@"@__city_0: Seattle

SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[City] = @__city_0

@__city_0: Redmond

SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[City] = @__city_0",
                TestSqlLoggerFactory.Sql);
            }
        }

        [Fact]
        public virtual void Include_navigation()
        {
            using (var context = CreateContext())
            {
                var customers
                    = context.Set<Customer>()
                        .Include(c => c.Orders)
                        .ToList();

                Assert.NotNull(customers);
                Assert.StartsWith(Microsoft.Data.Entity.Internal.Strings.DebugLogWarning(nameof(LogLevel.Debug), nameof(ILoggerFactory) + "." + nameof(ILoggerFactory.MinimumLevel), nameof(LogLevel) + "." + nameof(LogLevel.Verbose)) + @"
    Compiling query model: 'value(Microsoft.Data.Entity.Query.EntityQueryable`1[Microsoft.Data.Entity.FunctionalTests.TestModels.Northwind.Customer]) => AnnotateQuery(Include([c].Orders))'
    Optimized query model: 'value(Microsoft.Data.Entity.Query.EntityQueryable`1[Microsoft.Data.Entity.FunctionalTests.TestModels.Northwind.Customer])'
    Including navigation: 'Microsoft.Data.Entity.FunctionalTests.TestModels.Northwind.Customer.Orders'
    Tracking query sources: [c]
    TRACKED: True
(QueryContext prm0, QueryResultScope prm1) => () => IEnumerable<Customer> _Select(
    source: IEnumerable<QueryResultScope> _SelectMany(
        source: IEnumerable<QueryResultScope> _ToSequence(
            element: prm1
        )
        , 
        selector: (QueryResultScope prm1) => IEnumerable<QueryResultScope<Customer>> _Include(
            queryContext: (RelationalQueryContext) prm0, 
            innerResults: IEnumerable<QueryResultScope<Customer>> _ShapedQuery(
                queryContext: prm0, 
                commandBuilder: SelectExpression: 
                    SELECT ""c"".""CustomerID"", ""c"".""Address"", ""c"".""City"", ""c"".""CompanyName"", ""c"".""ContactName"", ""c"".""ContactTitle"", ""c"".""Country"", ""c"".""Fax"", ""c"".""Phone"", ""c"".""PostalCode"", ""c"".""Region""
                    FROM ""Customers"" AS ""c""
                    ORDER BY ""c"".""CustomerID""
                , 
                shaper: (ValueBuffer prm2) => QueryResultScope<Customer> CreateEntity(
                    querySource: from Customer c in value(EntityQueryable`1[FunctionalTests.TestModels.Northwind.Customer]), 
                    queryContext: prm0, 
                    parentQueryResultScope: prm1, 
                    valueBuffer: prm2, 
                    valueBufferOffset: 0, 
                    entityType: FunctionalTests.TestModels.Northwind.Customer, 
                    queryStateManager: True, 
                    entityKeyFactory: SimpleNullSentinelEntityKeyFactory`1, 
                    keyProperties: List<Property> { Customer.CustomerID, }, 
                    materializer: (ValueBuffer prm3) => 
                    {
                        var var4
                        var4 = new Customer()
                        var4.CustomerID = (string) object prm3.get_Item(0)
                        var4.Address = (string) object prm3.get_Item(1)
                        var4.City = (string) object prm3.get_Item(2)
                        var4.CompanyName = (string) object prm3.get_Item(3)
                        var4.ContactName = (string) object prm3.get_Item(4)
                        var4.ContactTitle = (string) object prm3.get_Item(5)
                        var4.Country = (string) object prm3.get_Item(6)
                        var4.Fax = (string) object prm3.get_Item(7)
                        var4.Phone = (string) object prm3.get_Item(8)
                        var4.PostalCode = (string) object prm3.get_Item(9)
                        var4.Region = (string) object prm3.get_Item(10)
                        var4
                    }
                )
            )
            , 
            querySource: from Customer c in value(EntityQueryable`1[FunctionalTests.TestModels.Northwind.Customer]), 
            navigationPath: INavigation[] { Customer.Orders, }, 
            includeRelatedValuesStrategyFactories: new Func<IIncludeRelatedValuesStrategy>[]{ () => IIncludeRelatedValuesStrategy _CreateCollectionIncludeStrategy(
                    relatedValueBuffers: IEnumerable<ValueBuffer> _Query(
                        queryContext: prm0, 
                        commandBuilder: SelectExpression: 
                            SELECT ""o"".""OrderID"", ""o"".""CustomerID"", ""o"".""EmployeeID"", ""o"".""OrderDate""
                            FROM ""Orders"" AS ""o""
                            INNER JOIN (
                                SELECT DISTINCT ""c"".""CustomerID""
                                FROM ""Customers"" AS ""c""
                            ) AS ""c"" ON ""o"".""CustomerID"" = ""c"".""CustomerID""
                            ORDER BY ""c"".""CustomerID""
                    )
                    , 
                    materializer: (ValueBuffer prm5) => 
                    {
                        var var6
                        var6 = new Order()
                        var6.OrderID = (int) object prm5.get_Item(0)
                        var6.CustomerID = (string) object prm5.get_Item(1)
                        var6.EmployeeID = (Nullable<int>) object prm5.get_Item(2)
                        var6.OrderDate = (Nullable<DateTime>) object prm5.get_Item(3)
                        var6
                    }
                )
                 }
            , 
            querySourceRequiresTracking: True
        )
    )
    , 
    selector: (QueryResultScope prm1) => Customer prm1._GetResult(
        querySource: from Customer c in value(EntityQueryable`1[FunctionalTests.TestModels.Northwind.Customer])
    )
)", TestSqlLoggerFactory.Log);
            }
        }

        private readonly NorthwindQuerySqlCeFixture _fixture;

        public QueryLoggingSqlCeTest(NorthwindQuerySqlCeFixture fixture)
        {
            _fixture = fixture;
        }

        protected NorthwindContext CreateContext()
        {
            return _fixture.CreateContext();
        }
    }
}
