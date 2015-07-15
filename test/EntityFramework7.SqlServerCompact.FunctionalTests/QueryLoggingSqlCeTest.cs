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
    Compiled query expression.
", TestSqlLoggerFactory.Log);
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
    Compiled query expression.
", TestSqlLoggerFactory.Log);
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
