using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.TestUtilities;
using System;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class MappingQuerySqlCeTest : MappingQueryTestBase<MappingQuerySqlCeTest.MappingQuerySqlCeFixture>
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

        public MappingQuerySqlCeTest(MappingQuerySqlCeFixture fixture)
            : base(fixture)
        {
            Fixture.TestSqlLoggerFactory.Clear();
        }

        private const string FileLineEnding = @"
";
        private string Sql => Fixture.TestSqlLoggerFactory.Sql;

        public class MappingQuerySqlCeFixture : MappingQueryFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => SqlCeNorthwindTestStoreFactory.Instance;

            protected override string DatabaseSchema { get; } = null;

            protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
            {
                base.OnModelCreating(modelBuilder, context);

                modelBuilder.Entity<MappedCustomer>(e =>
                {
                    e.Property(c => c.CompanyName2).Metadata.SqlCe().ColumnName = "CompanyName";
                    e.Metadata.SqlCe().TableName = "Customers";
                });

                modelBuilder.Entity<MappedEmployee>()
                    .Property(c => c.EmployeeID)
                    .HasColumnType("int");
            }
        }
    }
}
