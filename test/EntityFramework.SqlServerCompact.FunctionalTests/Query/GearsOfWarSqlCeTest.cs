using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class GearsOfWarQuerySqlCeTest : GearsOfWarQueryTestBase<GearsOfWarQuerySqlCeFixture>
    {
        private static readonly string _eol = Environment.NewLine;

        // ReSharper disable once UnusedParameter.Local
        public GearsOfWarQuerySqlCeTest(GearsOfWarQuerySqlCeFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            Fixture.TestSqlLoggerFactory.Clear();
            //Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Select_subquery_int_with_inside_cast_and_coalesce(bool isAsync)
        {
            return base.Select_subquery_int_with_inside_cast_and_coalesce(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task DateTimeOffset_DateAdd_AddDays(bool isAsync)
        {
            return base.DateTimeOffset_DateAdd_AddDays(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task DateTimeOffset_DateAdd_AddHours(bool isAsync)
        {
            return base.DateTimeOffset_DateAdd_AddHours(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task DateTimeOffset_DateAdd_AddMilliseconds(bool isAsync)
        {
            return base.DateTimeOffset_DateAdd_AddMilliseconds(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task DateTimeOffset_DateAdd_AddMinutes(bool isAsync)
        {
            return base.DateTimeOffset_DateAdd_AddMinutes(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task DateTimeOffset_DateAdd_AddMonths(bool isAsync)
        {
            return base.DateTimeOffset_DateAdd_AddMonths(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task DateTimeOffset_DateAdd_AddSeconds(bool isAsync)
        {
            return base.DateTimeOffset_DateAdd_AddSeconds(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task DateTimeOffset_DateAdd_AddYears(bool isAsync)
        {
            return base.DateTimeOffset_DateAdd_AddYears(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Where_datetimeoffset_date_component(bool isAsync)
        {
            return base.Where_datetimeoffset_date_component(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Where_datetimeoffset_dayofyear_component(bool isAsync)
        {
            return base.Where_datetimeoffset_dayofyear_component(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Where_datetimeoffset_day_component(bool isAsync)
        {
            return base.Where_datetimeoffset_day_component(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Where_datetimeoffset_hour_component(bool isAsync)
        {
            return base.Where_datetimeoffset_hour_component(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Where_datetimeoffset_millisecond_component(bool isAsync)
        {
            return base.Where_datetimeoffset_millisecond_component(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Where_datetimeoffset_minute_component(bool isAsync)
        {
            return base.Where_datetimeoffset_minute_component(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Where_datetimeoffset_month_component(bool isAsync)
        {
            return base.Where_datetimeoffset_month_component(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Where_datetimeoffset_now(bool isAsync)
        {
            return base.Where_datetimeoffset_now(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Where_datetimeoffset_second_component(bool isAsync)
        {
            return base.Where_datetimeoffset_second_component(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Where_datetimeoffset_utcnow(bool isAsync)
        {
            return base.Where_datetimeoffset_utcnow(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task GetValueOrDefault_on_DateTimeOffset(bool isAsync)
        {
            return base.GetValueOrDefault_on_DateTimeOffset(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Time_of_day_datetimeoffset(bool isAsync)
        {
            return base.Time_of_day_datetimeoffset(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Where_datetimeoffset_year_component(bool isAsync)
        {
            return base.Where_datetimeoffset_year_component(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Where_subquery_distinct_orderby_firstordefault_boolean(bool isAsync)
        {
            return base.Where_subquery_distinct_orderby_firstordefault_boolean(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Select_subquery_boolean_with_pushdown(bool isAsync)
        {
            return base.Select_subquery_boolean_with_pushdown(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Select_subquery_int_with_pushdown_and_coalesce(bool isAsync)
        {
            return base.Select_subquery_int_with_pushdown_and_coalesce(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Where_enum_has_flag_subquery_with_pushdown(bool isAsync)
        {
            return base.Where_enum_has_flag_subquery_with_pushdown(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Select_subquery_projecting_single_constant_int(bool isAsync)
        {
            return base.Select_subquery_projecting_single_constant_int(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Correlated_collection_with_complex_order_by_funcletized_to_constant_bool(bool isAsync)
        {
            return base.Correlated_collection_with_complex_order_by_funcletized_to_constant_bool(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Select_subquery_int_with_outside_cast_and_coalesce(bool isAsync)
        {
            return base.Select_subquery_int_with_outside_cast_and_coalesce(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Select_subquery_distinct_firstordefault(bool isAsync)
        {
            return base.Select_subquery_distinct_firstordefault(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Select_subquery_boolean_empty_with_pushdown(bool isAsync)
        {
            return base.Select_subquery_boolean_empty_with_pushdown(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Select_subquery_projecting_single_constant_string(bool isAsync)
        {
            return base.Select_subquery_projecting_single_constant_string(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Where_subquery_distinct_firstordefault_boolean(bool isAsync)
        {
            return base.Where_subquery_distinct_firstordefault_boolean(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Include_collection_OrderBy_aggregate(bool isAsync)
        {
            return base.Include_collection_OrderBy_aggregate(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Include_collection_with_complex_OrderBy2(bool isAsync)
        {
            return base.Include_collection_with_complex_OrderBy2(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Query_with_complex_let_containing_ordering_and_filter_projecting_firstOrDefefault_element_of_let(bool isAsync)
        {
            return base.Query_with_complex_let_containing_ordering_and_filter_projecting_firstOrDefefault_element_of_let(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Where_enum_has_flag_subquery(bool isAsync)
        {
            return base.Where_enum_has_flag_subquery(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Select_subquery_projecting_single_constant_bool(bool isAsync)
        {
            return base.Select_subquery_projecting_single_constant_bool(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Filter_on_subquery_projecting_one_value_type_from_empty_collection(bool isAsync)
        {
            return base.Filter_on_subquery_projecting_one_value_type_from_empty_collection(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Select_subquery_boolean(bool isAsync)
        {
            return base.Select_subquery_boolean(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Filter_with_compex_predicate_containig_subquery(bool isAsync)
        {
            return base.Filter_with_compex_predicate_containig_subquery(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Correlated_collection_with_complex_OrderBy(bool isAsync)
        {
            return base.Correlated_collection_with_complex_OrderBy(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Correlated_collection_with_very_complex_order_by(bool isAsync)
        {
            return base.Correlated_collection_with_very_complex_order_by(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Project_one_value_type_from_empty_collection(bool isAsync)
        {
            return base.Project_one_value_type_from_empty_collection(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Include_collection_with_complex_OrderBy3(bool isAsync)
        {
            return base.Include_collection_with_complex_OrderBy3(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Where_subquery_boolean(bool isAsync)
        {
            return base.Where_subquery_boolean(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Correlated_collections_with_FirstOrDefault(bool isAsync)
        {
            return base.Correlated_collections_with_FirstOrDefault(isAsync);
        }

        [Theory(Skip = "SQLCE limitation")]
        public override Task Select_subquery_boolean_empty(bool isAsync)
        {
            return base.Select_subquery_boolean_empty(isAsync);
        }

        public override void Property_access_on_derived_entity_using_cast()
        {
            base.Property_access_on_derived_entity_using_cast();

            AssertSql(
                @"SELECT [f].[Name], [f].[Eradicated]
FROM [Factions] AS [f]
WHERE ([f].[Discriminator] = N'LocustHorde') AND ([f].[Discriminator] = N'LocustHorde')
ORDER BY [f].[Name]");
        }

        public override void Navigation_access_on_derived_entity_using_cast()
        {
            base.Navigation_access_on_derived_entity_using_cast();

            AssertSql(
                @"SELECT [f].[Name], [t].[ThreatLevel] AS [Threat]
FROM [Factions] AS [f]
LEFT JOIN (
    SELECT [f.Commander].*
    FROM [LocustLeaders] AS [f.Commander]
    WHERE [f.Commander].[Discriminator] = N'LocustCommander'
) AS [t] ON ([f].[Discriminator] = N'LocustHorde') AND ([f].[CommanderName] = [t].[Name])
WHERE ([f].[Discriminator] = N'LocustHorde') AND ([f].[Discriminator] = N'LocustHorde')
ORDER BY [f].[Name]");
        }

        public override void Navigation_access_on_derived_materialized_entity_using_cast()
        {
            base.Navigation_access_on_derived_materialized_entity_using_cast();

            AssertSql(
                @"SELECT [f].[Id], [f].[CapitalName], [f].[Discriminator], [f].[Name], [f].[CommanderName], [f].[Eradicated], [t].[ThreatLevel]
FROM [Factions] AS [f]
LEFT JOIN (
    SELECT [f.Commander].*
    FROM [LocustLeaders] AS [f.Commander]
    WHERE [f.Commander].[Discriminator] = N'LocustCommander'
) AS [t] ON ([f].[Discriminator] = N'LocustHorde') AND ([f].[CommanderName] = [t].[Name])
WHERE ([f].[Discriminator] = N'LocustHorde') AND ([f].[Discriminator] = N'LocustHorde')
ORDER BY [f].[Name]");
        }

        public override void Navigation_access_via_EFProperty_on_derived_entity_using_cast()
        {
            base.Navigation_access_via_EFProperty_on_derived_entity_using_cast();

            AssertSql(
                @"SELECT [f].[Name], [t].[ThreatLevel] AS [Threat]
FROM [Factions] AS [f]
LEFT JOIN (
    SELECT [f.Commander].*
    FROM [LocustLeaders] AS [f.Commander]
    WHERE [f.Commander].[Discriminator] = N'LocustCommander'
) AS [t] ON ([f].[Discriminator] = N'LocustHorde') AND ([f].[CommanderName] = [t].[Name])
WHERE ([f].[Discriminator] = N'LocustHorde') AND ([f].[Discriminator] = N'LocustHorde')
ORDER BY [f].[Name]");
        }

        public override void Navigation_access_fk_on_derived_entity_using_cast()
        {
            base.Navigation_access_fk_on_derived_entity_using_cast();

            AssertSql(
                @"SELECT [f].[Name], [f].[CommanderName]
FROM [Factions] AS [f]
WHERE ([f].[Discriminator] = N'LocustHorde') AND ([f].[Discriminator] = N'LocustHorde')
ORDER BY [f].[Name]");
        }

        [Fact(Skip = "SQLCE limitation")]
        public override void Collection_navigation_access_on_derived_entity_using_cast()
        {
            base.Collection_navigation_access_on_derived_entity_using_cast();

            AssertSql(
                @"SELECT [f].[Name], (
    SELECT COUNT(*)
    FROM [LocustLeaders] AS [l]
    WHERE [l].[Discriminator] IN (N'LocustCommander', N'LocustLeader') AND ([f].[Id] = [l].[LocustHordeId])
) AS [LeadersCount]
FROM [Factions] AS [f]
WHERE ([f].[Discriminator] = N'LocustHorde') AND ([f].[Discriminator] = N'LocustHorde')
ORDER BY [f].[Name]");
        }

        public override void Collection_navigation_access_on_derived_entity_using_cast_in_SelectMany()
        {
            base.Collection_navigation_access_on_derived_entity_using_cast_in_SelectMany();

            AssertSql(
                @"SELECT [f].[Name] AS [Name0], [f.Leaders].[Name] AS [LeaderName]
FROM [Factions] AS [f]
INNER JOIN [LocustLeaders] AS [f.Leaders] ON [f].[Id] = [f.Leaders].[LocustHordeId]
WHERE (([f].[Discriminator] = N'LocustHorde') AND ([f].[Discriminator] = N'LocustHorde')) AND [f.Leaders].[Discriminator] IN (N'LocustCommander', N'LocustLeader')
ORDER BY [LeaderName]");
        }

        public override void Include_on_derived_entity_using_OfType()
        {
            base.Include_on_derived_entity_using_OfType();

            AssertSql(
                @"SELECT [lh].[Id], [lh].[CapitalName], [lh].[Discriminator], [lh].[Name], [lh].[CommanderName], [lh].[Eradicated], [t].[Name], [t].[Discriminator], [t].[LocustHordeId], [t].[ThreatLevel], [t].[DefeatedByNickname], [t].[DefeatedBySquadId], [t].[HighCommandId]
FROM [Factions] AS [lh]
LEFT JOIN (
    SELECT [lh.Commander].*
    FROM [LocustLeaders] AS [lh.Commander]
    WHERE [lh.Commander].[Discriminator] = N'LocustCommander'
) AS [t] ON [lh].[CommanderName] = [t].[Name]
WHERE [lh].[Discriminator] = N'LocustHorde'
ORDER BY [lh].[Name], [lh].[Id]",
                //
                @"SELECT [lh.Leaders].[Name], [lh.Leaders].[Discriminator], [lh.Leaders].[LocustHordeId], [lh.Leaders].[ThreatLevel], [lh.Leaders].[DefeatedByNickname], [lh.Leaders].[DefeatedBySquadId], [lh.Leaders].[HighCommandId]
FROM [LocustLeaders] AS [lh.Leaders]
INNER JOIN (
    SELECT DISTINCT [lh0].[Id], [lh0].[Name]
    FROM [Factions] AS [lh0]
    LEFT JOIN (
        SELECT [lh.Commander0].*
        FROM [LocustLeaders] AS [lh.Commander0]
        WHERE [lh.Commander0].[Discriminator] = N'LocustCommander'
    ) AS [t0] ON [lh0].[CommanderName] = [t0].[Name]
    WHERE [lh0].[Discriminator] = N'LocustHorde'
) AS [t1] ON [lh.Leaders].[LocustHordeId] = [t1].[Id]
WHERE [lh.Leaders].[Discriminator] IN (N'LocustCommander', N'LocustLeader')
ORDER BY [t1].[Name], [t1].[Id]");
        }

        public override void Include_on_derived_entity_using_subquery_with_cast()
        {
            base.Include_on_derived_entity_using_subquery_with_cast();

            AssertSql(
                @"SELECT [f].[Id], [f].[CapitalName], [f].[Discriminator], [f].[Name], [f].[CommanderName], [f].[Eradicated], [t].[Name], [t].[Discriminator], [t].[LocustHordeId], [t].[ThreatLevel], [t].[DefeatedByNickname], [t].[DefeatedBySquadId], [t].[HighCommandId]
FROM [Factions] AS [f]
LEFT JOIN (
    SELECT [f.Commander].*
    FROM [LocustLeaders] AS [f.Commander]
    WHERE [f.Commander].[Discriminator] = N'LocustCommander'
) AS [t] ON ([f].[Discriminator] = N'LocustHorde') AND ([f].[CommanderName] = [t].[Name])
WHERE ([f].[Discriminator] = N'LocustHorde') AND ([f].[Discriminator] = N'LocustHorde')
ORDER BY [f].[Name], [f].[Id]",
                //
                @"SELECT [f.Leaders].[Name], [f.Leaders].[Discriminator], [f.Leaders].[LocustHordeId], [f.Leaders].[ThreatLevel], [f.Leaders].[DefeatedByNickname], [f.Leaders].[DefeatedBySquadId], [f.Leaders].[HighCommandId]
FROM [LocustLeaders] AS [f.Leaders]
INNER JOIN (
    SELECT DISTINCT [f0].[Id], [f0].[Name]
    FROM [Factions] AS [f0]
    LEFT JOIN (
        SELECT [f.Commander0].*
        FROM [LocustLeaders] AS [f.Commander0]
        WHERE [f.Commander0].[Discriminator] = N'LocustCommander'
    ) AS [t0] ON ([f0].[Discriminator] = N'LocustHorde') AND ([f0].[CommanderName] = [t0].[Name])
    WHERE ([f0].[Discriminator] = N'LocustHorde') AND ([f0].[Discriminator] = N'LocustHorde')
) AS [t1] ON [f.Leaders].[LocustHordeId] = [t1].[Id]
WHERE [f.Leaders].[Discriminator] IN (N'LocustCommander', N'LocustLeader')
ORDER BY [t1].[Name], [t1].[Id]");
        }

        public override void Include_on_derived_entity_using_subquery_with_cast_AsNoTracking()
        {
            base.Include_on_derived_entity_using_subquery_with_cast_AsNoTracking();

            AssertSql(
                @"SELECT [f].[Id], [f].[CapitalName], [f].[Discriminator], [f].[Name], [f].[CommanderName], [f].[Eradicated], [t].[Name], [t].[Discriminator], [t].[LocustHordeId], [t].[ThreatLevel], [t].[DefeatedByNickname], [t].[DefeatedBySquadId], [t].[HighCommandId]
FROM [Factions] AS [f]
LEFT JOIN (
    SELECT [f.Commander].*
    FROM [LocustLeaders] AS [f.Commander]
    WHERE [f.Commander].[Discriminator] = N'LocustCommander'
) AS [t] ON ([f].[Discriminator] = N'LocustHorde') AND ([f].[CommanderName] = [t].[Name])
WHERE ([f].[Discriminator] = N'LocustHorde') AND ([f].[Discriminator] = N'LocustHorde')
ORDER BY [f].[Name], [f].[Id]",
                //
                @"SELECT [f.Leaders].[Name], [f.Leaders].[Discriminator], [f.Leaders].[LocustHordeId], [f.Leaders].[ThreatLevel], [f.Leaders].[DefeatedByNickname], [f.Leaders].[DefeatedBySquadId], [f.Leaders].[HighCommandId]
FROM [LocustLeaders] AS [f.Leaders]
INNER JOIN (
    SELECT DISTINCT [f0].[Id], [f0].[Name]
    FROM [Factions] AS [f0]
    LEFT JOIN (
        SELECT [f.Commander0].*
        FROM [LocustLeaders] AS [f.Commander0]
        WHERE [f.Commander0].[Discriminator] = N'LocustCommander'
    ) AS [t0] ON ([f0].[Discriminator] = N'LocustHorde') AND ([f0].[CommanderName] = [t0].[Name])
    WHERE ([f0].[Discriminator] = N'LocustHorde') AND ([f0].[Discriminator] = N'LocustHorde')
) AS [t1] ON [f.Leaders].[LocustHordeId] = [t1].[Id]
WHERE [f.Leaders].[Discriminator] IN (N'LocustCommander', N'LocustLeader')
ORDER BY [t1].[Name], [t1].[Id]");
        }

        public override void Include_on_derived_entity_using_subquery_with_cast_cross_product_base_entity()
        {
            base.Include_on_derived_entity_using_subquery_with_cast_cross_product_base_entity();

            AssertSql(
                @"SELECT [f2].[Id], [f2].[CapitalName], [f2].[Discriminator], [f2].[Name], [f2].[CommanderName], [f2].[Eradicated], [t].[Name], [t].[Discriminator], [t].[LocustHordeId], [t].[ThreatLevel], [t].[DefeatedByNickname], [t].[DefeatedBySquadId], [t].[HighCommandId], [ff].[Id], [ff].[CapitalName], [ff].[Discriminator], [ff].[Name], [ff].[CommanderName], [ff].[Eradicated], [ff.Capital].[Name], [ff.Capital].[Location]
FROM [Factions] AS [f2]
LEFT JOIN (
    SELECT [f2.Commander].*
    FROM [LocustLeaders] AS [f2.Commander]
    WHERE [f2.Commander].[Discriminator] = N'LocustCommander'
) AS [t] ON ([f2].[Discriminator] = N'LocustHorde') AND ([f2].[CommanderName] = [t].[Name])
CROSS JOIN [Factions] AS [ff]
LEFT JOIN [Cities] AS [ff.Capital] ON [ff].[CapitalName] = [ff.Capital].[Name]
WHERE ([f2].[Discriminator] = N'LocustHorde') AND ([f2].[Discriminator] = N'LocustHorde')
ORDER BY [f2].[Name], [ff].[Name], [f2].[Id]",
                //
                @"SELECT [f2.Leaders].[Name], [f2.Leaders].[Discriminator], [f2.Leaders].[LocustHordeId], [f2.Leaders].[ThreatLevel], [f2.Leaders].[DefeatedByNickname], [f2.Leaders].[DefeatedBySquadId], [f2.Leaders].[HighCommandId]
FROM [LocustLeaders] AS [f2.Leaders]
INNER JOIN (
    SELECT DISTINCT [f20].[Id], [f20].[Name], [ff0].[Name] AS [Name0]
    FROM [Factions] AS [f20]
    LEFT JOIN (
        SELECT [f2.Commander0].*
        FROM [LocustLeaders] AS [f2.Commander0]
        WHERE [f2.Commander0].[Discriminator] = N'LocustCommander'
    ) AS [t0] ON ([f20].[Discriminator] = N'LocustHorde') AND ([f20].[CommanderName] = [t0].[Name])
    CROSS JOIN [Factions] AS [ff0]
    LEFT JOIN [Cities] AS [ff.Capital0] ON [ff0].[CapitalName] = [ff.Capital0].[Name]
    WHERE ([f20].[Discriminator] = N'LocustHorde') AND ([f20].[Discriminator] = N'LocustHorde')
) AS [t1] ON [f2.Leaders].[LocustHordeId] = [t1].[Id]
WHERE [f2.Leaders].[Discriminator] IN (N'LocustCommander', N'LocustLeader')
ORDER BY [t1].[Name], [t1].[Name0], [t1].[Id]");
        }

        public override void Distinct_on_subquery_doesnt_get_lifted()
        {
            base.Distinct_on_subquery_doesnt_get_lifted();

            AssertSql(
                @"SELECT [t].[HasSoulPatch]
FROM (
    SELECT DISTINCT [ig].*
    FROM [Gears] AS [ig]
    WHERE [ig].[Discriminator] IN (N'Officer', N'Gear')
) AS [t]");
        }

        public override void Cast_result_operator_on_subquery_is_properly_lifted_to_a_convert()
        {
            base.Cast_result_operator_on_subquery_is_properly_lifted_to_a_convert();

            AssertSql(
                @"SELECT [f].[Eradicated]
FROM [Factions] AS [f]
WHERE [f].[Discriminator] = N'LocustHorde'");
        }

        public override void Comparing_two_collection_navigations_composite_key()
        {
            base.Comparing_two_collection_navigations_composite_key();

            AssertSql(
                @"SELECT [g1].[Nickname] AS [Nickname1], [g2].[Nickname] AS [Nickname2]
FROM [Gears] AS [g1]
CROSS JOIN [Gears] AS [g2]
WHERE [g1].[Discriminator] IN (N'Officer', N'Gear') AND (([g1].[Nickname] = [g2].[Nickname]) AND ([g1].[SquadId] = [g2].[SquadId]))
ORDER BY [Nickname1]");
        }

        public override void Comparing_two_collection_navigations_inheritance()
        {
            base.Comparing_two_collection_navigations_inheritance();

            AssertSql(
                @"SELECT [f].[Name], [o].[Nickname]
FROM [Factions] AS [f]
LEFT JOIN (
    SELECT [f.Commander].*
    FROM [LocustLeaders] AS [f.Commander]
    WHERE [f.Commander].[Discriminator] = N'LocustCommander'
) AS [t] ON ([f].[Discriminator] = N'LocustHorde') AND ([f].[CommanderName] = [t].[Name])
LEFT JOIN (
    SELECT [f.Commander.DefeatedBy].*
    FROM [Gears] AS [f.Commander.DefeatedBy]
    WHERE [f.Commander.DefeatedBy].[Discriminator] IN (N'Officer', N'Gear')
) AS [t0] ON ([t].[DefeatedByNickname] = [t0].[Nickname]) AND ([t].[DefeatedBySquadId] = [t0].[SquadId])
CROSS JOIN [Gears] AS [o]
WHERE (([f].[Discriminator] = N'LocustHorde') AND (([f].[Discriminator] = N'LocustHorde') AND ([o].[HasSoulPatch] = 1))) AND (([t0].[Nickname] = [o].[Nickname]) AND ([t0].[SquadId] = [o].[SquadId]))");
        }

        public override void Comparing_entities_using_Equals_inheritance()
        {
            base.Comparing_entities_using_Equals_inheritance();

            AssertSql(
                "");
        }

        public override void Contains_on_nullable_array_produces_correct_sql()
        {
            base.Contains_on_nullable_array_produces_correct_sql();

            AssertSql(
                @"SELECT [g].[Nickname], [g].[SquadId], [g].[AssignedCityName], [g].[CityOrBirthName], [g].[Discriminator], [g].[FullName], [g].[HasSoulPatch], [g].[LeaderNickname], [g].[LeaderSquadId], [g].[Rank]
FROM [Gears] AS [g]
WHERE [g].[Discriminator] IN (N'Officer', N'Gear') AND (([g].[SquadId] < 2) AND ([g].[AssignedCityName] IN (N'Ephyra') OR [g].[AssignedCityName] IS NULL))");
        }

        [Fact(Skip = "SQLCE limitation")]
        public override void Optional_navigation_with_collection_composite_key()
        {
            base.Optional_navigation_with_collection_composite_key();

            AssertSql(
                @"SELECT [t].[Id], [t].[GearNickName], [t].[GearSquadId], [t].[Note]
FROM [Tags] AS [t]
LEFT JOIN (
    SELECT [t.Gear].*
    FROM [Gears] AS [t.Gear]
    WHERE [t.Gear].[Discriminator] IN (N'Officer', N'Gear')
) AS [t0] ON ([t].[GearNickName] = [t0].[Nickname]) AND ([t].[GearSquadId] = [t0].[SquadId])
WHERE ([t0].[Discriminator] = N'Officer') AND ((
    SELECT COUNT(*)
    FROM [Gears] AS [r]
    WHERE ([r].[Discriminator] IN (N'Officer', N'Gear') AND ([r].[Nickname] = N'Dom')) AND (([t0].[Nickname] = [r].[LeaderNickname]) AND ([t0].[SquadId] = [r].[LeaderSquadId]))
) > 0)");
        }

        public override void Select_null_conditional_with_inheritance()
        {
            base.Select_null_conditional_with_inheritance();

            AssertSql(
                @"SELECT [f].[CommanderName]
FROM [Factions] AS [f]
WHERE ([f].[Discriminator] = N'LocustHorde') AND ([f].[Discriminator] = N'LocustHorde')");
        }

        public override void Select_null_conditional_with_inheritance_negative()
        {
            base.Select_null_conditional_with_inheritance_negative();

            AssertSql(
                @"SELECT CASE
    WHEN [f].[CommanderName] IS NOT NULL
    THEN [f].[Eradicated] ELSE NULL
END
FROM [Factions] AS [f]
WHERE ([f].[Discriminator] = N'LocustHorde') AND ([f].[Discriminator] = N'LocustHorde')");
        }

        public override void Project_collection_navigation_with_inheritance1()
        {
            base.Project_collection_navigation_with_inheritance1();

            AssertSql(
                @"SELECT [h].[Id], [t0].[Id]
FROM [Factions] AS [h]
LEFT JOIN (
    SELECT [h.Commander].*
    FROM [LocustLeaders] AS [h.Commander]
    WHERE [h.Commander].[Discriminator] = N'LocustCommander'
) AS [t] ON [h].[CommanderName] = [t].[Name]
LEFT JOIN (
    SELECT [h.Commander.CommandingFaction].*
    FROM [Factions] AS [h.Commander.CommandingFaction]
    WHERE [h.Commander.CommandingFaction].[Discriminator] = N'LocustHorde'
) AS [t0] ON [t].[Name] = [t0].[CommanderName]
WHERE [h].[Discriminator] = N'LocustHorde'
ORDER BY [h].[Id], [t0].[Id]",
                //
                @"SELECT [h.Commander.CommandingFaction.Leaders].[Name], [h.Commander.CommandingFaction.Leaders].[Discriminator], [h.Commander.CommandingFaction.Leaders].[LocustHordeId], [h.Commander.CommandingFaction.Leaders].[ThreatLevel], [h.Commander.CommandingFaction.Leaders].[DefeatedByNickname], [h.Commander.CommandingFaction.Leaders].[DefeatedBySquadId], [h.Commander.CommandingFaction.Leaders].[HighCommandId], [t3].[Id], [t3].[Id0]
FROM [LocustLeaders] AS [h.Commander.CommandingFaction.Leaders]
INNER JOIN (
    SELECT [h0].[Id], [t2].[Id] AS [Id0]
    FROM [Factions] AS [h0]
    LEFT JOIN (
        SELECT [h.Commander0].*
        FROM [LocustLeaders] AS [h.Commander0]
        WHERE [h.Commander0].[Discriminator] = N'LocustCommander'
    ) AS [t1] ON [h0].[CommanderName] = [t1].[Name]
    LEFT JOIN (
        SELECT [h.Commander.CommandingFaction0].*
        FROM [Factions] AS [h.Commander.CommandingFaction0]
        WHERE [h.Commander.CommandingFaction0].[Discriminator] = N'LocustHorde'
    ) AS [t2] ON [t1].[Name] = [t2].[CommanderName]
    WHERE [h0].[Discriminator] = N'LocustHorde'
) AS [t3] ON [h.Commander.CommandingFaction.Leaders].[LocustHordeId] = [t3].[Id]
WHERE [h.Commander.CommandingFaction.Leaders].[Discriminator] IN (N'LocustCommander', N'LocustLeader')
ORDER BY [t3].[Id], [t3].[Id0]");
        }

        public override void Project_collection_navigation_with_inheritance2()
        {
            base.Project_collection_navigation_with_inheritance2();

            AssertSql(
                @"SELECT [h].[Id], [t0].[Nickname], [t0].[SquadId]
FROM [Factions] AS [h]
LEFT JOIN (
    SELECT [h.Commander].*
    FROM [LocustLeaders] AS [h.Commander]
    WHERE [h.Commander].[Discriminator] = N'LocustCommander'
) AS [t] ON [h].[CommanderName] = [t].[Name]
LEFT JOIN (
    SELECT [h.Commander.DefeatedBy].*
    FROM [Gears] AS [h.Commander.DefeatedBy]
    WHERE [h.Commander.DefeatedBy].[Discriminator] IN (N'Officer', N'Gear')
) AS [t0] ON ([t].[DefeatedByNickname] = [t0].[Nickname]) AND ([t].[DefeatedBySquadId] = [t0].[SquadId])
WHERE [h].[Discriminator] = N'LocustHorde'
ORDER BY [h].[Id], [t0].[Nickname], [t0].[SquadId]",
                //
                @"SELECT [h.Commander.DefeatedBy.Reports].[Nickname], [h.Commander.DefeatedBy.Reports].[SquadId], [h.Commander.DefeatedBy.Reports].[AssignedCityName], [h.Commander.DefeatedBy.Reports].[CityOrBirthName], [h.Commander.DefeatedBy.Reports].[Discriminator], [h.Commander.DefeatedBy.Reports].[FullName], [h.Commander.DefeatedBy.Reports].[HasSoulPatch], [h.Commander.DefeatedBy.Reports].[LeaderNickname], [h.Commander.DefeatedBy.Reports].[LeaderSquadId], [h.Commander.DefeatedBy.Reports].[Rank], [t3].[Id], [t3].[Nickname], [t3].[SquadId]
FROM [Gears] AS [h.Commander.DefeatedBy.Reports]
INNER JOIN (
    SELECT [h0].[Id], [t2].[Nickname], [t2].[SquadId]
    FROM [Factions] AS [h0]
    LEFT JOIN (
        SELECT [h.Commander0].*
        FROM [LocustLeaders] AS [h.Commander0]
        WHERE [h.Commander0].[Discriminator] = N'LocustCommander'
    ) AS [t1] ON [h0].[CommanderName] = [t1].[Name]
    LEFT JOIN (
        SELECT [h.Commander.DefeatedBy0].*
        FROM [Gears] AS [h.Commander.DefeatedBy0]
        WHERE [h.Commander.DefeatedBy0].[Discriminator] IN (N'Officer', N'Gear')
    ) AS [t2] ON ([t1].[DefeatedByNickname] = [t2].[Nickname]) AND ([t1].[DefeatedBySquadId] = [t2].[SquadId])
    WHERE [h0].[Discriminator] = N'LocustHorde'
) AS [t3] ON ([h.Commander.DefeatedBy.Reports].[LeaderNickname] = [t3].[Nickname]) AND ([h.Commander.DefeatedBy.Reports].[LeaderSquadId] = [t3].[SquadId])
WHERE [h.Commander.DefeatedBy.Reports].[Discriminator] IN (N'Officer', N'Gear')
ORDER BY [t3].[Id], [t3].[Nickname], [t3].[SquadId]");
        }

        public override void Project_collection_navigation_with_inheritance3()
        {
            base.Project_collection_navigation_with_inheritance3();

            AssertSql(
                @"SELECT [f].[Id], [t0].[Nickname], [t0].[SquadId]
FROM [Factions] AS [f]
LEFT JOIN (
    SELECT [f.Commander].*
    FROM [LocustLeaders] AS [f.Commander]
    WHERE [f.Commander].[Discriminator] = N'LocustCommander'
) AS [t] ON ([f].[Discriminator] = N'LocustHorde') AND ([f].[CommanderName] = [t].[Name])
LEFT JOIN (
    SELECT [f.Commander.DefeatedBy].*
    FROM [Gears] AS [f.Commander.DefeatedBy]
    WHERE [f.Commander.DefeatedBy].[Discriminator] IN (N'Officer', N'Gear')
) AS [t0] ON ([t].[DefeatedByNickname] = [t0].[Nickname]) AND ([t].[DefeatedBySquadId] = [t0].[SquadId])
WHERE ([f].[Discriminator] = N'LocustHorde') AND ([f].[Discriminator] = N'LocustHorde')
ORDER BY [f].[Id], [t0].[Nickname], [t0].[SquadId]",
                //
                @"SELECT [f.Commander.DefeatedBy.Reports].[Nickname], [f.Commander.DefeatedBy.Reports].[SquadId], [f.Commander.DefeatedBy.Reports].[AssignedCityName], [f.Commander.DefeatedBy.Reports].[CityOrBirthName], [f.Commander.DefeatedBy.Reports].[Discriminator], [f.Commander.DefeatedBy.Reports].[FullName], [f.Commander.DefeatedBy.Reports].[HasSoulPatch], [f.Commander.DefeatedBy.Reports].[LeaderNickname], [f.Commander.DefeatedBy.Reports].[LeaderSquadId], [f.Commander.DefeatedBy.Reports].[Rank], [t3].[Id], [t3].[Nickname], [t3].[SquadId]
FROM [Gears] AS [f.Commander.DefeatedBy.Reports]
INNER JOIN (
    SELECT [f0].[Id], [t2].[Nickname], [t2].[SquadId]
    FROM [Factions] AS [f0]
    LEFT JOIN (
        SELECT [f.Commander0].*
        FROM [LocustLeaders] AS [f.Commander0]
        WHERE [f.Commander0].[Discriminator] = N'LocustCommander'
    ) AS [t1] ON ([f0].[Discriminator] = N'LocustHorde') AND ([f0].[CommanderName] = [t1].[Name])
    LEFT JOIN (
        SELECT [f.Commander.DefeatedBy0].*
        FROM [Gears] AS [f.Commander.DefeatedBy0]
        WHERE [f.Commander.DefeatedBy0].[Discriminator] IN (N'Officer', N'Gear')
    ) AS [t2] ON ([t1].[DefeatedByNickname] = [t2].[Nickname]) AND ([t1].[DefeatedBySquadId] = [t2].[SquadId])
    WHERE ([f0].[Discriminator] = N'LocustHorde') AND ([f0].[Discriminator] = N'LocustHorde')
) AS [t3] ON ([f.Commander.DefeatedBy.Reports].[LeaderNickname] = [t3].[Nickname]) AND ([f.Commander.DefeatedBy.Reports].[LeaderSquadId] = [t3].[SquadId])
WHERE [f.Commander.DefeatedBy.Reports].[Discriminator] IN (N'Officer', N'Gear')
ORDER BY [t3].[Id], [t3].[Nickname], [t3].[SquadId]");
        }

        private void AssertSql(params string[] expected)
            => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);
    }
}
