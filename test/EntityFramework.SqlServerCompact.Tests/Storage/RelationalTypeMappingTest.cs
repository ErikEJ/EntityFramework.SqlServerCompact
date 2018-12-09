// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Xunit;

// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore.Storage
{
    public abstract class RelationalTypeMappingTest
    {
        protected class FakeValueConverter : ValueConverter<object, object>
        {
            public FakeValueConverter()
                : base(_ => _, _ => _)
            {
            }

            public override Type ModelClrType { get; } = typeof(object);
            public override Type ProviderClrType { get; } = typeof(object);
        }

        protected class FakeValueComparer : ValueComparer<object>
        {
            public FakeValueComparer()
                : base(false)
            {
            }

            public override Type Type { get; } = typeof(object);
        }

        [Theory]
        [InlineData(typeof(BoolTypeMapping), typeof(bool))]
        [InlineData(typeof(ByteTypeMapping), typeof(byte))]
        [InlineData(typeof(CharTypeMapping), typeof(char))]
        [InlineData(typeof(DateTimeOffsetTypeMapping), typeof(DateTimeOffset))]
        [InlineData(typeof(DateTimeTypeMapping), typeof(DateTime))]
        [InlineData(typeof(DecimalTypeMapping), typeof(decimal))]
        [InlineData(typeof(DoubleTypeMapping), typeof(double))]
        [InlineData(typeof(FloatTypeMapping), typeof(float))]
        [InlineData(typeof(GuidTypeMapping), typeof(Guid))]
        [InlineData(typeof(IntTypeMapping), typeof(int))]
        [InlineData(typeof(LongTypeMapping), typeof(long))]
        [InlineData(typeof(SByteTypeMapping), typeof(sbyte))]
        [InlineData(typeof(ShortTypeMapping), typeof(short))]
        [InlineData(typeof(TimeSpanTypeMapping), typeof(TimeSpan))]
        [InlineData(typeof(UIntTypeMapping), typeof(uint))]
        [InlineData(typeof(ULongTypeMapping), typeof(ulong))]
        [InlineData(typeof(UShortTypeMapping), typeof(ushort))]
        public virtual void Create_and_clone_with_converter(Type mappingType, Type clrType)
        {
            var mapping = (RelationalTypeMapping)Activator.CreateInstance(
                mappingType,
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance,
                null,
                new[] { FakeTypeMapping.CreateParameters(clrType) },
                null,
                null);

            var clone = mapping.Clone("<clone>", null);

            Assert.NotSame(mapping, clone);
            Assert.Same(mapping.GetType(), clone.GetType());
            Assert.Equal("<clone>", clone.StoreType);
            Assert.Equal(DbType.VarNumeric, clone.DbType);
            Assert.Null(clone.Size);
            Assert.NotNull(mapping.Converter);
            Assert.Same(mapping.Converter, clone.Converter);
            Assert.Same(mapping.Comparer, clone.Comparer);
            Assert.Same(mapping.KeyComparer, clone.KeyComparer);
            Assert.Same(typeof(object), clone.ClrType);
            Assert.Equal(StoreTypePostfix.PrecisionAndScale, clone.StoreTypePostfix);

            var newConverter = new FakeValueConverter();
            clone = (RelationalTypeMapping)mapping.Clone(newConverter);

            Assert.NotSame(mapping, clone);
            Assert.Same(mapping.GetType(), clone.GetType());
            Assert.Equal("<original>", clone.StoreType);
            Assert.Equal(DbType.VarNumeric, clone.DbType);
            Assert.Null(clone.Size);
            Assert.NotSame(mapping.Converter, clone.Converter);
            Assert.Same(mapping.Comparer, clone.Comparer);
            Assert.Same(mapping.KeyComparer, clone.KeyComparer);
            Assert.Same(typeof(object), clone.ClrType);
            Assert.Equal(StoreTypePostfix.PrecisionAndScale, clone.StoreTypePostfix);
        }

        [Theory]
        [InlineData(typeof(ByteArrayTypeMapping), typeof(byte[]))]
        public virtual void Create_and_clone_sized_mappings_with_converter(Type mappingType, Type clrType)
        {
            var mapping = (RelationalTypeMapping)Activator.CreateInstance(
                mappingType,
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance,
                null,
                new[]
                {
                    FakeTypeMapping.CreateParameters(
                        clrType,
                        size: 33,
                        fixedLength: true,
                        storeTypePostfix: StoreTypePostfix.Size)
                },
                null,
                null);

            var clone = mapping.Clone("<clone>", 66);

            Assert.NotSame(mapping, clone);
            Assert.Same(mapping.GetType(), clone.GetType());
            Assert.Equal("<original>(33)", mapping.StoreType);
            Assert.Equal("<clone>(66)", clone.StoreType);
            Assert.Equal(DbType.VarNumeric, clone.DbType);
            Assert.Equal(33, mapping.Size);
            Assert.Equal(66, clone.Size);
            Assert.NotNull(mapping.Converter);
            Assert.Same(mapping.Converter, clone.Converter);
            Assert.Same(mapping.Comparer, clone.Comparer);
            Assert.Same(mapping.KeyComparer, clone.KeyComparer);
            Assert.Same(typeof(object), clone.ClrType);
            Assert.True(mapping.IsFixedLength);
            Assert.True(clone.IsFixedLength);
            Assert.Equal(StoreTypePostfix.Size, clone.StoreTypePostfix);

            var newConverter = new FakeValueConverter();
            clone = (RelationalTypeMapping)mapping.Clone(newConverter);

            Assert.NotSame(mapping, clone);
            Assert.Same(mapping.GetType(), clone.GetType());
            Assert.Equal("<original>(33)", mapping.StoreType);
            Assert.Equal("<original>(33)", clone.StoreType);
            Assert.Equal(DbType.VarNumeric, clone.DbType);
            Assert.Equal(33, mapping.Size);
            Assert.Equal(33, clone.Size);
            Assert.NotSame(mapping.Converter, clone.Converter);
            Assert.Same(mapping.Comparer, clone.Comparer);
            Assert.Same(mapping.KeyComparer, clone.KeyComparer);
            Assert.Same(typeof(object), clone.ClrType);
            Assert.True(mapping.IsFixedLength);
            Assert.True(clone.IsFixedLength);
            Assert.Equal(StoreTypePostfix.Size, clone.StoreTypePostfix);
        }

        [Theory]
        [InlineData(typeof(StringTypeMapping), typeof(string))]
        public virtual void Create_and_clone_unicode_sized_mappings_with_converter(Type mappingType, Type clrType)
        {
            var mapping = (RelationalTypeMapping)Activator.CreateInstance(
                mappingType,
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance,
                null,
                new[]
                {
                    FakeTypeMapping.CreateParameters(
                        clrType,
                        size: 33,
                        unicide: false,
                        fixedLength: true,
                        storeTypePostfix: StoreTypePostfix.Size)
                },
                null,
                null);

            var clone = mapping.Clone("<clone>", 66);

            Assert.NotSame(mapping, clone);
            Assert.Same(mapping.GetType(), clone.GetType());
            Assert.Equal("<original>(33)", mapping.StoreType);
            Assert.Equal("<clone>(66)", clone.StoreType);
            Assert.Equal(DbType.VarNumeric, clone.DbType);
            Assert.Equal(33, mapping.Size);
            Assert.Equal(66, clone.Size);
            Assert.False(mapping.IsUnicode);
            Assert.False(clone.IsUnicode);
            Assert.NotNull(mapping.Converter);
            Assert.Same(mapping.Converter, clone.Converter);
            Assert.Same(mapping.Comparer, clone.Comparer);
            Assert.Same(mapping.KeyComparer, clone.KeyComparer);
            Assert.Same(typeof(object), clone.ClrType);
            Assert.True(mapping.IsFixedLength);
            Assert.True(clone.IsFixedLength);
            Assert.Equal(StoreTypePostfix.Size, clone.StoreTypePostfix);

            var newConverter = new FakeValueConverter();
            clone = (RelationalTypeMapping)mapping.Clone(newConverter);

            Assert.NotSame(mapping, clone);
            Assert.Same(mapping.GetType(), clone.GetType());
            Assert.Equal("<original>(33)", mapping.StoreType);
            Assert.Equal("<original>(33)", clone.StoreType);
            Assert.Equal(DbType.VarNumeric, clone.DbType);
            Assert.Equal(33, mapping.Size);
            Assert.Equal(33, clone.Size);
            Assert.False(mapping.IsUnicode);
            Assert.False(clone.IsUnicode);
            Assert.NotSame(mapping.Converter, clone.Converter);
            Assert.Same(mapping.Comparer, clone.Comparer);
            Assert.Same(mapping.KeyComparer, clone.KeyComparer);
            Assert.Same(typeof(object), clone.ClrType);
            Assert.True(mapping.IsFixedLength);
            Assert.True(clone.IsFixedLength);
            Assert.Equal(StoreTypePostfix.Size, clone.StoreTypePostfix);
        }

        private class FakeTypeMapping : RelationalTypeMapping
        {
            private FakeTypeMapping(RelationalTypeMappingParameters parameters)
                : base(parameters)
            {
            }

            public FakeTypeMapping()
                : base("storeType", typeof(object))
            {
            }

            public static object CreateParameters(
                Type clrType,
                int? size = null,
                bool unicide = false,
                bool fixedLength = false,
                StoreTypePostfix storeTypePostfix = StoreTypePostfix.PrecisionAndScale)
            {
                return new RelationalTypeMappingParameters(
                    new CoreTypeMappingParameters(
                        clrType,
                        new FakeValueConverter(),
                        new FakeValueComparer(),
                        new FakeValueComparer()),
                    "<original>",
                    storeTypePostfix,
                    System.Data.DbType.VarNumeric,
                    size: size,
                    unicode: unicide,
                    fixedLength: fixedLength);
            }
        }

        [Fact]
        public void Can_create_simple_parameter()
        {
            using (var command = CreateTestCommand())
            {
                var parameter = new IntTypeMapping("int")
                    .CreateParameter(command, "Name", 17, nullable: false);

                Assert.Equal(ParameterDirection.Input, parameter.Direction);
                Assert.Equal("Name", parameter.ParameterName);
                Assert.Equal(17, parameter.Value);
                Assert.Equal(DefaultParameterType, parameter.DbType);
                Assert.False(parameter.IsNullable);
            }
        }

        [Fact]
        public void Can_create_simple_nullable_parameter()
        {
            using (var command = CreateTestCommand())
            {
                var parameter = new IntTypeMapping("int")
                    .CreateParameter(command, "Name", 17, nullable: true);

                Assert.Equal(ParameterDirection.Input, parameter.Direction);
                Assert.Equal("Name", parameter.ParameterName);
                Assert.Equal(17, parameter.Value);
                Assert.Equal(DefaultParameterType, parameter.DbType);
                Assert.True(parameter.IsNullable);
            }
        }

        [Fact]
        public void Can_create_simple_parameter_with_DbType()
        {
            using (var command = CreateTestCommand())
            {
                var parameter = new IntTypeMapping("int", DbType.Int32)
                    .CreateParameter(command, "Name", 17, nullable: false);

                Assert.Equal(ParameterDirection.Input, parameter.Direction);
                Assert.Equal("Name", parameter.ParameterName);
                Assert.Equal(17, parameter.Value);
                Assert.Equal(DbType.Int32, parameter.DbType);
                Assert.False(parameter.IsNullable);
            }
        }

        [Fact]
        public void Can_create_simple_nullable_parameter_with_DbType()
        {
            using (var command = CreateTestCommand())
            {
                var parameter = new IntTypeMapping("int", DbType.Int32)
                    .CreateParameter(command, "Name", 17, nullable: true);

                Assert.Equal(ParameterDirection.Input, parameter.Direction);
                Assert.Equal("Name", parameter.ParameterName);
                Assert.Equal(17, parameter.Value);
                Assert.Equal(DbType.Int32, parameter.DbType);
                Assert.True(parameter.IsNullable);
            }
        }

        [Fact]
        public void Can_create_required_string_parameter()
        {
            using (var command = CreateTestCommand())
            {
                var parameter = new StringTypeMapping("nvarchar(23)", DbType.String, unicode: true, size: 23)
                    .CreateParameter(command, "Name", "Value", nullable: false);

                Assert.Equal(ParameterDirection.Input, parameter.Direction);
                Assert.Equal("Name", parameter.ParameterName);
                Assert.Equal("Value", parameter.Value);
                Assert.Equal(DbType.String, parameter.DbType);
                Assert.False(parameter.IsNullable);
                Assert.Equal(5, parameter.Size);
            }
        }

        [Fact]
        public void Can_create_string_parameter()
        {
            using (var command = CreateTestCommand())
            {
                var parameter = new StringTypeMapping("nvarchar(23)", DbType.String, unicode: true, size: 23)
                    .CreateParameter(command, "Name", "Value", nullable: true);

                Assert.Equal(ParameterDirection.Input, parameter.Direction);
                Assert.Equal("Name", parameter.ParameterName);
                Assert.Equal("Value", parameter.Value);
                Assert.Equal(DbType.String, parameter.DbType);
                Assert.True(parameter.IsNullable);
                Assert.Equal(5, parameter.Size);
            }
        }

        protected virtual void Test_GenerateSqlLiteral_helper(
            RelationalTypeMapping typeMapping, object value, string literalValue)
        {
            Assert.Equal(literalValue, typeMapping.GenerateSqlLiteral(value));
        }

        [Fact]
        public virtual void Bool_literal_generated_correctly()
        {
            var typeMapping = new BoolTypeMapping("bool");

            Test_GenerateSqlLiteral_helper(typeMapping, true, "1");
            Test_GenerateSqlLiteral_helper(typeMapping, false, "0");
        }

        [Fact]
        public virtual void ByteArray_literal_generated_correctly()
        {
            Test_GenerateSqlLiteral_helper(new ByteArrayTypeMapping("byte[]"), new byte[] { 0xDA, 0x7A }, "X'DA7A'");
        }

        [Fact]
        public virtual void Byte_literal_generated_correctly()
        {
            var typeMapping = new ByteTypeMapping("byte", DbType.Byte);

            Test_GenerateSqlLiteral_helper(typeMapping, byte.MinValue, "0");
            Test_GenerateSqlLiteral_helper(typeMapping, byte.MaxValue, "255");
        }

        [Fact]
        public virtual void Char_literal_generated_correctly()
        {
            Test_GenerateSqlLiteral_helper(new CharTypeMapping("char"), 'A', "'A'");
        }

        [Fact]
        public virtual void DateTimeOffset_literal_generated_correctly()
        {
            Test_GenerateSqlLiteral_helper(
                new DateTimeOffsetTypeMapping("DateTimeOffset"),
                new DateTimeOffset(2015, 3, 12, 13, 36, 37, 371, new TimeSpan(-7, 0, 0)),
                "TIMESTAMP '2015-03-12 13:36:37.3710000-07:00'");
        }

        [Fact]
        public virtual void DateTime_literal_generated_correctly()
        {
            Test_GenerateSqlLiteral_helper(
                new DateTimeTypeMapping("DateTime"),
                new DateTime(2015, 3, 12, 13, 36, 37, 371, DateTimeKind.Utc),
                "TIMESTAMP '2015-03-12 13:36:37.3710000'");
        }

        [Fact]
        public virtual void Decimal_literal_generated_correctly()
        {
            var typeMapping = new DecimalTypeMapping("decimal", DbType.Decimal);

            Test_GenerateSqlLiteral_helper(typeMapping, decimal.MinValue, "-79228162514264337593543950335.0");
            Test_GenerateSqlLiteral_helper(typeMapping, decimal.MaxValue, "79228162514264337593543950335.0");
        }

        [Fact]
        public virtual void Double_literal_generated_correctly()
        {
            var typeMapping = new DoubleTypeMapping("double", DbType.Double);

            Test_GenerateSqlLiteral_helper(typeMapping, double.NaN, "NaN");
            Test_GenerateSqlLiteral_helper(typeMapping, double.PositiveInfinity, "Infinity");
            Test_GenerateSqlLiteral_helper(typeMapping, double.NegativeInfinity, "-Infinity");
            Test_GenerateSqlLiteral_helper(typeMapping, double.MinValue, "-1.7976931348623157E+308");
            Test_GenerateSqlLiteral_helper(typeMapping, double.MaxValue, "1.7976931348623157E+308");
        }

        [Fact]
        public virtual void Float_literal_generated_correctly()
        {
            var typeMapping = new FloatTypeMapping("float", DbType.Single);

            Test_GenerateSqlLiteral_helper(typeMapping, float.NaN, "NaN");
            Test_GenerateSqlLiteral_helper(typeMapping, float.PositiveInfinity, "Infinity");
            Test_GenerateSqlLiteral_helper(typeMapping, float.NegativeInfinity, "-Infinity");
            Test_GenerateSqlLiteral_helper(typeMapping, float.MinValue, "-3.40282347E+38");
            Test_GenerateSqlLiteral_helper(typeMapping, float.MaxValue, "3.40282347E+38");
        }

        [Fact]
        public virtual void Guid_literal_generated_correctly()
        {
            Test_GenerateSqlLiteral_helper(
                new GuidTypeMapping("guid"),
                new Guid("c6f43a9e-91e1-45ef-a320-832ea23b7292"),
                "'c6f43a9e-91e1-45ef-a320-832ea23b7292'");
        }

        [Fact]
        public virtual void NullableInt_literal_generated_correctly()
        {
            var typeMapping = new IntTypeMapping("int?", DbType.Int32);

            Test_GenerateSqlLiteral_helper(typeMapping, default(int?), "NULL");
            Test_GenerateSqlLiteral_helper(typeMapping, (int?)123, "123");
        }

        [Fact]
        public virtual void Int_literal_generated_correctly()
        {
            var typeMapping = new IntTypeMapping("int", DbType.Int32);

            Test_GenerateSqlLiteral_helper(typeMapping, int.MinValue, "-2147483648");
            Test_GenerateSqlLiteral_helper(typeMapping, int.MaxValue, "2147483647");
        }

        [Fact]
        public virtual void Long_literal_generated_correctly()
        {
            var typeMapping = new LongTypeMapping("long", DbType.Int64);

            Test_GenerateSqlLiteral_helper(typeMapping, long.MinValue, "-9223372036854775808");
            Test_GenerateSqlLiteral_helper(typeMapping, long.MaxValue, "9223372036854775807");
        }

        [Fact]
        public virtual void SByte_literal_generated_correctly()
        {
            var typeMapping = new SByteTypeMapping("sbyte", DbType.SByte);

            Test_GenerateSqlLiteral_helper(typeMapping, sbyte.MinValue, "-128");
            Test_GenerateSqlLiteral_helper(typeMapping, sbyte.MaxValue, "127");
        }

        [Fact]
        public virtual void Short_literal_generated_correctly()
        {
            var typeMapping = new ShortTypeMapping("short", DbType.Int16);

            Test_GenerateSqlLiteral_helper(typeMapping, short.MinValue, "-32768");
            Test_GenerateSqlLiteral_helper(typeMapping, short.MaxValue, "32767");
        }

        [Fact]
        public virtual void String_literal_generated_correctly()
        {
            Test_GenerateSqlLiteral_helper(new StringTypeMapping("string"), "Text", "'Text'");
        }

        [Fact]
        public virtual void Timespan_literal_generated_correctly()
        {
            Test_GenerateSqlLiteral_helper(new TimeSpanTypeMapping("time"), new TimeSpan(7, 14, 30), "'07:14:30'");
        }

        [Fact]
        public virtual void UInt_literal_generated_correctly()
        {
            var typeMapping = new UIntTypeMapping("uint", DbType.UInt32);

            Test_GenerateSqlLiteral_helper(typeMapping, uint.MinValue, "0");
            Test_GenerateSqlLiteral_helper(typeMapping, uint.MaxValue, "4294967295");
        }

        [Fact]
        public virtual void ULong_literal_generated_correctly()
        {
            var typeMapping = new ULongTypeMapping("ulong", DbType.UInt64);

            Test_GenerateSqlLiteral_helper(typeMapping, ulong.MinValue, "0");
            Test_GenerateSqlLiteral_helper(typeMapping, ulong.MaxValue, "18446744073709551615");
        }

        [Fact]
        public virtual void UShort_literal_generated_correctly()
        {
            var typeMapping = new UShortTypeMapping("ushort", DbType.UInt16);

            Test_GenerateSqlLiteral_helper(typeMapping, ushort.MinValue, "0");
            Test_GenerateSqlLiteral_helper(typeMapping, ushort.MaxValue, "65535");
        }

        [Fact]
        public virtual void Primary_key_type_mapping_is_picked_up_by_FK_without_going_through_store_type()
        {
            using (var context = new FruityContext(ContextOptions))
            {
                Assert.Same(
                    context.Model.FindEntityType(typeof(Banana)).FindProperty("Id").FindMapping(),
                    context.Model.FindEntityType(typeof(Kiwi)).FindProperty("BananaId").FindMapping());
            }
        }

        private class FruityContext : DbContext
        {
            public FruityContext(DbContextOptions options)
                : base(options)
            {
            }

            public DbSet<Banana> Bananas { get; set; }
            public DbSet<Kiwi> Kiwi { get; set; }
        }

        [Fact]
        public virtual void Primary_key_type_mapping_can_differ_from_FK()
        {
            using (var context = new MismatchedFruityContext(ContextOptions))
            {
                Assert.Equal(
                    typeof(short),
                    context.Model.FindEntityType(typeof(Banana)).FindProperty("Id").FindMapping().Converter.ProviderClrType);
                Assert.Null(context.Model.FindEntityType(typeof(Kiwi)).FindProperty("Id").FindMapping().Converter);
            }
        }

        private class MismatchedFruityContext : FruityContext
        {
            public MismatchedFruityContext(DbContextOptions options)
                : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Banana>().Property(e => e.Id).HasConversion<short>();
                modelBuilder.Entity<Kiwi>().Property(e => e.Id).HasConversion<int>();
                modelBuilder.Entity<Kiwi>().HasOne(e => e.Banana).WithMany(e => e.Kiwis).HasForeignKey(e => e.Id);
            }
        }

        private class Banana
        {
            public int Id { get; set; }

            public ICollection<Kiwi> Kiwis { get; set; }
        }

        private class Kiwi
        {
            public int Id { get; set; }

            public int BananaId { get; set; }
            public Banana Banana { get; set; }
        }

        protected abstract DbContextOptions ContextOptions { get; }

        protected abstract DbCommand CreateTestCommand();

        protected abstract DbType DefaultParameterType { get; }
    }
}
