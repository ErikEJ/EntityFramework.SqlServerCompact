using System;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class MappingQuerySqlCeTest : MappingQueryTestBase, IClassFixture<MappingQuerySqlCeFixture>
    {
        public override void All_customers()
        {
            base.All_customers();

            Assert.Equal(
                @"SELECT [c].[CustomerID], [c].[CompanyName]
FROM [Customers] AS [c]",
                Sql);
        }

        public override void All_employees()
        {
            base.All_employees();

            Assert.Equal(
                @"SELECT [e].[EmployeeID], [e].[City]
FROM [Employees] AS [e]",
                Sql);
        }

        public override void All_orders()
        {
            base.All_orders();

            Assert.Equal(
                @"SELECT [o].[OrderID], [o].[ShipVia]
FROM [Orders] AS [o]",
                Sql);
        }

        public override void Project_nullable_enum()
        {
            base.Project_nullable_enum();

            Assert.Equal(
                @"SELECT [o].[ShipVia]
FROM [Orders] AS [o]",
                Sql);
        }

        private readonly MappingQuerySqlCeFixture _fixture;

        public MappingQuerySqlCeTest(MappingQuerySqlCeFixture fixture)
        {
            _fixture = fixture;
            _fixture.TestSqlLoggerFactory.Clear();
        }

        protected override DbContext CreateContext() => _fixture.CreateContext();

        private const string FileLineEnding = @"
";

        private string Sql => _fixture.TestSqlLoggerFactory.Sql.Replace(Environment.NewLine, FileLineEnding);
    }
}
