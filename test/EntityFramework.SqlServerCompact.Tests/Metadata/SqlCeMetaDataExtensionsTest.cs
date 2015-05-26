using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.ModelConventions;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.Tests.Metadata
{
    public class SqlCeMetadataExtensionsTest
    {
        [Fact]
        public void Throws_setting_identity_generation_for_invalid_type()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .Metadata;

            Assert.Equal(
                string.Format(Strings.IdentityBadType, "Name", typeof(Customer).FullName, "String"),
                Assert.Throws<ArgumentException>(
                    () => property.SqlCe().IdentityKeyGeneration = true).Message);
        }

        [Fact]
        public void Throws_setting_identity_generation_for_byte_property()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Byte)
                .Metadata;

            Assert.Equal(
                string.Format(Strings.IdentityBadType, "Byte", typeof(Customer).FullName, "Byte"),
                Assert.Throws<ArgumentException>(
                    () => property.SqlCe().IdentityKeyGeneration = true).Message);
        }

        [Fact]
        public void Throws_setting_identity_generation_for_nullable_byte_property()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.NullableByte)
                .Metadata;

            Assert.Equal(
                string.Format(Strings.IdentityBadType, "NullableByte", typeof(Customer).FullName, "Nullable`1"),
                Assert.Throws<ArgumentException>(
                    () => property.SqlCe().IdentityKeyGeneration = true).Message);
        }

        private class Customer
        {
            public int Id { get; set; }
            public int? NullableInt { get; set; }
            public string Name { get; set; }
            public byte Byte { get; set; }
            public byte? NullableByte { get; set; }
        }

        private class Order
        {
            public int OrderId { get; set; }
            public int CustomerId { get; set; }
        }
    }
}

