﻿using Microsoft.EntityFrameworkCore.FunctionalTests;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.SqlCe.FunctionalTests
{
    public class GearsOfWarQuerySqlCeTest : GearsOfWarQueryTestBase<SqlCeTestStore, GearsOfWarQuerySqlCeFixture>
    {
        public override void Include_multiple_one_to_one_and_one_to_many()
        {
            base.Include_multiple_one_to_one_and_one_to_many();

            Assert.Equal(
                @"SELECT [t].[Id], [t].[GearNickName], [t].[GearSquadId], [t].[Note], [g].[Nickname], [g].[SquadId], [g].[AssignedCityName], [g].[CityOrBirthName], [g].[Discriminator], [g].[FullName], [g].[LeaderNickname], [g].[LeaderSquadId], [g].[Rank]
FROM [CogTag] AS [t]
LEFT JOIN (
    SELECT [g].*
    FROM [Gear] AS [g]
    WHERE ([g].[Discriminator] = 'Officer') OR ([g].[Discriminator] = 'Gear')
) AS [g] ON ([t].[GearNickName] = [g].[Nickname]) AND ([t].[GearSquadId] = [g].[SquadId])
ORDER BY [g].[FullName]

SELECT [w].[Id], [w].[AmmunitionType], [w].[IsAutomatic], [w].[Name], [w].[OwnerFullName], [w].[SynergyWithId]
FROM [Weapon] AS [w]
INNER JOIN (
    SELECT DISTINCT [g].[FullName]
    FROM [CogTag] AS [t]
    LEFT JOIN (
        SELECT [g].*
        FROM [Gear] AS [g]
        WHERE ([g].[Discriminator] = 'Officer') OR ([g].[Discriminator] = 'Gear')
    ) AS [g] ON ([t].[GearNickName] = [g].[Nickname]) AND ([t].[GearSquadId] = [g].[SquadId])
) AS [g0] ON [w].[OwnerFullName] = [g0].[FullName]
ORDER BY [g0].[FullName]",
                Sql);
        }

        public override void Include_multiple_one_to_one_and_one_to_many_self_reference()
        {
            base.Include_multiple_one_to_one_and_one_to_many_self_reference();

            Assert.Equal(
                @"SELECT [t].[Id], [t].[GearNickName], [t].[GearSquadId], [t].[Note], [g].[Nickname], [g].[SquadId], [g].[AssignedCityName], [g].[CityOrBirthName], [g].[Discriminator], [g].[FullName], [g].[LeaderNickname], [g].[LeaderSquadId], [g].[Rank]
FROM [CogTag] AS [t]
LEFT JOIN (
    SELECT [g].*
    FROM [Gear] AS [g]
    WHERE ([g].[Discriminator] = 'Officer') OR ([g].[Discriminator] = 'Gear')
) AS [g] ON ([t].[GearNickName] = [g].[Nickname]) AND ([t].[GearSquadId] = [g].[SquadId])
ORDER BY [g].[FullName]

SELECT [w].[Id], [w].[AmmunitionType], [w].[IsAutomatic], [w].[Name], [w].[OwnerFullName], [w].[SynergyWithId]
FROM [Weapon] AS [w]
INNER JOIN (
    SELECT DISTINCT [g].[FullName]
    FROM [CogTag] AS [t]
    LEFT JOIN (
        SELECT [g].*
        FROM [Gear] AS [g]
        WHERE ([g].[Discriminator] = 'Officer') OR ([g].[Discriminator] = 'Gear')
    ) AS [g] ON ([t].[GearNickName] = [g].[Nickname]) AND ([t].[GearSquadId] = [g].[SquadId])
) AS [g0] ON [w].[OwnerFullName] = [g0].[FullName]
ORDER BY [g0].[FullName]",
                Sql);
        }

        public override void Include_multiple_one_to_one_and_one_to_one_and_one_to_many()
        {
            base.Include_multiple_one_to_one_and_one_to_one_and_one_to_many();

            Assert.Equal(
                @"SELECT [t].[Id], [t].[GearNickName], [t].[GearSquadId], [t].[Note], [g].[Nickname], [g].[SquadId], [g].[AssignedCityName], [g].[CityOrBirthName], [g].[Discriminator], [g].[FullName], [g].[LeaderNickname], [g].[LeaderSquadId], [g].[Rank], [s].[Id], [s].[InternalNumber], [s].[Name]
FROM [CogTag] AS [t]
LEFT JOIN (
    SELECT [g].*
    FROM [Gear] AS [g]
    WHERE ([g].[Discriminator] = 'Officer') OR ([g].[Discriminator] = 'Gear')
) AS [g] ON ([t].[GearNickName] = [g].[Nickname]) AND ([t].[GearSquadId] = [g].[SquadId])
LEFT JOIN [Squad] AS [s] ON [g].[SquadId] = [s].[Id]
ORDER BY [s].[Id]

SELECT [g0].[Nickname], [g0].[SquadId], [g0].[AssignedCityName], [g0].[CityOrBirthName], [g0].[Discriminator], [g0].[FullName], [g0].[LeaderNickname], [g0].[LeaderSquadId], [g0].[Rank]
FROM [Gear] AS [g0]
INNER JOIN (
    SELECT DISTINCT [s].[Id]
    FROM [CogTag] AS [t]
    LEFT JOIN (
        SELECT [g].*
        FROM [Gear] AS [g]
        WHERE ([g].[Discriminator] = 'Officer') OR ([g].[Discriminator] = 'Gear')
    ) AS [g] ON ([t].[GearNickName] = [g].[Nickname]) AND ([t].[GearSquadId] = [g].[SquadId])
    LEFT JOIN [Squad] AS [s] ON [g].[SquadId] = [s].[Id]
) AS [s0] ON [g0].[SquadId] = [s0].[Id]
WHERE ([g0].[Discriminator] = 'Officer') OR ([g0].[Discriminator] = 'Gear')
ORDER BY [s0].[Id]",
                Sql);
        }

        public override void Include_multiple_one_to_one_optional_and_one_to_one_required()
        {
            base.Include_multiple_one_to_one_optional_and_one_to_one_required();

            Assert.Equal(
                @"SELECT [t].[Id], [t].[GearNickName], [t].[GearSquadId], [t].[Note], [g].[Nickname], [g].[SquadId], [g].[AssignedCityName], [g].[CityOrBirthName], [g].[Discriminator], [g].[FullName], [g].[LeaderNickname], [g].[LeaderSquadId], [g].[Rank], [s].[Id], [s].[InternalNumber], [s].[Name]
FROM [CogTag] AS [t]
LEFT JOIN (
    SELECT [g].*
    FROM [Gear] AS [g]
    WHERE ([g].[Discriminator] = 'Officer') OR ([g].[Discriminator] = 'Gear')
) AS [g] ON ([t].[GearNickName] = [g].[Nickname]) AND ([t].[GearSquadId] = [g].[SquadId])
LEFT JOIN [Squad] AS [s] ON [g].[SquadId] = [s].[Id]",
                Sql);
        }

        public override void Include_multiple_circular()
        {
            base.Include_multiple_circular();

            Assert.Equal(
                @"SELECT [g].[Nickname], [g].[SquadId], [g].[AssignedCityName], [g].[CityOrBirthName], [g].[Discriminator], [g].[FullName], [g].[LeaderNickname], [g].[LeaderSquadId], [g].[Rank], [c].[Name], [c].[Location]
FROM [Gear] AS [g]
INNER JOIN [City] AS [c] ON [g].[CityOrBirthName] = [c].[Name]
WHERE [g].[Discriminator] IN ('Officer', 'Gear')
ORDER BY [c].[Name]

SELECT [g0].[Nickname], [g0].[SquadId], [g0].[AssignedCityName], [g0].[CityOrBirthName], [g0].[Discriminator], [g0].[FullName], [g0].[LeaderNickname], [g0].[LeaderSquadId], [g0].[Rank]
FROM [Gear] AS [g0]
INNER JOIN (
    SELECT DISTINCT [c].[Name]
    FROM [Gear] AS [g]
    INNER JOIN [City] AS [c] ON [g].[CityOrBirthName] = [c].[Name]
    WHERE [g].[Discriminator] IN ('Officer', 'Gear')
) AS [c0] ON [g0].[AssignedCityName] = [c0].[Name]
WHERE ([g0].[Discriminator] = 'Officer') OR ([g0].[Discriminator] = 'Gear')
ORDER BY [c0].[Name]",
                Sql);
        }

        public override void Include_multiple_circular_with_filter()
        {
            base.Include_multiple_circular_with_filter();

            Assert.Equal(
                @"SELECT [g].[Nickname], [g].[SquadId], [g].[AssignedCityName], [g].[CityOrBirthName], [g].[Discriminator], [g].[FullName], [g].[LeaderNickname], [g].[LeaderSquadId], [g].[Rank], [c].[Name], [c].[Location]
FROM [Gear] AS [g]
INNER JOIN [City] AS [c] ON [g].[CityOrBirthName] = [c].[Name]
WHERE [g].[Discriminator] IN ('Officer', 'Gear') AND ([g].[Nickname] = 'Marcus')
ORDER BY [c].[Name]

SELECT [g0].[Nickname], [g0].[SquadId], [g0].[AssignedCityName], [g0].[CityOrBirthName], [g0].[Discriminator], [g0].[FullName], [g0].[LeaderNickname], [g0].[LeaderSquadId], [g0].[Rank]
FROM [Gear] AS [g0]
INNER JOIN (
    SELECT DISTINCT [c].[Name]
    FROM [Gear] AS [g]
    INNER JOIN [City] AS [c] ON [g].[CityOrBirthName] = [c].[Name]
    WHERE [g].[Discriminator] IN ('Officer', 'Gear') AND ([g].[Nickname] = 'Marcus')
) AS [c0] ON [g0].[AssignedCityName] = [c0].[Name]
WHERE ([g0].[Discriminator] = 'Officer') OR ([g0].[Discriminator] = 'Gear')
ORDER BY [c0].[Name]",
                Sql);
        }

        public override void Include_using_alternate_key()
        {
            base.Include_using_alternate_key();

            Assert.Equal(
                @"SELECT [g].[Nickname], [g].[SquadId], [g].[AssignedCityName], [g].[CityOrBirthName], [g].[Discriminator], [g].[FullName], [g].[LeaderNickname], [g].[LeaderSquadId], [g].[Rank]
FROM [Gear] AS [g]
WHERE [g].[Discriminator] IN ('Officer', 'Gear') AND ([g].[Nickname] = 'Marcus')
ORDER BY [g].[FullName]

SELECT [w].[Id], [w].[AmmunitionType], [w].[IsAutomatic], [w].[Name], [w].[OwnerFullName], [w].[SynergyWithId]
FROM [Weapon] AS [w]
INNER JOIN (
    SELECT DISTINCT [g].[FullName]
    FROM [Gear] AS [g]
    WHERE [g].[Discriminator] IN ('Officer', 'Gear') AND ([g].[Nickname] = 'Marcus')
) AS [g0] ON [w].[OwnerFullName] = [g0].[FullName]
ORDER BY [g0].[FullName]",
                Sql);
        }

        public override void Include_multiple_include_then_include()
        {
            base.Include_multiple_include_then_include();

            Assert.Equal(
                @"SELECT [g].[Nickname], [g].[SquadId], [g].[AssignedCityName], [g].[CityOrBirthName], [g].[Discriminator], [g].[FullName], [g].[LeaderNickname], [g].[LeaderSquadId], [g].[Rank], [c].[Name], [c].[Location], [c2].[Name], [c2].[Location], [c4].[Name], [c4].[Location], [c6].[Name], [c6].[Location]
FROM [Gear] AS [g]
LEFT JOIN [City] AS [c] ON [g].[AssignedCityName] = [c].[Name]
LEFT JOIN [City] AS [c2] ON [g].[AssignedCityName] = [c2].[Name]
INNER JOIN [City] AS [c4] ON [g].[CityOrBirthName] = [c4].[Name]
INNER JOIN [City] AS [c6] ON [g].[CityOrBirthName] = [c6].[Name]
WHERE [g].[Discriminator] IN ('Officer', 'Gear')
ORDER BY [g].[Nickname], [c].[Name], [c2].[Name], [c4].[Name], [c6].[Name]

SELECT [g3].[Nickname], [g3].[SquadId], [g3].[AssignedCityName], [g3].[CityOrBirthName], [g3].[Discriminator], [g3].[FullName], [g3].[LeaderNickname], [g3].[LeaderSquadId], [g3].[Rank], [c7].[Id], [c7].[GearNickName], [c7].[GearSquadId], [c7].[Note]
FROM [Gear] AS [g3]
INNER JOIN (
    SELECT DISTINCT [g].[Nickname], [c].[Name], [c2].[Name] AS [Name0], [c4].[Name] AS [Name1], [c6].[Name] AS [Name2]
    FROM [Gear] AS [g]
    LEFT JOIN [City] AS [c] ON [g].[AssignedCityName] = [c].[Name]
    LEFT JOIN [City] AS [c2] ON [g].[AssignedCityName] = [c2].[Name]
    INNER JOIN [City] AS [c4] ON [g].[CityOrBirthName] = [c4].[Name]
    INNER JOIN [City] AS [c6] ON [g].[CityOrBirthName] = [c6].[Name]
    WHERE [g].[Discriminator] IN ('Officer', 'Gear')
) AS [c60] ON [g3].[AssignedCityName] = [c60].[Name2]
LEFT JOIN [CogTag] AS [c7] ON ([c7].[GearNickName] = [g3].[Nickname]) AND ([c7].[GearSquadId] = [g3].[SquadId])
WHERE ([g3].[Discriminator] = 'Officer') OR ([g3].[Discriminator] = 'Gear')
ORDER BY [c60].[Nickname], [c60].[Name], [c60].[Name0], [c60].[Name1], [c60].[Name2]

SELECT [g2].[Nickname], [g2].[SquadId], [g2].[AssignedCityName], [g2].[CityOrBirthName], [g2].[Discriminator], [g2].[FullName], [g2].[LeaderNickname], [g2].[LeaderSquadId], [g2].[Rank], [c5].[Id], [c5].[GearNickName], [c5].[GearSquadId], [c5].[Note]
FROM [Gear] AS [g2]
INNER JOIN (
    SELECT DISTINCT [g].[Nickname], [c].[Name], [c2].[Name] AS [Name0], [c4].[Name] AS [Name1]
    FROM [Gear] AS [g]
    LEFT JOIN [City] AS [c] ON [g].[AssignedCityName] = [c].[Name]
    LEFT JOIN [City] AS [c2] ON [g].[AssignedCityName] = [c2].[Name]
    INNER JOIN [City] AS [c4] ON [g].[CityOrBirthName] = [c4].[Name]
    WHERE [g].[Discriminator] IN ('Officer', 'Gear')
) AS [c40] ON [g2].[CityOrBirthName] = [c40].[Name1]
LEFT JOIN [CogTag] AS [c5] ON ([c5].[GearNickName] = [g2].[Nickname]) AND ([c5].[GearSquadId] = [g2].[SquadId])
WHERE ([g2].[Discriminator] = 'Officer') OR ([g2].[Discriminator] = 'Gear')
ORDER BY [c40].[Nickname], [c40].[Name], [c40].[Name0], [c40].[Name1]

SELECT [g1].[Nickname], [g1].[SquadId], [g1].[AssignedCityName], [g1].[CityOrBirthName], [g1].[Discriminator], [g1].[FullName], [g1].[LeaderNickname], [g1].[LeaderSquadId], [g1].[Rank], [c3].[Id], [c3].[GearNickName], [c3].[GearSquadId], [c3].[Note]
FROM [Gear] AS [g1]
INNER JOIN (
    SELECT DISTINCT [g].[Nickname], [c].[Name], [c2].[Name] AS [Name0]
    FROM [Gear] AS [g]
    LEFT JOIN [City] AS [c] ON [g].[AssignedCityName] = [c].[Name]
    LEFT JOIN [City] AS [c2] ON [g].[AssignedCityName] = [c2].[Name]
    WHERE [g].[Discriminator] IN ('Officer', 'Gear')
) AS [c20] ON [g1].[AssignedCityName] = [c20].[Name0]
LEFT JOIN [CogTag] AS [c3] ON ([c3].[GearNickName] = [g1].[Nickname]) AND ([c3].[GearSquadId] = [g1].[SquadId])
WHERE ([g1].[Discriminator] = 'Officer') OR ([g1].[Discriminator] = 'Gear')
ORDER BY [c20].[Nickname], [c20].[Name], [c20].[Name0]

SELECT [g0].[Nickname], [g0].[SquadId], [g0].[AssignedCityName], [g0].[CityOrBirthName], [g0].[Discriminator], [g0].[FullName], [g0].[LeaderNickname], [g0].[LeaderSquadId], [g0].[Rank], [c1].[Id], [c1].[GearNickName], [c1].[GearSquadId], [c1].[Note]
FROM [Gear] AS [g0]
INNER JOIN (
    SELECT DISTINCT [g].[Nickname], [c].[Name]
    FROM [Gear] AS [g]
    LEFT JOIN [City] AS [c] ON [g].[AssignedCityName] = [c].[Name]
    WHERE [g].[Discriminator] IN ('Officer', 'Gear')
) AS [c0] ON [g0].[CityOrBirthName] = [c0].[Name]
LEFT JOIN [CogTag] AS [c1] ON ([c1].[GearNickName] = [g0].[Nickname]) AND ([c1].[GearSquadId] = [g0].[SquadId])
WHERE ([g0].[Discriminator] = 'Officer') OR ([g0].[Discriminator] = 'Gear')
ORDER BY [c0].[Nickname], [c0].[Name]",
                Sql);
        }

        public override void Include_navigation_on_derived_type()
        {
            base.Include_navigation_on_derived_type();

            Assert.Equal(
                @"SELECT [g].[Nickname], [g].[SquadId], [g].[AssignedCityName], [g].[CityOrBirthName], [g].[Discriminator], [g].[FullName], [g].[LeaderNickname], [g].[LeaderSquadId], [g].[Rank]
FROM [Gear] AS [g]
WHERE [g].[Discriminator] = 'Officer'
ORDER BY [g].[Nickname], [g].[SquadId]

SELECT [g0].[Nickname], [g0].[SquadId], [g0].[AssignedCityName], [g0].[CityOrBirthName], [g0].[Discriminator], [g0].[FullName], [g0].[LeaderNickname], [g0].[LeaderSquadId], [g0].[Rank]
FROM [Gear] AS [g0]
INNER JOIN (
    SELECT DISTINCT [g].[Nickname], [g].[SquadId]
    FROM [Gear] AS [g]
    WHERE [g].[Discriminator] = 'Officer'
) AS [g1] ON ([g0].[LeaderNickname] = [g1].[Nickname]) AND ([g0].[LeaderSquadId] = [g1].[SquadId])
WHERE ([g0].[Discriminator] = 'Officer') OR ([g0].[Discriminator] = 'Gear')
ORDER BY [g1].[Nickname], [g1].[SquadId]",
                Sql);
        }

        public override void Select_Where_Navigation_Included()
        {
            base.Select_Where_Navigation_Included();

            Assert.Equal(
                @"SELECT [o].[Id], [o].[GearNickName], [o].[GearSquadId], [o].[Note], [g].[Nickname], [g].[SquadId], [g].[AssignedCityName], [g].[CityOrBirthName], [g].[Discriminator], [g].[FullName], [g].[LeaderNickname], [g].[LeaderSquadId], [g].[Rank]
FROM [CogTag] AS [o]
INNER JOIN [Gear] AS [o.Gear] ON ([o].[GearNickName] = [o.Gear].[Nickname]) AND ([o].[GearSquadId] = [o.Gear].[SquadId])
LEFT JOIN (
    SELECT [g].*
    FROM [Gear] AS [g]
    WHERE ([g].[Discriminator] = 'Officer') OR ([g].[Discriminator] = 'Gear')
) AS [g] ON ([o].[GearNickName] = [g].[Nickname]) AND ([o].[GearSquadId] = [g].[SquadId])
WHERE [o.Gear].[Nickname] = 'Marcus'",
                Sql);
        }

        public override void Include_with_join_reference1()
        {
            base.Include_with_join_reference1();

            Assert.Equal(
                @"SELECT [g].[Nickname], [g].[SquadId], [g].[AssignedCityName], [g].[CityOrBirthName], [g].[Discriminator], [g].[FullName], [g].[LeaderNickname], [g].[LeaderSquadId], [g].[Rank], [c].[Name], [c].[Location]
FROM [Gear] AS [g]
INNER JOIN [CogTag] AS [t] ON ([g].[SquadId] = [t].[GearSquadId]) AND ([g].[Nickname] = [t].[GearNickName])
INNER JOIN [City] AS [c] ON [g].[CityOrBirthName] = [c].[Name]
WHERE [g].[Discriminator] IN ('Officer', 'Gear')",
                Sql);
        }

        public override void Include_with_join_reference2()
        {
            base.Include_with_join_reference2();

            Assert.Equal(
                @"SELECT [g].[Nickname], [g].[SquadId], [g].[AssignedCityName], [g].[CityOrBirthName], [g].[Discriminator], [g].[FullName], [g].[LeaderNickname], [g].[LeaderSquadId], [g].[Rank], [c].[Name], [c].[Location]
FROM [CogTag] AS [t]
INNER JOIN [Gear] AS [g] ON ([t].[GearSquadId] = [g].[SquadId]) AND ([t].[GearNickName] = [g].[Nickname])
INNER JOIN [City] AS [c] ON [g].[CityOrBirthName] = [c].[Name]",
                Sql);
        }

        public override void Include_with_join_collection1()
        {
            base.Include_with_join_collection1();

            Assert.Equal(
                @"SELECT [g].[Nickname], [g].[SquadId], [g].[AssignedCityName], [g].[CityOrBirthName], [g].[Discriminator], [g].[FullName], [g].[LeaderNickname], [g].[LeaderSquadId], [g].[Rank]
FROM [Gear] AS [g]
INNER JOIN [CogTag] AS [t] ON ([g].[SquadId] = [t].[GearSquadId]) AND ([g].[Nickname] = [t].[GearNickName])
WHERE [g].[Discriminator] IN ('Officer', 'Gear')
ORDER BY [g].[FullName]

SELECT [w].[Id], [w].[AmmunitionType], [w].[IsAutomatic], [w].[Name], [w].[OwnerFullName], [w].[SynergyWithId]
FROM [Weapon] AS [w]
INNER JOIN (
    SELECT DISTINCT [g].[FullName]
    FROM [Gear] AS [g]
    INNER JOIN [CogTag] AS [t] ON ([g].[SquadId] = [t].[GearSquadId]) AND ([g].[Nickname] = [t].[GearNickName])
    WHERE [g].[Discriminator] IN ('Officer', 'Gear')
) AS [g0] ON [w].[OwnerFullName] = [g0].[FullName]
ORDER BY [g0].[FullName]",
                Sql);
        }

        public override void Include_with_join_collection2()
        {
            base.Include_with_join_collection2();

            Assert.Equal(
                @"SELECT [g].[Nickname], [g].[SquadId], [g].[AssignedCityName], [g].[CityOrBirthName], [g].[Discriminator], [g].[FullName], [g].[LeaderNickname], [g].[LeaderSquadId], [g].[Rank]
FROM [CogTag] AS [t]
INNER JOIN [Gear] AS [g] ON ([t].[GearSquadId] = [g].[SquadId]) AND ([t].[GearNickName] = [g].[Nickname])
ORDER BY [g].[FullName]

SELECT [w].[Id], [w].[AmmunitionType], [w].[IsAutomatic], [w].[Name], [w].[OwnerFullName], [w].[SynergyWithId]
FROM [Weapon] AS [w]
INNER JOIN (
    SELECT DISTINCT [g].[FullName]
    FROM [CogTag] AS [t]
    INNER JOIN [Gear] AS [g] ON ([t].[GearSquadId] = [g].[SquadId]) AND ([t].[GearNickName] = [g].[Nickname])
) AS [g0] ON [w].[OwnerFullName] = [g0].[FullName]
ORDER BY [g0].[FullName]",
                Sql);
        }

        public override void Include_with_join_multi_level()
        {
            base.Include_with_join_multi_level();

            Assert.Equal(
                @"SELECT [g].[Nickname], [g].[SquadId], [g].[AssignedCityName], [g].[CityOrBirthName], [g].[Discriminator], [g].[FullName], [g].[LeaderNickname], [g].[LeaderSquadId], [g].[Rank], [c].[Name], [c].[Location]
FROM [Gear] AS [g]
INNER JOIN [CogTag] AS [t] ON ([g].[SquadId] = [t].[GearSquadId]) AND ([g].[Nickname] = [t].[GearNickName])
INNER JOIN [City] AS [c] ON [g].[CityOrBirthName] = [c].[Name]
WHERE [g].[Discriminator] IN ('Officer', 'Gear')
ORDER BY [c].[Name]

SELECT [g0].[Nickname], [g0].[SquadId], [g0].[AssignedCityName], [g0].[CityOrBirthName], [g0].[Discriminator], [g0].[FullName], [g0].[LeaderNickname], [g0].[LeaderSquadId], [g0].[Rank]
FROM [Gear] AS [g0]
INNER JOIN (
    SELECT DISTINCT [c].[Name]
    FROM [Gear] AS [g]
    INNER JOIN [CogTag] AS [t] ON ([g].[SquadId] = [t].[GearSquadId]) AND ([g].[Nickname] = [t].[GearNickName])
    INNER JOIN [City] AS [c] ON [g].[CityOrBirthName] = [c].[Name]
    WHERE [g].[Discriminator] IN ('Officer', 'Gear')
) AS [c0] ON [g0].[AssignedCityName] = [c0].[Name]
WHERE ([g0].[Discriminator] = 'Officer') OR ([g0].[Discriminator] = 'Gear')
ORDER BY [c0].[Name]",
                Sql);
        }

        public override void Include_with_join_and_inheritance1()
        {
            base.Include_with_join_and_inheritance1();

            Assert.Equal(
                @"SELECT [t0].[Nickname], [t0].[SquadId], [t0].[AssignedCityName], [t0].[CityOrBirthName], [t0].[Discriminator], [t0].[FullName], [t0].[LeaderNickname], [t0].[LeaderSquadId], [t0].[Rank], [c].[Name], [c].[Location]
FROM [CogTag] AS [t]
INNER JOIN (
    SELECT [g0].[Nickname], [g0].[SquadId], [g0].[AssignedCityName], [g0].[CityOrBirthName], [g0].[Discriminator], [g0].[FullName], [g0].[LeaderNickname], [g0].[LeaderSquadId], [g0].[Rank]
    FROM [Gear] AS [g0]
    WHERE [g0].[Discriminator] = 'Officer'
) AS [t0] ON ([t].[GearSquadId] = [t0].[SquadId]) AND ([t].[GearNickName] = [t0].[Nickname])
INNER JOIN [City] AS [c] ON [t0].[CityOrBirthName] = [c].[Name]",
                Sql);
        }

        public override void Include_with_join_and_inheritance2()
        {
            base.Include_with_join_and_inheritance2();

            Assert.Equal(
                @"SELECT [t].[Nickname], [t].[SquadId], [t].[AssignedCityName], [t].[CityOrBirthName], [t].[Discriminator], [t].[FullName], [t].[LeaderNickname], [t].[LeaderSquadId], [t].[Rank]
FROM (
    SELECT [g0].[Nickname], [g0].[SquadId], [g0].[AssignedCityName], [g0].[CityOrBirthName], [g0].[Discriminator], [g0].[FullName], [g0].[LeaderNickname], [g0].[LeaderSquadId], [g0].[Rank]
    FROM [Gear] AS [g0]
    WHERE [g0].[Discriminator] = 'Officer'
) AS [t]
INNER JOIN [CogTag] AS [t0] ON ([t].[SquadId] = [t0].[GearSquadId]) AND ([t].[Nickname] = [t0].[GearNickName])
ORDER BY [t].[FullName]

SELECT [w].[Id], [w].[AmmunitionType], [w].[IsAutomatic], [w].[Name], [w].[OwnerFullName], [w].[SynergyWithId]
FROM [Weapon] AS [w]
INNER JOIN (
    SELECT DISTINCT [t].[FullName]
    FROM (
        SELECT [g0].[Nickname], [g0].[SquadId], [g0].[AssignedCityName], [g0].[CityOrBirthName], [g0].[Discriminator], [g0].[FullName], [g0].[LeaderNickname], [g0].[LeaderSquadId], [g0].[Rank]
        FROM [Gear] AS [g0]
        WHERE [g0].[Discriminator] = 'Officer'
    ) AS [t]
    INNER JOIN [CogTag] AS [t0] ON ([t].[SquadId] = [t0].[GearSquadId]) AND ([t].[Nickname] = [t0].[GearNickName])
) AS [t1] ON [w].[OwnerFullName] = [t1].[FullName]
ORDER BY [t1].[FullName]",
                Sql);
        }

        public override void Include_with_join_and_inheritance3()
        {
            base.Include_with_join_and_inheritance3();

            Assert.Equal(
                @"SELECT [t0].[Nickname], [t0].[SquadId], [t0].[AssignedCityName], [t0].[CityOrBirthName], [t0].[Discriminator], [t0].[FullName], [t0].[LeaderNickname], [t0].[LeaderSquadId], [t0].[Rank]
FROM [CogTag] AS [t]
INNER JOIN (
    SELECT [g0].[Nickname], [g0].[SquadId], [g0].[AssignedCityName], [g0].[CityOrBirthName], [g0].[Discriminator], [g0].[FullName], [g0].[LeaderNickname], [g0].[LeaderSquadId], [g0].[Rank]
    FROM [Gear] AS [g0]
    WHERE [g0].[Discriminator] = 'Officer'
) AS [t0] ON ([t].[GearSquadId] = [t0].[SquadId]) AND ([t].[GearNickName] = [t0].[Nickname])
ORDER BY [t0].[Nickname], [t0].[SquadId]

SELECT [g1].[Nickname], [g1].[SquadId], [g1].[AssignedCityName], [g1].[CityOrBirthName], [g1].[Discriminator], [g1].[FullName], [g1].[LeaderNickname], [g1].[LeaderSquadId], [g1].[Rank]
FROM [Gear] AS [g1]
INNER JOIN (
    SELECT DISTINCT [t0].[Nickname], [t0].[SquadId]
    FROM [CogTag] AS [t]
    INNER JOIN (
        SELECT [g0].[Nickname], [g0].[SquadId], [g0].[AssignedCityName], [g0].[CityOrBirthName], [g0].[Discriminator], [g0].[FullName], [g0].[LeaderNickname], [g0].[LeaderSquadId], [g0].[Rank]
        FROM [Gear] AS [g0]
        WHERE [g0].[Discriminator] = 'Officer'
    ) AS [t0] ON ([t].[GearSquadId] = [t0].[SquadId]) AND ([t].[GearNickName] = [t0].[Nickname])
) AS [t00] ON ([g1].[LeaderNickname] = [t00].[Nickname]) AND ([g1].[LeaderSquadId] = [t00].[SquadId])
WHERE ([g1].[Discriminator] = 'Officer') OR ([g1].[Discriminator] = 'Gear')
ORDER BY [t00].[Nickname], [t00].[SquadId]",
                Sql);
        }

        public override void Include_with_nested_navigation_in_order_by()
        {
            base.Include_with_nested_navigation_in_order_by();

            Assert.Equal(
                @"SELECT [w].[Id], [w].[AmmunitionType], [w].[IsAutomatic], [w].[Name], [w].[OwnerFullName], [w].[SynergyWithId], [g].[Nickname], [g].[SquadId], [g].[AssignedCityName], [g].[CityOrBirthName], [g].[Discriminator], [g].[FullName], [g].[LeaderNickname], [g].[LeaderSquadId], [g].[Rank]
FROM [Weapon] AS [w]
INNER JOIN [Gear] AS [w.Owner] ON [w].[OwnerFullName] = [w.Owner].[FullName]
INNER JOIN [City] AS [w.Owner.CityOfBirth] ON [w.Owner].[CityOrBirthName] = [w.Owner.CityOfBirth].[Name]
LEFT JOIN (
    SELECT [g].*
    FROM [Gear] AS [g]
    WHERE ([g].[Discriminator] = 'Officer') OR ([g].[Discriminator] = 'Gear')
) AS [g] ON [w].[OwnerFullName] = [g].[FullName]
ORDER BY [w.Owner.CityOfBirth].[Name]",
                Sql);
        }

        public override void Where_enum()
        {
            base.Where_enum();

            Assert.Equal(
                @"SELECT [g].[Nickname], [g].[SquadId], [g].[AssignedCityName], [g].[CityOrBirthName], [g].[Discriminator], [g].[FullName], [g].[LeaderNickname], [g].[LeaderSquadId], [g].[Rank]
FROM [Gear] AS [g]
WHERE [g].[Discriminator] IN ('Officer', 'Gear') AND ([g].[Rank] = 2)",
                Sql);
        }

        public override void Where_nullable_enum_with_constant()
        {
            base.Where_nullable_enum_with_constant();

            Assert.Equal(
                @"SELECT [w].[Id], [w].[AmmunitionType], [w].[IsAutomatic], [w].[Name], [w].[OwnerFullName], [w].[SynergyWithId]
FROM [Weapon] AS [w]
WHERE [w].[AmmunitionType] = 1",
                Sql);
        }

        public override void Where_nullable_enum_with_null_constant()
        {
            base.Where_nullable_enum_with_null_constant();

            Assert.Equal(
                @"SELECT [w].[Id], [w].[AmmunitionType], [w].[IsAutomatic], [w].[Name], [w].[OwnerFullName], [w].[SynergyWithId]
FROM [Weapon] AS [w]
WHERE [w].[AmmunitionType] IS NULL",
                Sql);
        }

        public override void Where_nullable_enum_with_non_nullable_parameter()
        {
            base.Where_nullable_enum_with_non_nullable_parameter();

            Assert.Equal(
                @"@__p_0: 1

SELECT [w].[Id], [w].[AmmunitionType], [w].[Name], [w].[OwnerFullName], [w].[SynergyWithId]
FROM [Weapon] AS [w]
WHERE [w].[AmmunitionType] = @__p_0",
                Sql);
        }

        public override void Where_nullable_enum_with_nullable_parameter()
        {
            base.Where_nullable_enum_with_nullable_parameter();

            Assert.Equal(
                @"@__ammunitionType_0: Cartridge

SELECT [w].[Id], [w].[AmmunitionType], [w].[IsAutomatic], [w].[Name], [w].[OwnerFullName], [w].[SynergyWithId]
FROM [Weapon] AS [w]
WHERE [w].[AmmunitionType] = @__ammunitionType_0

SELECT [w].[Id], [w].[AmmunitionType], [w].[IsAutomatic], [w].[Name], [w].[OwnerFullName], [w].[SynergyWithId]
FROM [Weapon] AS [w]
WHERE [w].[AmmunitionType] IS NULL",
                Sql);
        }

        public override void Where_count_subquery_without_collision()
        {
            //TODO ErikEJ Same old subquery issue
//            base.Where_count_subquery_without_collision();

//            Assert.Equal(
//                @"SELECT [w].[Nickname], [w].[SquadId], [w].[AssignedCityName], [w].[CityOrBirthName], [w].[Discriminator], [w].[FullName], [w].[LeaderNickname], [w].[LeaderSquadId], [w].[Rank]
//FROM [Gear] AS [w]
//WHERE [w].[Discriminator] IN ('Officer', 'Gear') AND ((
//    SELECT COUNT(*)
//    FROM [Weapon] AS [w0]
//    WHERE [w].[FullName] = [w0].[OwnerFullName]
//) = 2)",
//                Sql);
        }

        public override void Where_any_subquery_without_collision()
        {
            base.Where_any_subquery_without_collision();

            Assert.Equal(
                @"SELECT [w].[Nickname], [w].[SquadId], [w].[AssignedCityName], [w].[CityOrBirthName], [w].[Discriminator], [w].[FullName], [w].[LeaderNickname], [w].[LeaderSquadId], [w].[Rank]
FROM [Gear] AS [w]
WHERE [w].[Discriminator] IN ('Officer', 'Gear') AND EXISTS (
    SELECT 1
    FROM [Weapon] AS [w0]
    WHERE [w].[FullName] = [w0].[OwnerFullName])",
                Sql);
        }

        public override void Select_inverted_boolean()
        {
            base.Select_inverted_boolean();

            Assert.Equal(
                @"SELECT [w].[Id], CASE
    WHEN [w].[IsAutomatic] = 0
    THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
END
FROM [Weapon] AS [w]
WHERE [w].[IsAutomatic] = 1",
                Sql);
        }

        public override void Select_comparison_with_null()
        {
            base.Select_comparison_with_null();

            Assert.Equal(
                @"@__ammunitionType_1: Cartridge
@__ammunitionType_0: Cartridge

SELECT [w].[Id], CASE
    WHEN [w].[AmmunitionType] = @__ammunitionType_1
    THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
END
FROM [Weapon] AS [w]
WHERE [w].[AmmunitionType] = @__ammunitionType_0

SELECT [w].[Id], CASE
    WHEN [w].[AmmunitionType] IS NULL
    THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
END
FROM [Weapon] AS [w]
WHERE [w].[AmmunitionType] IS NULL",
                Sql);
        }

        public override void Select_ternary_operation_with_boolean()
        {
            base.Select_ternary_operation_with_boolean();

            Assert.Equal(
                @"SELECT [w].[Id], CASE
    WHEN [w].[IsAutomatic] = 1
    THEN 1 ELSE 0
END
FROM [Weapon] AS [w]",
                Sql);
        }

        public override void Select_ternary_operation_with_inverted_boolean()
        {
            base.Select_ternary_operation_with_inverted_boolean();

            Assert.Equal(
                @"SELECT [w].[Id], CASE
    WHEN [w].[IsAutomatic] = 0
    THEN 1 ELSE 0
END
FROM [Weapon] AS [w]",
                Sql);
        }

        public override void Select_ternary_operation_with_has_value_not_null()
        {
            // TODO: Optimize this query (See #4267)
            base.Select_ternary_operation_with_has_value_not_null();

            Assert.Equal(
                @"SELECT [w].[Id], CASE
    WHEN [w].[AmmunitionType] IS NOT NULL AND (([w].[AmmunitionType] = 1) AND [w].[AmmunitionType] IS NOT NULL)
    THEN 'Yes' ELSE 'No'
END
FROM [Weapon] AS [w]
WHERE [w].[AmmunitionType] IS NOT NULL AND (([w].[AmmunitionType] = 1) AND [w].[AmmunitionType] IS NOT NULL)",
                Sql);
        }

        public override void Select_ternary_operation_multiple_conditions()
        {
            base.Select_ternary_operation_multiple_conditions();

            Assert.Equal(
                @"SELECT [w].[Id], CASE
    WHEN ([w].[AmmunitionType] = 2) AND ([w].[SynergyWithId] = 1)
    THEN 'Yes' ELSE 'No'
END
FROM [Weapon] AS [w]",
                Sql);
        }

        public override void Select_ternary_operation_multiple_conditions_2()
        {
            base.Select_ternary_operation_multiple_conditions_2();

            Assert.Equal(
                @"SELECT [w].[Id], CASE
    WHEN [w].[IsAutomatic] = 0 AND (([w].[SynergyWithId] = 1) AND [w].[SynergyWithId] IS NOT NULL)
    THEN 'Yes' ELSE 'No'
END
FROM [Weapon] AS [w]",
                Sql);
        }

        public override void Select_multiple_conditions()
        {
            base.Select_multiple_conditions();

            Assert.Equal(
                @"SELECT [w].[Id], CASE
    WHEN [w].[IsAutomatic] = 0 AND (([w].[SynergyWithId] = 1) AND [w].[SynergyWithId] IS NOT NULL)
    THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
END
FROM [Weapon] AS [w]",
                Sql);
        }

        public override void Select_nested_ternary_operations()
        {
            base.Select_nested_ternary_operations();

            Assert.Equal(
                @"SELECT [w].[Id], CASE
    WHEN [w].[IsAutomatic] = 0
    THEN CASE
        WHEN ([w].[AmmunitionType] = 1) AND [w].[AmmunitionType] IS NOT NULL
        THEN 'ManualCartridge' ELSE 'Manual'
    END ELSE 'Auto'
END
FROM [Weapon] AS [w]",
                Sql);
        }

        public override void Select_Where_Navigation_Scalar_Equals_Navigation_Scalar()
        {
            base.Select_Where_Navigation_Scalar_Equals_Navigation_Scalar();

            Assert.Equal(
                @"",
                Sql);
        }

        public override void Select_Singleton_Navigation_With_Member_Access()
        {
            base.Select_Singleton_Navigation_With_Member_Access();

            Assert.Equal(
                @"SELECT [ct].[Id], [ct].[GearNickName], [ct].[GearSquadId], [ct].[Note], [ct.Gear].[Nickname], [ct.Gear].[SquadId], [ct.Gear].[AssignedCityName], [ct.Gear].[CityOrBirthName], [ct.Gear].[Discriminator], [ct.Gear].[FullName], [ct.Gear].[LeaderNickname], [ct.Gear].[LeaderSquadId], [ct.Gear].[Rank]
FROM [CogTag] AS [ct]
LEFT JOIN [Gear] AS [ct.Gear] ON ([ct].[GearNickName] = [ct.Gear].[Nickname]) AND ([ct].[GearSquadId] = [ct.Gear].[SquadId])
ORDER BY [ct].[GearNickName], [ct].[GearSquadId]",
                Sql);
        }

        public override void Select_Where_Navigation()
        {
            base.Select_Where_Navigation();

            Assert.Equal(
                @"SELECT [ct].[Id], [ct].[GearNickName], [ct].[GearSquadId], [ct].[Note], [ct.Gear].[Nickname], [ct.Gear].[SquadId], [ct.Gear].[AssignedCityName], [ct.Gear].[CityOrBirthName], [ct.Gear].[Discriminator], [ct.Gear].[FullName], [ct.Gear].[LeaderNickname], [ct.Gear].[LeaderSquadId], [ct.Gear].[Rank]
FROM [CogTag] AS [ct]
LEFT JOIN [Gear] AS [ct.Gear] ON ([ct].[GearNickName] = [ct.Gear].[Nickname]) AND ([ct].[GearSquadId] = [ct.Gear].[SquadId])
ORDER BY [ct].[GearNickName], [ct].[GearSquadId]",
                Sql);
        }

        public override void Select_Where_Navigation_Client()
        {
            base.Select_Where_Navigation_Client();

            Assert.Equal(
                @"SELECT [o].[Id], [o].[GearNickName], [o].[GearSquadId], [o].[Note], [o.Gear].[Nickname], [o.Gear].[SquadId], [o.Gear].[AssignedCityName], [o.Gear].[CityOrBirthName], [o.Gear].[Discriminator], [o.Gear].[FullName], [o.Gear].[LeaderNickname], [o.Gear].[LeaderSquadId], [o.Gear].[Rank]
FROM [CogTag] AS [o]
LEFT JOIN [Gear] AS [o.Gear] ON ([o].[GearNickName] = [o.Gear].[Nickname]) AND ([o].[GearSquadId] = [o.Gear].[SquadId])
WHERE [o].[GearNickName] IS NOT NULL OR [o].[GearSquadId] IS NOT NULL
ORDER BY [o].[GearNickName], [o].[GearSquadId]",
                Sql);
        }

        public override void Select_Where_Navigation_Equals_Navigation()
        {
            base.Select_Where_Navigation_Equals_Navigation();

            Assert.Equal(
                @"SELECT [ct1].[Id], [ct1].[GearNickName], [ct1].[GearSquadId], [ct1].[Note], [ct2].[Id], [ct2].[GearNickName], [ct2].[GearSquadId], [ct2].[Note]
FROM [CogTag] AS [ct1]
CROSS JOIN [CogTag] AS [ct2]
WHERE (([ct1].[GearNickName] = [ct2].[GearNickName]) OR ([ct1].[GearNickName] IS NULL AND [ct2].[GearNickName] IS NULL)) AND (([ct1].[GearSquadId] = [ct2].[GearSquadId]) OR ([ct1].[GearSquadId] IS NULL AND [ct2].[GearSquadId] IS NULL))",
                Sql);
        }

        public override void Select_Where_Navigation_Null()
        {
            base.Select_Where_Navigation_Null();

            Assert.Equal(
                @"SELECT [ct].[Id], [ct].[GearNickName], [ct].[GearSquadId], [ct].[Note]
FROM [CogTag] AS [ct]
WHERE [ct].[GearNickName] IS NULL AND [ct].[GearSquadId] IS NULL",
                Sql);
        }

        public override void Select_Where_Navigation_Null_Reverse()
        {
            base.Select_Where_Navigation_Null_Reverse();

            Assert.Equal(
                @"SELECT [ct].[Id], [ct].[GearNickName], [ct].[GearSquadId], [ct].[Note]
FROM [CogTag] AS [ct]
WHERE [ct].[GearNickName] IS NULL AND [ct].[GearSquadId] IS NULL",
                Sql);
        }

        public override void Select_Where_Navigation_Scalar_Equals_Navigation_Scalar_Projected()
        {
            base.Select_Where_Navigation_Scalar_Equals_Navigation_Scalar_Projected();

            Assert.StartsWith(
                @"SELECT [ct2.Gear].[Nickname], [ct2.Gear].[SquadId], [ct2.Gear].[AssignedCityName], [ct2.Gear].[CityOrBirthName], [ct2.Gear].[Discriminator], [ct2.Gear].[FullName], [ct2.Gear].[LeaderNickname], [ct2.Gear].[LeaderSquadId], [ct2.Gear].[Rank]
FROM [Gear] AS [ct2.Gear]
WHERE ([ct2.Gear].[Discriminator] = 'Officer') OR ([ct2.Gear].[Discriminator] = 'Gear')

SELECT [ct1].[Id], [ct1].[GearNickName], [ct1].[GearSquadId], [ct1].[Note], [ct1.Gear].[Nickname], [ct1.Gear].[SquadId], [ct1.Gear].[AssignedCityName], [ct1.Gear].[CityOrBirthName], [ct1.Gear].[Discriminator], [ct1.Gear].[FullName], [ct1.Gear].[LeaderNickname], [ct1.Gear].[LeaderSquadId], [ct1.Gear].[Rank]
FROM [CogTag] AS [ct1]
LEFT JOIN [Gear] AS [ct1.Gear] ON ([ct1].[GearNickName] = [ct1.Gear].[Nickname]) AND ([ct1].[GearSquadId] = [ct1.Gear].[SquadId])
ORDER BY [ct1].[GearNickName], [ct1].[GearSquadId]",
                Sql);
        }

        public override void Singleton_Navigation_With_Member_Access()
        {
            base.Singleton_Navigation_With_Member_Access();

            Assert.Equal(
                @"SELECT [ct].[Id], [ct].[GearNickName], [ct].[GearSquadId], [ct].[Note], [ct.Gear].[Nickname], [ct.Gear].[SquadId], [ct.Gear].[AssignedCityName], [ct.Gear].[CityOrBirthName], [ct.Gear].[Discriminator], [ct.Gear].[FullName], [ct.Gear].[LeaderNickname], [ct.Gear].[LeaderSquadId], [ct.Gear].[Rank]
FROM [CogTag] AS [ct]
LEFT JOIN [Gear] AS [ct.Gear] ON ([ct].[GearNickName] = [ct.Gear].[Nickname]) AND ([ct].[GearSquadId] = [ct.Gear].[SquadId])
ORDER BY [ct].[GearNickName], [ct].[GearSquadId]",
                Sql);
        }

        public override void GroupJoin_Composite_Key()
        {
            base.GroupJoin_Composite_Key();

            Assert.Equal(
                @"SELECT [g].[Nickname], [g].[SquadId], [g].[AssignedCityName], [g].[CityOrBirthName], [g].[Discriminator], [g].[FullName], [g].[LeaderNickname], [g].[LeaderSquadId], [g].[Rank]
FROM [CogTag] AS [ct]
LEFT JOIN [Gear] AS [g] ON ([ct].[GearNickName] = [g].[Nickname]) AND ([ct].[GearSquadId] = [g].[SquadId])
ORDER BY [ct].[GearNickName], [ct].[GearSquadId]",
                Sql);
        }

        public override void Join_navigation_translated_to_subquery_composite_key()
        {
            //TODO ErikEJ Same old subquery issue
//            base.Join_navigation_translated_to_subquery_composite_key();

//            Assert.Equal(
//                @"SELECT [g].[FullName], [t].[Note]
//FROM [Gear] AS [g]
//INNER JOIN [CogTag] AS [t] ON [g].[FullName] = (
//    SELECT TOP(1) [subQuery0].[FullName]
//    FROM [Gear] AS [subQuery0]
//    WHERE ([subQuery0].[Nickname] = [t].[GearNickName]) AND ([subQuery0].[SquadId] = [t].[GearSquadId])
//)",
//                Sql);
        }

        public override void Collection_with_inheritance_and_join_include_joined()
        {
            base.Collection_with_inheritance_and_join_include_joined();

            Assert.Equal(
                @"SELECT [t0].[Nickname], [t0].[SquadId], [t0].[AssignedCityName], [t0].[CityOrBirthName], [t0].[Discriminator], [t0].[FullName], [t0].[LeaderNickname], [t0].[LeaderSquadId], [t0].[Rank], [c].[Id], [c].[GearNickName], [c].[GearSquadId], [c].[Note]
FROM [CogTag] AS [t]
INNER JOIN (
    SELECT [g0].[Nickname], [g0].[SquadId], [g0].[AssignedCityName], [g0].[CityOrBirthName], [g0].[Discriminator], [g0].[FullName], [g0].[LeaderNickname], [g0].[LeaderSquadId], [g0].[Rank]
    FROM [Gear] AS [g0]
    WHERE [g0].[Discriminator] = 'Officer'
) AS [t0] ON ([t].[GearSquadId] = [t0].[SquadId]) AND ([t].[GearNickName] = [t0].[Nickname])
LEFT JOIN [CogTag] AS [c] ON ([c].[GearNickName] = [t0].[Nickname]) AND ([c].[GearSquadId] = [t0].[SquadId])",
                Sql);
        }

        public override void Collection_with_inheritance_and_join_include_source()
        {
            base.Collection_with_inheritance_and_join_include_source();

            Assert.Equal(
                @"SELECT [t].[Nickname], [t].[SquadId], [t].[AssignedCityName], [t].[CityOrBirthName], [t].[Discriminator], [t].[FullName], [t].[LeaderNickname], [t].[LeaderSquadId], [t].[Rank], [c].[Id], [c].[GearNickName], [c].[GearSquadId], [c].[Note]
FROM (
    SELECT [g0].[Nickname], [g0].[SquadId], [g0].[AssignedCityName], [g0].[CityOrBirthName], [g0].[Discriminator], [g0].[FullName], [g0].[LeaderNickname], [g0].[LeaderSquadId], [g0].[Rank]
    FROM [Gear] AS [g0]
    WHERE [g0].[Discriminator] = 'Officer'
) AS [t]
INNER JOIN [CogTag] AS [t0] ON ([t].[SquadId] = [t0].[GearSquadId]) AND ([t].[Nickname] = [t0].[GearNickName])
LEFT JOIN [CogTag] AS [c] ON ([c].[GearNickName] = [t].[Nickname]) AND ([c].[GearSquadId] = [t].[SquadId])",
                Sql);
        }

        public GearsOfWarQuerySqlCeTest(GearsOfWarQuerySqlCeFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            //TestSqlLoggerFactory.CaptureOutput(testOutputHelper);
        }

        protected override void ClearLog() => TestSqlLoggerFactory.Reset();

        private static string Sql => TestSqlLoggerFactory.Sql;
    }
}
