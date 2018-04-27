﻿using System;
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
    [SqlServerCondition(SqlServerCondition.IsNotSqlAzure)]
    public class EverythingIsBytesSqlCeTest : BuiltInDataTypesTestBase<EverythingIsBytesSqlCeTest.EverythingIsBytesSqlCeFixture>
    {
        public EverythingIsBytesSqlCeTest(EverythingIsBytesSqlCeFixture fixture)
            : base(fixture)
        {
        }

        [Fact(Skip ="SQLCE limitation")]
        public override void Can_perform_query_with_ansi_strings_test()
        {
            base.Can_perform_query_with_ansi_strings_test();
        }

        //System.Data.SqlServerCe.SqlCeException : The data was truncated while converting from one data type to another. [ Name of function(if known) =  ]
        [Fact(Skip = "ErikEJ Investigate")]
        public override void Can_insert_and_read_with_max_length_set()
        {
            base.Can_insert_and_read_with_max_length_set();
        }

        //System.Data.SqlServerCe.SqlCeException : The data was truncated while converting from one data type to another. [ Name of function(if known) =  ]
        [Fact(Skip = "ErikEJ Investigate")]
        public override void Can_perform_query_with_max_length()
        {
            base.Can_perform_query_with_max_length();
        }

        [ConditionalFact]
        public virtual void Columns_have_expected_data_types()
        {
            var actual = BuiltInDataTypesSqlCeTest.QueryForColumnTypes(CreateContext());

            const string expected = @"BinaryForeignKeyDataType.BinaryKeyDataTypeId ---> [nullable varbinary] [MaxLength = 512]
BinaryForeignKeyDataType.Id ---> [varbinary] [MaxLength = 4]
BinaryKeyDataType.Id ---> [varbinary] [MaxLength = 512]
BuiltInDataTypes.Enum16 ---> [varbinary] [MaxLength = 2]
BuiltInDataTypes.Enum32 ---> [varbinary] [MaxLength = 4]
BuiltInDataTypes.Enum64 ---> [varbinary] [MaxLength = 8]
BuiltInDataTypes.Enum8 ---> [varbinary] [MaxLength = 1]
BuiltInDataTypes.EnumS8 ---> [varbinary] [MaxLength = 1]
BuiltInDataTypes.EnumU16 ---> [varbinary] [MaxLength = 2]
BuiltInDataTypes.EnumU32 ---> [varbinary] [MaxLength = 4]
BuiltInDataTypes.EnumU64 ---> [varbinary] [MaxLength = 8]
BuiltInDataTypes.Id ---> [varbinary] [MaxLength = 4]
BuiltInDataTypes.PartitionId ---> [varbinary] [MaxLength = 4]
BuiltInDataTypes.TestBoolean ---> [varbinary] [MaxLength = 1]
BuiltInDataTypes.TestByte ---> [varbinary] [MaxLength = 1]
BuiltInDataTypes.TestCharacter ---> [varbinary] [MaxLength = 2]
BuiltInDataTypes.TestDateTime ---> [varbinary] [MaxLength = 8]
BuiltInDataTypes.TestDateTimeOffset ---> [varbinary] [MaxLength = 12]
BuiltInDataTypes.TestDecimal ---> [varbinary] [MaxLength = 16]
BuiltInDataTypes.TestDouble ---> [varbinary] [MaxLength = 8]
BuiltInDataTypes.TestInt16 ---> [varbinary] [MaxLength = 2]
BuiltInDataTypes.TestInt32 ---> [varbinary] [MaxLength = 4]
BuiltInDataTypes.TestInt64 ---> [varbinary] [MaxLength = 8]
BuiltInDataTypes.TestSignedByte ---> [varbinary] [MaxLength = 1]
BuiltInDataTypes.TestSingle ---> [varbinary] [MaxLength = 4]
BuiltInDataTypes.TestTimeSpan ---> [varbinary] [MaxLength = 8]
BuiltInDataTypes.TestUnsignedInt16 ---> [varbinary] [MaxLength = 2]
BuiltInDataTypes.TestUnsignedInt32 ---> [varbinary] [MaxLength = 4]
BuiltInDataTypes.TestUnsignedInt64 ---> [varbinary] [MaxLength = 8]
BuiltInDataTypesShadow.Enum16 ---> [varbinary] [MaxLength = 2]
BuiltInDataTypesShadow.Enum32 ---> [varbinary] [MaxLength = 4]
BuiltInDataTypesShadow.Enum64 ---> [varbinary] [MaxLength = 8]
BuiltInDataTypesShadow.Enum8 ---> [varbinary] [MaxLength = 1]
BuiltInDataTypesShadow.EnumS8 ---> [varbinary] [MaxLength = 1]
BuiltInDataTypesShadow.EnumU16 ---> [varbinary] [MaxLength = 2]
BuiltInDataTypesShadow.EnumU32 ---> [varbinary] [MaxLength = 4]
BuiltInDataTypesShadow.EnumU64 ---> [varbinary] [MaxLength = 8]
BuiltInDataTypesShadow.Id ---> [varbinary] [MaxLength = 4]
BuiltInDataTypesShadow.PartitionId ---> [varbinary] [MaxLength = 4]
BuiltInDataTypesShadow.TestBoolean ---> [varbinary] [MaxLength = 1]
BuiltInDataTypesShadow.TestByte ---> [varbinary] [MaxLength = 1]
BuiltInDataTypesShadow.TestCharacter ---> [varbinary] [MaxLength = 2]
BuiltInDataTypesShadow.TestDateTime ---> [varbinary] [MaxLength = 8]
BuiltInDataTypesShadow.TestDateTimeOffset ---> [varbinary] [MaxLength = 12]
BuiltInDataTypesShadow.TestDecimal ---> [varbinary] [MaxLength = 16]
BuiltInDataTypesShadow.TestDouble ---> [varbinary] [MaxLength = 8]
BuiltInDataTypesShadow.TestInt16 ---> [varbinary] [MaxLength = 2]
BuiltInDataTypesShadow.TestInt32 ---> [varbinary] [MaxLength = 4]
BuiltInDataTypesShadow.TestInt64 ---> [varbinary] [MaxLength = 8]
BuiltInDataTypesShadow.TestSignedByte ---> [varbinary] [MaxLength = 1]
BuiltInDataTypesShadow.TestSingle ---> [varbinary] [MaxLength = 4]
BuiltInDataTypesShadow.TestTimeSpan ---> [varbinary] [MaxLength = 8]
BuiltInDataTypesShadow.TestUnsignedInt16 ---> [varbinary] [MaxLength = 2]
BuiltInDataTypesShadow.TestUnsignedInt32 ---> [varbinary] [MaxLength = 4]
BuiltInDataTypesShadow.TestUnsignedInt64 ---> [varbinary] [MaxLength = 8]
BuiltInNullableDataTypes.Enum16 ---> [nullable varbinary] [MaxLength = 2]
BuiltInNullableDataTypes.Enum32 ---> [nullable varbinary] [MaxLength = 4]
BuiltInNullableDataTypes.Enum64 ---> [nullable varbinary] [MaxLength = 8]
BuiltInNullableDataTypes.Enum8 ---> [nullable varbinary] [MaxLength = 1]
BuiltInNullableDataTypes.EnumS8 ---> [nullable varbinary] [MaxLength = 1]
BuiltInNullableDataTypes.EnumU16 ---> [nullable varbinary] [MaxLength = 2]
BuiltInNullableDataTypes.EnumU32 ---> [nullable varbinary] [MaxLength = 4]
BuiltInNullableDataTypes.EnumU64 ---> [nullable varbinary] [MaxLength = 8]
BuiltInNullableDataTypes.Id ---> [varbinary] [MaxLength = 4]
BuiltInNullableDataTypes.PartitionId ---> [varbinary] [MaxLength = 4]
BuiltInNullableDataTypes.TestByteArray ---> [nullable image] [MaxLength = 1073741823]
BuiltInNullableDataTypes.TestNullableBoolean ---> [nullable varbinary] [MaxLength = 1]
BuiltInNullableDataTypes.TestNullableByte ---> [nullable varbinary] [MaxLength = 1]
BuiltInNullableDataTypes.TestNullableCharacter ---> [nullable varbinary] [MaxLength = 2]
BuiltInNullableDataTypes.TestNullableDateTime ---> [nullable varbinary] [MaxLength = 8]
BuiltInNullableDataTypes.TestNullableDateTimeOffset ---> [nullable varbinary] [MaxLength = 12]
BuiltInNullableDataTypes.TestNullableDecimal ---> [nullable varbinary] [MaxLength = 16]
BuiltInNullableDataTypes.TestNullableDouble ---> [nullable varbinary] [MaxLength = 8]
BuiltInNullableDataTypes.TestNullableInt16 ---> [nullable varbinary] [MaxLength = 2]
BuiltInNullableDataTypes.TestNullableInt32 ---> [nullable varbinary] [MaxLength = 4]
BuiltInNullableDataTypes.TestNullableInt64 ---> [nullable varbinary] [MaxLength = 8]
BuiltInNullableDataTypes.TestNullableSignedByte ---> [nullable varbinary] [MaxLength = 1]
BuiltInNullableDataTypes.TestNullableSingle ---> [nullable varbinary] [MaxLength = 4]
BuiltInNullableDataTypes.TestNullableTimeSpan ---> [nullable varbinary] [MaxLength = 8]
BuiltInNullableDataTypes.TestNullableUnsignedInt16 ---> [nullable varbinary] [MaxLength = 2]
BuiltInNullableDataTypes.TestNullableUnsignedInt32 ---> [nullable varbinary] [MaxLength = 4]
BuiltInNullableDataTypes.TestNullableUnsignedInt64 ---> [nullable varbinary] [MaxLength = 8]
BuiltInNullableDataTypes.TestString ---> [nullable image] [MaxLength = 1073741823]
BuiltInNullableDataTypesShadow.Enum16 ---> [nullable varbinary] [MaxLength = 2]
BuiltInNullableDataTypesShadow.Enum32 ---> [nullable varbinary] [MaxLength = 4]
BuiltInNullableDataTypesShadow.Enum64 ---> [nullable varbinary] [MaxLength = 8]
BuiltInNullableDataTypesShadow.Enum8 ---> [nullable varbinary] [MaxLength = 1]
BuiltInNullableDataTypesShadow.EnumS8 ---> [nullable varbinary] [MaxLength = 1]
BuiltInNullableDataTypesShadow.EnumU16 ---> [nullable varbinary] [MaxLength = 2]
BuiltInNullableDataTypesShadow.EnumU32 ---> [nullable varbinary] [MaxLength = 4]
BuiltInNullableDataTypesShadow.EnumU64 ---> [nullable varbinary] [MaxLength = 8]
BuiltInNullableDataTypesShadow.Id ---> [varbinary] [MaxLength = 4]
BuiltInNullableDataTypesShadow.PartitionId ---> [varbinary] [MaxLength = 4]
BuiltInNullableDataTypesShadow.TestByteArray ---> [nullable image] [MaxLength = 1073741823]
BuiltInNullableDataTypesShadow.TestNullableBoolean ---> [nullable varbinary] [MaxLength = 1]
BuiltInNullableDataTypesShadow.TestNullableByte ---> [nullable varbinary] [MaxLength = 1]
BuiltInNullableDataTypesShadow.TestNullableCharacter ---> [nullable varbinary] [MaxLength = 2]
BuiltInNullableDataTypesShadow.TestNullableDateTime ---> [nullable varbinary] [MaxLength = 8]
BuiltInNullableDataTypesShadow.TestNullableDateTimeOffset ---> [nullable varbinary] [MaxLength = 12]
BuiltInNullableDataTypesShadow.TestNullableDecimal ---> [nullable varbinary] [MaxLength = 16]
BuiltInNullableDataTypesShadow.TestNullableDouble ---> [nullable varbinary] [MaxLength = 8]
BuiltInNullableDataTypesShadow.TestNullableInt16 ---> [nullable varbinary] [MaxLength = 2]
BuiltInNullableDataTypesShadow.TestNullableInt32 ---> [nullable varbinary] [MaxLength = 4]
BuiltInNullableDataTypesShadow.TestNullableInt64 ---> [nullable varbinary] [MaxLength = 8]
BuiltInNullableDataTypesShadow.TestNullableSignedByte ---> [nullable varbinary] [MaxLength = 1]
BuiltInNullableDataTypesShadow.TestNullableSingle ---> [nullable varbinary] [MaxLength = 4]
BuiltInNullableDataTypesShadow.TestNullableTimeSpan ---> [nullable varbinary] [MaxLength = 8]
BuiltInNullableDataTypesShadow.TestNullableUnsignedInt16 ---> [nullable varbinary] [MaxLength = 2]
BuiltInNullableDataTypesShadow.TestNullableUnsignedInt32 ---> [nullable varbinary] [MaxLength = 4]
BuiltInNullableDataTypesShadow.TestNullableUnsignedInt64 ---> [nullable varbinary] [MaxLength = 8]
BuiltInNullableDataTypesShadow.TestString ---> [nullable image] [MaxLength = 1073741823]
MaxLengthDataTypes.ByteArray5 ---> [nullable varbinary] [MaxLength = 5]
MaxLengthDataTypes.ByteArray9000 ---> [nullable image] [MaxLength = 1073741823]
MaxLengthDataTypes.Id ---> [varbinary] [MaxLength = 4]
MaxLengthDataTypes.String3 ---> [nullable varbinary] [MaxLength = 3]
MaxLengthDataTypes.String9000 ---> [nullable image] [MaxLength = 1073741823]
StringForeignKeyDataType.Id ---> [varbinary] [MaxLength = 4]
StringForeignKeyDataType.StringKeyDataTypeId ---> [nullable varbinary] [MaxLength = 512]
StringKeyDataType.Id ---> [varbinary] [MaxLength = 512]
UnicodeDataTypes.Id ---> [varbinary] [MaxLength = 4]
UnicodeDataTypes.StringAnsi ---> [nullable image] [MaxLength = 1073741823]
UnicodeDataTypes.StringAnsi3 ---> [nullable varbinary] [MaxLength = 3]
UnicodeDataTypes.StringAnsi9000 ---> [nullable image] [MaxLength = 1073741823]
UnicodeDataTypes.StringDefault ---> [nullable image] [MaxLength = 1073741823]
UnicodeDataTypes.StringUnicode ---> [nullable image] [MaxLength = 1073741823]
";

            Assert.Equal(expected, actual, ignoreLineEndingDifferences: true);
        }

        public class EverythingIsBytesSqlCeFixture : BuiltInDataTypesFixtureBase
        {
            public override bool StrictEquality => true;

            public override bool SupportsAnsi => true;

            public override bool SupportsUnicodeToAnsiConversion => false;

            public override bool SupportsLargeStringComparisons => true;

            protected override string StoreName { get; } = "EverythingIsBytes";

            protected override ITestStoreFactory TestStoreFactory => SqlCeBytesTestStoreFactory.Instance;

            public override bool SupportsBinaryKeys => true;

            public override DateTime DefaultDateTime => new DateTime();

            public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
                => base
                    .AddOptions(builder)
                    .ConfigureWarnings(
                        c => c.Log(RelationalEventId.QueryClientEvaluationWarning));
                            //.Log(SqlServerEventId.DecimalTypeDefaultWarning));
        }

        public class SqlCeBytesTestStoreFactory : SqlCeTestStoreFactory
        {
            public new static SqlCeBytesTestStoreFactory Instance { get; } = new SqlCeBytesTestStoreFactory();

            public override IServiceCollection AddProviderServices(IServiceCollection serviceCollection)
                => base.AddProviderServices(
                    serviceCollection.AddSingleton<IRelationalTypeMappingSource, SqlCeBytesTypeMappingSource>());
        }

        public class SqlCeBytesTypeMappingSource : RelationalTypeMappingSource
        {
            private readonly SqlCeByteArrayTypeMapping _rowversion
                = new SqlCeByteArrayTypeMapping("rowversion", dbType: DbType.Binary, size: 8);

            private readonly SqlCeByteArrayTypeMapping _variableLengthBinary
                = new SqlCeByteArrayTypeMapping("varbinary");

            private readonly SqlCeByteArrayTypeMapping _fixedLengthBinary
                = new SqlCeByteArrayTypeMapping("binary");

            private readonly Dictionary<string, RelationalTypeMapping> _storeTypeMappings;

            public SqlCeBytesTypeMappingSource(
                TypeMappingSourceDependencies dependencies,
                RelationalTypeMappingSourceDependencies relationalDependencies)
                : base(dependencies, relationalDependencies)
            {
                _storeTypeMappings
                    = new Dictionary<string, RelationalTypeMapping>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "binary varying", _variableLengthBinary },
                        { "binary", _fixedLengthBinary },
                        { "image", _variableLengthBinary },
                        { "rowversion", _rowversion },
                        { "varbinary", _variableLengthBinary }
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
                    if (clrType == typeof(byte[]))
                    {
                        if (mappingInfo.IsRowVersion == true)
                        {
                            return _rowversion;
                        }

                        var size = mappingInfo.Size ?? (mappingInfo.IsKeyOrIndex ? (int?)512 : null);
                        if (size > 8000)
                        {
                            size = null;
                        }

                        var storeType = "image";
                        if (size != null)
                        {
                            storeType = ("varbinary(") + size.ToString() + ")";
                        }

                        return new SqlCeByteArrayTypeMapping(
                            storeType,
                            DbType.Binary,
                            size);
                    }
                }

                return null;
            }
        }
    }
}
