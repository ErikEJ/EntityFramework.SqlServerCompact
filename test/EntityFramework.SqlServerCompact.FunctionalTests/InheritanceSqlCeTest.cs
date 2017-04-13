﻿using System;
using Microsoft.EntityFrameworkCore.Specification.Tests.TestModels.Inheritance;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class InheritanceSqlCeTest : InheritanceTestBase<InheritanceSqlCeFixture>
    {
        public InheritanceSqlCeTest(InheritanceSqlCeFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            //TestSqlLoggerFactory.CaptureOutput(testOutputHelper);
        }

        [Fact]
        public virtual void Common_property_shares_column()
        {
            using (var context = CreateContext())
            {
                var liltType = context.Model.FindEntityType(typeof(Lilt));
                var cokeType = context.Model.FindEntityType(typeof(Coke));
                var teaType = context.Model.FindEntityType(typeof(Tea));

                Assert.Equal("SugarGrams", cokeType.FindProperty("SugarGrams").Relational().ColumnName);
                Assert.Equal("CaffeineGrams", cokeType.FindProperty("CaffeineGrams").Relational().ColumnName);
                Assert.Equal("CokeCO2", cokeType.FindProperty("Carbination").Relational().ColumnName);

                Assert.Equal("SugarGrams", liltType.FindProperty("SugarGrams").Relational().ColumnName);
                Assert.Equal("LiltCO2", liltType.FindProperty("Carbination").Relational().ColumnName);

                Assert.Equal("CaffeineGrams", teaType.FindProperty("CaffeineGrams").Relational().ColumnName);
                Assert.Equal("HasMilk", teaType.FindProperty("HasMilk").Relational().ColumnName);
            }
        }

        [Fact]
        public override void Can_query_when_shared_column()
        {
            base.Can_query_when_shared_column();

            Assert.Equal(
                @"SELECT TOP(2) [d].[Id], [d].[Discriminator], [d].[CaffeineGrams], [d].[CokeCO2], [d].[SugarGrams]
FROM [Drink] AS [d]
WHERE [d].[Discriminator] = N'Coke'

SELECT TOP(2) [d].[Id], [d].[Discriminator], [d].[LiltCO2], [d].[SugarGrams]
FROM [Drink] AS [d]
WHERE [d].[Discriminator] = N'Lilt'

SELECT TOP(2) [d].[Id], [d].[Discriminator], [d].[CaffeineGrams], [d].[HasMilk]
FROM [Drink] AS [d]
WHERE [d].[Discriminator] = N'Tea'",
                Sql);
        }

        [Fact]
        public override void Can_query_all_types_when_shared_column()
        {
            base.Can_query_all_types_when_shared_column();

            Assert.Equal(
                @"SELECT [d].[Id], [d].[Discriminator], [d].[CaffeineGrams], [d].[CokeCO2], [d].[SugarGrams], [d].[LiltCO2], [d].[HasMilk]
FROM [Drink] AS [d]
WHERE [d].[Discriminator] IN (N'Tea', N'Lilt', N'Coke', N'Drink')",
                Sql);
        }

        public override void Can_use_of_type_animal()
        {
            base.Can_use_of_type_animal();

            Assert.Equal(
                @"SELECT [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
WHERE [a].[Discriminator] IN (N'Kiwi', N'Eagle')
ORDER BY [a].[Species]",
                Sql);
        }

        public override void Can_use_is_kiwi()
        {
            base.Can_use_is_kiwi();

            Assert.Equal(
                @"SELECT [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
WHERE [a].[Discriminator] IN (N'Kiwi', N'Eagle') AND ([a].[Discriminator] = N'Kiwi')",
                Sql);
        }

        public override void Can_use_is_kiwi_with_other_predicate()
        {
            base.Can_use_is_kiwi_with_other_predicate();

            Assert.Equal(
                @"SELECT [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
WHERE [a].[Discriminator] IN (N'Kiwi', N'Eagle') AND (([a].[Discriminator] = N'Kiwi') AND ([a].[CountryId] = 1))",
                Sql);
        }

        public override void Can_use_is_kiwi_in_projection()
        {
            base.Can_use_is_kiwi_in_projection();

            Assert.Equal(
                @"SELECT CASE
    WHEN [a].[Discriminator] = N'Kiwi'
    THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
END
FROM [Animal] AS [a]
WHERE [a].[Discriminator] IN (N'Kiwi', N'Eagle')",
                Sql);
        }

        public override void Can_use_of_type_bird()
        {
            base.Can_use_of_type_bird();

            Assert.Equal(
                @"SELECT [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
WHERE [a].[Discriminator] IN (N'Kiwi', N'Eagle')
ORDER BY [a].[Species]",
                Sql);
        }

        public override void Can_use_of_type_bird_predicate()
        {
            base.Can_use_of_type_bird_predicate();

            Assert.Equal(
                @"SELECT [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
WHERE [a].[Discriminator] IN (N'Kiwi', N'Eagle') AND ([a].[CountryId] = 1)
ORDER BY [a].[Species]",
                Sql);
        }

        public override void Can_use_of_type_bird_with_projection()
        {
            base.Can_use_of_type_bird_with_projection();

            Assert.Equal(
                @"SELECT [b].[EagleId]
FROM [Animal] AS [b]
WHERE [b].[Discriminator] IN (N'Kiwi', N'Eagle')",
                Sql);
        }

        public override void Can_use_of_type_bird_first()
        {
            base.Can_use_of_type_bird_first();

            Assert.Equal(
                @"SELECT TOP(1) [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[Group], [a].[FoundOn]
FROM [Animal] AS [a]
WHERE [a].[Discriminator] IN (N'Kiwi', N'Eagle')
ORDER BY [a].[Species]",
                Sql);
        }

        public override void Can_use_of_type_kiwi()
        {
            base.Can_use_of_type_kiwi();

            Assert.Equal(
                @"SELECT [a].[Species], [a].[CountryId], [a].[Discriminator], [a].[Name], [a].[EagleId], [a].[IsFlightless], [a].[FoundOn]
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

SELECT [e.Prey].[Species], [e.Prey].[CountryId], [e.Prey].[Discriminator], [e.Prey].[Name], [e.Prey].[EagleId], [e.Prey].[IsFlightless], [e.Prey].[Group], [e.Prey].[FoundOn]
FROM [Animal] AS [e.Prey]
INNER JOIN (
    SELECT TOP(1) [e0].[Species]
    FROM [Animal] AS [e0]
    WHERE [e0].[Discriminator] = N'Eagle'
    ORDER BY [e0].[Species]
) AS [t] ON [e.Prey].[EagleId] = [t].[Species]
WHERE [e.Prey].[Discriminator] IN (N'Kiwi', N'Eagle')
ORDER BY [t].[Species]",
                Sql);
        }

        public override void Can_include_animals()
        {
            base.Can_include_animals();

            Assert.Equal(
                @"SELECT [c].[Id], [c].[Name]
FROM [Country] AS [c]
ORDER BY [c].[Name], [c].[Id]

SELECT [c.Animals].[Species], [c.Animals].[CountryId], [c.Animals].[Discriminator], [c.Animals].[Name], [c.Animals].[EagleId], [c.Animals].[IsFlightless], [c.Animals].[Group], [c.Animals].[FoundOn]
FROM [Animal] AS [c.Animals]
INNER JOIN (
    SELECT [c0].[Id], [c0].[Name]
    FROM [Country] AS [c0]
) AS [t] ON [c.Animals].[CountryId] = [t].[Id]
WHERE [c.Animals].[Discriminator] IN (N'Kiwi', N'Eagle')
ORDER BY [t].[Name], [t].[Id]",
                Sql);
        }

        public override void Can_use_of_type_kiwi_where_north_on_derived_property()
        {
            base.Can_use_of_type_kiwi_where_north_on_derived_property();

            Assert.Equal(@"SELECT [x].[Species], [x].[CountryId], [x].[Discriminator], [x].[Name], [x].[EagleId], [x].[IsFlightless], [x].[FoundOn]
FROM [Animal] AS [x]
WHERE ([x].[Discriminator] = N'Kiwi') AND ([x].[FoundOn] = 0)",
                Sql);
        }

        public override void Can_use_of_type_kiwi_where_south_on_derived_property()
        {
            base.Can_use_of_type_kiwi_where_south_on_derived_property();

            Assert.Equal(@"SELECT [x].[Species], [x].[CountryId], [x].[Discriminator], [x].[Name], [x].[EagleId], [x].[IsFlightless], [x].[FoundOn]
FROM [Animal] AS [x]
WHERE ([x].[Discriminator] = N'Kiwi') AND ([x].[FoundOn] = 1)",
                Sql);
        }

        public override void Discriminator_used_when_projection_over_derived_type()
        {
            base.Discriminator_used_when_projection_over_derived_type();

            Assert.Equal(
                @"SELECT [k].[FoundOn]
FROM [Animal] AS [k]
WHERE [k].[Discriminator] = N'Kiwi'",
                Sql);
        }

        public override void Discriminator_used_when_projection_over_derived_type2()
        {
            base.Discriminator_used_when_projection_over_derived_type2();

            Assert.Equal(
                @"SELECT [b].[IsFlightless], [b].[Discriminator]
FROM [Animal] AS [b]
WHERE [b].[Discriminator] IN (N'Kiwi', N'Eagle')",
                Sql);
        }

        public override void Discriminator_used_when_projection_over_of_type()
        {
            base.Discriminator_used_when_projection_over_of_type();

            Assert.Equal(
                @"SELECT [k].[FoundOn]
FROM [Animal] AS [k]
WHERE [k].[Discriminator] = N'Kiwi'",
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

        private const string FileLineEnding = @"
";

        private static string Sql => TestSqlLoggerFactory.Sql.Replace(Environment.NewLine, FileLineEnding);
    }
}