using Xunit;

namespace Microsoft.Data.Entity.FunctionalTests
{
    public class DataAnnotationSqlCeTest : DataAnnotationTestBase<SqlCeTestStore, DataAnnotationSqlCeFixture>
    {
        public DataAnnotationSqlCeTest(DataAnnotationSqlCeFixture fixture)
            : base(fixture)
        {
        }

        //TODO ErikEJ Fix broken logging after error due to new command handling
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
@p0: ModifiedData
@p1: 00000000-0000-0000-0003-000000000001
@p3: 00000001-0000-0000-0000-000000000001

UPDATE [Sample] SET [Name] = @p0, [RowVersion] = @p1
WHERE [UniqueNo] = @p2 AND [RowVersion] = @p3;

@p2: 1
@p0: ChangedData
@p1: 00000000-0000-0000-0002-000000000001
@p3: 00000001-0000-0000-0000-000000000001

UPDATE [Sample] SET [Name] = @p0, [RowVersion] = @p1
WHERE [UniqueNo] = @p2 AND [RowVersion] = @p3;", Sql);
        }

        public override void DatabaseGeneratedAttribute_autogenerates_values_when_set_to_identity()
        {
            base.DatabaseGeneratedAttribute_autogenerates_values_when_set_to_identity();
            Assert.Equal(@"@p0: 
@p1: Third
@p2: 00000000-0000-0000-0000-000000000003

INSERT INTO [Sample] ([MaxLengthProperty], [Name], [RowVersion])
VALUES (@p0, @p1, @p2);

@p0: 
@p1: Third
@p2: 00000000-0000-0000-0000-000000000003

SELECT [UniqueNo]
FROM [Sample]
WHERE 1 = 1 AND [UniqueNo] = CAST (@@IDENTITY AS int);",
                Sql);
        }

        public override void MaxLengthAttribute_throws_while_inserting_value_longer_than_max_length()
        {
            base.MaxLengthAttribute_throws_while_inserting_value_longer_than_max_length();

            Assert.Equal(@"@p0: Short
@p1: ValidString
@p2: 00000000-0000-0000-0000-000000000001

INSERT INTO [Sample] ([MaxLengthProperty], [Name], [RowVersion])
VALUES (@p0, @p1, @p2);

@p0: Short
@p1: ValidString
@p2: 00000000-0000-0000-0000-000000000001

SELECT [UniqueNo]
FROM [Sample]
WHERE 1 = 1 AND [UniqueNo] = CAST (@@IDENTITY AS int);

",
                Sql);
        }

        public override void RequiredAttribute_for_navigation_throws_while_inserting_null_value()
        {
            base.RequiredAttribute_for_navigation_throws_while_inserting_null_value();

            Assert.Equal(@"@p0: 0
@p1: Book1

INSERT INTO [BookDetail] ([Id], [BookId])
VALUES (@p0, @p1);

",
                Sql);
        }

        public override void RequiredAttribute_for_property_throws_while_inserting_null_value()
        {
            base.RequiredAttribute_for_property_throws_while_inserting_null_value();

            Assert.Equal(@"@p0: 
@p1: ValidString
@p2: 00000000-0000-0000-0000-000000000001

INSERT INTO [Sample] ([MaxLengthProperty], [Name], [RowVersion])
VALUES (@p0, @p1, @p2);

@p0: 
@p1: ValidString
@p2: 00000000-0000-0000-0000-000000000001

SELECT [UniqueNo]
FROM [Sample]
WHERE 1 = 1 AND [UniqueNo] = CAST (@@IDENTITY AS int);

",
                Sql);
        }


        public override void StringLengthAttribute_throws_while_inserting_value_longer_than_max_length()
        {
            TestSqlLoggerFactory.SqlStatements.Clear();
            base.StringLengthAttribute_throws_while_inserting_value_longer_than_max_length();

            Assert.Equal(@"@p0: ValidString

INSERT INTO [Two] ([Data])
VALUES (@p0);

@p0: ValidString

SELECT [Id], [Timestamp]
FROM [Two]
WHERE 1 = 1 AND [Id] = CAST (@@IDENTITY AS int);

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
@p0: ModifiedData
@p2: System.Byte[]

UPDATE [Two] SET [Data] = @p0
WHERE [Id] = @p1 AND [Timestamp] = @p2;

@p1: 1
@p0: ModifiedData
@p2: System.Byte[]

SELECT [Timestamp]
FROM [Two]
WHERE 1 = 1 AND [Id] = @p1;

@p1: 1
@p0: ChangedData
@p2: System.Byte[]

UPDATE [Two] SET [Data] = @p0
WHERE [Id] = @p1 AND [Timestamp] = @p2;

@p1: 1
@p0: ChangedData
@p2: System.Byte[]

SELECT [Timestamp]
FROM [Two]
WHERE 1 = 1 AND [Id] = @p1;",
                Sql);
        }

        private static string Sql => TestSqlLoggerFactory.Sql;
    }
}
