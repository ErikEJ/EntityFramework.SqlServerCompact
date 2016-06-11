using Xunit;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class DataAnnotationSqlCeTest : DataAnnotationTestBase<SqlCeTestStore, DataAnnotationSqlCeFixture>
    {
        public DataAnnotationSqlCeTest(DataAnnotationSqlCeFixture fixture)
            : base(fixture)
        {
        }

        public override void ConcurrencyCheckAttribute_throws_if_value_in_database_changed()
        {
            base.ConcurrencyCheckAttribute_throws_if_value_in_database_changed();

            Assert.Equal(@"SELECT TOP(1) [r].[UniqueNo], [r].[MaxLengthProperty], [r].[Name], [r].[RowVersion]
FROM [Sample] AS [r]
WHERE [r].[UniqueNo] = 1

SELECT TOP(1) [r].[UniqueNo], [r].[MaxLengthProperty], [r].[Name], [r].[RowVersion]
FROM [Sample] AS [r]
WHERE [r].[UniqueNo] = 1

@p2: 1
@p0: ModifiedData (Nullable = false) (Size = 4000)
@p1: 00000000-0000-0000-0003-000000000001
@p3: 00000001-0000-0000-0000-000000000001

UPDATE [Sample] SET [Name] = @p0, [RowVersion] = @p1
WHERE [UniqueNo] = @p2 AND [RowVersion] = @p3

@p2: 1
@p0: ChangedData (Nullable = false) (Size = 4000)
@p1: 00000000-0000-0000-0002-000000000001
@p3: 00000001-0000-0000-0000-000000000001

UPDATE [Sample] SET [Name] = @p0, [RowVersion] = @p1
WHERE [UniqueNo] = @p2 AND [RowVersion] = @p3", Sql);
        }

        public override void DatabaseGeneratedAttribute_autogenerates_values_when_set_to_identity()
        {
            base.DatabaseGeneratedAttribute_autogenerates_values_when_set_to_identity();
            Assert.Equal(@"@p0:  (Size = 10) (DbType = String)
@p1: Third (Nullable = false) (Size = 4000)
@p2: 00000000-0000-0000-0000-000000000003

INSERT INTO [Sample] ([MaxLengthProperty], [Name], [RowVersion])
VALUES (@p0, @p1, @p2)

@p0:  (Size = 10) (DbType = String)
@p1: Third (Nullable = false) (Size = 4000)
@p2: 00000000-0000-0000-0000-000000000003

SELECT [UniqueNo]
FROM [Sample]
WHERE 1 = 1 AND [UniqueNo] = CAST (@@IDENTITY AS int)",
                Sql);
        }

        public override void MaxLengthAttribute_throws_while_inserting_value_longer_than_max_length()
        {
            base.MaxLengthAttribute_throws_while_inserting_value_longer_than_max_length();

            Assert.Equal(@"@p0: Short (Size = 10)
@p1: ValidString (Nullable = false) (Size = 4000)
@p2: 00000000-0000-0000-0000-000000000001

INSERT INTO [Sample] ([MaxLengthProperty], [Name], [RowVersion])
VALUES (@p0, @p1, @p2)

@p0: Short (Size = 10)
@p1: ValidString (Nullable = false) (Size = 4000)
@p2: 00000000-0000-0000-0000-000000000001

SELECT [UniqueNo]
FROM [Sample]
WHERE 1 = 1 AND [UniqueNo] = CAST (@@IDENTITY AS int)

",
                Sql);
        }

        public override void RequiredAttribute_for_navigation_throws_while_inserting_null_value()
        {
            base.RequiredAttribute_for_navigation_throws_while_inserting_null_value();

            Assert.Equal(@"@p0:  (DbType = Int32)
@p1: Book1 (Nullable = false) (Size = 256)

INSERT INTO [BookDetail] ([AdditionalBookDetailId], [BookId])
VALUES (@p0, @p1)

@p0:  (DbType = Int32)
@p1: Book1 (Nullable = false) (Size = 256)

SELECT [Id]
FROM [BookDetail]
WHERE 1 = 1 AND [Id] = CAST (@@IDENTITY AS int)

",
                Sql);
        }

        public override void RequiredAttribute_for_property_throws_while_inserting_null_value()
        {
            base.RequiredAttribute_for_property_throws_while_inserting_null_value();

            Assert.Equal(@"@p0:  (Size = 10) (DbType = String)
@p1: ValidString (Nullable = false) (Size = 4000)
@p2: 00000000-0000-0000-0000-000000000001

INSERT INTO [Sample] ([MaxLengthProperty], [Name], [RowVersion])
VALUES (@p0, @p1, @p2)

@p0:  (Size = 10) (DbType = String)
@p1: ValidString (Nullable = false) (Size = 4000)
@p2: 00000000-0000-0000-0000-000000000001

SELECT [UniqueNo]
FROM [Sample]
WHERE 1 = 1 AND [UniqueNo] = CAST (@@IDENTITY AS int)

",
                Sql);
        }


        public override void StringLengthAttribute_throws_while_inserting_value_longer_than_max_length()
        {
            TestSqlLoggerFactory.Reset();
            base.StringLengthAttribute_throws_while_inserting_value_longer_than_max_length();

            Assert.Equal(@"@p0: ValidString (Size = 16)

INSERT INTO [Two] ([Data])
VALUES (@p0)

@p0: ValidString (Size = 16)

SELECT [Id], [Timestamp]
FROM [Two]
WHERE 1 = 1 AND [Id] = CAST (@@IDENTITY AS int)

",
                Sql);
        }

        public override void TimestampAttribute_throws_if_value_in_database_changed()
        {
            base.TimestampAttribute_throws_if_value_in_database_changed();

            Assert.Equal(@"SELECT TOP(1) [r].[Id], [r].[Data], [r].[Timestamp]
FROM [Two] AS [r]
WHERE [r].[Id] = 1

SELECT TOP(1) [r].[Id], [r].[Data], [r].[Timestamp]
FROM [Two] AS [r]
WHERE [r].[Id] = 1

@p1: 1
@p0: ModifiedData (Size = 16)
@p2: 0x0000000000000044 (Size = 8)

UPDATE [Two] SET [Data] = @p0
WHERE [Id] = @p1 AND [Timestamp] = @p2

@p1: 1
@p0: ModifiedData (Size = 16)
@p2: 0x0000000000000044 (Size = 8)

SELECT [Timestamp]
FROM [Two]
WHERE 1 = 1 AND [Id] = @p1

@p1: 1
@p0: ChangedData (Size = 16)
@p2: 0x0000000000000044 (Size = 8)

UPDATE [Two] SET [Data] = @p0
WHERE [Id] = @p1 AND [Timestamp] = @p2

@p1: 1
@p0: ChangedData (Size = 16)
@p2: 0x0000000000000044 (Size = 8)

SELECT [Timestamp]
FROM [Two]
WHERE 1 = 1 AND [Id] = @p1",
                Sql);
        }

        private static string Sql => TestSqlLoggerFactory.Sql;
    }
}
