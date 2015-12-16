using System.Collections.Generic;
using Microsoft.Data.Entity.Metadata.Conventions;
using Xunit;

namespace Microsoft.Data.Entity.Tests.Extensions.Metadata.Builders
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
                .ForSqlCeHasColumnName("MyNameIs")
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
                .ForSqlCeHasColumnName("MyNameIs")
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
                .ForSqlCeHasColumnType("nvarchar(DA)")
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
                .ForSqlCeHasColumnType("nvarchar(DA)")
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
                .ForSqlCeHasDefaultValueSql("VanillaCoke")
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
                .ForSqlCeHasDefaultValueSql("VanillaCoke")
                .Metadata;

            Assert.Equal("VanillaCoke", property.SqlCe().GeneratedValueSql);
        }

        [Fact]
        public void Can_set_key_name()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var key = modelBuilder
                .Entity<Customer>()
                .HasKey(e => e.Id)
                .ForSqlCeHasName("LemonSupreme")
                .Metadata;

            Assert.Equal("LemonSupreme", key.SqlCe().Name);
        }

        [Fact]
        public void Can_set_foreign_key_name_for_one_to_many()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var foreignKey = modelBuilder
                .Entity<Customer>().HasMany(e => e.Orders).WithOne(e => e.Customer)
                .ForSqlCeHasConstraintName("ChocolateLimes")
                .Metadata;

            Assert.Equal("ChocolateLimes", foreignKey.SqlCe().Name);
        }

        [Fact]
        public void Can_set_foreign_key_name_for_one_to_many_non_generic()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var foreignKey = modelBuilder
                .Entity<Customer>().HasMany(typeof(Order)).WithOne()
                .ForSqlCeHasConstraintName("ChocolateLimes")
                .Metadata;

            Assert.Equal("ChocolateLimes", foreignKey.SqlCe().Name);
        }

        [Fact]
        public void Can_set_foreign_key_name_for_many_to_one()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var foreignKey = modelBuilder
                .Entity<Order>().HasOne(e => e.Customer).WithMany(e => e.Orders)
                .ForSqlCeHasConstraintName("ChocolateLimes")
                .Metadata;

            Assert.Equal("ChocolateLimes", foreignKey.SqlCe().Name);
        }

        [Fact]
        public void Can_set_foreign_key_name_for_many_to_one_non_generic()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var foreignKey = modelBuilder
                .Entity<Order>().HasMany(typeof(Customer)).WithOne()
                .ForSqlCeHasConstraintName("ChocolateLimes")
                .Metadata;

            Assert.Equal("ChocolateLimes", foreignKey.SqlCe().Name);
        }

        [Fact]
        public void Can_set_foreign_key_name_for_one_to_one()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var foreignKey = modelBuilder
                .Entity<Order>().HasOne(e => e.Details).WithOne(e => e.Order)
                .ForSqlCeHasConstraintName("ChocolateLimes")
                .Metadata;

            Assert.Equal("ChocolateLimes", foreignKey.SqlCe().Name);
        }

        [Fact]
        public void Can_set_foreign_key_name_for_one_to_one_non_generic()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var foreignKey = modelBuilder
                .Entity<Order>().HasMany(typeof(OrderDetails)).WithOne()
                .ForSqlCeHasConstraintName("ChocolateLimes")
                .Metadata;

            Assert.Equal("ChocolateLimes", foreignKey.SqlCe().Name);
        }

        [Fact]
        public void Can_set_index_name()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var index = modelBuilder
                .Entity<Customer>()
                .HasIndex(e => e.Id)
                .ForSqlCeHasName("Dexter")
                .Metadata;

            Assert.Equal("Dexter", index.SqlCe().Name);
        }

        [Fact]
        public void Can_set_table_name()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var entityType = modelBuilder
                .Entity<Customer>()
                .ForSqlCeToTable("Custardizer")
                .Metadata;

            Assert.Equal("Custardizer", entityType.SqlCe().TableName);
        }

        [Fact]
        public void Can_set_table_name_non_generic()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var entityType = modelBuilder
                .Entity("Customer")
                .ForSqlCeToTable("Custardizer")
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
