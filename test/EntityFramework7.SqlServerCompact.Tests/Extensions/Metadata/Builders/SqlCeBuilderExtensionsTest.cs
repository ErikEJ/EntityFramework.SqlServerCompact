using System.Collections.Generic;
using Microsoft.Data.Entity.Metadata.Conventions;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Xunit;

namespace Microsoft.Data.Entity.SqlServerCompact.Metadata.Builders
{
    public class SqlCeBuilderExtensionsTest
    {
        [Fact]
        public void Can_set_column_name()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .HasSqlCeColumnName("MyNameIs")
                .Metadata;

            Assert.Equal("MyNameIs", property.SqlCe().ColumnName);
        }

        [Fact]
        public void Can_set_column_name_non_generic()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var property = modelBuilder
                .Entity<Customer>()
                .Property<string>("Name")
                .HasSqlCeColumnName("MyNameIs")
                .Metadata;

            Assert.Equal("MyNameIs", property.SqlCe().ColumnName);
        }

        [Fact]
        public void Can_set_column_type()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .HasSqlCeColumnType("nvarchar(DA)")
                .Metadata;

            Assert.Equal("nvarchar(DA)", property.SqlCe().ColumnType);
        }

        [Fact]
        public void Can_set_column_type_non_generic()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var property = modelBuilder
                .Entity<Customer>()
                .Property<string>("Name")
                .HasSqlCeColumnType("nvarchar(DA)")
                .Metadata;

            Assert.Equal("nvarchar(DA)", property.SqlCe().ColumnType);
        }

        [Fact]
        public void Can_set_column_default_expression()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .HasSqlCeDefaultValueSql("VanillaCoke")
                .Metadata;

            Assert.Equal("VanillaCoke", property.SqlCe().GeneratedValueSql);
        }

        [Fact]
        public void Can_set_column_default_expression_non_generic()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var property = modelBuilder
                .Entity<Customer>()
                .Property<string>("Name")
                .HasSqlCeDefaultValueSql("VanillaCoke")
                .Metadata;

            Assert.Equal("VanillaCoke", property.SqlCe().GeneratedValueSql);
        }

        [Fact]
        public void Can_set_key_name()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var key = modelBuilder
                .Entity<Customer>()
                .Key(e => e.Id)
                .SqlCeKeyName("LemonSupreme")
                .Metadata;

            Assert.Equal("LemonSupreme", key.SqlCe().Name);
        }

        [Fact]
        public void Can_set_foreign_key_name_for_one_to_many()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var foreignKey = modelBuilder
                .Entity<Customer>().Collection(e => e.Orders).InverseReference(e => e.Customer)
                .SqlCeConstraintName("ChocolateLimes")
                .Metadata;

            Assert.Equal("ChocolateLimes", foreignKey.SqlCe().Name);
        }

        [Fact]
        public void Can_set_foreign_key_name_for_one_to_many_non_generic()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var foreignKey = modelBuilder
                .Entity<Customer>().Collection(typeof(Order)).InverseReference()
                .SqlCeConstraintName("ChocolateLimes")
                .Metadata;

            Assert.Equal("ChocolateLimes", foreignKey.SqlCe().Name);
        }

        [Fact]
        public void Can_set_foreign_key_name_for_many_to_one()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var foreignKey = modelBuilder
                .Entity<Order>().Reference(e => e.Customer).InverseCollection(e => e.Orders)
                .SqlCeConstraintName("ChocolateLimes")
                .Metadata;

            Assert.Equal("ChocolateLimes", foreignKey.SqlCe().Name);
        }

        [Fact]
        public void Can_set_foreign_key_name_for_many_to_one_non_generic()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var foreignKey = modelBuilder
                .Entity<Order>().Reference(typeof(Customer)).InverseCollection()
                .SqlCeConstraintName("ChocolateLimes")
                .Metadata;

            Assert.Equal("ChocolateLimes", foreignKey.SqlCe().Name);
        }

        [Fact]
        public void Can_set_foreign_key_name_for_one_to_one()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var foreignKey = modelBuilder
                .Entity<Order>().Reference(e => e.Details).InverseReference(e => e.Order)
                .SqlCeConstraintName("ChocolateLimes")
                .Metadata;

            Assert.Equal("ChocolateLimes", foreignKey.SqlCe().Name);
        }

        [Fact]
        public void Can_set_foreign_key_name_for_one_to_one_non_generic()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var foreignKey = modelBuilder
                .Entity<Order>().Reference(typeof(OrderDetails)).InverseReference()
                .SqlCeConstraintName("ChocolateLimes")
                .Metadata;

            Assert.Equal("ChocolateLimes", foreignKey.SqlCe().Name);
        }

        [Fact]
        public void Can_set_index_name()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var index = modelBuilder
                .Entity<Customer>()
                .Index(e => e.Id)
                .SqlCeIndexName("Dexter")
                .Metadata;

            Assert.Equal("Dexter", index.SqlCe().Name);
        }

        [Fact]
        public void Can_set_table_name()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var entityType = modelBuilder
                .Entity<Customer>()
                .ToSqlCeTable("Custardizer")
                .Metadata;

            Assert.Equal("Custardizer", entityType.SqlCe().TableName);
        }

        [Fact]
        public void Can_set_table_name_non_generic()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var entityType = modelBuilder
                .Entity("Customer")
                .ToSqlCeTable("Custardizer")
                .Metadata;

            Assert.Equal("Custardizer", entityType.SqlCe().TableName);
        }

        private class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public IEnumerable<Order> Orders { get; set; }
        }

        private class Order
        {
            public Customer Customer { get; set; }
            public OrderDetails Details { get; set; }
        }

        private class OrderDetails
        {
            public Order Order { get; set; }
        }
    }
}
