using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;
using Xunit;

namespace Microsoft.EntityFrameworkCore
{
    [SqlServerCondition(SqlServerCondition.IsNotSqlAzure)]
    public class ConvertToProviderTypesSqlCeTest : ConvertToProviderTypesTestBase<ConvertToProviderTypesSqlCeTest.ConvertToProviderTypesSqlCeFixture>
    {
        public ConvertToProviderTypesSqlCeTest(ConvertToProviderTypesSqlCeFixture fixture)
            : base(fixture)
        {
        }

        //System.Data.SqlServerCe.SqlCeException : The data was truncated while converting from one data type to another. [ Name of function(if known) =  ]
        [Fact(Skip ="SQLCE limitation")]
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

        [Fact(Skip = "SQLCE does not support ANSI")]
        public override void Can_perform_query_with_ansi_strings_test()
        {
            base.Can_perform_query_with_ansi_strings_test();
        }

        //TODO ErikEJ Await log string update
        //[ConditionalFact]
        //public virtual void Warning_when_suspicious_conversion_in_sql()
        //{
        //    using (var context = CreateContext())
        //    {
        //        Assert.Contains(
        //            RelationalStrings.LogValueConversionSqlLiteralWarning
        //                .GenerateMessage(
        //                    typeof(decimal).ShortDisplayName(),
        //                    new NumberToBytesConverter<decimal>().GetType().ShortDisplayName()),
        //            Assert.Throws<InvalidOperationException>(
        //                () =>
        //                    context.Set<BuiltInDataTypes>().Where(b => b.TestDecimal > 123.0m).ToList()).Message);
        //    }
        //}

        [ConditionalFact]
        public virtual void Columns_have_expected_data_types()
        {
            var actual = BuiltInDataTypesSqlCeTest.QueryForColumnTypes(CreateContext());

            const string expected = @"BinaryForeignKeyDataType.BinaryKeyDataTypeId ---> [nullable nvarchar] [MaxLength = 256]
BinaryForeignKeyDataType.Id ---> [int] [Precision = 10]
BinaryKeyDataType.Id ---> [nvarchar] [MaxLength = 256]
BuiltInDataTypes.Enum16 ---> [bigint] [Precision = 19]
BuiltInDataTypes.Enum32 ---> [bigint] [Precision = 19]
BuiltInDataTypes.Enum64 ---> [bigint] [Precision = 19]
BuiltInDataTypes.Enum8 ---> [nvarchar] [MaxLength = 17]
BuiltInDataTypes.EnumS8 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypes.EnumU16 ---> [bigint] [Precision = 19]
BuiltInDataTypes.EnumU32 ---> [bigint] [Precision = 19]
BuiltInDataTypes.EnumU64 ---> [bigint] [Precision = 19]
BuiltInDataTypes.Id ---> [int] [Precision = 10]
BuiltInDataTypes.PartitionId ---> [bigint] [Precision = 19]
BuiltInDataTypes.TestBoolean ---> [nvarchar] [MaxLength = 1]
BuiltInDataTypes.TestByte ---> [int] [Precision = 10]
BuiltInDataTypes.TestCharacter ---> [bigint] [Precision = 19]
BuiltInDataTypes.TestDateTime ---> [bigint] [Precision = 19]
BuiltInDataTypes.TestDateTimeOffset ---> [bigint] [Precision = 19]
BuiltInDataTypes.TestDecimal ---> [varbinary] [MaxLength = 16]
BuiltInDataTypes.TestDouble ---> [numeric] [Precision = 38 Scale = 17]
BuiltInDataTypes.TestInt16 ---> [bigint] [Precision = 19]
BuiltInDataTypes.TestInt32 ---> [bigint] [Precision = 19]
BuiltInDataTypes.TestInt64 ---> [bigint] [Precision = 19]
BuiltInDataTypes.TestSignedByte ---> [bigint] [Precision = 19]
BuiltInDataTypes.TestSingle ---> [numeric] [Precision = 38 Scale = 17]
BuiltInDataTypes.TestTimeSpan ---> [bigint] [Precision = 19]
BuiltInDataTypes.TestUnsignedInt16 ---> [numeric] [Precision = 20 Scale = 0]
BuiltInDataTypes.TestUnsignedInt32 ---> [numeric] [Precision = 20 Scale = 0]
BuiltInDataTypes.TestUnsignedInt64 ---> [numeric] [Precision = 20 Scale = 0]
BuiltInDataTypesShadow.Enum16 ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.Enum32 ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.Enum64 ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.Enum8 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypesShadow.EnumS8 ---> [nvarchar] [MaxLength = 4000]
BuiltInDataTypesShadow.EnumU16 ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.EnumU32 ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.EnumU64 ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.Id ---> [int] [Precision = 10]
BuiltInDataTypesShadow.PartitionId ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.TestBoolean ---> [nvarchar] [MaxLength = 1]
BuiltInDataTypesShadow.TestByte ---> [int] [Precision = 10]
BuiltInDataTypesShadow.TestCharacter ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.TestDateTime ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.TestDateTimeOffset ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.TestDecimal ---> [varbinary] [MaxLength = 16]
BuiltInDataTypesShadow.TestDouble ---> [numeric] [Precision = 38 Scale = 17]
BuiltInDataTypesShadow.TestInt16 ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.TestInt32 ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.TestInt64 ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.TestSignedByte ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.TestSingle ---> [numeric] [Precision = 38 Scale = 17]
BuiltInDataTypesShadow.TestTimeSpan ---> [bigint] [Precision = 19]
BuiltInDataTypesShadow.TestUnsignedInt16 ---> [numeric] [Precision = 20 Scale = 0]
BuiltInDataTypesShadow.TestUnsignedInt32 ---> [numeric] [Precision = 20 Scale = 0]
BuiltInDataTypesShadow.TestUnsignedInt64 ---> [numeric] [Precision = 20 Scale = 0]
BuiltInNullableDataTypes.Enum16 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.Enum32 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.Enum64 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.Enum8 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypes.EnumS8 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypes.EnumU16 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.EnumU32 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.EnumU64 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.Id ---> [int] [Precision = 10]
BuiltInNullableDataTypes.PartitionId ---> [bigint] [Precision = 19]
BuiltInNullableDataTypes.TestByteArray ---> [nullable image] [MaxLength = 1073741823]
BuiltInNullableDataTypes.TestNullableBoolean ---> [nullable nvarchar] [MaxLength = 1]
BuiltInNullableDataTypes.TestNullableByte ---> [nullable int] [Precision = 10]
BuiltInNullableDataTypes.TestNullableCharacter ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.TestNullableDateTime ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.TestNullableDateTimeOffset ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.TestNullableDecimal ---> [nullable varbinary] [MaxLength = 16]
BuiltInNullableDataTypes.TestNullableDouble ---> [nullable numeric] [Precision = 38 Scale = 17]
BuiltInNullableDataTypes.TestNullableInt16 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.TestNullableInt32 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.TestNullableInt64 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.TestNullableSignedByte ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.TestNullableSingle ---> [nullable numeric] [Precision = 38 Scale = 17]
BuiltInNullableDataTypes.TestNullableTimeSpan ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.TestNullableUnsignedInt16 ---> [nullable numeric] [Precision = 20 Scale = 0]
BuiltInNullableDataTypes.TestNullableUnsignedInt32 ---> [nullable numeric] [Precision = 20 Scale = 0]
BuiltInNullableDataTypes.TestNullableUnsignedInt64 ---> [nullable numeric] [Precision = 20 Scale = 0]
BuiltInNullableDataTypes.TestString ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypesShadow.Enum16 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypesShadow.Enum32 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypesShadow.Enum64 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypesShadow.Enum8 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypesShadow.EnumS8 ---> [nullable nvarchar] [MaxLength = 4000]
BuiltInNullableDataTypesShadow.EnumU16 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypesShadow.EnumU32 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypesShadow.EnumU64 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypesShadow.Id ---> [int] [Precision = 10]
BuiltInNullableDataTypesShadow.PartitionId ---> [bigint] [Precision = 19]
BuiltInNullableDataTypesShadow.TestByteArray ---> [nullable image] [MaxLength = 1073741823]
BuiltInNullableDataTypesShadow.TestNullableBoolean ---> [nullable nvarchar] [MaxLength = 1]
BuiltInNullableDataTypesShadow.TestNullableByte ---> [nullable int] [Precision = 10]
BuiltInNullableDataTypesShadow.TestNullableCharacter ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypesShadow.TestNullableDateTime ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypesShadow.TestNullableDateTimeOffset ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypesShadow.TestNullableDecimal ---> [nullable varbinary] [MaxLength = 16]
BuiltInNullableDataTypesShadow.TestNullableDouble ---> [nullable numeric] [Precision = 38 Scale = 17]
BuiltInNullableDataTypesShadow.TestNullableInt16 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypesShadow.TestNullableInt32 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypesShadow.TestNullableInt64 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypesShadow.TestNullableSignedByte ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypesShadow.TestNullableSingle ---> [nullable numeric] [Precision = 38 Scale = 17]
BuiltInNullableDataTypesShadow.TestNullableTimeSpan ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypesShadow.TestNullableUnsignedInt16 ---> [nullable numeric] [Precision = 20 Scale = 0]
BuiltInNullableDataTypesShadow.TestNullableUnsignedInt32 ---> [nullable numeric] [Precision = 20 Scale = 0]
BuiltInNullableDataTypesShadow.TestNullableUnsignedInt64 ---> [nullable numeric] [Precision = 20 Scale = 0]
BuiltInNullableDataTypesShadow.TestString ---> [nullable nvarchar] [MaxLength = 4000]
EmailTemplate.Id ---> [uniqueidentifier]
EmailTemplate.TemplateType ---> [int] [Precision = 10]
MaxLengthDataTypes.ByteArray5 ---> [nullable nvarchar] [MaxLength = 8]
MaxLengthDataTypes.ByteArray9000 ---> [nullable nvarchar] [MaxLength = 4000]
MaxLengthDataTypes.Id ---> [int] [Precision = 10]
MaxLengthDataTypes.String3 ---> [nullable varbinary] [MaxLength = 3]
MaxLengthDataTypes.String9000 ---> [nullable varbinary] [MaxLength = 4000]
StringForeignKeyDataType.Id ---> [int] [Precision = 10]
StringForeignKeyDataType.StringKeyDataTypeId ---> [nullable varbinary] [MaxLength = 512]
StringKeyDataType.Id ---> [varbinary] [MaxLength = 512]
UnicodeDataTypes.Id ---> [int] [Precision = 10]
UnicodeDataTypes.StringAnsi ---> [nullable nvarchar] [MaxLength = 4000]
UnicodeDataTypes.StringAnsi3 ---> [nullable nvarchar] [MaxLength = 3]
UnicodeDataTypes.StringAnsi9000 ---> [nullable nvarchar] [MaxLength = 4000]
UnicodeDataTypes.StringDefault ---> [nullable nvarchar] [MaxLength = 4000]
UnicodeDataTypes.StringUnicode ---> [nullable nvarchar] [MaxLength = 4000]
";

            Assert.Equal(expected, actual, ignoreLineEndingDifferences: true);
        }

        public class ConvertToProviderTypesSqlCeFixture : ConvertToProviderTypesFixtureBase
        {
            public override bool StrictEquality => true;

            public override bool SupportsAnsi => false;

            public override bool SupportsUnicodeToAnsiConversion => false;

            public override bool SupportsLargeStringComparisons => false;

            protected override ITestStoreFactory TestStoreFactory => SqlCeTestStoreFactory.Instance;

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

                modelBuilder.Entity<BuiltInDataTypes>().Property(e => e.Enum8).IsFixedLength();
            }
        }
    }
}
