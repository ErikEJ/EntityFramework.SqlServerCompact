using System;
using System.Collections.Generic;
using System.Data;
using EFCore.SqlCe.Storage.Internal;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Microsoft.EntityFrameworkCore
{
    public class EverythingIsStringsSqlCeTest : BuiltInDataTypesTestBase<EverythingIsStringsSqlCeTest.EverythingIsStringsSqlCeFixture>
    {
        public EverythingIsStringsSqlCeTest(EverythingIsStringsSqlCeFixture fixture)
            : base(fixture)
        {
        }

        //System.Data.SqlServerCe.SqlCeException : The data was truncated while converting from one data type to another. [ Name of function(if known) =  ]
        [Fact(Skip = "SQLCE limitation")]
        public override void Can_insert_and_read_with_max_length_set()
        {
            base.Can_insert_and_read_with_max_length_set();
        }

        //System.Data.SqlServerCe.SqlCeException : The data was truncated while converting from one data type to another. [ Name of function(if known) =  ]
        [Fact(Skip = "SQLCE limitation")]
        public override void Can_perform_query_with_max_length()
        {
            base.Can_perform_query_with_max_length();
        }

        [ConditionalFact]
        public virtual void Columns_have_expected_data_types()
        {
            var actual = BuiltInDataTypesSqlCeTest.QueryForColumnTypes(CreateContext());

            const string expected = @"BinaryForeignKeyDataType.BinaryKeyDataTypeId ---> [nullable nvarchar] [MaxLength = 256]
BinaryForeignKeyDataType.Id ---> [nvarchar] [MaxLength = 64]
BinaryKeyDataType.Id ---> [nvarchar] [MaxLength = 256]
BuiltInDataTypes.Enum16 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypes.Enum32 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypes.Enum64 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypes.Enum8 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypes.EnumS8 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypes.EnumU16 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypes.EnumU32 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypes.EnumU64 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypes.Id ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypes.PartitionId ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypes.TestBoolean ---> [nvarchar] [MaxLength = 1]
BuiltInDataTypes.TestByte ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypes.TestCharacter ---> [nvarchar] [MaxLength = 1]
BuiltInDataTypes.TestDateTime ---> [nvarchar] [MaxLength = 48]
BuiltInDataTypes.TestDateTimeOffset ---> [nvarchar] [MaxLength = 48]
BuiltInDataTypes.TestDecimal ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypes.TestDouble ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypes.TestInt16 ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypes.TestInt32 ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypes.TestInt64 ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypes.TestSignedByte ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypes.TestSingle ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypes.TestTimeSpan ---> [nvarchar] [MaxLength = 48]
BuiltInDataTypes.TestUnsignedInt16 ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypes.TestUnsignedInt32 ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypes.TestUnsignedInt64 ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypesShadow.Enum16 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypesShadow.Enum32 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypesShadow.Enum64 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypesShadow.Enum8 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypesShadow.EnumS8 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypesShadow.EnumU16 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypesShadow.EnumU32 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypesShadow.EnumU64 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypesShadow.Id ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypesShadow.PartitionId ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypesShadow.TestBoolean ---> [nvarchar] [MaxLength = 1]
BuiltInDataTypesShadow.TestByte ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypesShadow.TestCharacter ---> [nvarchar] [MaxLength = 1]
BuiltInDataTypesShadow.TestDateTime ---> [nvarchar] [MaxLength = 48]
BuiltInDataTypesShadow.TestDateTimeOffset ---> [nvarchar] [MaxLength = 48]
BuiltInDataTypesShadow.TestDecimal ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypesShadow.TestDouble ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypesShadow.TestInt16 ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypesShadow.TestInt32 ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypesShadow.TestInt64 ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypesShadow.TestSignedByte ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypesShadow.TestSingle ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypesShadow.TestTimeSpan ---> [nvarchar] [MaxLength = 48]
BuiltInDataTypesShadow.TestUnsignedInt16 ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypesShadow.TestUnsignedInt32 ---> [nvarchar] [MaxLength = 64]
BuiltInDataTypesShadow.TestUnsignedInt64 ---> [nvarchar] [MaxLength = 64]
BuiltInNullableDataTypes.Enum16 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypes.Enum32 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypes.Enum64 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypes.Enum8 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypes.EnumS8 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypes.EnumU16 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypes.EnumU32 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypes.EnumU64 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypes.Id ---> [nvarchar] [MaxLength = 64]
BuiltInNullableDataTypes.PartitionId ---> [nvarchar] [MaxLength = 64]
BuiltInNullableDataTypes.TestByteArray ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypes.TestNullableBoolean ---> [nullable nvarchar] [MaxLength = 1]
BuiltInNullableDataTypes.TestNullableByte ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypes.TestNullableCharacter ---> [nullable nvarchar] [MaxLength = 1]
BuiltInNullableDataTypes.TestNullableDateTime ---> [nullable nvarchar] [MaxLength = 48]
BuiltInNullableDataTypes.TestNullableDateTimeOffset ---> [nullable nvarchar] [MaxLength = 48]
BuiltInNullableDataTypes.TestNullableDecimal ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypes.TestNullableDouble ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypes.TestNullableInt16 ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypes.TestNullableInt32 ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypes.TestNullableInt64 ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypes.TestNullableSignedByte ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypes.TestNullableSingle ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypes.TestNullableTimeSpan ---> [nullable nvarchar] [MaxLength = 48]
BuiltInNullableDataTypes.TestNullableUnsignedInt16 ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypes.TestNullableUnsignedInt32 ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypes.TestNullableUnsignedInt64 ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypes.TestString ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypesShadow.Enum16 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypesShadow.Enum32 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypesShadow.Enum64 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypesShadow.Enum8 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypesShadow.EnumS8 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypesShadow.EnumU16 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypesShadow.EnumU32 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypesShadow.EnumU64 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypesShadow.Id ---> [nvarchar] [MaxLength = 64]
BuiltInNullableDataTypesShadow.PartitionId ---> [nvarchar] [MaxLength = 64]
BuiltInNullableDataTypesShadow.TestByteArray ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypesShadow.TestNullableBoolean ---> [nullable nvarchar] [MaxLength = 1]
BuiltInNullableDataTypesShadow.TestNullableByte ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypesShadow.TestNullableCharacter ---> [nullable nvarchar] [MaxLength = 1]
BuiltInNullableDataTypesShadow.TestNullableDateTime ---> [nullable nvarchar] [MaxLength = 48]
BuiltInNullableDataTypesShadow.TestNullableDateTimeOffset ---> [nullable nvarchar] [MaxLength = 48]
BuiltInNullableDataTypesShadow.TestNullableDecimal ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypesShadow.TestNullableDouble ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypesShadow.TestNullableInt16 ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypesShadow.TestNullableInt32 ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypesShadow.TestNullableInt64 ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypesShadow.TestNullableSignedByte ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypesShadow.TestNullableSingle ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypesShadow.TestNullableTimeSpan ---> [nullable nvarchar] [MaxLength = 48]
BuiltInNullableDataTypesShadow.TestNullableUnsignedInt16 ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypesShadow.TestNullableUnsignedInt32 ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypesShadow.TestNullableUnsignedInt64 ---> [nullable nvarchar] [MaxLength = 64]
BuiltInNullableDataTypesShadow.TestString ---> [nullable nvarchar] [MaxLength = 4000]
MaxLengthDataTypes.ByteArray5 ---> [nullable nvarchar] [MaxLength = 8]
MaxLengthDataTypes.ByteArray9000 ---> [nullable nvarchar] [MaxLength = 4000]
MaxLengthDataTypes.Id ---> [nvarchar] [MaxLength = 64]
MaxLengthDataTypes.String3 ---> [nullable nvarchar] [MaxLength = 3]
MaxLengthDataTypes.String9000 ---> [nullable nvarchar] [MaxLength = 4000]
StringForeignKeyDataType.Id ---> [nvarchar] [MaxLength = 64]
StringForeignKeyDataType.StringKeyDataTypeId ---> [nullable nvarchar] [MaxLength = 256]
StringKeyDataType.Id ---> [nvarchar] [MaxLength = 256]
UnicodeDataTypes.Id ---> [nvarchar] [MaxLength = 64]
UnicodeDataTypes.StringAnsi ---> [nullable nvarchar] [MaxLength = 4000]
UnicodeDataTypes.StringAnsi3 ---> [nullable nvarchar] [MaxLength = 3]
UnicodeDataTypes.StringAnsi9000 ---> [nullable nvarchar] [MaxLength = 4000]
UnicodeDataTypes.StringDefault ---> [nullable nvarchar] [MaxLength = 4000]
UnicodeDataTypes.StringUnicode ---> [nullable nvarchar] [MaxLength = 4000]
";

            Assert.Equal(expected, actual, ignoreLineEndingDifferences: true);
        }

        public class EverythingIsStringsSqlCeFixture : BuiltInDataTypesFixtureBase
        {
            public override bool StrictEquality => true;

            public override bool SupportsAnsi => false;

            public override bool SupportsUnicodeToAnsiConversion => false;

            public override bool SupportsLargeStringComparisons => true;

            protected override string StoreName { get; } = "EverythingIsStrings";

            protected override ITestStoreFactory TestStoreFactory => SqlCeStringsTestStoreFactory.Instance;

            public override bool SupportsBinaryKeys => true;

            public override int LongStringLength => 4000;

            public override DateTime DefaultDateTime => new DateTime(1753, 1, 1);

            public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
                => base
                    .AddOptions(builder)
                    .ConfigureWarnings(
                        c => c.Log(RelationalEventId.QueryClientEvaluationWarning));
                            //.Log(SqlServerEventId.DecimalTypeDefaultWarning));

            protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
            {
                base.OnModelCreating(modelBuilder, context);

                modelBuilder.Entity<MaxLengthDataTypes>().Property(e => e.ByteArray5).HasMaxLength(8);
            }
        }

        public class SqlCeStringsTestStoreFactory : SqlCeTestStoreFactory
        {
            public new static SqlCeStringsTestStoreFactory Instance { get; } = new SqlCeStringsTestStoreFactory();

            public override IServiceCollection AddProviderServices(IServiceCollection serviceCollection)
                => base.AddProviderServices(
                    serviceCollection.AddSingleton<IRelationalTypeMappingSource, SqlCeStringsTypeMappingSource>());
        }

        public class SqlCeStringsTypeMappingSource : RelationalTypeMappingSource
        {
            private readonly SqlCeStringTypeMapping _fixedLengthUnicodeString
                = new SqlCeStringTypeMapping("nchar", dbType: DbType.String);

            private readonly SqlCeStringTypeMapping _variableLengthUnicodeString
                = new SqlCeStringTypeMapping("nvarchar", dbType: null);

            private readonly Dictionary<string, RelationalTypeMapping> _storeTypeMappings;

            /// <summary>
            ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            public SqlCeStringsTypeMappingSource(
                TypeMappingSourceDependencies dependencies,
                RelationalTypeMappingSourceDependencies relationalDependencies)
                : base(dependencies, relationalDependencies)
            {
                _storeTypeMappings
                    = new Dictionary<string, RelationalTypeMapping>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "national char varying", _variableLengthUnicodeString },
                        { "national character varying", _variableLengthUnicodeString },
                        { "national character", _fixedLengthUnicodeString },
                        { "nchar", _fixedLengthUnicodeString },
                        { "ntext", _variableLengthUnicodeString },
                        { "nvarchar", _variableLengthUnicodeString },
                    };
            }

            protected override RelationalTypeMapping FindMapping(RelationalTypeMappingInfo mappingInfo)
                => FindRawMapping(mappingInfo)?.Clone(mappingInfo);

            private RelationalTypeMapping FindRawMapping(RelationalTypeMappingInfo mappingInfo)
            {
                var clrType = mappingInfo.ClrType;
                var storeTypeName = mappingInfo.StoreTypeName;
                var storeTypeNameBase = mappingInfo.StoreTypeNameBase;

                if (storeTypeName != null)
                {
                    if (_storeTypeMappings.TryGetValue(storeTypeName, out var mapping)
                        || _storeTypeMappings.TryGetValue(storeTypeNameBase, out mapping))
                    {
                        return clrType == null
                               || mapping.ClrType == clrType
                            ? mapping
                            : null;
                    }
                }

                if (clrType != null)
                {
                    if (clrType == typeof(string))
                    {
                        var isFixedLength = mappingInfo.IsFixedLength == true;
                        var baseName = "n" + (isFixedLength ? "char" : "varchar");
                        var maxSize = 4000;

                        var size = mappingInfo.Size ?? (mappingInfo.IsKeyOrIndex ? (int?)(256) : maxSize);

                        if (size > maxSize)
                        {
                            size = null;
                        }

                        var storeType = "nvarchar(4000)";
                        if (size != null && mappingInfo.StoreTypeName == null)
                        {
                            storeType = baseName + "(" + size.ToString() + ")";
                        }

                        var dbType = isFixedLength ? DbType.StringFixedLength : (DbType?)null;

                        return new SqlCeStringTypeMapping(
                            storeType,
                            dbType,
                            size,
                            isFixedLength);
                    }
                }

                return null;
            }
        }
    }
}
