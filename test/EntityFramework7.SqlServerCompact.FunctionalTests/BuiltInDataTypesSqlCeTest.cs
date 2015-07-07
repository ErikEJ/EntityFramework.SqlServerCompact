using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.FunctionalTests;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class BuiltInDataTypesSqlCeTest : BuiltInDataTypesTestBase<BuiltInDataTypesSqlCeFixture>
    {
        public BuiltInDataTypesSqlCeTest(BuiltInDataTypesSqlCeFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public virtual void Can_query_using_any_mapped_data_type()
        {
            using (var context = CreateContext())
            {
                context.Set<MappedNullableDataTypes>().Add(
                    new MappedNullableDataTypes
                    {
                        Int = 999,
                        Bigint = 78L,
                        Smallint = 79,
                        Tinyint = 80,
                        Bit = true,
                        Money = 81.1m,
                        Float = 83.3,
                        Real = 84.4f,
                        Datetime = new DateTime(2019, 1, 2, 14, 11, 12),
                        Nchar = "D",
                        National_character = "E",
                        Nvarchar = "F",
                        National_char_varying = "G",
                        National_character_varying = "H",
                        NvarcharMax = "don't",
                        National_char_varyingMax = "help",
                        National_character_varyingMax = "anyone!",
                        Ntext = "Gumball Rules OK!",
                        Binary = new byte[] { 86 },
                        Varbinary = new byte[] { 87 },
                        Image = new byte[] { 97, 98, 99, 100 },
                        Decimal = 101.7m,
                        Dec = 102.8m,
                        Numeric = 103.9m
                    });

                Assert.Equal(1, context.SaveChanges());
            }

            using (var context = CreateContext())
            {
                var entity = context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999);

                long? param1 = 78L;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.Bigint == param1));

                short? param2 = 79;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.Smallint == param2));

                byte? param3 = 80;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.Tinyint == param3));

                bool? param4 = true;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.Bit == param4));

                decimal? param5 = 81.1m;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.Money == param5));

                double? param7a = 83.3;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.Float == param7a));

                float? param7b = 84.4f;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.Real == param7b));

                DateTime? param11 = new DateTime(2019, 1, 2, 14, 11, 12);
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.Datetime == param11));

                var param22 = "D";
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.Nchar == param22));

                var param23 = "E";
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.National_character == param23));

                var param24 = "F";
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.Nvarchar == param24));

                var param25 = "G";
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.National_char_varying == param25));

                var param26 = "H";
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.National_character_varying == param26));

                var param27 = "don't";
                Assert.Equal(
                    "The ntext and image data types cannot be used in WHERE, HAVING, GROUP BY, ON, or IN clauses, except when these data types are used with the LIKE or IS NULL predicates.",
                    Assert.Throws<SqlCeException>(() => context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.NvarcharMax == param27)).Message);

                var param28 = "help";
                Assert.Equal(
                    "The ntext and image data types cannot be used in WHERE, HAVING, GROUP BY, ON, or IN clauses, except when these data types are used with the LIKE or IS NULL predicates.",
                    Assert.Throws<SqlCeException>(() => context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.National_char_varyingMax == param28)).Message);

                var param29 = "anyone!";
                Assert.Equal(
                    "The ntext and image data types cannot be used in WHERE, HAVING, GROUP BY, ON, or IN clauses, except when these data types are used with the LIKE or IS NULL predicates.",
                    Assert.Throws<SqlCeException>(() => context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.National_character_varyingMax == param29)).Message);

                var param32 = new byte[] { 86 };
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.Binary == param32));

                var param33 = new byte[] { 87 };
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.Varbinary == param33));

                decimal? param38 = 102m;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.Decimal == param38));

                decimal? param39 = 103m;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.Dec == param39));

                decimal? param40 = 104m;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 999 && e.Numeric == param40));
            }
        }

        [Fact]
        public virtual void Can_query_using_any_mapped_data_types_with_nulls()
        {
            using (var context = CreateContext())
            {
                context.Set<MappedNullableDataTypes>().Add(
                    new MappedNullableDataTypes
                    {
                        Int = 911,
                    });

                Assert.Equal(1, context.SaveChanges());
            }

            using (var context = CreateContext())
            {
                var entity = context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911);

                long? param1 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.Bigint == param1));

                short? param2 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.Smallint == param2));
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && ((long?)((int?)e.Smallint)) == param2));

                byte? param3 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.Tinyint == param3));

                bool? param4 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.Bit == param4));

                decimal? param5 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.Money == param5));

                double? param7a = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.Float == param7a));

                float? param7b = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.Real == param7b));

                DateTime? param11 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.Datetime == param11));

                string param22 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.Nchar == param22));

                string param23 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.National_character == param23));

                string param24 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.Nvarchar == param24));

                string param25 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.National_char_varying == param25));

                string param26 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.National_character_varying == param26));

                string param27 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.NvarcharMax == param27));

                string param28 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.National_char_varyingMax == param28));

                string param29 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.National_character_varyingMax == param29));

                string param31 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.Ntext == param31));

                byte[] param32 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.Binary == param32));

                byte[] param33 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.Varbinary == param33));

                byte[] param37 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.Image == param37));

                decimal? param38 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.Decimal == param38));

                decimal? param39 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.Dec == param39));

                decimal? param40 = null;
                Assert.Same(entity, context.Set<MappedNullableDataTypes>().Single(e => e.Int == 911 && e.Numeric == param40));
            }
        }

        [Fact]
        public override void Can_perform_query_with_max_length()
        {
            //Copy from base test!
            var shortString = "Sky";
            var shortBinary = new byte[] { 8, 8, 7, 8, 7 };
            var longString = new string('X', 9000);
            var longBinary = new byte[9000];
            for (var i = 0; i < longBinary.Length; i++)
            {
                longBinary[i] = (byte)(i);
            }

            using (var context = CreateContext())
            {
                context.Set<MaxLengthDataTypes>().Add(
                    new MaxLengthDataTypes
                    {
                        Id = 799,
                        String3 = shortString,
                        ByteArray5 = shortBinary,
                        String9000 = longString,
                        ByteArray9000 = longBinary
                    });

                Assert.Equal(1, context.SaveChanges());
            }

            using (var context = CreateContext())
            {
                Assert.NotNull(context.Set<MaxLengthDataTypes>().SingleOrDefault(e => e.Id == 799 && e.String3 == shortString));
                Assert.NotNull(context.Set<MaxLengthDataTypes>().SingleOrDefault(e => e.Id == 799 && e.ByteArray5 == shortBinary));

                Assert.Equal(
                    "The ntext and image data types cannot be used in WHERE, HAVING, GROUP BY, ON, or IN clauses, except when these data types are used with the LIKE or IS NULL predicates.",
                    Assert.Throws<SqlCeException>(() => context.Set<MaxLengthDataTypes>().SingleOrDefault(e => e.Id == 799 && e.ByteArray9000 == longBinary)).Message);

                Assert.Equal(
                    "The ntext and image data types cannot be used in WHERE, HAVING, GROUP BY, ON, or IN clauses, except when these data types are used with the LIKE or IS NULL predicates.",
                    Assert.Throws<SqlCeException>(() => context.Set<MaxLengthDataTypes>().SingleOrDefault(e => e.Id == 799 && e.String9000 == longString)).Message);
            }
        }

        [Fact]
        public virtual void Can_insert_and_read_back_all_mapped_data_types()
        {
            using (var context = CreateContext())
            {
                context.Set<MappedDataTypes>().Add(
                    new MappedDataTypes
                        {
                            Int = 77,
                            Bigint = 78L,
                            Smallint = 79,
                            Tinyint = 80,
                            Bit = true,
                            Money = 81.1m,
                            Float = 83.3,
                            Real = 84.4f,
                            Datetime = new DateTime(2019, 1, 2, 14, 11, 12),
                            Nchar = "D",
                            National_character = "E",
                            Nvarchar = "F",
                            National_char_varying = "G",
                            National_character_varying = "H",
                            NvarcharMax = "don't",
                            National_char_varyingMax = "help",
                            National_character_varyingMax = "anyone!",
                            Ntext = "Gumball Rules!",
                            Binary = new byte[] { 86 },
                            Varbinary = new byte[] { 87 },
                            Image = new byte[] { 97, 98, 99, 100 },
                            Decimal = 101.1m,
                            Dec = 102.2m,
                            Numeric = 103.3m
                        });

                Assert.Equal(1, context.SaveChanges());
            }

            using (var context = CreateContext())
            {
                var entity = context.Set<MappedDataTypes>().Single(e => e.Int == 77);

                Assert.Equal(77, entity.Int);
                Assert.Equal(78, entity.Bigint);
                Assert.Equal(79, entity.Smallint);
                Assert.Equal(80, entity.Tinyint);
                Assert.Equal(true, entity.Bit);
                Assert.Equal(81.1m, entity.Money);
                Assert.Equal(83.3, entity.Float);
                Assert.Equal(84.4f, entity.Real);
                Assert.Equal(new DateTime(2019, 1, 2, 14, 11, 12), entity.Datetime);
                Assert.Equal("D", entity.Nchar);
                Assert.Equal("E", entity.National_character);
                Assert.Equal("F", entity.Nvarchar);
                Assert.Equal("G", entity.National_char_varying);
                Assert.Equal("H", entity.National_character_varying);
                Assert.Equal("don't", entity.NvarcharMax);
                Assert.Equal("help", entity.National_char_varyingMax);
                Assert.Equal("anyone!", entity.National_character_varyingMax);
                Assert.Equal("Gumball Rules!", entity.Ntext);
                Assert.Equal(new byte[] { 86 }, entity.Binary);
                Assert.Equal(new byte[] { 87 }, entity.Varbinary);
                Assert.Equal(new byte[] { 97, 98, 99, 100 }, entity.Image);
                Assert.Equal(101m, entity.Decimal);
                Assert.Equal(102m, entity.Dec);
                Assert.Equal(103m, entity.Numeric);
            }
        }

        [Fact]
        public virtual void Can_insert_and_read_back_all_mapped_nullable_data_types()
        {
            using (var context = CreateContext())
            {
                context.Set<MappedNullableDataTypes>().Add(
                    new MappedNullableDataTypes
                        {
                            Int = 77,
                            Bigint = 78L,
                            Smallint = 79,
                            Tinyint = 80,
                            Bit = true,
                            Money = 81.1m,
                            Float = 83.3,
                            Real = 84.4f,
                            Datetime = new DateTime(2019, 1, 2, 14, 11, 12),
                            Nchar = "D",
                            National_character = "E",
                            Nvarchar = "F",
                            National_char_varying = "G",
                            National_character_varying = "H",
                            NvarcharMax = "don't",
                            National_char_varyingMax = "help",
                            National_character_varyingMax = "anyone!",
                            Ntext = "Gumball Rules!",
                            Binary = new byte[] { 86 },
                            Varbinary = new byte[] { 87 },
                            Image = new byte[] { 97, 98, 99, 100 },
                            Decimal = 101.1m,
                            Dec = 102.2m,
                            Numeric = 103.3m
                        });

                Assert.Equal(1, context.SaveChanges());
            }

            using (var context = CreateContext())
            {
                var entity = context.Set<MappedNullableDataTypes>().Single(e => e.Int == 77);

                Assert.Equal(77, entity.Int);
                Assert.Equal(78, entity.Bigint);
                Assert.Equal(79, entity.Smallint.Value);
                Assert.Equal(80, entity.Tinyint.Value);
                Assert.Equal(true, entity.Bit);
                Assert.Equal(81.1m, entity.Money);
                Assert.Equal(83.3, entity.Float);
                Assert.Equal(84.4f, entity.Real);
                Assert.Equal(new DateTime(2019, 1, 2, 14, 11, 12), entity.Datetime);
                Assert.Equal("D", entity.Nchar);
                Assert.Equal("E", entity.National_character);
                Assert.Equal("F", entity.Nvarchar);
                Assert.Equal("G", entity.National_char_varying);
                Assert.Equal("H", entity.National_character_varying);
                Assert.Equal("don't", entity.NvarcharMax);
                Assert.Equal("help", entity.National_char_varyingMax);
                Assert.Equal("anyone!", entity.National_character_varyingMax);
                Assert.Equal("Gumball Rules!", entity.Ntext);
                Assert.Equal(new byte[] { 86 }, entity.Binary);
                Assert.Equal(new byte[] { 87 }, entity.Varbinary);
                Assert.Equal(new byte[] { 97, 98, 99, 100 }, entity.Image);
                Assert.Equal(101m, entity.Decimal);
                Assert.Equal(102m, entity.Dec);
                Assert.Equal(103m, entity.Numeric);
            }
        }

        [Fact]
        public virtual void Can_insert_and_read_back_all_mapped_data_types_set_to_null()
        {
            using (var context = CreateContext())
            {
                context.Set<MappedNullableDataTypes>().Add(
                    new MappedNullableDataTypes
                        {
                            Int = 78
                        });

                Assert.Equal(1, context.SaveChanges());
            }

            using (var context = CreateContext())
            {
                var entity = context.Set<MappedNullableDataTypes>().Single(e => e.Int == 78);

                Assert.Null(entity.Bigint);
                Assert.Null(entity.Smallint);
                Assert.Null(entity.Tinyint);
                Assert.Null(entity.Bit);
                Assert.Null(entity.Money);
                Assert.Null(entity.Float);
                Assert.Null(entity.Real);
                Assert.Null(entity.Datetime);
                Assert.Null(entity.Nchar);
                Assert.Null(entity.National_character);
                Assert.Null(entity.Nvarchar);
                Assert.Null(entity.National_char_varying);
                Assert.Null(entity.National_character_varying);
                Assert.Null(entity.NvarcharMax);
                Assert.Null(entity.National_char_varyingMax);
                Assert.Null(entity.National_character_varyingMax);
                Assert.Null(entity.Ntext);
                Assert.Null(entity.Binary);
                Assert.Null(entity.Varbinary);
                Assert.Null(entity.Image);
                Assert.Null(entity.Decimal);
                Assert.Null(entity.Dec);
                Assert.Null(entity.Numeric);
            }
        }

        [Fact]
        public virtual void Can_insert_and_read_back_all_mapped_sized_data_types()
        {
            using (var context = CreateContext())
            {
                context.Set<MappedSizedDataTypes>().Add(
                    new MappedSizedDataTypes
                    {
                        Id = 77,
                        Nchar = "Won",
                        National_character = "Squ",
                        Nvarchar = "Int",
                        National_char_varying = "The",
                        National_character_varying = "Col",
                        Binary = new byte[] { 10, 11, 12 },
                        Varbinary = new byte[] { 11, 12, 13 },
                    });

                Assert.Equal(1, context.SaveChanges());
            }

            using (var context = CreateContext())
            {
                var entity = context.Set<MappedSizedDataTypes>().Single(e => e.Id == 77);

                Assert.Equal("Won", entity.Nchar);
                Assert.Equal("Squ", entity.National_character);
                Assert.Equal("Int", entity.Nvarchar);
                Assert.Equal("The", entity.National_char_varying);
                Assert.Equal("Col", entity.National_character_varying);
                Assert.Equal(new byte[] { 10, 11, 12 }, entity.Binary);
                Assert.Equal(new byte[] { 11, 12, 13 }, entity.Varbinary);
            }
        }

        [Fact]
        public virtual void Can_insert_and_read_back_nulls_for_all_mapped_sized_data_types()
        {
            using (var context = CreateContext())
            {
                context.Set<MappedSizedDataTypes>().Add(
                    new MappedSizedDataTypes
                        {
                            Id = 78
                        });

                Assert.Equal(1, context.SaveChanges());
            }

            using (var context = CreateContext())
            {
                var entity = context.Set<MappedSizedDataTypes>().Single(e => e.Id == 78);

                Assert.Null(entity.Nchar);
                Assert.Null(entity.National_character);
                Assert.Null(entity.Nvarchar);
                Assert.Null(entity.National_char_varying);
                Assert.Null(entity.National_character_varying);
                Assert.Null(entity.Binary);
                Assert.Null(entity.Varbinary);
            }
        }

        [Fact]
        public virtual void Can_insert_and_read_back_all_mapped_data_types_with_scale()
        {
            using (var context = CreateContext())
            {
                context.Set<MappedScaledDataTypes>().Add(
                    new MappedScaledDataTypes
                        {
                            Id = 77,
                            Decimal = 101.1m,
                            Dec = 102.2m,
                            Numeric = 103.3m
                        });

                Assert.Equal(1, context.SaveChanges());
            }

            using (var context = CreateContext())
            {
                var entity = context.Set<MappedScaledDataTypes>().Single(e => e.Id == 77);

                Assert.Equal(101m, entity.Decimal);
                Assert.Equal(102m, entity.Dec);
                Assert.Equal(103m, entity.Numeric);
            }
        }

        [Fact]
        public virtual void Can_insert_and_read_back_all_mapped_data_types_with_precision_and_scale()
        {
            using (var context = CreateContext())
            {
                context.Set<MappedPrecisionAndScaledDataTypes>().Add(
                    new MappedPrecisionAndScaledDataTypes
                        {
                            Id = 77,
                            Decimal = 101.1m,
                            Dec = 102.2m,
                            Numeric = 103.3m
                        });

                Assert.Equal(1, context.SaveChanges());
            }

            using (var context = CreateContext())
            {
                var entity = context.Set<MappedPrecisionAndScaledDataTypes>().Single(e => e.Id == 77);

                Assert.Equal(101.1m, entity.Decimal);
                Assert.Equal(102.2m, entity.Dec);
                Assert.Equal(103.3m, entity.Numeric);
            }
        }

        [Fact]
        public virtual void Columns_have_expected_data_types()
        {
            const string query
                = @"SELECT 
                        TABLE_NAME, 
                        COLUMN_NAME, 
                        DATA_TYPE, 
                        IS_NULLABLE,
                        CHARACTER_MAXIMUM_LENGTH, 
                        NUMERIC_PRECISION, 
                        NUMERIC_SCALE, 
                        DATETIME_PRECISION 
                    FROM INFORMATION_SCHEMA.COLUMNS";

            var columns = new List<ColumnInfo>();

            using (var context = CreateContext())
            {
                var connection = context.Database.GetDbConnection();

                var command = connection.CreateCommand();
                command.CommandText = query;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var columnInfo = new ColumnInfo
                            {
                                TableName = reader.GetString(0),
                                ColumnName = reader.GetString(1),
                                DataType = reader.GetString(2),
                                IsNullable = reader.IsDBNull(3) ? null : (bool?)(reader.GetString(3) == "YES"),
                                MaxLength = reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4),
                                NumericPrecision = reader.IsDBNull(5) ? null : (int?)reader.GetInt16(5),
                                NumericScale = reader.IsDBNull(6) ? null : (int?)reader.GetInt16(6),
                                DateTimePrecision = null
                            };

                        columns.Add(columnInfo);
                    }
                }
            }

            var builder = new StringBuilder();

            foreach (var column in columns.OrderBy(e => e.TableName).ThenBy(e => e.ColumnName))
            {
                builder.Append(column.TableName);
                builder.Append(".");
                builder.Append(column.ColumnName);
                builder.Append(" ---> [");

                if (column.IsNullable == true)
                {
                    builder.Append("nullable ");
                }

                builder.Append(column.DataType);
                builder.Append("]");

                if (column.MaxLength.HasValue)
                {
                    builder.Append(" [MaxLength = ");
                    builder.Append(column.MaxLength);
                    builder.Append("]");
                }

                if (column.NumericPrecision.HasValue)
                {
                    builder.Append(" [Precision = ");
                    builder.Append(column.NumericPrecision);
                }

                if (column.DateTimePrecision.HasValue)
                {
                    builder.Append(" [Precision = ");
                    builder.Append(column.DateTimePrecision);
                }

                if (column.NumericScale.HasValue)
                {
                    builder.Append(" Scale = ");
                    builder.Append(column.NumericScale);
                }

                if (column.NumericPrecision.HasValue
                    || column.DateTimePrecision.HasValue
                    || column.NumericScale.HasValue)
                {
                    builder.Append("]");
                }

                builder.AppendLine();
            }

            var actual = builder.ToString();

            const string expected = @"BinaryForeignKeyDataType.BinaryKeyDataTypeId ---> [nullable varbinary] [MaxLength = 512]
BinaryForeignKeyDataType.Id ---> [int] [Precision = 10]
BinaryKeyDataType.Id ---> [varbinary] [MaxLength = 512]
BuiltInDataTypes.Enum16 ---> [smallint] [Precision = 5]
BuiltInDataTypes.Enum32 ---> [int] [Precision = 10]
BuiltInDataTypes.Enum64 ---> [bigint] [Precision = 19]
BuiltInDataTypes.Enum8 ---> [tinyint] [Precision = 3]
BuiltInDataTypes.Id ---> [int] [Precision = 10]
BuiltInDataTypes.PartitionId ---> [int] [Precision = 10]
BuiltInDataTypes.TestBoolean ---> [bit] [Precision = 1 Scale = 0]
BuiltInDataTypes.TestByte ---> [tinyint] [Precision = 3]
BuiltInDataTypes.TestDateTime ---> [datetime] [Precision = 23 Scale = 3]
BuiltInDataTypes.TestDecimal ---> [numeric] [Precision = 18 Scale = 2]
BuiltInDataTypes.TestDouble ---> [float] [Precision = 53]
BuiltInDataTypes.TestInt16 ---> [smallint] [Precision = 5]
BuiltInDataTypes.TestInt32 ---> [int] [Precision = 10]
BuiltInDataTypes.TestInt64 ---> [bigint] [Precision = 19]
BuiltInDataTypes.TestSingle ---> [real] [Precision = 24]
BuiltInNullableDataTypes.Enum16 ---> [nullable smallint] [Precision = 5]
BuiltInNullableDataTypes.Enum32 ---> [nullable int] [Precision = 10]
BuiltInNullableDataTypes.Enum64 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.Enum8 ---> [nullable tinyint] [Precision = 3]
BuiltInNullableDataTypes.Id ---> [int] [Precision = 10]
BuiltInNullableDataTypes.PartitionId ---> [int] [Precision = 10]
BuiltInNullableDataTypes.TestByteArray ---> [nullable image] [MaxLength = 1073741823]
BuiltInNullableDataTypes.TestNullableBoolean ---> [nullable bit] [Precision = 1 Scale = 0]
BuiltInNullableDataTypes.TestNullableByte ---> [nullable tinyint] [Precision = 3]
BuiltInNullableDataTypes.TestNullableDateTime ---> [nullable datetime] [Precision = 23 Scale = 3]
BuiltInNullableDataTypes.TestNullableDecimal ---> [nullable numeric] [Precision = 18 Scale = 2]
BuiltInNullableDataTypes.TestNullableDouble ---> [nullable float] [Precision = 53]
BuiltInNullableDataTypes.TestNullableInt16 ---> [nullable smallint] [Precision = 5]
BuiltInNullableDataTypes.TestNullableInt32 ---> [nullable int] [Precision = 10]
BuiltInNullableDataTypes.TestNullableInt64 ---> [nullable bigint] [Precision = 19]
BuiltInNullableDataTypes.TestNullableSingle ---> [nullable real] [Precision = 24]
BuiltInNullableDataTypes.TestString ---> [nullable nvarchar] [MaxLength = 4000]
MappedDataTypes.Bigint ---> [bigint] [Precision = 19]
MappedDataTypes.Binary ---> [binary] [MaxLength = 1]
MappedDataTypes.Bit ---> [bit] [Precision = 1 Scale = 0]
MappedDataTypes.Datetime ---> [datetime] [Precision = 23 Scale = 3]
MappedDataTypes.Dec ---> [numeric] [Precision = 18 Scale = 0]
MappedDataTypes.Decimal ---> [numeric] [Precision = 18 Scale = 0]
MappedDataTypes.Float ---> [float] [Precision = 53]
MappedDataTypes.Image ---> [image] [MaxLength = 1073741823]
MappedDataTypes.Int ---> [int] [Precision = 10]
MappedDataTypes.Money ---> [money] [Precision = 19 Scale = 4]
MappedDataTypes.National_char_varying ---> [nvarchar] [MaxLength = 1]
MappedDataTypes.National_char_varyingMax ---> [ntext] [MaxLength = 536870911]
MappedDataTypes.National_character ---> [nchar] [MaxLength = 1]
MappedDataTypes.National_character_varying ---> [nvarchar] [MaxLength = 1]
MappedDataTypes.National_character_varyingMax ---> [ntext] [MaxLength = 536870911]
MappedDataTypes.Nchar ---> [nchar] [MaxLength = 1]
MappedDataTypes.Ntext ---> [ntext] [MaxLength = 536870911]
MappedDataTypes.Numeric ---> [numeric] [Precision = 18 Scale = 0]
MappedDataTypes.Nvarchar ---> [nvarchar] [MaxLength = 1]
MappedDataTypes.NvarcharMax ---> [ntext] [MaxLength = 536870911]
MappedDataTypes.Real ---> [real] [Precision = 24]
MappedDataTypes.Smallint ---> [smallint] [Precision = 5]
MappedDataTypes.Tinyint ---> [tinyint] [Precision = 3]
MappedDataTypes.Varbinary ---> [varbinary] [MaxLength = 1]
MappedNullableDataTypes.Bigint ---> [nullable bigint] [Precision = 19]
MappedNullableDataTypes.Binary ---> [nullable binary] [MaxLength = 1]
MappedNullableDataTypes.Bit ---> [nullable bit] [Precision = 1 Scale = 0]
MappedNullableDataTypes.Datetime ---> [nullable datetime] [Precision = 23 Scale = 3]
MappedNullableDataTypes.Dec ---> [nullable numeric] [Precision = 18 Scale = 0]
MappedNullableDataTypes.Decimal ---> [nullable numeric] [Precision = 18 Scale = 0]
MappedNullableDataTypes.Float ---> [nullable float] [Precision = 53]
MappedNullableDataTypes.Image ---> [nullable image] [MaxLength = 1073741823]
MappedNullableDataTypes.Int ---> [int] [Precision = 10]
MappedNullableDataTypes.Money ---> [nullable money] [Precision = 19 Scale = 4]
MappedNullableDataTypes.National_char_varying ---> [nullable nvarchar] [MaxLength = 1]
MappedNullableDataTypes.National_char_varyingMax ---> [nullable ntext] [MaxLength = 536870911]
MappedNullableDataTypes.National_character ---> [nullable nchar] [MaxLength = 1]
MappedNullableDataTypes.National_character_varying ---> [nullable nvarchar] [MaxLength = 1]
MappedNullableDataTypes.National_character_varyingMax ---> [nullable ntext] [MaxLength = 536870911]
MappedNullableDataTypes.Nchar ---> [nullable nchar] [MaxLength = 1]
MappedNullableDataTypes.Ntext ---> [nullable ntext] [MaxLength = 536870911]
MappedNullableDataTypes.Numeric ---> [nullable numeric] [Precision = 18 Scale = 0]
MappedNullableDataTypes.Nvarchar ---> [nullable nvarchar] [MaxLength = 1]
MappedNullableDataTypes.NvarcharMax ---> [nullable ntext] [MaxLength = 536870911]
MappedNullableDataTypes.Real ---> [nullable real] [Precision = 24]
MappedNullableDataTypes.Smallint ---> [nullable smallint] [Precision = 5]
MappedNullableDataTypes.Tinyint ---> [nullable tinyint] [Precision = 3]
MappedNullableDataTypes.Varbinary ---> [nullable varbinary] [MaxLength = 1]
MappedPrecisionAndScaledDataTypes.Dec ---> [numeric] [Precision = 5 Scale = 2]
MappedPrecisionAndScaledDataTypes.Decimal ---> [numeric] [Precision = 5 Scale = 2]
MappedPrecisionAndScaledDataTypes.Id ---> [int] [Precision = 10]
MappedPrecisionAndScaledDataTypes.Numeric ---> [numeric] [Precision = 5 Scale = 2]
MappedScaledDataTypes.Dec ---> [numeric] [Precision = 3 Scale = 0]
MappedScaledDataTypes.Decimal ---> [numeric] [Precision = 3 Scale = 0]
MappedScaledDataTypes.Id ---> [int] [Precision = 10]
MappedScaledDataTypes.Numeric ---> [numeric] [Precision = 3 Scale = 0]
MappedSizedDataTypes.Binary ---> [nullable binary] [MaxLength = 3]
MappedSizedDataTypes.Id ---> [int] [Precision = 10]
MappedSizedDataTypes.National_char_varying ---> [nullable nvarchar] [MaxLength = 3]
MappedSizedDataTypes.National_character ---> [nullable nchar] [MaxLength = 3]
MappedSizedDataTypes.National_character_varying ---> [nullable nvarchar] [MaxLength = 3]
MappedSizedDataTypes.Nchar ---> [nullable nchar] [MaxLength = 3]
MappedSizedDataTypes.Nvarchar ---> [nullable nvarchar] [MaxLength = 3]
MappedSizedDataTypes.Varbinary ---> [nullable varbinary] [MaxLength = 3]
MaxLengthDataTypes.ByteArray5 ---> [nullable varbinary] [MaxLength = 5]
MaxLengthDataTypes.ByteArray9000 ---> [nullable image] [MaxLength = 1073741823]
MaxLengthDataTypes.Id ---> [int] [Precision = 10]
MaxLengthDataTypes.String3 ---> [nullable nvarchar] [MaxLength = 3]
MaxLengthDataTypes.String9000 ---> [nullable ntext] [MaxLength = 536870911]
StringForeignKeyDataType.Id ---> [int] [Precision = 10]
StringForeignKeyDataType.StringKeyDataTypeId ---> [nullable nvarchar] [MaxLength = 256]
StringKeyDataType.Id ---> [nvarchar] [MaxLength = 256]
";

            Assert.Equal(expected, actual);
        }

        private class ColumnInfo
        {
            public string TableName { get; set; }
            public string ColumnName { get; set; }
            public string DataType { get; set; }
            public bool? IsNullable { get; set; }
            public int? MaxLength { get; set; }
            public int? NumericPrecision { get; set; }
            public int? NumericScale { get; set; }
            public int? DateTimePrecision { get; set; }
        }
    }
}
