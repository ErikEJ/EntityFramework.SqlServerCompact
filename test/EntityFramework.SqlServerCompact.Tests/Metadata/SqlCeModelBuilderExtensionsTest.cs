using System.Collections.Generic;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Tests;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.Tests.Metadata
{
    public class SqlCeBuilderExtensionsTest
    {
        [Fact]
        public void Can_set_identities_for_model_with_convention_builder()
        {
            var modelBuilder = CreateConventionModelBuilder();

            modelBuilder
                .ForSqlCe()
                .UseIdentity();

            var sqlServerExtensions = modelBuilder.Model.SqlCe();

            Assert.Equal(true, sqlServerExtensions.IdentityKeyGeneration);
        }

        [Fact]
        public void Can_set_identities_for_model_with_convention_builder_using_nested_closure()
        {
            var modelBuilder = CreateConventionModelBuilder();

            modelBuilder
                .ForSqlCe(b => { b.UseIdentity(); });

            var relationalExtensions = modelBuilder.Model.Relational();
            var sqlServerExtensions = modelBuilder.Model.SqlCe();

            Assert.Equal(true, sqlServerExtensions.IdentityKeyGeneration);
        }

        [Fact]
        public void Can_set_identities_for_property_with_convention_builder()
        {
            var modelBuilder = CreateConventionModelBuilder();

            modelBuilder
                .Entity<Customer>()
                .Property(e => e.Id)
                .ForSqlCe()
                .UseIdentity();

            var model = modelBuilder.Model;
            var property = model.GetEntityType(typeof(Customer)).GetProperty("Id");

            Assert.Equal(true, property.SqlCe().IdentityKeyGeneration);
            Assert.Equal(StoreGeneratedPattern.Identity, property.StoreGeneratedPattern);
        }

        [Fact]
        public void Can_set_identities_for_property_with_convention_builder_using_nested_closure()
        {
            var modelBuilder = CreateConventionModelBuilder();

            modelBuilder
                .Entity<Customer>()
                .Property(e => e.Id)
                .ForSqlCe(b => { b.UseIdentity(); });

            var model = modelBuilder.Model;
            var property = model.GetEntityType(typeof(Customer)).GetProperty("Id");

            Assert.Equal(true, property.SqlCe().IdentityKeyGeneration);
            Assert.Equal(StoreGeneratedPattern.Identity, property.StoreGeneratedPattern);
        }

        [Fact]
        public void ForSqlServer_methods_dont_break_out_of_the_generics()
        {
            var modelBuilder = CreateConventionModelBuilder();

            AssertIsGeneric(
                modelBuilder
                    .Entity<Customer>()
                    .Property(e => e.Name)
                    .ForSqlCe(b => { }));
        }

        private void AssertIsGeneric(PropertyBuilder<string> _)
        {
        }

        protected virtual ModelBuilder CreateConventionModelBuilder()
        {
            return SqlCeTestHelpers.Instance.CreateConventionBuilder();
        }

        private class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public IEnumerable<Order> Orders { get; set; }
        }

        private class Order
        {
            public int OrderId { get; set; }

            public int CustomerId { get; set; }
            public Customer Customer { get; set; }

            public OrderDetails Details { get; set; }
        }

        private class OrderDetails
        {
            public int Id { get; set; }

            public int OrderId { get; set; }
            public Order Order { get; set; }
        }
    }
}

