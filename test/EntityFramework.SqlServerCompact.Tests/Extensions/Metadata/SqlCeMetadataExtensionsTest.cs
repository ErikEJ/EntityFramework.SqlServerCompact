using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Tests.Extensions.Metadata
{
    public class SqlCeMetadataExtensionsTest
    {
        [Fact]
        public void Can_get_and_set_column_name()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .Metadata;

            Assert.Equal("Name", property.SqlCe().ColumnName);
            Assert.Equal("Name", ((IProperty)property).SqlCe().ColumnName);

            property.Relational().ColumnName = "Eman";

            Assert.Equal("Eman", property.SqlCe().ColumnName);
            Assert.Equal("Eman", ((IProperty)property).SqlCe().ColumnName);

            property.SqlCe().ColumnName = "MyNameIs";

            Assert.Equal("MyNameIs", property.SqlCe().ColumnName);
            Assert.Equal("MyNameIs", ((IProperty)property).SqlCe().ColumnName);

            property.SqlCe().ColumnName = null;

            Assert.Equal("Eman", property.SqlCe().ColumnName);
            Assert.Equal("Eman", ((IProperty)property).SqlCe().ColumnName);
        }

        [Fact]
        public void Can_get_and_set_table_name()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var entityType = modelBuilder
                .Entity<Customer>()
                .Metadata;

            Assert.Equal("Customer", entityType.SqlCe().TableName);
            Assert.Equal("Customer", ((IEntityType)entityType).SqlCe().TableName);

            entityType.Relational().TableName = "Customizer";

            Assert.Equal("Customizer", entityType.SqlCe().TableName);
            Assert.Equal("Customizer", ((IEntityType)entityType).SqlCe().TableName);

            entityType.SqlCe().TableName = "Custardizer";

            Assert.Equal("Custardizer", entityType.SqlCe().TableName);
            Assert.Equal("Custardizer", ((IEntityType)entityType).SqlCe().TableName);

            entityType.SqlCe().TableName = null;

            Assert.Equal("Customizer", entityType.SqlCe().TableName);
            Assert.Equal("Customizer", ((IEntityType)entityType).SqlCe().TableName);
        }

        [Fact]
        public void Can_get_schema_name()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var entityType = modelBuilder
                .Entity<Customer>()
                .Metadata;

            Assert.Null(entityType.SqlCe().Schema);
            Assert.Null(((IEntityType)entityType).SqlCe().Schema);

            entityType.Relational().Schema = "db0";

            Assert.Equal("db0", entityType.SqlCe().Schema);
            Assert.Equal("db0", ((IEntityType)entityType).SqlCe().Schema);
        }

        [Fact]
        public void Can_get_and_set_column_type()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .Metadata;

            Assert.Null(property.SqlCe().ColumnType);
            Assert.Null(((IProperty)property).SqlCe().ColumnType);

            property.Relational().ColumnType = "nvarchar(max)";

            Assert.Equal("nvarchar(max)", property.SqlCe().ColumnType);
            Assert.Equal("nvarchar(max)", ((IProperty)property).SqlCe().ColumnType);

            property.SqlCe().ColumnType = "nvarchar(verstappen)";

            Assert.Equal("nvarchar(verstappen)", property.SqlCe().ColumnType);
            Assert.Equal("nvarchar(verstappen)", ((IProperty)property).SqlCe().ColumnType);

            property.SqlCe().ColumnType = null;

            Assert.Equal("nvarchar(max)", property.SqlCe().ColumnType);
            Assert.Equal("nvarchar(max)", ((IProperty)property).SqlCe().ColumnType);
        }

        [Fact]
        public void Can_get_and_set_column_default_expression()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .Metadata;

            Assert.Null(property.SqlCe().ComputedValueSql);
            Assert.Null(((IProperty)property).SqlCe().ComputedValueSql);

            property.Relational().ComputedValueSql = "newsequentialid()";

            Assert.Equal("newsequentialid()", property.SqlCe().ComputedValueSql);
            Assert.Equal("newsequentialid()", ((IProperty)property).SqlCe().ComputedValueSql);

            property.SqlCe().ComputedValueSql = "expressyourself()";

            Assert.Equal("expressyourself()", property.SqlCe().ComputedValueSql);
            Assert.Equal("expressyourself()", ((IProperty)property).SqlCe().ComputedValueSql);

            property.SqlCe().ComputedValueSql = null;

            Assert.Equal("newsequentialid()", property.SqlCe().ComputedValueSql);
            Assert.Equal("newsequentialid()", ((IProperty)property).SqlCe().ComputedValueSql);
        }

        [Fact]
        public void Can_get_and_set_column_key_name()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var key = modelBuilder
                .Entity<Customer>()
                .HasKey(e => e.Id)
                .Metadata;

            Assert.Equal("PK_Customer", key.SqlCe().Name);
            Assert.Equal("PK_Customer", ((IKey)key).SqlCe().Name);

            key.Relational().Name = "PrimaryKey";

            Assert.Equal("PrimaryKey", key.SqlCe().Name);
            Assert.Equal("PrimaryKey", ((IKey)key).SqlCe().Name);

            key.SqlCe().Name = "PrimarySchool";

            Assert.Equal("PrimarySchool", key.SqlCe().Name);
            Assert.Equal("PrimarySchool", ((IKey)key).SqlCe().Name);

            key.SqlCe().Name = null;

            Assert.Equal("PrimaryKey", key.SqlCe().Name);
            Assert.Equal("PrimaryKey", ((IKey)key).SqlCe().Name);
        }

        [Fact]
        public void Can_get_and_set_column_foreign_key_name()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            modelBuilder
                .Entity<Customer>()
                .HasKey(e => e.Id);

            var foreignKey = modelBuilder
                .Entity<Order>()
                .HasOne<Customer>()
                .WithOne()
                .HasForeignKey<Order>(e => e.CustomerId)
                .Metadata;

            Assert.Equal("FK_Order_Customer_CustomerId", foreignKey.Relational().Name);
            Assert.Equal("FK_Order_Customer_CustomerId", foreignKey.SqlCe().Name);
            Assert.Equal("FK_Order_Customer_CustomerId", ((IForeignKey)foreignKey).SqlCe().Name);

            foreignKey.Relational().Name = "FK";

            Assert.Equal("FK", foreignKey.Relational().Name);
            Assert.Equal("FK", foreignKey.SqlCe().Name);
            Assert.Equal("FK", ((IForeignKey)foreignKey).SqlCe().Name);

            foreignKey.SqlCe().Name = "KFC";

            Assert.Equal("FK", foreignKey.Relational().Name);
            Assert.Equal("KFC", foreignKey.SqlCe().Name);
            Assert.Equal("KFC", ((IForeignKey)foreignKey).SqlCe().Name);

            foreignKey.SqlCe().Name = null;

            Assert.Equal("FK", foreignKey.Relational().Name);
            Assert.Equal("FK", foreignKey.SqlCe().Name);
            Assert.Equal("FK", ((IForeignKey)foreignKey).SqlCe().Name);
        }

        [Fact]
        public void Can_get_and_set_index_name()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var index = modelBuilder
                .Entity<Customer>()
                .HasIndex(e => e.Id)
                .Metadata;

            Assert.Equal("IX_Customer_Id", index.SqlCe().Name);
            Assert.Equal("IX_Customer_Id", ((IIndex)index).SqlCe().Name);

            index.Relational().Name = "MyIndex";

            Assert.Equal("MyIndex", index.SqlCe().Name);
            Assert.Equal("MyIndex", ((IIndex)index).SqlCe().Name);

            index.SqlCe().Name = "DexKnows";

            Assert.Equal("DexKnows", index.SqlCe().Name);
            Assert.Equal("DexKnows", ((IIndex)index).SqlCe().Name);

            index.SqlCe().Name = null;

            Assert.Equal("MyIndex", index.SqlCe().Name);
            Assert.Equal("MyIndex", ((IIndex)index).SqlCe().Name);
        }

        private class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private class Order
        {
            public int CustomerId { get; set; }
        }
    }
}
