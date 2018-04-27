using System;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;
using Xunit;

// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore
{
    [SqlServerCondition(SqlServerCondition.IsNotSqlAzure)]
    public class CustomConvertersSqlCeTest : CustomConvertersTestBase<CustomConvertersSqlCeTest.CustomConvertersSqlCeFixture>
    {
        public CustomConvertersSqlCeTest(CustomConvertersSqlCeFixture fixture)
            : base(fixture)
        {
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
BinaryForeignKeyDataType.Id ---> [int] [Precision = 10]
BinaryKeyDataType.Id ---> [varbinary] [MaxLength = 512]
BuiltInDataTypes.Enum16 ---> [bigint] [Precision = 19]
BuiltInDataTypes.Enum32 ---> [bigint] [Precision = 19]
BuiltInDataTypes.Enum64 ---> [bigint] [Precision = 19]
BuiltInDataTypes.Enum8 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypes.EnumS8 ---> [nvarchar] [MaxLength = 24]
BuiltInDataTypes.EnumU16 ---> [numeric] [Precision = 20 Scale = 0]
BuiltInDataTypes.EnumU32 ---> [numeric] [Precision = 20 Scale = 0]
BuiltInDataTypes.EnumU64 ---> [numeric] [Precision = 20 Scale = 0]
BuiltInDataTypes.Id ---> [int] [Precision = 10]
BuiltInDataTypes.PartitionId ---> [bigint] [Precision = 19]
BuiltInDataTypes.TestBoolean ---> [nvarchar] [MaxLength = 4]
BuiltInDataTypes.TestByte ---> [int] [Precision = 10]
BuiltInDataTypes.TestCharacter ---> [int] [Precision = 10]
BuiltInDataTypes.TestDateTime ---> [bigint] [Precision = 19]
BuiltInDataTypes.TestDateTimeOffset ---> [bigint] [Precision = 19]
BuiltInDataTypes.TestDecimal ---> [varbinary] [MaxLength = 16]
BuiltInDataTypes.TestDouble ---> [numeric] [Precision = 26 Scale = 16]
BuiltInDataTypes.TestInt16 ---> [bigint] [Precision = 19]
BuiltInDataTypes.TestInt32 ---> [bigint] [Precision = 19]
BuiltInDataTypes.TestInt64 ---> [bigint] [Precision = 19]
BuiltInDataTypes.TestSignedByte ---> [numeric] [Precision = 18 Scale = 2]
BuiltInDataTypes.TestSingle ---> [float] [Precision = 53]
BuiltInDataTypes.TestTimeSpan ---> [float] [Precision = 53]
BuiltInDataTypes.TestUnsignedInt16 ---> [numeric] [Precision = 20 Scale = 0]
BuiltInDataTypes.TestUnsignedInt32 ---> [numeric] [Precision = 20 Scale = 0]
BuiltInDataTypes.TestUnsignedInt64 ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.Enum16 ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.Enum32 ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.Enum64 ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.Enum8 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypesShadow.EnumS8 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypesShadow.EnumU16 ---> [numeric] [Precision = 20 Scale = 0]
BuiltInDataTypesShadow.EnumU32 ---> [numeric] [Precision = 20 Scale = 0]
BuiltInDataTypesShadow.EnumU64 ---> [numeric] [Precision = 20 Scale = 0]
BuiltInDataTypesShadow.Id ---> [int] [Precision = 10]
BuiltInDataTypesShadow.PartitionId ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.TestBoolean ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypesShadow.TestByte ---> [int] [Precision = 10]
BuiltInDataTypesShadow.TestCharacter ---> [int] [Precision = 10]
BuiltInDataTypesShadow.TestDateTime ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.TestDateTimeOffset ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.TestDecimal ---> [varbinary] [MaxLength = 16]
BuiltInDataTypesShadow.TestDouble ---> [numeric] [Precision = 26 Scale = 16]
BuiltInDataTypesShadow.TestInt16 ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.TestInt32 ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.TestInt64 ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.TestSignedByte ---> [numeric] [Precision = 18 Scale = 2]
BuiltInDataTypesShadow.TestSingle ---> [float] [Precision = 53]
BuiltInDataTypesShadow.TestTimeSpan ---> [float] [Precision = 53]
BuiltInDataTypesShadow.TestUnsignedInt16 ---> [numeric] [Precision = 20 Scale = 0]
BuiltInDataTypesShadow.TestUnsignedInt32 ---> [numeric] [Precision = 20 Scale = 0]
BuiltInDataTypesShadow.TestUnsignedInt64 ---> [bigint] [Precision = 19]
BuiltInNullableDataTypes.Enum16 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.Enum32 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.Enum64 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.Enum8 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypes.EnumS8 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypes.EnumU16 ---> [nullable numeric] [Precision = 20 Scale = 0]
BuiltInNullableDataTypes.EnumU32 ---> [nullable numeric] [Precision = 20 Scale = 0]
BuiltInNullableDataTypes.EnumU64 ---> [nullable numeric] [Precision = 20 Scale = 0]
BuiltInNullableDataTypes.Id ---> [int] [Precision = 10]
BuiltInNullableDataTypes.PartitionId ---> [bigint] [Precision = 19]
BuiltInNullableDataTypes.TestByteArray ---> [nullable image] [MaxLength = 1073741823]
BuiltInNullableDataTypes.TestNullableBoolean ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypes.TestNullableByte ---> [nullable int] [Precision = 10]
BuiltInNullableDataTypes.TestNullableCharacter ---> [nullable int] [Precision = 10]
BuiltInNullableDataTypes.TestNullableDateTime ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.TestNullableDateTimeOffset ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.TestNullableDecimal ---> [nullable varbinary] [MaxLength = 16]
BuiltInNullableDataTypes.TestNullableDouble ---> [nullable numeric] [Precision = 26 Scale = 16]
BuiltInNullableDataTypes.TestNullableInt16 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.TestNullableInt32 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.TestNullableInt64 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.TestNullableSignedByte ---> [nullable numeric] [Precision = 18 Scale = 2]
BuiltInNullableDataTypes.TestNullableSingle ---> [nullable float] [Precision = 53]
BuiltInNullableDataTypes.TestNullableTimeSpan ---> [nullable float] [Precision = 53]
BuiltInNullableDataTypes.TestNullableUnsignedInt16 ---> [nullable numeric] [Precision = 20 Scale = 0]
BuiltInNullableDataTypes.TestNullableUnsignedInt32 ---> [nullable numeric] [Precision = 20 Scale = 0]
BuiltInNullableDataTypes.TestNullableUnsignedInt64 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.TestString ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypesShadow.Enum16 ---> [nullable smallint] [Precision = 5]
BuiltInNullableDataTypesShadow.Enum32 ---> [nullable int] [Precision = 10]
BuiltInNullableDataTypesShadow.Enum64 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypesShadow.Enum8 ---> [nullable tinyint] [Precision = 3]
BuiltInNullableDataTypesShadow.EnumS8 ---> [nullable smallint] [Precision = 5]
BuiltInNullableDataTypesShadow.EnumU16 ---> [nullable int] [Precision = 10]
BuiltInNullableDataTypesShadow.EnumU32 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypesShadow.EnumU64 ---> [nullable numeric] [Precision = 20 Scale = 0]
BuiltInNullableDataTypesShadow.Id ---> [int] [Precision = 10]
BuiltInNullableDataTypesShadow.PartitionId ---> [int] [Precision = 10]
BuiltInNullableDataTypesShadow.TestByteArray ---> [nullable image] [MaxLength = 1073741823]
BuiltInNullableDataTypesShadow.TestNullableBoolean ---> [nullable bit] [Precision = 1 Scale = 0]
BuiltInNullableDataTypesShadow.TestNullableByte ---> [nullable tinyint] [Precision = 3]
BuiltInNullableDataTypesShadow.TestNullableCharacter ---> [nullable nvarchar] [MaxLength = 1]
BuiltInNullableDataTypesShadow.TestNullableDateTime ---> [nullable datetime] [Precision = 23 Scale = 3]
BuiltInNullableDataTypesShadow.TestNullableDateTimeOffset ---> [nullable nvarchar] [MaxLength = 48]
BuiltInNullableDataTypesShadow.TestNullableDecimal ---> [nullable numeric] [Precision = 18 Scale = 2]
BuiltInNullableDataTypesShadow.TestNullableDouble ---> [nullable float] [Precision = 53]
BuiltInNullableDataTypesShadow.TestNullableInt16 ---> [nullable smallint] [Precision = 5]
BuiltInNullableDataTypesShadow.TestNullableInt32 ---> [nullable int] [Precision = 10]
BuiltInNullableDataTypesShadow.TestNullableInt64 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypesShadow.TestNullableSignedByte ---> [nullable smallint] [Precision = 5]
BuiltInNullableDataTypesShadow.TestNullableSingle ---> [nullable real] [Precision = 24]
BuiltInNullableDataTypesShadow.TestNullableTimeSpan ---> [nullable nvarchar] [MaxLength = 48]
BuiltInNullableDataTypesShadow.TestNullableUnsignedInt16 ---> [nullable int] [Precision = 10]
BuiltInNullableDataTypesShadow.TestNullableUnsignedInt32 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypesShadow.TestNullableUnsignedInt64 ---> [nullable numeric] [Precision = 20 Scale = 0]
BuiltInNullableDataTypesShadow.TestString ---> [nullable nvarchar] [MaxLength = 4000]
MaxLengthDataTypes.ByteArray5 ---> [nullable varbinary] [MaxLength = 7]
MaxLengthDataTypes.ByteArray9000 ---> [nullable nvarchar] [MaxLength = 4000]
MaxLengthDataTypes.Id ---> [int] [Precision = 10]
MaxLengthDataTypes.String3 ---> [nullable nvarchar] [MaxLength = 12]
MaxLengthDataTypes.String9000 ---> [nullable varbinary] [MaxLength = 4000]
StringForeignKeyDataType.Id ---> [int] [Precision = 10]
StringForeignKeyDataType.StringKeyDataTypeId ---> [nullable nvarchar] [MaxLength = 256]
StringKeyDataType.Id ---> [nvarchar] [MaxLength = 256]
UnicodeDataTypes.Id ---> [int] [Precision = 10]
UnicodeDataTypes.StringAnsi ---> [nullable nvarchar] [MaxLength = 4000]
UnicodeDataTypes.StringAnsi3 ---> [nullable nvarchar] [MaxLength = 3]
UnicodeDataTypes.StringAnsi9000 ---> [nullable nvarchar] [MaxLength = 4000]
UnicodeDataTypes.StringDefault ---> [nullable nvarchar] [MaxLength = 4000]
UnicodeDataTypes.StringUnicode ---> [nullable nvarchar] [MaxLength = 4000]
User.Email ---> [nullable nvarchar] [MaxLength = 4000]
User.Id ---> [uniqueidentifier]
";

            Assert.Equal(expected, actual, ignoreLineEndingDifferences: true);
        }

        public class CustomConvertersSqlCeFixture : CustomConvertersFixtureBase
        {
            public override bool StrictEquality => true;

            public override bool SupportsAnsi => false;

            public override bool SupportsUnicodeToAnsiConversion => false;

            public override bool SupportsLargeStringComparisons => true;

            public override int LongStringLength => 4000;

            protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;

            public override bool SupportsBinaryKeys => true;

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

                modelBuilder.Entity<BuiltInDataTypes>().Property(e => e.TestBoolean).IsFixedLength();
            }
        }
    }
}
