using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using EFCore.SqlCe.Storage.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Tests;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;

namespace Microsoft.EntityFrameworkCore
{
    public class SqlCeTypeMapperTest : RelationalTypeMapperTestBase
    {
        [Fact]
        public void Does_simple_SQL_Server_mappings_to_DDL_types()
        {
            Assert.Equal("int", GetTypeMapping(typeof(int)).StoreType);
            Assert.Equal("datetime", GetTypeMapping(typeof(DateTime)).StoreType);
            Assert.Equal("uniqueidentifier", GetTypeMapping(typeof(Guid)).StoreType);
            Assert.Equal("tinyint", GetTypeMapping(typeof(byte)).StoreType);
            Assert.Equal("float", GetTypeMapping(typeof(double)).StoreType);
            Assert.Equal("bit", GetTypeMapping(typeof(bool)).StoreType);
            Assert.Equal("smallint", GetTypeMapping(typeof(short)).StoreType);
            Assert.Equal("bigint", GetTypeMapping(typeof(long)).StoreType);
            Assert.Equal("real", GetTypeMapping(typeof(float)).StoreType);
            Assert.Equal("nvarchar(48)", GetTypeMapping(typeof(DateTimeOffset)).StoreType);
        }

        [Fact]
        public void Does_simple_SQL_Server_mappings_for_nullable_CLR_types_to_DDL_types()
        {
            Assert.Equal("int", GetTypeMapping(typeof(int?)).StoreType);
            Assert.Equal("datetime", GetTypeMapping(typeof(DateTime?)).StoreType);
            Assert.Equal("uniqueidentifier", GetTypeMapping(typeof(Guid?)).StoreType);
            Assert.Equal("tinyint", GetTypeMapping(typeof(byte?)).StoreType);
            Assert.Equal("float", GetTypeMapping(typeof(double?)).StoreType);
            Assert.Equal("bit", GetTypeMapping(typeof(bool?)).StoreType);
            Assert.Equal("smallint", GetTypeMapping(typeof(short?)).StoreType);
            Assert.Equal("bigint", GetTypeMapping(typeof(long?)).StoreType);
            Assert.Equal("real", GetTypeMapping(typeof(float?)).StoreType);
            Assert.Equal("nvarchar(48)", GetTypeMapping(typeof(DateTimeOffset?)).StoreType);
        }

        [Fact]
        public void Does_simple_SQL_Server_mappings_for_enums_to_DDL_types()
        {
            Assert.Equal("int", GetTypeMapping(typeof(IntEnum)).StoreType);
            Assert.Equal("tinyint", GetTypeMapping(typeof(ByteEnum)).StoreType);
            Assert.Equal("smallint", GetTypeMapping(typeof(ShortEnum)).StoreType);
            Assert.Equal("bigint", GetTypeMapping(typeof(LongEnum)).StoreType);
            Assert.Equal("int", GetTypeMapping(typeof(IntEnum?)).StoreType);
            Assert.Equal("tinyint", GetTypeMapping(typeof(ByteEnum?)).StoreType);
            Assert.Equal("smallint", GetTypeMapping(typeof(ShortEnum?)).StoreType);
            Assert.Equal("bigint", GetTypeMapping(typeof(LongEnum?)).StoreType);
        }

        [Fact]
        public void Does_simple_SQL_Server_mappings_to_DbTypes()
        {
            Assert.Equal(DbType.Int32, GetTypeMapping(typeof(int)).DbType);
            Assert.Null(GetTypeMapping(typeof(string)).DbType);
            Assert.Equal(DbType.Binary, GetTypeMapping(typeof(byte[])).DbType);
            Assert.Null(GetTypeMapping(typeof(TimeSpan)).DbType);
            Assert.Equal(DbType.Guid, GetTypeMapping(typeof(Guid)).DbType);
            Assert.Equal(DbType.Byte, GetTypeMapping(typeof(byte)).DbType);
            Assert.Equal(DbType.Double, GetTypeMapping(typeof(double)).DbType);
            Assert.Equal(DbType.Boolean, GetTypeMapping(typeof(bool)).DbType);
            Assert.Equal(DbType.Int16, GetTypeMapping(typeof(short)).DbType);
            Assert.Equal(DbType.Int64, GetTypeMapping(typeof(long)).DbType);
            Assert.Null(GetTypeMapping(typeof(float)).DbType);
            Assert.Null(GetTypeMapping(typeof(DateTimeOffset)).DbType);
        }

        [Fact]
        public void Does_simple_SQL_Server_mappings_for_nullable_CLR_types_to_DbTypes()
        {
            Assert.Equal(DbType.Int32, GetTypeMapping(typeof(int?)).DbType);
            Assert.Null(GetTypeMapping(typeof(string)).DbType);
            Assert.Equal(DbType.Binary, GetTypeMapping(typeof(byte[])).DbType);
            Assert.Null(GetTypeMapping(typeof(TimeSpan?)).DbType);
            Assert.Equal(DbType.Guid, GetTypeMapping(typeof(Guid?)).DbType);
            Assert.Equal(DbType.Byte, GetTypeMapping(typeof(byte?)).DbType);
            Assert.Equal(DbType.Double, GetTypeMapping(typeof(double?)).DbType);
            Assert.Equal(DbType.Boolean, GetTypeMapping(typeof(bool?)).DbType);
            Assert.Equal(DbType.Int16, GetTypeMapping(typeof(short?)).DbType);
            Assert.Equal(DbType.Int64, GetTypeMapping(typeof(long?)).DbType);
            Assert.Null(GetTypeMapping(typeof(float?)).DbType);
            Assert.Null(GetTypeMapping(typeof(DateTimeOffset?)).DbType);
        }

        [Fact]
        public void Does_simple_SQL_Server_mappings_for_enums_to_DbTypes()
        {
            Assert.Equal(DbType.Int32, GetTypeMapping(typeof(IntEnum)).DbType);
            Assert.Equal(DbType.Byte, GetTypeMapping(typeof(ByteEnum)).DbType);
            Assert.Equal(DbType.Int16, GetTypeMapping(typeof(ShortEnum)).DbType);
            Assert.Equal(DbType.Int64, GetTypeMapping(typeof(LongEnum)).DbType);
            Assert.Equal(DbType.Int32, GetTypeMapping(typeof(IntEnum?)).DbType);
            Assert.Equal(DbType.Byte, GetTypeMapping(typeof(ByteEnum?)).DbType);
            Assert.Equal(DbType.Int16, GetTypeMapping(typeof(ShortEnum?)).DbType);
            Assert.Equal(DbType.Int64, GetTypeMapping(typeof(LongEnum?)).DbType);
        }

        [Fact]
        public void Does_decimal_mapping()
        {
            var typeMapping = GetTypeMapping(typeof(decimal));

            Assert.Equal(DbType.Decimal, typeMapping.DbType);
            Assert.Equal("decimal(18, 2)", typeMapping.StoreType);
        }

        [Fact]
        public void Does_decimal_mapping_for_nullable_CLR_types()
        {
            var typeMapping = GetTypeMapping(typeof(decimal?));

            Assert.Equal(DbType.Decimal, typeMapping.DbType);
            Assert.Equal("decimal(18, 2)", typeMapping.StoreType);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(null)]
        public void Does_non_key_SQL_Server_string_mapping(bool? unicode)
        {
            var typeMapping = GetTypeMapping(typeof(string), unicode: unicode);

            Assert.Null(typeMapping.DbType);
            Assert.Equal("nvarchar(4000)", typeMapping.StoreType);
            Assert.Equal(4000, typeMapping.Size);
            Assert.True(typeMapping.IsUnicode);
            Assert.Equal(4000, typeMapping.CreateParameter(new TestCommand(), "Name", "Value").Size);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(null)]
        public void Does_non_key_SQL_Server_string_mapping_with_max_length(bool? unicode)
        {
            var typeMapping = GetTypeMapping(typeof(string), null, 3, unicode: unicode);

            Assert.Null(typeMapping.DbType);
            Assert.Equal("nvarchar(3)", typeMapping.StoreType);
            Assert.Equal(3, typeMapping.Size);
            Assert.True(typeMapping.IsUnicode);
            Assert.Equal(0, typeMapping.CreateParameter(new SqlCeCommand(), "Name", "Value").Size);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(null)]
        public void Does_non_key_SQL_Server_string_mapping_with_long_string(bool? unicode)
        {
            var typeMapping = GetTypeMapping(typeof(string), unicode: unicode);

            Assert.Null(typeMapping.DbType);
            Assert.Equal("nvarchar(4000)", typeMapping.StoreType);
            Assert.Equal(4000, typeMapping.Size);
            Assert.True(typeMapping.IsUnicode);
            Assert.Equal(0, typeMapping.CreateParameter(new SqlCeCommand(), "Name", new string('X', 4001)).Size);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(null)]
        public void Does_non_key_SQL_Server_string_mapping_with_max_length_with_long_string(bool? unicode)
        {
            var typeMapping = GetTypeMapping(typeof(string), null, 3, unicode: unicode);

            Assert.Null(typeMapping.DbType);
            Assert.Equal("nvarchar(3)", typeMapping.StoreType);
            Assert.Equal(3, typeMapping.Size);
            Assert.True(typeMapping.IsUnicode);
            Assert.Equal(0, typeMapping.CreateParameter(new SqlCeCommand(), "Name", new string('X', 4001)).Size);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(null)]
        public void Does_non_key_SQL_Server_required_string_mapping(bool? unicode)
        {
            var typeMapping = GetTypeMapping(typeof(string), nullable: false, unicode: unicode);

            Assert.Null(typeMapping.DbType);
            Assert.Equal("nvarchar(4000)", typeMapping.StoreType);
            Assert.Equal(4000, typeMapping.Size);
            Assert.True(typeMapping.IsUnicode);
            Assert.Equal(4000, typeMapping.CreateParameter(new TestCommand(), "Name", "Value").Size);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(null)]
        public void Does_key_SQL_Server_string_mapping(bool? unicode)
        {
            var property = CreateEntityType().AddProperty("MyProp", typeof(string));
            property.IsNullable = false;
            property.IsUnicode(unicode);
            property.DeclaringEntityType.SetPrimaryKey(property);

            var typeMapping = CreateTypeMapper().GetMapping(property);

            Assert.Null(typeMapping.DbType);
            Assert.Equal("nvarchar(256)", typeMapping.StoreType);
            Assert.Equal(256, typeMapping.Size);
            Assert.True(typeMapping.IsUnicode);
            Assert.Equal(256, typeMapping.CreateParameter(new TestCommand(), "Name", "Value").Size);
        }

        private static IRelationalTypeMappingSource CreateTypeMapper()
            => new SqlCeTypeMappingSource(
                TestServiceFactory.Instance.Create<TypeMappingSourceDependencies>(),
                TestServiceFactory.Instance.Create<RelationalTypeMappingSourceDependencies>());

        [Theory]
        [InlineData(true)]
        [InlineData(null)]
        public void Does_foreign_key_SQL_Server_string_mapping(bool? unicode)
        {
            var property = CreateEntityType().AddProperty("MyProp", typeof(string));
            property.IsNullable = false;
            property.IsUnicode(unicode);
            var fkProperty = property.DeclaringEntityType.AddProperty("FK", typeof(string));
            var pk = property.DeclaringEntityType.SetPrimaryKey(property);
            property.DeclaringEntityType.AddForeignKey(fkProperty, pk, property.DeclaringEntityType);

            var typeMapping = CreateTypeMapper().GetMapping(fkProperty);

            Assert.Null(typeMapping.DbType);
            Assert.Equal("nvarchar(256)", typeMapping.StoreType);
            Assert.Equal(256, typeMapping.Size);
            Assert.True(typeMapping.IsUnicode);
            Assert.Equal(256, typeMapping.CreateParameter(new TestCommand(), "Name", "Value").Size);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(null)]
        public void Does_required_foreign_key_SQL_Server_string_mapping(bool? unicode)
        {
            var property = CreateEntityType().AddProperty("MyProp", typeof(string));
            property.IsNullable = false;
            property.IsUnicode(unicode);
            var fkProperty = property.DeclaringEntityType.AddProperty("FK", typeof(string));
            var pk = property.DeclaringEntityType.SetPrimaryKey(property);
            property.DeclaringEntityType.AddForeignKey(fkProperty, pk, property.DeclaringEntityType);
            fkProperty.IsNullable = false;

            var typeMapping = CreateTypeMapper().GetMapping(fkProperty);

            Assert.Null(typeMapping.DbType);
            Assert.Equal("nvarchar(256)", typeMapping.StoreType);
            Assert.Equal(256, typeMapping.Size);
            Assert.True(typeMapping.IsUnicode);
            Assert.Equal(256, typeMapping.CreateParameter(new TestCommand(), "Name", "Value").Size);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(null)]
        public void Does_indexed_column_SQL_Server_string_mapping(bool? unicode)
        {
            var entityType = CreateEntityType();
            var property = entityType.AddProperty("MyProp", typeof(string));
            property.IsUnicode(unicode);
            entityType.AddIndex(property);

            var typeMapping = CreateTypeMapper().GetMapping(property);

            Assert.Null(typeMapping.DbType);
            Assert.Equal("nvarchar(256)", typeMapping.StoreType);
            Assert.Equal(256, typeMapping.Size);
            Assert.True(typeMapping.IsUnicode);
            Assert.Equal(256, typeMapping.CreateParameter(new TestCommand(), "Name", "Value").Size);
        }

        [Fact]
        public void Does_non_key_SQL_Server_binary_mapping()
        {
            var typeMapping = GetTypeMapping(typeof(byte[]));

            Assert.Equal(DbType.Binary, typeMapping.DbType);
            Assert.Equal("image", typeMapping.StoreType);
            Assert.Null(typeMapping.Size);
            Assert.Equal(8000, typeMapping.CreateParameter(new TestCommand(), "Name", new byte[3]).Size);
        }

        [Fact]
        public void Does_non_key_SQL_Server_binary_mapping_with_max_length()
        {
            var typeMapping = GetTypeMapping(typeof(byte[]), null, 3);

            Assert.Equal(DbType.Binary, typeMapping.DbType);
            Assert.Equal("varbinary(3)", typeMapping.StoreType);
            Assert.Equal(3, typeMapping.Size);
            Assert.Equal(3, typeMapping.CreateParameter(new TestCommand(), "Name", new byte[3]).Size);
        }

        [Fact]
        public void Does_non_key_SQL_Server_binary_mapping_with_long_array()
        {
            var typeMapping = GetTypeMapping(typeof(byte[]));

            Assert.Equal(DbType.Binary, typeMapping.DbType);
            Assert.Equal("image", typeMapping.StoreType);
            Assert.Null(typeMapping.Size);
            Assert.Equal(0, typeMapping.CreateParameter(new SqlCeCommand(), "Name", new byte[8001]).Size);
        }

        [Fact]
        public void Does_non_key_SQL_Server_binary_mapping_with_max_length_with_long_array()
        {
            var typeMapping = GetTypeMapping(typeof(byte[]), null, 3);

            Assert.Equal(DbType.Binary, typeMapping.DbType);
            Assert.Equal("varbinary(3)", typeMapping.StoreType);
            Assert.Equal(3, typeMapping.Size);
            Assert.Equal(0, typeMapping.CreateParameter(new SqlCeCommand(), "Name", new byte[8001]).Size);
        }

        [Fact]
        public void Does_non_key_SQL_Server_required_binary_mapping()
        {
            var typeMapping = GetTypeMapping(typeof(byte[]), nullable: false);

            Assert.Equal(DbType.Binary, typeMapping.DbType);
            Assert.Equal("image", typeMapping.StoreType);
            Assert.Null(typeMapping.Size);
            Assert.Equal(8000, typeMapping.CreateParameter(new TestCommand(), "Name", new byte[3]).Size);
        }

        [Fact]
        public void Does_non_key_SQL_Server_fixed_length_binary_mapping()
        {
            var property = CreateEntityType().AddProperty("MyBinaryProp", typeof(byte[]));
            property.Relational().ColumnType = "binary(100)";

            var typeMapping = CreateTypeMapper().GetMapping(property);

            Assert.Equal(DbType.Binary, typeMapping.DbType);
            Assert.Equal("binary(100)", typeMapping.StoreType);
        }

        [Fact]
        public void Does_key_SQL_Server_binary_mapping()
        {
            var property = CreateEntityType().AddProperty("MyProp", typeof(byte[]));
            property.IsNullable = false;
            property.DeclaringEntityType.SetPrimaryKey(property);

            var typeMapping = CreateTypeMapper().GetMapping(property);

            Assert.Equal(DbType.Binary, typeMapping.DbType);
            Assert.Equal("varbinary(512)", typeMapping.StoreType);
            Assert.Equal(512, typeMapping.CreateParameter(new TestCommand(), "Name", new byte[3]).Size);
        }

        [Fact]
        public void Does_foreign_key_SQL_Server_binary_mapping()
        {
            var property = CreateEntityType().AddProperty("MyProp", typeof(byte[]));
            property.IsNullable = false;
            var fkProperty = property.DeclaringEntityType.AddProperty("FK", typeof(byte[]));
            var pk = property.DeclaringEntityType.SetPrimaryKey(property);
            property.DeclaringEntityType.AddForeignKey(fkProperty, pk, property.DeclaringEntityType);

            var typeMapping = CreateTypeMapper().GetMapping(fkProperty);

            Assert.Equal(DbType.Binary, typeMapping.DbType);
            Assert.Equal("varbinary(512)", typeMapping.StoreType);
            Assert.Equal(512, typeMapping.CreateParameter(new TestCommand(), "Name", new byte[3]).Size);
        }

        [Fact]
        public void Does_required_foreign_key_SQL_Server_binary_mapping()
        {
            var property = CreateEntityType().AddProperty("MyProp", typeof(byte[]));
            property.IsNullable = false;
            var fkProperty = property.DeclaringEntityType.AddProperty("FK", typeof(byte[]));
            var pk = property.DeclaringEntityType.SetPrimaryKey(property);
            property.DeclaringEntityType.AddForeignKey(fkProperty, pk, property.DeclaringEntityType);
            fkProperty.IsNullable = false;

            var typeMapping = CreateTypeMapper().GetMapping(fkProperty);

            Assert.Equal(DbType.Binary, typeMapping.DbType);
            Assert.Equal("varbinary(512)", typeMapping.StoreType);
            Assert.Equal(512, typeMapping.CreateParameter(new TestCommand(), "Name", new byte[3]).Size);
        }

        [Fact]
        public void Does_indexed_column_SQL_Server_binary_mapping()
        {
            var entityType = CreateEntityType();
            var property = entityType.AddProperty("MyProp", typeof(byte[]));
            entityType.AddIndex(property);

            var typeMapping = CreateTypeMapper().GetMapping(property);

            Assert.Equal(DbType.Binary, typeMapping.DbType);
            Assert.Equal("varbinary(512)", typeMapping.StoreType);
            Assert.Equal(512, typeMapping.CreateParameter(new TestCommand(), "Name", new byte[] { 0, 1, 2, 3 }).Size);
        }

        [Fact]
        public void Does_non_key_SQL_Server_rowversion_mapping()
        {
            var property = CreateEntityType().AddProperty("MyProp", typeof(byte[]));
            property.IsConcurrencyToken = true;
            property.ValueGenerated = ValueGenerated.OnAddOrUpdate;

            var typeMapping = CreateTypeMapper().GetMapping(property);

            Assert.Equal(DbType.Binary, typeMapping.DbType);
            Assert.Equal("rowversion", typeMapping.StoreType);
            Assert.Equal(8, typeMapping.Size);
            Assert.Equal(8, typeMapping.CreateParameter(new TestCommand(), "Name", new byte[8]).Size);
        }

        [Fact]
        public void Does_non_key_SQL_Server_required_rowversion_mapping()
        {
            var property = CreateEntityType().AddProperty("MyProp", typeof(byte[]));
            property.IsConcurrencyToken = true;
            property.ValueGenerated = ValueGenerated.OnAddOrUpdate;
            property.IsNullable = false;

            var typeMapping = CreateTypeMapper().GetMapping(property);

            Assert.Equal(DbType.Binary, typeMapping.DbType);
            Assert.Equal("rowversion", typeMapping.StoreType);
            Assert.Equal(8, typeMapping.Size);
            Assert.Equal(8, typeMapping.CreateParameter(new TestCommand(), "Name", new byte[8]).Size);
        }

        [Fact]
        public void Does_not_do_rowversion_mapping_for_non_computed_concurrency_tokens()
        {
            var property = CreateEntityType().AddProperty("MyProp", typeof(byte[]));
            property.IsConcurrencyToken = true;

            var typeMapping = CreateTypeMapper().GetMapping(property);

            Assert.Equal(DbType.Binary, typeMapping.DbType);
            Assert.Equal("image", typeMapping.StoreType);
        }

        private RelationalTypeMapping GetTypeMapping(
            Type propertyType,
            bool? nullable = null,
            int? maxLength = null,
            bool? unicode = null)
        {
            var property = CreateEntityType().AddProperty("MyProp", propertyType);

            if (nullable.HasValue)
            {
                property.IsNullable = nullable.Value;
            }

            if (maxLength.HasValue)
            {
                property.SetMaxLength(maxLength);
            }

            if (unicode.HasValue)
            {
                property.IsUnicode(unicode);
            }

            return CreateTypeMapper().GetMapping(property);
        }

        [Fact]
        public void Does_default_mappings_for_sequence_types()
        {
            Assert.Equal("int", CreateTypeMapper().GetMapping(typeof(int)).StoreType);
            Assert.Equal("smallint", CreateTypeMapper().GetMapping(typeof(short)).StoreType);
            Assert.Equal("bigint", CreateTypeMapper().GetMapping(typeof(long)).StoreType);
            Assert.Equal("tinyint", CreateTypeMapper().GetMapping(typeof(byte)).StoreType);
        }

        [Fact]
        public void Does_default_mappings_for_strings_and_byte_arrays()
        {
            Assert.Equal("nvarchar(4000)", CreateTypeMapper().GetMapping(typeof(string)).StoreType);
            Assert.Equal("image", CreateTypeMapper().GetMapping(typeof(byte[])).StoreType);
        }

        [Fact]
        public void Does_default_mappings_for_values()
        {
            Assert.Equal("nvarchar(4000)", CreateTypeMapper().GetMappingForValue("Cheese").StoreType);
            Assert.Equal("image", CreateTypeMapper().GetMappingForValue(new byte[1]).StoreType);
            Assert.Equal("datetime", CreateTypeMapper().GetMappingForValue(new DateTime()).StoreType);
        }

        [Fact]
        public void Does_default_mappings_for_null_values()
        {
            Assert.Equal("NULL", CreateTypeMapper().GetMappingForValue(null).StoreType);
            Assert.Equal("NULL", CreateTypeMapper().GetMappingForValue(DBNull.Value).StoreType);
        }

        [Fact]
        public void Throws_for_unrecognized_property_types()
        {
            var property = new Model().AddEntityType("Entity1").AddProperty("Strange", typeof(object));
            var ex = Assert.Throws<InvalidOperationException>(() => CreateTypeMapper().GetMapping(property));
            Assert.Equal(RelationalStrings.UnsupportedPropertyType("Entity1", "Strange", "object"), ex.Message);
        }

        [Theory]
        [InlineData("bigint", typeof(long), null, false)]
        [InlineData("binary varying(333)", typeof(byte[]), 333, false)]
        [InlineData("binary(333)", typeof(byte[]), 333, false)]
        [InlineData("bit", typeof(bool), null, false)]
        [InlineData("datetime", typeof(DateTime), null, false)]
        [InlineData("dec", typeof(decimal), null, false)]
        [InlineData("decimal", typeof(decimal), null, false)]
        [InlineData("float", typeof(double), null, false)] // This is correct. SQL Server 'float' type maps to C# double
        [InlineData("float(10,8)", typeof(double), null, false)]
        [InlineData("image", typeof(byte[]), null, false)]
        [InlineData("int", typeof(int), null, false)]
        [InlineData("money", typeof(decimal), null, false)]
        [InlineData("national char varying(333)", typeof(string), 333, true)]
        [InlineData("national character varying(333)", typeof(string), 333, true)]
        [InlineData("national character(333)", typeof(string), 333, true)]
        [InlineData("nchar(333)", typeof(string), 333, true)]
        [InlineData("ntext", typeof(string), null, true)]
        [InlineData("numeric", typeof(decimal), null, false)]
        [InlineData("nvarchar(333)", typeof(string), 333, true)]
        [InlineData("nvarchar(4000)", typeof(string), 4000, true)]
        [InlineData("real", typeof(float), null, false)]
        [InlineData("rowversion", typeof(byte[]), 8, false)]
        [InlineData("smallint", typeof(short), null, false)]
        [InlineData("tinyint", typeof(byte), null, false)]
        [InlineData("uniqueidentifier", typeof(Guid), null, false)]
        [InlineData("varbinary(333)", typeof(byte[]), 333, false)]
        [InlineData("nVarCHaR(333)", typeof(string), 333, true)] // case-insensitive
        public void Can_map_by_type_name(string typeName, Type clrType, int? size, bool unicode)
        {
            var mapping = CreateTypeMapper().FindMapping(typeName);

            Assert.Equal(clrType, mapping.ClrType);
            Assert.Equal(size, mapping.Size);
            Assert.Equal(unicode, mapping.IsUnicode);
            Assert.Equal(typeName, mapping.StoreType);
        }

        [Theory]
        [InlineData("binary varying")]
        [InlineData("binary")]
        [InlineData("national char varying")]
        [InlineData("national character varying")]
        [InlineData("national character")]
        [InlineData("nchar")]
        [InlineData("nvarchar")]
        [InlineData("nVarCHaR")]
        [InlineData("NVARCHAR")]
        [InlineData("varbinary")]
        public void Throws_for_naked_type_name(string typeName)
        {
            var mapper = CreateTypeMapper();

            Assert.Equal(
                $"Unqualified data type {typeName}",
                Assert.Throws<ArgumentException>(() => mapper.FindMapping(typeName)).Message);
        }

        [Theory]
        [InlineData("binary varying")]
        [InlineData("binary")] 
        [InlineData("national char varying")]
        [InlineData("national character varying")]
        [InlineData("national character")]
        [InlineData("nchar")]
        [InlineData("nvarchar")]
        [InlineData("nVarCHaR")]
        [InlineData("NVARCHAR")]
        [InlineData("varbinary")]
        public void Throws_for_naked_type_name_on_property(string typeName)
        {
            var builder = CreateModelBuilder();

            var property = builder.Entity<StringCheese>()
                .Property(e => e.StringWithSize)
                .HasColumnType(typeName)
                .Metadata;

            var mapper = CreateTypeMapper();

            Assert.Equal(
                $"Unqualified data type {typeName} on property {nameof(StringCheese.StringWithSize)}",
                Assert.Throws<ArgumentException>(() => mapper.FindMapping(property)).Message);
        }

        [Theory]
        [InlineData("national char varying")]
        [InlineData("national character varying")]
        [InlineData("national character")]
        [InlineData("nchar")]
        [InlineData("nvarchar")]
        [InlineData("nVarCHaR")]
        [InlineData("NVARCHAR")]
        public void Can_map_string_base_type_name_and_size(string typeName)
        {
            var builder = CreateModelBuilder();

            var property = builder.Entity<StringCheese>()
                .Property(e => e.StringWithSize)
                .HasColumnType(typeName)
                .HasMaxLength(2018)
                .Metadata;

            var mapping = CreateTypeMapper().FindMapping(property);

            Assert.Same(typeof(string), mapping.ClrType);
            Assert.Equal(2018, mapping.Size);
            Assert.Equal(typeName.StartsWith("n", StringComparison.OrdinalIgnoreCase), mapping.IsUnicode);
            Assert.Equal(typeName + "(2018)", mapping.StoreType);
        }

        [Theory]
        [InlineData("binary varying")]
        [InlineData("binary")]
        [InlineData("varbinary")]
        public void Can_map_binary_base_type_name_and_size(string typeName)
        {
            var builder = CreateModelBuilder();

            var property = builder.Entity<StringCheese>()
                .Property(e => e.BinaryWithSize)
                .HasColumnType(typeName)
                .HasMaxLength(2018)
                .Metadata;

            var mapping = CreateTypeMapper().FindMapping(property);

            Assert.Same(typeof(byte[]), mapping.ClrType);
            Assert.Equal(2018, mapping.Size);
            Assert.Equal(typeName + "(2018)", mapping.StoreType);
        }

        private class StringCheese
        {
            public int Id { get; set; }
            public string StringWithSize { get; set; }
            public byte[] BinaryWithSize { get; set; }
        }

        [Fact]
        public void Key_with_store_type_is_picked_up_by_FK()
        {
            var model = CreateModel();
            var mapper = CreateTypeMapper();

            Assert.Equal(
                "money",
                mapper.GetMapping(model.FindEntityType(typeof(MyType)).FindProperty("Id")).StoreType);

            Assert.Equal(
                "money",
                mapper.GetMapping(model.FindEntityType(typeof(MyRelatedType1)).FindProperty("Relationship1Id")).StoreType);
        }

        [Fact]
        public void String_key_with_max_length_is_picked_up_by_FK()
        {
            var model = CreateModel();
            var mapper = CreateTypeMapper();

            Assert.Equal(
                "nvarchar(200)",
                mapper.GetMapping(model.FindEntityType(typeof(MyRelatedType1)).FindProperty("Id")).StoreType);

            Assert.Equal(
                "nvarchar(200)",
                mapper.GetMapping(model.FindEntityType(typeof(MyRelatedType2)).FindProperty("Relationship1Id")).StoreType);
        }

        [Fact]
        public void Binary_key_with_max_length_is_picked_up_by_FK()
        {
            var model = CreateModel();
            var mapper = CreateTypeMapper();

            Assert.Equal(
                "varbinary(100)",
                mapper.GetMapping(model.FindEntityType(typeof(MyRelatedType2)).FindProperty("Id")).StoreType);

            Assert.Equal(
                "varbinary(100)",
                mapper.GetMapping(model.FindEntityType(typeof(MyRelatedType3)).FindProperty("Relationship1Id")).StoreType);
        }

        [Fact]
        public void String_key_with_unicode_is_picked_up_by_FK()
        {
            var model = CreateModel();
            var mapper = CreateTypeMapper();

            Assert.Equal(
                "nvarchar(256)",
                mapper.GetMapping(model.FindEntityType(typeof(MyRelatedType3)).FindProperty("Id")).StoreType);

            Assert.Equal(
                "nvarchar(256)",
                mapper.GetMapping(model.FindEntityType(typeof(MyRelatedType4)).FindProperty("Relationship1Id")).StoreType);
        }

        [Fact]
        public void Key_store_type_if_preferred_if_specified()
        {
            var model = CreateModel();
            var mapper = CreateTypeMapper();

            Assert.Equal(
                "money",
                mapper.GetMapping(model.FindEntityType(typeof(MyType)).FindProperty("Id")).StoreType);

            Assert.Equal(
                "dec",
                mapper.GetMapping(model.FindEntityType(typeof(MyRelatedType1)).FindProperty("Relationship2Id")).StoreType);
        }

        [Fact]
        public void String_FK_max_length_is_preferred_if_specified()
        {
            var model = CreateModel();
            var mapper = CreateTypeMapper();

            Assert.Equal(
                "nvarchar(200)",
                mapper.GetMapping(model.FindEntityType(typeof(MyRelatedType1)).FindProperty("Id")).StoreType);

            Assert.Equal(
                "nvarchar(787)",
                mapper.GetMapping(model.FindEntityType(typeof(MyRelatedType2)).FindProperty("Relationship2Id")).StoreType);
        }

        [Fact]
        public void Binary_FK_max_length_is_preferred_if_specified()
        {
            var model = CreateModel();
            var mapper = CreateTypeMapper();

            Assert.Equal(
                "varbinary(100)",
                mapper.GetMapping(model.FindEntityType(typeof(MyRelatedType2)).FindProperty("Id")).StoreType);

            Assert.Equal(
                "varbinary(767)",
                mapper.GetMapping(model.FindEntityType(typeof(MyRelatedType3)).FindProperty("Relationship2Id")).StoreType);
        }

        [Fact]
        public void String_FK_unicode_is_preferred_if_specified()
        {
            var model = CreateModel();
            var mapper = CreateTypeMapper();

            Assert.Equal(
                "nvarchar(256)",
                mapper.GetMapping(model.FindEntityType(typeof(MyRelatedType3)).FindProperty("Id")).StoreType);

            Assert.Equal(
                "nvarchar(256)",
                mapper.GetMapping(model.FindEntityType(typeof(MyRelatedType4)).FindProperty("Relationship2Id")).StoreType);
        }

        private enum LongEnum : long
        {
        }

        private enum IntEnum
        {
        }

        private enum ShortEnum : short
        {
        }

        private enum ByteEnum : byte
        {
        }

        protected override ModelBuilder CreateModelBuilder() => SqlCeTestHelpers.Instance.CreateConventionBuilder();

        private class TestParameter : DbParameter
        {
            public override void ResetDbType()
            {
            }

            public override DbType DbType { get; set; }
            public override ParameterDirection Direction { get; set; }
            public override bool IsNullable { get; set; }
            public override string ParameterName { get; set; }
            public override string SourceColumn { get; set; }
            public override object Value { get; set; }
            public override bool SourceColumnNullMapping { get; set; }
            public override int Size { get; set; }
        }

        private class TestCommand : DbCommand
        {
            public override void Prepare()
            {
            }

            public override string CommandText { get; set; }
            public override int CommandTimeout { get; set; }
            public override CommandType CommandType { get; set; }
            public override UpdateRowSource UpdatedRowSource { get; set; }
            protected override DbConnection DbConnection { get; set; }
            protected override DbParameterCollection DbParameterCollection { get; }
            protected override DbTransaction DbTransaction { get; set; }
            public override bool DesignTimeVisible { get; set; }

            public override void Cancel()
            {
            }

            protected override DbParameter CreateDbParameter()
            {
                return new TestParameter();
            }

            protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
            {
                throw new NotImplementedException();
            }

            public override int ExecuteNonQuery()
            {
                throw new NotImplementedException();
            }

            public override object ExecuteScalar()
            {
                throw new NotImplementedException();
            }
        }
    }
}
