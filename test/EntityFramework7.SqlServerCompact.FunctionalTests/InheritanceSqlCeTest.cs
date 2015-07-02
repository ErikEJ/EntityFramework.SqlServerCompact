﻿using Microsoft.Data.Entity.FunctionalTests;
using Xunit;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class InheritanceSqlCeTest : InheritanceTestBase<InheritanceSqlCeFixture>
    {
        public override void Can_use_of_type_animal()
        {
            base.Can_use_of_type_animal();

            Assert.Equal(
                @"SELECT [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
WHERE [a].[Discriminator] IN ('Kiwi', 'Eagle')
ORDER BY [a].[Species]",
                Sql);
        }

        public override void Can_use_of_type_bird()
        {
            base.Can_use_of_type_bird();

            Assert.Equal(
                @"SELECT [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
WHERE [a].[Discriminator] IN ('Kiwi', 'Eagle')
ORDER BY [a].[Species]",
                Sql);
        }

        public override void Can_use_of_type_bird_first()
        {
            base.Can_use_of_type_bird_first();

            Assert.Equal(
                @"SELECT TOP(1) [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
WHERE [a].[Discriminator] IN ('Kiwi', 'Eagle')
ORDER BY [a].[Species]",
                Sql);
        }

        public override void Can_use_of_type_kiwi()
        {
            base.Can_use_of_type_kiwi();

            Assert.Equal(
                @"SELECT [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
WHERE [a].[Discriminator] = 'Kiwi'",
                Sql);
        }

        public override void Can_query_all_animals()
        {
            base.Can_query_all_animals();

            Assert.Equal(
                @"SELECT [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
WHERE [a].[Discriminator] IN ('Kiwi', 'Eagle')
ORDER BY [a].[Species]",
                Sql);
        }

        public override void Can_filter_all_animals()
        {
            base.Can_filter_all_animals();

            Assert.Equal(
                @"SELECT [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
WHERE ([a].[Discriminator] IN ('Kiwi', 'Eagle') AND [a].[Name] = 'Great spotted kiwi')
ORDER BY [a].[Species]",
                Sql);
        }

        public override void Can_query_all_birds()
        {
            base.Can_query_all_birds();

            Assert.Equal(
                @"SELECT [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
WHERE [a].[Discriminator] IN ('Kiwi', 'Eagle')
ORDER BY [a].[Species]",
                Sql);
        }

        public override void Can_query_just_kiwis()
        {
            base.Can_query_just_kiwis();

            Assert.Equal(
                @"SELECT TOP(2) [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[FoundOn]
FROM [Animal] AS [a]
WHERE [a].[Discriminator] = 'Kiwi'",
                Sql);
        }

        public override void Can_include_prey()
        {
            base.Can_include_prey();

            Assert.Equal(
                @"SELECT TOP(2) [e].[Species], [e].[CountryId], [e].[Discriminator], [e].[Name], [e].[EagleId], [e].[IsFlightless], [e].[Group]
FROM [Animal] AS [e]
WHERE [e].[Discriminator] = 'Eagle'
ORDER BY [e].[Species]

SELECT [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
INNER JOIN (
    SELECT DISTINCT TOP(2) [e].[Species]
    FROM [Animal] AS [e]
    WHERE [e].[Discriminator] = 'Eagle'
) AS [e] ON [a].[EagleId] = [e].[Species]
WHERE ([a].[Discriminator] = 'Kiwi' OR [a].[Discriminator] = 'Eagle')
ORDER BY [e].[Species]",
                Sql);
        }

        public override void Can_include_animals()
        {
            base.Can_include_animals();

            Assert.Equal(
                @"SELECT [c].[Id], [c].[Name]
FROM [Country] AS [c]
ORDER BY [c].[Name], [c].[Id]

SELECT [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
INNER JOIN (
    SELECT DISTINCT [c].[Name], [c].[Id]
    FROM [Country] AS [c]
) AS [c] ON [a].[CountryId] = [c].[Id]
WHERE ([a].[Discriminator] = 'Kiwi' OR [a].[Discriminator] = 'Eagle')
ORDER BY [c].[Name], [c].[Id]",
                Sql);
        }

        public override void Can_insert_update_delete()
        {
            base.Can_insert_update_delete();

            Assert.Equal(
                @"SELECT TOP(2) [c].[Id], [c].[Name]
FROM [Country] AS [c]
WHERE [c].[Id] = 1

@p0: Apteryx owenii
@p1: 1
@p2: Kiwi
@p3: Little spotted kiwi
@p4: 
@p5: True
@p6: North

INSERT INTO [Animal] ([Species], [CountryId], [Discriminator], [Name], [EagleId], [IsFlightless], [FoundOn])
VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6);

SELECT TOP(2) [k].[Species], [k].[CountryId], [k].[Discriminator], [k].[Name], [k].[EagleId], [k].[IsFlightless], [k].[FoundOn]
FROM [Animal] AS [k]
WHERE ([k].[Discriminator] = 'Kiwi' AND [k].[Species] LIKE '%' + 'owenii')

@p1: Apteryx owenii
@p0: Aquila chrysaetos canadensis

UPDATE [Animal] SET [EagleId] = @p0
WHERE [Species] = @p1;

SELECT TOP(2) [k].[Species], [k].[CountryId], [k].[Discriminator], [k].[Name], [k].[EagleId], [k].[IsFlightless], [k].[FoundOn]
FROM [Animal] AS [k]
WHERE ([k].[Discriminator] = 'Kiwi' AND [k].[Species] LIKE '%' + 'owenii')

@p0: Apteryx owenii

DELETE FROM [Animal]
WHERE [Species] = @p0;

SELECT COUNT(*)
FROM [Animal] AS [k]
WHERE ([k].[Discriminator] = 'Kiwi' AND [k].[Species] LIKE '%' + 'owenii')",
                Sql);
        }

        public InheritanceSqlCeTest(InheritanceSqlCeFixture fixture)
            : base(fixture)
        {
            TestSqlLoggerFactory.Reset();
        }

        private static string Sql => TestSqlLoggerFactory.Sql;
    }
}
