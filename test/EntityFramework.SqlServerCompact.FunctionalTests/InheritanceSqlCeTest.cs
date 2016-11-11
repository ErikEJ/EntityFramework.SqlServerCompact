﻿using Xunit;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class InheritanceSqlCeTest : InheritanceTestBase<InheritanceSqlCeFixture>
    {
        public override void Can_use_of_type_animal()
        {
            base.Can_use_of_type_animal();

            Assert.Equal(
                @"SELECT [t].[Species], [t].[CountryId], [t].[Discriminator], [t].[Name], [t].[EagleId], [t].[IsFlightless], [t].[Group], [t].[FoundOn]
FROM (
    SELECT [a0].[Species], [a0].[CountryId], [a0].[Discriminator], [a0].[Name], [a0].[EagleId], [a0].[IsFlightless], [a0].[Group], [a0].[FoundOn]
    FROM [Animal] AS [a0]
    WHERE [a0].[Discriminator] IN (N'Kiwi', N'Eagle')
) AS [t]
ORDER BY [t].[Species]",
                Sql);
        }

        public override void Can_use_of_type_bird()
        {
            base.Can_use_of_type_bird();

            Assert.Equal(
                @"SELECT [t].[Species], [t].[CountryId], [t].[Discriminator], [t].[Name], [t].[EagleId], [t].[IsFlightless], [t].[Group], [t].[FoundOn]
FROM (
    SELECT [a0].[Species], [a0].[CountryId], [a0].[Discriminator], [a0].[Name], [a0].[EagleId], [a0].[IsFlightless], [a0].[Group], [a0].[FoundOn]
    FROM [Animal] AS [a0]
    WHERE [a0].[Discriminator] IN (N'Kiwi', N'Eagle')
) AS [t]
ORDER BY [t].[Species]",
                Sql);
        }

        public override void Can_use_of_type_bird_predicate()
        {
            base.Can_use_of_type_bird_predicate();

            Assert.Equal(
                @"SELECT [t].[Species], [t].[CountryId], [t].[Discriminator], [t].[Name], [t].[EagleId], [t].[IsFlightless], [t].[Group], [t].[FoundOn]
FROM (
    SELECT [a0].[Species], [a0].[CountryId], [a0].[Discriminator], [a0].[Name], [a0].[EagleId], [a0].[IsFlightless], [a0].[Group], [a0].[FoundOn]
    FROM [Animal] AS [a0]
    WHERE [a0].[Discriminator] IN (N'Kiwi', N'Eagle') AND ([a0].[CountryId] = 1)
) AS [t]
ORDER BY [t].[Species]",
                Sql);
        }

        public override void Can_use_of_type_bird_with_projection()
        {
            base.Can_use_of_type_bird_with_projection();

            Assert.Equal(
                @"SELECT [t].[EagleId]
FROM (
    SELECT [a0].[Species], [a0].[CountryId], [a0].[Discriminator], [a0].[Name], [a0].[EagleId], [a0].[IsFlightless], [a0].[Group], [a0].[FoundOn]
    FROM [Animal] AS [a0]
    WHERE [a0].[Discriminator] IN (N'Kiwi', N'Eagle')
) AS [t]",
                Sql);
        }

        public override void Can_use_of_type_bird_first()
        {
            base.Can_use_of_type_bird_first();

            Assert.Equal(
                @"SELECT TOP(1) [t].[Species], [t].[CountryId], [t].[Discriminator], [t].[Name], [t].[EagleId], [t].[IsFlightless], [t].[Group], [t].[FoundOn]
FROM (
    SELECT [a0].[Species], [a0].[CountryId], [a0].[Discriminator], [a0].[Name], [a0].[EagleId], [a0].[IsFlightless], [a0].[Group], [a0].[FoundOn]
    FROM [Animal] AS [a0]
    WHERE [a0].[Discriminator] IN (N'Kiwi', N'Eagle')
) AS [t]
ORDER BY [t].[Species]",
                Sql);
        }

        public override void Can_use_of_type_kiwi()
        {
            base.Can_use_of_type_kiwi();

            Assert.Equal(
                @"SELECT [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
WHERE [a].[Discriminator] = N'Kiwi'",
                Sql);
        }

        public override void Can_use_of_type_rose()
        {
            base.Can_use_of_type_rose();

            Assert.Equal(
                @"SELECT [p].[Species], [p].[CountryId], [p].[Genus], [p].[Name], [p].[HasThorns]
FROM [Plant] AS [p]
WHERE [p].[Genus] = 0",
                Sql);
        }

        public override void Can_query_all_animals()
        {
            base.Can_query_all_animals();

            Assert.Equal(
                @"SELECT [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
WHERE [a].[Discriminator] IN (N'Kiwi', N'Eagle')
ORDER BY [a].[Species]",
                Sql);
        }

        public override void Can_query_all_plants()
        {
            base.Can_query_all_plants();

            Assert.Equal(
                @"SELECT [a].[Species], [a].[CountryId], [a].[Genus], [a].[Name], [a].[HasThorns]
FROM [Plant] AS [a]
WHERE [a].[Genus] IN (0, 1)
ORDER BY [a].[Species]",
                Sql);
        }

        public override void Can_filter_all_animals()
        {
            base.Can_filter_all_animals();

            Assert.Equal(
                @"SELECT [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
WHERE [a].[Discriminator] IN (N'Kiwi', N'Eagle') AND ([a].[Name] = N'Great spotted kiwi')
ORDER BY [a].[Species]",
                Sql);
        }

        public override void Can_query_all_birds()
        {
            base.Can_query_all_birds();

            Assert.Equal(
                @"SELECT [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
WHERE [a].[Discriminator] IN (N'Kiwi', N'Eagle')
ORDER BY [a].[Species]",
                Sql);
        }

        public override void Can_query_just_kiwis()
        {
            base.Can_query_just_kiwis();

            Assert.Equal(
                @"SELECT TOP(2) [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[FoundOn]
FROM [Animal] AS [a]
WHERE [a].[Discriminator] = N'Kiwi'",
                Sql);
        }

        public override void Can_query_just_roses()
        {
            base.Can_query_just_roses();

            Assert.Equal(
                @"SELECT TOP(2) [p].[Species], [p].[CountryId], [p].[Genus], [p].[Name], [p].[HasThorns]
FROM [Plant] AS [p]
WHERE [p].[Genus] = 0",
                Sql
                );
        }

        public override void Can_include_prey()
        {
            base.Can_include_prey();

            Assert.Equal(
                @"SELECT TOP(2) [e].[Species], [e].[CountryId], [e].[Discriminator], [e].[Name], [e].[EagleId], [e].[IsFlightless], [e].[Group]
FROM [Animal] AS [e]
WHERE [e].[Discriminator] = N'Eagle'
ORDER BY [e].[Species]

SELECT [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
INNER JOIN (
    SELECT DISTINCT TOP(2) [e].[Species]
    FROM [Animal] AS [e]
    WHERE [e].[Discriminator] = N'Eagle'
    ORDER BY [e].[Species]
) AS [e0] ON [a].[EagleId] = [e0].[Species]
WHERE [a].[Discriminator] IN (N'Kiwi', N'Eagle')
ORDER BY [e0].[Species]",
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
) AS [c0] ON [a].[CountryId] = [c0].[Id]
WHERE [a].[Discriminator] IN (N'Kiwi', N'Eagle')
ORDER BY [c0].[Name], [c0].[Id]",
                Sql);
        }

        public override void Can_insert_update_delete()
        {
            base.Can_insert_update_delete();

            Assert.Equal(
                @"SELECT TOP(2) [c].[Id], [c].[Name]
FROM [Country] AS [c]
WHERE [c].[Id] = 1

@p0: Apteryx owenii (Nullable = false) (Size = 100)
@p1: 1
@p2: Kiwi (Nullable = false) (Size = 4000)
@p3: Little spotted kiwi (Size = 4000)
@p4:  (Size = 100) (DbType = String)
@p5: True
@p6: North

INSERT INTO [Animal] ([Species], [CountryId], [Discriminator], [Name], [EagleId], [IsFlightless], [FoundOn])
VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6)

SELECT TOP(2) [k].[Species], [k].[CountryId], [k].[Discriminator], [k].[Name], [k].[EagleId], [k].[IsFlightless], [k].[FoundOn]
FROM [Animal] AS [k]
WHERE ([k].[Discriminator] = N'Kiwi') AND (SUBSTRING([k].[Species], (LEN([k].[Species]) + 1) - LEN(N'owenii'), LEN(N'owenii')) = N'owenii')

@p1: Apteryx owenii (Nullable = false) (Size = 100)
@p0: Aquila chrysaetos canadensis (Size = 100)

UPDATE [Animal] SET [EagleId] = @p0
WHERE [Species] = @p1

SELECT TOP(2) [k].[Species], [k].[CountryId], [k].[Discriminator], [k].[Name], [k].[EagleId], [k].[IsFlightless], [k].[FoundOn]
FROM [Animal] AS [k]
WHERE ([k].[Discriminator] = N'Kiwi') AND (SUBSTRING([k].[Species], (LEN([k].[Species]) + 1) - LEN(N'owenii'), LEN(N'owenii')) = N'owenii')

@p0: Apteryx owenii (Nullable = false) (Size = 100)

DELETE FROM [Animal]
WHERE [Species] = @p0

SELECT COUNT(*)
FROM [Animal] AS [k]
WHERE ([k].[Discriminator] = N'Kiwi') AND (SUBSTRING([k].[Species], (LEN([k].[Species]) + 1) - LEN(N'owenii'), LEN(N'owenii')) = N'owenii')",
                Sql);
        }

        public InheritanceSqlCeTest(InheritanceSqlCeFixture fixture)
            : base(fixture)
        {
        }

        private static string Sql => TestSqlLoggerFactory.Sql;
    }
}
