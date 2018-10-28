using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using EFCore.SqlCe.Storage.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;

// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore.Storage
{
    public class SqlCeTypeMappingTest : RelationalTypeMappingTest
    {
        [Theory]
        [InlineData(nameof(ChangeTracker.DetectChanges), false)]
        [InlineData(nameof(PropertyEntry.CurrentValue), false)]
        [InlineData(nameof(PropertyEntry.OriginalValue), false)]
        [InlineData(nameof(ChangeTracker.DetectChanges), true)]
        [InlineData(nameof(PropertyEntry.CurrentValue), true)]
        [InlineData(nameof(PropertyEntry.OriginalValue), true)]
        public void Row_version_is_marked_as_modified_only_if_it_really_changed(string mode, bool changeValue)
        {
            using (var context = new OptimisticContext())
            {
                var token = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                var newToken = changeValue ? new byte[] { 1, 2, 3, 4, 0, 6, 7, 8 } : token;

                var entity = context.Attach(
                    new WithRowVersion
                    {
                        Id = 789,
                        Version = token.ToArray()
                    }).Entity;

                var propertyEntry = context.Entry(entity).Property(e => e.Version);

                Assert.Equal(token, propertyEntry.CurrentValue);
                Assert.Equal(token, propertyEntry.OriginalValue);
                Assert.False(propertyEntry.IsModified);
                Assert.Equal(EntityState.Unchanged, context.Entry(entity).State);

                switch (mode)
                {
                    case nameof(ChangeTracker.DetectChanges):
                        entity.Version = newToken.ToArray();
                        context.ChangeTracker.DetectChanges();
                        break;
                    case nameof(PropertyEntry.CurrentValue):
                        propertyEntry.CurrentValue = newToken.ToArray();
                        break;
                    case nameof(PropertyEntry.OriginalValue):
                        propertyEntry.OriginalValue = newToken.ToArray();
                        break;
                    default:
                        throw new NotImplementedException("Unexpected test mode.");
                }

                Assert.Equal(changeValue, propertyEntry.IsModified);
                Assert.Equal(changeValue ? EntityState.Modified : EntityState.Unchanged, context.Entry(entity).State);
            }
        }

        private class WithRowVersion
        {
            public int Id { get; set; }
            public byte[] Version { get; set; }
        }

        private class OptimisticContext : DbContext
        {
            public DbSet<WithRowVersion> _ { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseSqlCe("Data Source=Branston");

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<WithRowVersion>().Property(e => e.Version).IsRowVersion();
            }
        }

        protected override DbCommand CreateTestCommand()
            => new SqlCommand();

        protected override DbType DefaultParameterType
            => DbType.Int32;

        [Theory]
        [InlineData(typeof(SqlCeDateTimeTypeMapping), typeof(DateTime))]
        [InlineData(typeof(SqlCeFloatTypeMapping), typeof(float))]
        public override void Create_and_clone_with_converter(Type mappingType, Type clrType)
        {
            base.Create_and_clone_with_converter(mappingType, clrType);
        }

        [Theory]
        [InlineData(typeof(SqlCeByteArrayTypeMapping), typeof(byte[]))]
        public override void Create_and_clone_sized_mappings_with_converter(Type mappingType, Type clrType)
        {
            base.Create_and_clone_sized_mappings_with_converter(mappingType, clrType);
        }

        [Theory]
        [InlineData(typeof(SqlCeStringTypeMapping), typeof(string))]
        public override void Create_and_clone_unicode_sized_mappings_with_converter(Type mappingType, Type clrType)
        {
            base.Create_and_clone_unicode_sized_mappings_with_converter(mappingType, clrType);
        }

        public static RelationalTypeMapping GetMapping(Type type)
            => (RelationalTypeMapping)new SqlCeTypeMappingSource(
                    TestServiceFactory.Instance.Create<TypeMappingSourceDependencies>(),
                    TestServiceFactory.Instance.Create<RelationalTypeMappingSourceDependencies>())
                .FindMapping(type);


        public static RelationalTypeMapping GetMapping(string type)
            => new SqlCeTypeMappingSource(
                TestServiceFactory.Instance.Create<TypeMappingSourceDependencies>(),
                TestServiceFactory.Instance.Create<RelationalTypeMappingSourceDependencies>())
                .FindMapping(type);

        [Fact]
        public virtual void GenerateSqlLiteralValue_returns_Unicode_String_literal()
        {
            var mapping = GetMapping("nvarchar(max)");

            var literal = mapping.GenerateSqlLiteral("A Unicode String");

            Assert.Equal("N'A Unicode String'", literal);
        }

        protected override DbContextOptions ContextOptions { get; }
            = new DbContextOptionsBuilder().UseSqlCe("Server=Dummy").Options;
    }
}
