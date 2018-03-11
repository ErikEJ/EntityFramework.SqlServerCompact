Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Optional_navigation_with_Include_ThenInclude() : line 1986
            AssertSql(
                @"SELECT [l1.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK].[Date], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
ORDER BY [l1.OneToOne_Optional_FK].[Id]",
                //
                @"SELECT [l1.OneToOne_Optional_FK.OneToMany_Optional].[Id], [l1.OneToOne_Optional_FK.OneToMany_Optional].[Level2_Optional_Id], [l1.OneToOne_Optional_FK.OneToMany_Optional].[Level2_Required_Id], [l1.OneToOne_Optional_FK.OneToMany_Optional].[Name], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToOne_Optional_SelfId], [l.OneToOne_Optional_FK].[Id], [l.OneToOne_Optional_FK].[Level3_Optional_Id], [l.OneToOne_Optional_FK].[Level3_Required_Id], [l.OneToOne_Optional_FK].[Name], [l.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l1.OneToOne_Optional_FK.OneToMany_Optional]
LEFT JOIN [LevelFour] AS [l.OneToOne_Optional_FK] ON [l1.OneToOne_Optional_FK.OneToMany_Optional].[Id] = [l.OneToOne_Optional_FK].[Level3_Optional_Id]
INNER JOIN (
    SELECT DISTINCT [l1.OneToOne_Optional_FK0].[Id]
    FROM [LevelOne] AS [l10]
    LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK0] ON [l10].[Id] = [l1.OneToOne_Optional_FK0].[Level1_Optional_Id]
) AS [t] ON [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
ORDER BY [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Key_equality_using_property_method_and_member_expression2() : line 104
            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l.OneToOne_Required_FK] ON [l].[Id] = [l.OneToOne_Required_FK].[Level1_Required_Id]
WHERE [l.OneToOne_Required_FK].[Id] = 7");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Select_nav_prop_reference_optional2_via_DefaultIfEmpty() : line 662
            AssertSql(
                @"SELECT [l2].[Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_nested_navigation_property_optional_and_projection() : line 1205
            AssertSql(
                @"SELECT [l1.OneToOne_Optional_FK.OneToMany_Optional].[Name]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
INNER JOIN [LevelThree] AS [l1.OneToOne_Optional_FK.OneToMany_Optional] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Complex_multi_include_with_order_by_and_paging_joins_on_correct_key2() : line 1443
            AssertSql(
                @"@__p_0='0'
@__p_1='10'

SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK].[Date], [e.OneToOne_Optional_FK].[Level1_Optional_Id], [e.OneToOne_Optional_FK].[Level1_Required_Id], [e.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[Id], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[Level2_Optional_Id], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[Level2_Required_Id], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[Name], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [e.OneToOne_Optional_FK.OneToOne_Required_FK] ON [e.OneToOne_Optional_FK].[Id] = [e.OneToOne_Optional_FK.OneToOne_Required_FK].[Level2_Required_Id]
ORDER BY [e].[Name], [e.OneToOne_Optional_FK.OneToOne_Required_FK].[Id]
OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY",
                //
                @"@__p_0='0'
@__p_1='10'

SELECT [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToMany_Optional].[Id], [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToMany_Optional].[Level3_Optional_Id], [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToMany_Optional].[Level3_Required_Id], [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToMany_Optional].[Name], [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelFour] AS [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToMany_Optional]
INNER JOIN (
    SELECT DISTINCT [t].*
    FROM (
        SELECT [e.OneToOne_Optional_FK.OneToOne_Required_FK0].[Id], [e0].[Name]
        FROM [LevelOne] AS [e0]
        LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK0] ON [e0].[Id] = [e.OneToOne_Optional_FK0].[Level1_Optional_Id]
        LEFT JOIN [LevelThree] AS [e.OneToOne_Optional_FK.OneToOne_Required_FK0] ON [e.OneToOne_Optional_FK0].[Id] = [e.OneToOne_Optional_FK.OneToOne_Required_FK0].[Level2_Required_Id]
        ORDER BY [e0].[Name], [e.OneToOne_Optional_FK.OneToOne_Required_FK0].[Id]
        OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY
    ) AS [t]
) AS [t0] ON [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t0].[Id]
ORDER BY [t0].[Name], [t0].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Project_collection_and_root_entity() : line 3089
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
ORDER BY [l1].[Id]",
                //
                @"SELECT [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId], [t].[Id]
FROM [LevelTwo] AS [l1.OneToMany_Optional]
INNER JOIN (
    SELECT [l10].[Id]
    FROM [LevelOne] AS [l10]
) AS [t] ON [l1.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
ORDER BY [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_navigation_property_with_another_navigation_in_subquery() : line 1227
            AssertSql(
                @"SELECT [l1.OneToMany_Optional.OneToOne_Optional_FK].[Id], [l1.OneToMany_Optional.OneToOne_Optional_FK].[Level2_Optional_Id], [l1.OneToMany_Optional.OneToOne_Optional_FK].[Level2_Required_Id], [l1.OneToMany_Optional.OneToOne_Optional_FK].[Name], [l1.OneToMany_Optional.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
LEFT JOIN [LevelThree] AS [l1.OneToMany_Optional.OneToOne_Optional_FK] ON [l1.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToOne_Optional_FK].[Level2_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Multiple_required_navigation_with_string_based_Include() : line 1951
            AssertSql(
                @"SELECT [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Date], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Level1_Optional_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Level1_Required_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Name], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Level2_Optional_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Level2_Required_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Name], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [LevelFour] AS [l4]
INNER JOIN [LevelThree] AS [l4.OneToOne_Required_FK_Inverse] ON [l4].[Level3_Required_Id] = [l4.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [LevelTwo] AS [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse] ON [l4.OneToOne_Required_FK_Inverse].[Level2_Required_Id] = [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]
LEFT JOIN [LevelThree] AS [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK] ON [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id] = [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Level2_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Result_operator_nav_prop_reference_optional_via_DefaultIfEmpty() : line 1053
            AssertSql(
                @"SELECT SUM(CASE
    WHEN [l2].[Id] IS NULL
    THEN 0 ELSE [l2].[Level1_Required_Id]
END)
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Select_join_with_key_selector_being_a_subquery() : line 2169
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l2] ON [l1].[Id] IN (
    SELECT TOP(1) [l0].[Id]
    FROM [LevelTwo] AS [l0]
    ORDER BY [l0].[Id]
)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Projection_select_correct_table_in_subquery_when_materialization_is_not_required_in_multiple_joins() : line 1765
            AssertSql(
                @"@__p_0='3'

SELECT TOP(@__p_0) [l1].[Name]
FROM [LevelTwo] AS [l2]
INNER JOIN [LevelOne] AS [l1] ON [l2].[Level1_Required_Id] = [l1].[Id]
INNER JOIN [LevelThree] AS [l3] ON [l1].[Id] = [l3].[Level2_Required_Id]
WHERE ([l1].[Name] = N'L1 03') AND ([l3].[Name] = N'L3 08')
ORDER BY [l1].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Multiple_complex_includes() : line 542
            AssertSql(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK].[Date], [e.OneToOne_Optional_FK].[Level1_Optional_Id], [e.OneToOne_Optional_FK].[Level1_Required_Id], [e.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
ORDER BY [e.OneToOne_Optional_FK].[Id], [e].[Id]",
                //
                @"SELECT [e.OneToMany_Optional].[Id], [e.OneToMany_Optional].[Date], [e.OneToMany_Optional].[Level1_Optional_Id], [e.OneToMany_Optional].[Level1_Required_Id], [e.OneToMany_Optional].[Name], [e.OneToMany_Optional].[OneToMany_Optional_InverseId], [e.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [e.OneToMany_Optional].[OneToMany_Required_InverseId], [e.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [e.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [e.OneToMany_Optional].[OneToOne_Optional_SelfId], [l.OneToOne_Optional_FK].[Id], [l.OneToOne_Optional_FK].[Level2_Optional_Id], [l.OneToOne_Optional_FK].[Level2_Required_Id], [l.OneToOne_Optional_FK].[Name], [l.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [e.OneToMany_Optional]
LEFT JOIN [LevelThree] AS [l.OneToOne_Optional_FK] ON [e.OneToMany_Optional].[Id] = [l.OneToOne_Optional_FK].[Level2_Optional_Id]
INNER JOIN (
    SELECT DISTINCT [e1].[Id], [e.OneToOne_Optional_FK1].[Id] AS [Id0]
    FROM [LevelOne] AS [e1]
    LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK1] ON [e1].[Id] = [e.OneToOne_Optional_FK1].[Level1_Optional_Id]
) AS [t0] ON [e.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t0].[Id]
ORDER BY [t0].[Id0], [t0].[Id]",
                //
                @"SELECT [e.OneToOne_Optional_FK.OneToMany_Optional].[Id], [e.OneToOne_Optional_FK.OneToMany_Optional].[Level2_Optional_Id], [e.OneToOne_Optional_FK.OneToMany_Optional].[Level2_Required_Id], [e.OneToOne_Optional_FK.OneToMany_Optional].[Name], [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [e.OneToOne_Optional_FK.OneToMany_Optional]
INNER JOIN (
    SELECT DISTINCT [e.OneToOne_Optional_FK0].[Id]
    FROM [LevelOne] AS [e0]
    LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK0] ON [e0].[Id] = [e.OneToOne_Optional_FK0].[Level1_Optional_Id]
) AS [t] ON [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
ORDER BY [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_with_Include_ThenInclude() : line 1823
            AssertSql(
                @"SELECT [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional.OneToOne_Required_FK].[Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Optional_Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Required_Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Name], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
LEFT JOIN [LevelThree] AS [l1.OneToMany_Optional.OneToOne_Required_FK] ON [l1.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Required_Id]
ORDER BY [l1.OneToMany_Optional.OneToOne_Required_FK].[Id]",
                //
                @"SELECT [l1.OneToMany_Optional.OneToOne_Required_FK.OneToMany_Optional].[Id], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToMany_Optional].[Level3_Optional_Id], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToMany_Optional].[Level3_Required_Id], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToMany_Optional].[Name], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelFour] AS [l1.OneToMany_Optional.OneToOne_Required_FK.OneToMany_Optional]
INNER JOIN (
    SELECT DISTINCT [l1.OneToMany_Optional.OneToOne_Required_FK0].[Id]
    FROM [LevelOne] AS [l10]
    INNER JOIN [LevelTwo] AS [l1.OneToMany_Optional0] ON [l10].[Id] = [l1.OneToMany_Optional0].[OneToMany_Optional_InverseId]
    LEFT JOIN [LevelThree] AS [l1.OneToMany_Optional.OneToOne_Required_FK0] ON [l1.OneToMany_Optional0].[Id] = [l1.OneToMany_Optional.OneToOne_Required_FK0].[Level2_Required_Id]
) AS [t] ON [l1.OneToMany_Optional.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
ORDER BY [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Include_with_optional_navigation() : line 1066
            AssertSql(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK].[Date], [e.OneToOne_Optional_FK].[Level1_Optional_Id], [e.OneToOne_Optional_FK].[Level1_Required_Id], [e.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE ([e.OneToOne_Optional_FK].[Name] <> N'L2 05') OR [e.OneToOne_Optional_FK].[Name] IS NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_with_nested_navigation_filter_and_explicit_DefaultIfEmpty() : line 2108
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN (
    SELECT [l1.OneToOne_Optional_FK.OneToMany_Optional].*
    FROM [LevelThree] AS [l1.OneToOne_Optional_FK.OneToMany_Optional]
    WHERE [l1.OneToOne_Optional_FK.OneToMany_Optional].[Id] > 5
) AS [t] ON [l1.OneToOne_Optional_FK].[Id] = [t].[OneToMany_Optional_InverseId]
WHERE [t].[Id] IS NOT NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_with_navigation_filter_and_explicit_DefaultIfEmpty() : line 2081
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN (
    SELECT [l1.OneToMany_Optional].*
    FROM [LevelTwo] AS [l1.OneToMany_Optional]
    WHERE [l1.OneToMany_Optional].[Id] > 5
) AS [t] ON [l1].[Id] = [t].[OneToMany_Optional_InverseId]
WHERE [t].[Id] IS NOT NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Result_operator_nav_prop_reference_optional_Sum() : line 1013
            AssertSql(
                @"SELECT SUM([e.OneToOne_Optional_FK].[Level1_Required_Id])
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Project_collection_navigation() : line 2974
            AssertSql(
                @"SELECT [l1].[Id]
FROM [LevelOne] AS [l1]
ORDER BY [l1].[Id]",
                //
                @"SELECT [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId], [t].[Id]
FROM [LevelTwo] AS [l1.OneToMany_Optional]
INNER JOIN (
    SELECT [l10].[Id]
    FROM [LevelOne] AS [l10]
) AS [t] ON [l1.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
ORDER BY [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Optional_navigation_inside_property_method_translated_to_join() : line 341
            AssertSql(
                @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_InverseId], [e1].[OneToMany_Required_Self_InverseId], [e1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e1]
LEFT JOIN [LevelTwo] AS [e1.OneToOne_Optional_FK] ON [e1].[Id] = [e1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE [e1.OneToOne_Optional_FK].[Name] = N'L2 01'");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Level4_Include() : line 2884
            AssertSql(
                @"SELECT [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id], [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Date], [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Level1_Optional_Id], [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Level1_Required_Id], [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Name], [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId], [OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Id], [OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Level2_Optional_Id], [OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Level2_Required_Id], [OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Name], [OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Required_PK] ON [l1].[Id] = [l1.OneToOne_Required_PK].[Id]
LEFT JOIN [LevelThree] AS [l1.OneToOne_Required_PK.OneToOne_Required_PK] ON [l1.OneToOne_Required_PK].[Id] = [l1.OneToOne_Required_PK.OneToOne_Required_PK].[Id]
LEFT JOIN [LevelFour] AS [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK] ON [l1.OneToOne_Required_PK.OneToOne_Required_PK].[Id] = [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK].[Id]
LEFT JOIN [LevelThree] AS [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse] ON [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK].[Level3_Required_Id] = [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse].[Id]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse] ON [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse].[Level2_Required_Id] = [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]
LEFT JOIN [LevelThree] AS [OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK] ON [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id] = [OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Level2_Optional_Id]
WHERE ([l1.OneToOne_Required_PK].[Id] IS NOT NULL AND [l1.OneToOne_Required_PK.OneToOne_Required_PK].[Id] IS NOT NULL) AND [l1.OneToOne_Required_PK.OneToOne_Required_PK.OneToOne_Required_PK].[Id] IS NOT NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Join_navigation_in_inner_selector_translated_to_subquery() : line 442
            AssertSql(
                @"SELECT [e2].[Id] AS [Id2], [e1].[Id] AS [Id1]
FROM [LevelTwo] AS [e2]
INNER JOIN [LevelOne] AS [e1] ON [e2].[Id] IN (
    SELECT TOP(1) [subQuery0].[Id]
    FROM [LevelTwo] AS [subQuery0]
    WHERE [subQuery0].[Level1_Optional_Id] = [e1].[Id]
)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Optional_navigation_projected_into_DTO() : line 978
            AssertSql(
                @"SELECT [e].[Id], [e].[Name], CASE
    WHEN [e.OneToOne_Optional_FK].[Id] IS NOT NULL
    THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
END, [e.OneToOne_Optional_FK].[Id] AS [Id0], [e.OneToOne_Optional_FK].[Name] AS [Name0]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_client_method_on_outer() : line 2564
            AssertSql(
                @"SELECT [l1].[Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Select_multiple_nav_prop_optional_required() : line 844
            AssertSql(
                @"SELECT [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1.OneToOne_Optional_FK.OneToOne_Required_FK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Level2_Required_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Optional_navigation_take_optional_navigation() : line 1724
            AssertSql(
                @"@__p_0='10'

SELECT TOP(@__p_0) [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Name]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_FK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
ORDER BY [l1.OneToOne_Optional_FK].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Project_collection_navigation_nested() : line 2992
            AssertSql(
                @"SELECT [l1.OneToOne_Optional_FK].[Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
ORDER BY [l1].[Id], [l1.OneToOne_Optional_FK].[Id]",
                //
                @"SELECT [l1.OneToOne_Optional_FK.OneToMany_Optional].[Id], [l1.OneToOne_Optional_FK.OneToMany_Optional].[Level2_Optional_Id], [l1.OneToOne_Optional_FK.OneToMany_Optional].[Level2_Required_Id], [l1.OneToOne_Optional_FK.OneToMany_Optional].[Name], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToOne_Optional_SelfId], [t].[Id], [t].[Id0]
FROM [LevelThree] AS [l1.OneToOne_Optional_FK.OneToMany_Optional]
INNER JOIN (
    SELECT [l10].[Id], [l1.OneToOne_Optional_FK0].[Id] AS [Id0]
    FROM [LevelOne] AS [l10]
    LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK0] ON [l10].[Id] = [l1.OneToOne_Optional_FK0].[Level1_Optional_Id]
) AS [t] ON [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id0]
ORDER BY [t].[Id], [t].[Id0]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Where_multiple_nav_prop_reference_optional_member_compared_to_value() : line 739
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_FK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
WHERE ([l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Name] <> N'L3 05') OR [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Name] IS NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_with_Include1() : line 1792
            AssertSql(
                @"SELECT [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
ORDER BY [l1.OneToMany_Optional].[Id]",
                //
                @"SELECT [l1.OneToMany_Optional.OneToMany_Optional].[Id], [l1.OneToMany_Optional.OneToMany_Optional].[Level2_Optional_Id], [l1.OneToMany_Optional.OneToMany_Optional].[Level2_Required_Id], [l1.OneToMany_Optional.OneToMany_Optional].[Name], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l1.OneToMany_Optional.OneToMany_Optional]
INNER JOIN (
    SELECT DISTINCT [l1.OneToMany_Optional0].[Id]
    FROM [LevelOne] AS [l10]
    INNER JOIN [LevelTwo] AS [l1.OneToMany_Optional0] ON [l10].[Id] = [l1.OneToMany_Optional0].[OneToMany_Optional_InverseId]
) AS [t] ON [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
ORDER BY [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Order_by_key_of_navigation_similar_to_projected_gets_optimized_into_FK_access() : line 1685
            AssertSql(
                @"SELECT [l3.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id], [l3.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Date], [l3.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Name], [l3.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l3.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l3.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l3]
INNER JOIN [LevelTwo] AS [l3.OneToOne_Required_FK_Inverse] ON [l3].[Level2_Required_Id] = [l3.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [LevelOne] AS [l3.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse] ON [l3.OneToOne_Required_FK_Inverse].[Level1_Required_Id] = [l3.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]
ORDER BY [l3].[Level2_Required_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Entity_equality_empty() : line 29
            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l.OneToOne_Optional_FK] ON [l].[Id] = [l.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE [l.OneToOne_Optional_FK].[Id] = 0");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Include_collection_with_multiple_orderbys_complex() : line 3218
            AssertSql(
                @"SELECT [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [l2]
ORDER BY ABS([l2].[Level1_Required_Id]) + 7, [l2].[Name], [l2].[Id]",
                //
                @"SELECT [l2.OneToMany_Optional].[Id], [l2.OneToMany_Optional].[Level2_Optional_Id], [l2.OneToMany_Optional].[Level2_Required_Id], [l2.OneToMany_Optional].[Name], [l2.OneToMany_Optional].[OneToMany_Optional_InverseId], [l2.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l2.OneToMany_Optional].[OneToMany_Required_InverseId], [l2.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l2.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l2.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l2.OneToMany_Optional]
INNER JOIN (
    SELECT [l20].[Id], ABS([l20].[Level1_Required_Id]) + 7 AS [c], [l20].[Name], [l20].[Level1_Required_Id]
    FROM [LevelTwo] AS [l20]
) AS [t] ON [l2.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
ORDER BY [t].[c], [t].[Name], [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Multi_level_navigation_with_same_navigation_compared_to_null() : line 2810
            AssertSql(
                @"SELECT [l3].[Id]
FROM [LevelThree] AS [l3]
LEFT JOIN [LevelTwo] AS [l3.OneToMany_Optional_Inverse] ON [l3].[OneToMany_Optional_InverseId] = [l3.OneToMany_Optional_Inverse].[Id]
LEFT JOIN [LevelOne] AS [l3.OneToMany_Optional_Inverse.OneToOne_Required_FK_Inverse] ON [l3.OneToMany_Optional_Inverse].[Level1_Required_Id] = [l3.OneToMany_Optional_Inverse.OneToOne_Required_FK_Inverse].[Id]
WHERE (([l3.OneToMany_Optional_Inverse.OneToOne_Required_FK_Inverse].[Name] <> N'L1 07') OR [l3.OneToMany_Optional_Inverse.OneToOne_Required_FK_Inverse].[Name] IS NULL) AND [l3.OneToMany_Optional_Inverse].[Level1_Required_Id] IS NOT NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access1() : line 1652
            AssertSql(
                @"SELECT [l3.OneToOne_Required_FK_Inverse].[Id], [l3.OneToOne_Required_FK_Inverse].[Date], [l3.OneToOne_Required_FK_Inverse].[Level1_Optional_Id], [l3.OneToOne_Required_FK_Inverse].[Level1_Required_Id], [l3.OneToOne_Required_FK_Inverse].[Name], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l3]
INNER JOIN [LevelTwo] AS [l3.OneToOne_Required_FK_Inverse] ON [l3].[Level2_Required_Id] = [l3.OneToOne_Required_FK_Inverse].[Id]
ORDER BY [l3.OneToOne_Required_FK_Inverse].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access2() : line 1663
            AssertSql(
                @"SELECT [l3.OneToOne_Required_FK_Inverse].[Id], [l3.OneToOne_Required_FK_Inverse].[Date], [l3.OneToOne_Required_FK_Inverse].[Level1_Optional_Id], [l3.OneToOne_Required_FK_Inverse].[Level1_Required_Id], [l3.OneToOne_Required_FK_Inverse].[Name], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l3]
INNER JOIN [LevelTwo] AS [l3.OneToOne_Required_FK_Inverse] ON [l3].[Level2_Required_Id] = [l3.OneToOne_Required_FK_Inverse].[Id]
ORDER BY [l3.OneToOne_Required_FK_Inverse].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Multi_level_include_with_short_circuiting() : line 234
            AssertSql(
                @"SELECT [x].[Name], [x].[LabelDefaultText], [x].[PlaceholderDefaultText], [x.Placeholder].[DefaultText], [x.Label].[DefaultText]
FROM [Fields] AS [x]
LEFT JOIN [MultilingualStrings] AS [x.Placeholder] ON [x].[PlaceholderDefaultText] = [x.Placeholder].[DefaultText]
LEFT JOIN [MultilingualStrings] AS [x.Label] ON [x].[LabelDefaultText] = [x.Label].[DefaultText]
ORDER BY [x.Label].[DefaultText], [x.Placeholder].[DefaultText]",
                //
                @"SELECT [x.Label.Globalizations].[Text], [x.Label.Globalizations].[ComplexNavigationStringDefaultText], [x.Label.Globalizations].[LanguageName], [c.Language].[Name], [c.Language].[CultureString]
FROM [Globalizations] AS [x.Label.Globalizations]
LEFT JOIN [Languages] AS [c.Language] ON [x.Label.Globalizations].[LanguageName] = [c.Language].[Name]
INNER JOIN (
    SELECT DISTINCT [x.Label0].[DefaultText]
    FROM [Fields] AS [x0]
    LEFT JOIN [MultilingualStrings] AS [x.Placeholder0] ON [x0].[PlaceholderDefaultText] = [x.Placeholder0].[DefaultText]
    LEFT JOIN [MultilingualStrings] AS [x.Label0] ON [x0].[LabelDefaultText] = [x.Label0].[DefaultText]
) AS [t] ON [x.Label.Globalizations].[ComplexNavigationStringDefaultText] = [t].[DefaultText]
ORDER BY [t].[DefaultText]",
                //
                @"SELECT [x.Placeholder.Globalizations].[Text], [x.Placeholder.Globalizations].[ComplexNavigationStringDefaultText], [x.Placeholder.Globalizations].[LanguageName], [c.Language0].[Name], [c.Language0].[CultureString]
FROM [Globalizations] AS [x.Placeholder.Globalizations]
LEFT JOIN [Languages] AS [c.Language0] ON [x.Placeholder.Globalizations].[LanguageName] = [c.Language0].[Name]
INNER JOIN (
    SELECT DISTINCT [x.Placeholder1].[DefaultText], [x.Label1].[DefaultText] AS [DefaultText0]
    FROM [Fields] AS [x1]
    LEFT JOIN [MultilingualStrings] AS [x.Placeholder1] ON [x1].[PlaceholderDefaultText] = [x.Placeholder1].[DefaultText]
    LEFT JOIN [MultilingualStrings] AS [x.Label1] ON [x1].[LabelDefaultText] = [x.Label1].[DefaultText]
) AS [t0] ON [x.Placeholder.Globalizations].[ComplexNavigationStringDefaultText] = [t0].[DefaultText]
ORDER BY [t0].[DefaultText0], [t0].[DefaultText]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_where_with_subquery() : line 1638
            AssertSql(
                @"SELECT [l1.OneToMany_Required].[Id], [l1.OneToMany_Required].[Date], [l1.OneToMany_Required].[Level1_Optional_Id], [l1.OneToMany_Required].[Level1_Required_Id], [l1.OneToMany_Required].[Name], [l1.OneToMany_Required].[OneToMany_Optional_InverseId], [l1.OneToMany_Required].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Required].[OneToMany_Required_InverseId], [l1.OneToMany_Required].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Required].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Required].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToMany_Required] ON [l1].[Id] = [l1.OneToMany_Required].[OneToMany_Required_InverseId]
WHERE EXISTS (
    SELECT 1
    FROM [LevelThree] AS [l]
    WHERE [l1.OneToMany_Required].[Id] = [l].[OneToMany_Required_InverseId])");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_navigation_comparison1() : line 867
            AssertSql(
                @"SELECT [l11].[Id] AS [Id1], [l12].[Id] AS [Id2]
FROM [LevelOne] AS [l11]
CROSS JOIN [LevelOne] AS [l12]
WHERE [l11].[Id] = [l12].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Complex_multi_include_with_order_by_and_paging() : line 1336
            AssertSql(
                @"@__p_0='0'
@__p_1='10'

SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Required_FK].[Id], [e.OneToOne_Required_FK].[Date], [e.OneToOne_Required_FK].[Level1_Optional_Id], [e.OneToOne_Required_FK].[Level1_Required_Id], [e.OneToOne_Required_FK].[Name], [e.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Required_FK].[OneToMany_Required_InverseId], [e.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Required_FK] ON [e].[Id] = [e.OneToOne_Required_FK].[Level1_Required_Id]
ORDER BY [e].[Name], [e.OneToOne_Required_FK].[Id]
OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY",
                //
                @"@__p_0='0'
@__p_1='10'

SELECT [e.OneToOne_Required_FK.OneToMany_Optional].[Id], [e.OneToOne_Required_FK.OneToMany_Optional].[Level2_Optional_Id], [e.OneToOne_Required_FK.OneToMany_Optional].[Level2_Required_Id], [e.OneToOne_Required_FK.OneToMany_Optional].[Name], [e.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_InverseId], [e.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Required_InverseId], [e.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [e.OneToOne_Required_FK.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Required_FK.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [e.OneToOne_Required_FK.OneToMany_Optional]
INNER JOIN (
    SELECT DISTINCT [t].*
    FROM (
        SELECT [e.OneToOne_Required_FK0].[Id], [e0].[Name]
        FROM [LevelOne] AS [e0]
        LEFT JOIN [LevelTwo] AS [e.OneToOne_Required_FK0] ON [e0].[Id] = [e.OneToOne_Required_FK0].[Level1_Required_Id]
        ORDER BY [e0].[Name], [e.OneToOne_Required_FK0].[Id]
        OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY
    ) AS [t]
) AS [t0] ON [e.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t0].[Id]
ORDER BY [t0].[Name], [t0].[Id]",
                //
                @"@__p_0='0'
@__p_1='10'

SELECT [e.OneToOne_Required_FK.OneToMany_Required].[Id], [e.OneToOne_Required_FK.OneToMany_Required].[Level2_Optional_Id], [e.OneToOne_Required_FK.OneToMany_Required].[Level2_Required_Id], [e.OneToOne_Required_FK.OneToMany_Required].[Name], [e.OneToOne_Required_FK.OneToMany_Required].[OneToMany_Optional_InverseId], [e.OneToOne_Required_FK.OneToMany_Required].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Required_FK.OneToMany_Required].[OneToMany_Required_InverseId], [e.OneToOne_Required_FK.OneToMany_Required].[OneToMany_Required_Self_InverseId], [e.OneToOne_Required_FK.OneToMany_Required].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Required_FK.OneToMany_Required].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [e.OneToOne_Required_FK.OneToMany_Required]
INNER JOIN (
    SELECT DISTINCT [t1].*
    FROM (
        SELECT [e.OneToOne_Required_FK1].[Id], [e1].[Name]
        FROM [LevelOne] AS [e1]
        LEFT JOIN [LevelTwo] AS [e.OneToOne_Required_FK1] ON [e1].[Id] = [e.OneToOne_Required_FK1].[Level1_Required_Id]
        ORDER BY [e1].[Name], [e.OneToOne_Required_FK1].[Id]
        OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY
    ) AS [t1]
) AS [t2] ON [e.OneToOne_Required_FK.OneToMany_Required].[OneToMany_Required_InverseId] = [t2].[Id]
ORDER BY [t2].[Name], [t2].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Multiple_SelectMany_calls() : line 1216
            AssertSql(
                @"SELECT [e.OneToMany_Optional.OneToMany_Optional].[Id], [e.OneToMany_Optional.OneToMany_Optional].[Level2_Optional_Id], [e.OneToMany_Optional.OneToMany_Optional].[Level2_Required_Id], [e.OneToMany_Optional.OneToMany_Optional].[Name], [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e]
INNER JOIN [LevelTwo] AS [e.OneToMany_Optional] ON [e].[Id] = [e.OneToMany_Optional].[OneToMany_Optional_InverseId]
INNER JOIN [LevelThree] AS [e.OneToMany_Optional.OneToMany_Optional] ON [e.OneToMany_Optional].[Id] = [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Navigation_key_access_optional_comparison() : line 288
            AssertSql(
                @"SELECT [e2].[Id]
FROM [LevelTwo] AS [e2]
WHERE [e2].[OneToOne_Optional_PK_InverseId] > 5");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Null_protection_logic_work_for_inner_key_access_of_manually_created_GroupJoin1() : line 1596
            AssertSql(
                @"SELECT [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_InverseId], [t].[OneToMany_Optional_Self_InverseId], [t].[OneToMany_Required_InverseId], [t].[OneToMany_Required_Self_InverseId], [t].[OneToOne_Optional_PK_InverseId], [t].[OneToOne_Optional_SelfId]
FROM (
    SELECT [l1.OneToOne_Required_FK].[Id], [l1.OneToOne_Required_FK].[Date], [l1.OneToOne_Required_FK].[Level1_Optional_Id], [l1.OneToOne_Required_FK].[Level1_Required_Id], [l1.OneToOne_Required_FK].[Name], [l1.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
    FROM [LevelOne] AS [l10]
    LEFT JOIN [LevelTwo] AS [l1.OneToOne_Required_FK] ON [l10].[Id] = [l1.OneToOne_Required_FK].[Level1_Required_Id]
) AS [t]",
                //
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Null_reference_protection_complex_materialization() : line 2321
            AssertSql(
                @"SELECT [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name] AS [property], [t].[OneToMany_Optional_InverseId], [t].[OneToMany_Optional_Self_InverseId], [t].[OneToMany_Required_InverseId], [t].[OneToMany_Required_Self_InverseId], [t].[OneToOne_Optional_PK_InverseId], [t].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l3]
LEFT JOIN (
    SELECT [l2_inner].[Id], [l2_inner].[Date], [l2_inner].[Level1_Optional_Id], [l2_inner].[Level1_Required_Id], [l2_inner].[Name], [l2_inner].[OneToMany_Optional_InverseId], [l2_inner].[OneToMany_Optional_Self_InverseId], [l2_inner].[OneToMany_Required_InverseId], [l2_inner].[OneToMany_Required_Self_InverseId], [l2_inner].[OneToOne_Optional_PK_InverseId], [l2_inner].[OneToOne_Optional_SelfId]
    FROM [LevelOne] AS [l1_inner]
    LEFT JOIN [LevelTwo] AS [l2_inner] ON [l1_inner].[Id] = [l2_inner].[Level1_Optional_Id]
) AS [t] ON [l3].[Level2_Required_Id] = [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Optional_navigation_propagates_nullability_to_manually_created_left_join2() : line 2293
            AssertSql(
                @"SELECT [l3].[Name] AS [Name1], [t].[Name] AS [Name2]
FROM [LevelThree] AS [l3]
LEFT JOIN (
    SELECT [ll.OneToOne_Optional_FK].*
    FROM [LevelOne] AS [ll]
    LEFT JOIN [LevelTwo] AS [ll.OneToOne_Optional_FK] ON [ll].[Id] = [ll.OneToOne_Optional_FK].[Level1_Optional_Id]
) AS [t] ON [l3].[Level2_Required_Id] = [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Required_navigation_with_Include_ThenInclude() : line 1915
            AssertSql(
                @"SELECT [l4.OneToOne_Required_FK_Inverse].[Id], [l4.OneToOne_Required_FK_Inverse].[Level2_Optional_Id], [l4.OneToOne_Required_FK_Inverse].[Level2_Required_Id], [l4.OneToOne_Required_FK_Inverse].[Name], [l4.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l4.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l4.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l4.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l4.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l4.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId], [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[Id], [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[Date], [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[Level1_Optional_Id], [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[Level1_Required_Id], [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[Name], [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[OneToMany_Optional_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[OneToMany_Optional_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[OneToMany_Required_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[OneToMany_Required_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[OneToOne_Optional_PK_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[OneToOne_Optional_SelfId], [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse.OneToMany_Optional_Inverse].[Id], [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse.OneToMany_Optional_Inverse].[Date], [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse.OneToMany_Optional_Inverse].[Name], [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse.OneToMany_Optional_Inverse].[OneToMany_Optional_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse.OneToMany_Optional_Inverse].[OneToMany_Required_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse.OneToMany_Optional_Inverse].[OneToOne_Optional_SelfId]
FROM [LevelFour] AS [l4]
INNER JOIN [LevelThree] AS [l4.OneToOne_Required_FK_Inverse] ON [l4].[Level3_Required_Id] = [l4.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [LevelTwo] AS [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse] ON [l4.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId] = [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[Id]
LEFT JOIN [LevelOne] AS [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse.OneToMany_Optional_Inverse] ON [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[OneToMany_Optional_InverseId] = [l4.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse.OneToMany_Optional_Inverse].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Where_complex_predicate_with_with_nav_prop_and_OrElse2() : line 914
            AssertSql(
                @"SELECT [l1].[Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1.OneToOne_Optional_FK.OneToOne_Required_FK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Level2_Required_Id]
WHERE ([l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Name] = N'L3 05') OR (([l1.OneToOne_Optional_FK].[Name] <> N'L2 05') OR [l1.OneToOne_Optional_FK].[Name] IS NULL)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Select_subquery_with_client_eval_and_navigation1() : line 2911
            AssertSql(
                @"SELECT 1
FROM [LevelTwo] AS [l2]",
                //
                @"SELECT TOP(1) [l.OneToOne_Required_FK_Inverse0].[Name]
FROM [LevelTwo] AS [l0]
INNER JOIN [LevelOne] AS [l.OneToOne_Required_FK_Inverse0] ON [l0].[Level1_Required_Id] = [l.OneToOne_Required_FK_Inverse0].[Id]
ORDER BY [l0].[Id]",
                //
                @"SELECT TOP(1) [l.OneToOne_Required_FK_Inverse0].[Name]
FROM [LevelTwo] AS [l0]
INNER JOIN [LevelOne] AS [l.OneToOne_Required_FK_Inverse0] ON [l0].[Level1_Required_Id] = [l.OneToOne_Required_FK_Inverse0].[Id]
ORDER BY [l0].[Id]",
                //
                @"SELECT TOP(1) [l.OneToOne_Required_FK_Inverse0].[Name]
FROM [LevelTwo] AS [l0]
INNER JOIN [LevelOne] AS [l.OneToOne_Required_FK_Inverse0] ON [l0].[Level1_Required_Id] = [l.OneToOne_Required_FK_Inverse0].[Id]
ORDER BY [l0].[Id]",
                //
                @"SELECT TOP(1) [l.OneToOne_Required_FK_Inverse0].[Name]
FROM [LevelTwo] AS [l0]
INNER JOIN [LevelOne] AS [l.OneToOne_Required_FK_Inverse0] ON [l0].[Level1_Required_Id] = [l.OneToOne_Required_FK_Inverse0].[Id]
ORDER BY [l0].[Id]",
                //
                @"SELECT TOP(1) [l.OneToOne_Required_FK_Inverse0].[Name]
FROM [LevelTwo] AS [l0]
INNER JOIN [LevelOne] AS [l.OneToOne_Required_FK_Inverse0] ON [l0].[Level1_Required_Id] = [l.OneToOne_Required_FK_Inverse0].[Id]
ORDER BY [l0].[Id]",
                //
                @"SELECT TOP(1) [l.OneToOne_Required_FK_Inverse0].[Name]
FROM [LevelTwo] AS [l0]
INNER JOIN [LevelOne] AS [l.OneToOne_Required_FK_Inverse0] ON [l0].[Level1_Required_Id] = [l.OneToOne_Required_FK_Inverse0].[Id]
ORDER BY [l0].[Id]",
                //
                @"SELECT TOP(1) [l.OneToOne_Required_FK_Inverse0].[Name]
FROM [LevelTwo] AS [l0]
INNER JOIN [LevelOne] AS [l.OneToOne_Required_FK_Inverse0] ON [l0].[Level1_Required_Id] = [l.OneToOne_Required_FK_Inverse0].[Id]
ORDER BY [l0].[Id]",
                //
                @"SELECT TOP(1) [l.OneToOne_Required_FK_Inverse0].[Name]
FROM [LevelTwo] AS [l0]
INNER JOIN [LevelOne] AS [l.OneToOne_Required_FK_Inverse0] ON [l0].[Level1_Required_Id] = [l.OneToOne_Required_FK_Inverse0].[Id]
ORDER BY [l0].[Id]");

Output truncated.

Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Join_navigation_in_outer_selector_translated_to_extra_join() : line 407
            AssertSql(
                @"SELECT [e1].[Id] AS [Id1], [e2].[Id] AS [Id2]
FROM [LevelOne] AS [e1]
LEFT JOIN [LevelTwo] AS [e1.OneToOne_Optional_FK] ON [e1].[Id] = [e1.OneToOne_Optional_FK].[Level1_Optional_Id]
INNER JOIN [LevelTwo] AS [e2] ON [e1.OneToOne_Optional_FK].[Id] = [e2].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Navigation_inside_method_call_translated_to_join() : line 308
            AssertSql(
                @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_InverseId], [e1].[OneToMany_Required_Self_InverseId], [e1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e1]
LEFT JOIN [LevelTwo] AS [e1.OneToOne_Required_FK] ON [e1].[Id] = [e1.OneToOne_Required_FK].[Level1_Required_Id]
WHERE [e1.OneToOne_Required_FK].[Name] LIKE N'L' + N'%' AND (CHARINDEX(N'L', [e1.OneToOne_Required_FK].[Name]) = 1)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Select_optional_navigation_property_string_concat() : line 3153
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
ORDER BY [l1].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Complex_navigations_with_predicate_projected_into_anonymous_type2() : line 965
            AssertSql(
                @"SELECT [e].[Name], [e.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Id]
FROM [LevelThree] AS [e]
INNER JOIN [LevelTwo] AS [e.OneToOne_Required_FK_Inverse] ON [e].[Level2_Required_Id] = [e.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [LevelOne] AS [e.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse] ON [e.OneToOne_Required_FK_Inverse].[Level1_Required_Id] = [e.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]
LEFT JOIN [LevelOne] AS [e.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse] ON [e.OneToOne_Required_FK_Inverse].[Level1_Optional_Id] = [e.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Id]
WHERE ([e.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id] = [e.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Id]) AND (([e.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Id] <> 7) OR [e.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Id] IS NULL)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Where_nav_prop_reference_optional1_via_DefaultIfEmpty() : line 693
            AssertSql(
                @"SELECT [l1].[Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l2Left] ON [l1].[Id] = [l2Left].[Level1_Optional_Id]
LEFT JOIN [LevelTwo] AS [l2Right] ON [l1].[Id] = [l2Right].[Level1_Optional_Id]
WHERE ([l2Left].[Name] = N'L2 05') OR ([l2Right].[Name] = N'L2 07')");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Multiple_complex_include_select() : line 602
            AssertSql(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK].[Date], [e.OneToOne_Optional_FK].[Level1_Optional_Id], [e.OneToOne_Optional_FK].[Level1_Required_Id], [e.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
ORDER BY [e.OneToOne_Optional_FK].[Id], [e].[Id]",
                //
                @"SELECT [e.OneToMany_Optional].[Id], [e.OneToMany_Optional].[Date], [e.OneToMany_Optional].[Level1_Optional_Id], [e.OneToMany_Optional].[Level1_Required_Id], [e.OneToMany_Optional].[Name], [e.OneToMany_Optional].[OneToMany_Optional_InverseId], [e.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [e.OneToMany_Optional].[OneToMany_Required_InverseId], [e.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [e.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [e.OneToMany_Optional].[OneToOne_Optional_SelfId], [l.OneToOne_Optional_FK].[Id], [l.OneToOne_Optional_FK].[Level2_Optional_Id], [l.OneToOne_Optional_FK].[Level2_Required_Id], [l.OneToOne_Optional_FK].[Name], [l.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [e.OneToMany_Optional]
LEFT JOIN [LevelThree] AS [l.OneToOne_Optional_FK] ON [e.OneToMany_Optional].[Id] = [l.OneToOne_Optional_FK].[Level2_Optional_Id]
INNER JOIN (
    SELECT DISTINCT [e1].[Id], [e.OneToOne_Optional_FK1].[Id] AS [Id0]
    FROM [LevelOne] AS [e1]
    LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK1] ON [e1].[Id] = [e.OneToOne_Optional_FK1].[Level1_Optional_Id]
) AS [t0] ON [e.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t0].[Id]
ORDER BY [t0].[Id0], [t0].[Id]",
                //
                @"SELECT [e.OneToOne_Optional_FK.OneToMany_Optional].[Id], [e.OneToOne_Optional_FK.OneToMany_Optional].[Level2_Optional_Id], [e.OneToOne_Optional_FK.OneToMany_Optional].[Level2_Required_Id], [e.OneToOne_Optional_FK.OneToMany_Optional].[Name], [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [e.OneToOne_Optional_FK.OneToMany_Optional]
INNER JOIN (
    SELECT DISTINCT [e.OneToOne_Optional_FK0].[Id]
    FROM [LevelOne] AS [e0]
    LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK0] ON [e0].[Id] = [e.OneToOne_Optional_FK0].[Level1_Optional_Id]
) AS [t] ON [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
ORDER BY [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Navigation_key_access_required_comparison() : line 298
            AssertSql(
                @"SELECT [e2].[Id]
FROM [LevelTwo] AS [e2]
WHERE [e2].[Id] > 5");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Key_equality_using_property_method_required2() : line 62
            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [l]
WHERE [l].[Level1_Required_Id] > 7");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_with_navigation_and_explicit_DefaultIfEmpty() : line 2051
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
WHERE [l1.OneToMany_Optional].[Id] IS NOT NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Explicit_GroupJoin_in_subquery_with_unrelated_projection() : line 2637
            AssertSql(
                @"@__p_0='15'

SELECT TOP(@__p_0) [l1].[Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
WHERE ([l2].[Name] <> N'Foo') OR [l2].[Name] IS NULL
ORDER BY [l1].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Navigations_compared_to_each_other5() : line 2869
            AssertSql(
                @"SELECT [l2].[Name]
FROM [LevelTwo] AS [l2]
LEFT JOIN [LevelThree] AS [l2.OneToOne_Optional_PK] ON [l2].[Id] = [l2.OneToOne_Optional_PK].[OneToOne_Optional_PK_InverseId]
LEFT JOIN [LevelThree] AS [l2.OneToOne_Required_FK] ON [l2].[Id] = [l2.OneToOne_Required_FK].[Level2_Required_Id]
WHERE EXISTS (
    SELECT 1
    FROM [LevelFour] AS [i]
    WHERE [l2.OneToOne_Required_FK].[Id] = [i].[OneToMany_Optional_InverseId])");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Include_collection_with_multiple_orderbys_member() : line 3164
            AssertSql(
                @"SELECT [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [l2]
ORDER BY [l2].[Name], [l2].[Level1_Required_Id], [l2].[Id]",
                //
                @"SELECT [l2.OneToMany_Optional].[Id], [l2.OneToMany_Optional].[Level2_Optional_Id], [l2.OneToMany_Optional].[Level2_Required_Id], [l2.OneToMany_Optional].[Name], [l2.OneToMany_Optional].[OneToMany_Optional_InverseId], [l2.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l2.OneToMany_Optional].[OneToMany_Required_InverseId], [l2.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l2.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l2.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l2.OneToMany_Optional]
INNER JOIN (
    SELECT [l20].[Id], [l20].[Name], [l20].[Level1_Required_Id]
    FROM [LevelTwo] AS [l20]
) AS [t] ON [l2.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
ORDER BY [t].[Name], [t].[Level1_Required_Id], [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Comparing_collection_navigation_on_optional_reference_to_null() : line 2900
            AssertSql(
                @"SELECT [l1].[Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE [l1.OneToOne_Optional_FK].[Id] IS NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_with_complex_subquery_with_joins_with_reference_to_grouping2() : line 2406
            AssertSql(
                @"SELECT [l1_outer].[Id], [l1_outer].[Date], [l1_outer].[Name], [l1_outer].[OneToMany_Optional_Self_InverseId], [l1_outer].[OneToMany_Required_Self_InverseId], [l1_outer].[OneToOne_Optional_SelfId], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_InverseId], [t].[OneToMany_Optional_Self_InverseId], [t].[OneToMany_Required_InverseId], [t].[OneToMany_Required_Self_InverseId], [t].[OneToOne_Optional_PK_InverseId], [t].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1_outer]
LEFT JOIN (
    SELECT [l2_inner].[Id], [l2_inner].[Date], [l2_inner].[Level1_Optional_Id], [l2_inner].[Level1_Required_Id], [l2_inner].[Name], [l2_inner].[OneToMany_Optional_InverseId], [l2_inner].[OneToMany_Optional_Self_InverseId], [l2_inner].[OneToMany_Required_InverseId], [l2_inner].[OneToMany_Required_Self_InverseId], [l2_inner].[OneToOne_Optional_PK_InverseId], [l2_inner].[OneToOne_Optional_SelfId]
    FROM [LevelTwo] AS [l2_inner]
    INNER JOIN [LevelOne] AS [l1_inner] ON [l2_inner].[Level1_Required_Id] = [l1_inner].[Id]
) AS [t] ON [l1_outer].[Id] = [t].[Level1_Optional_Id]
ORDER BY [l1_outer].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_with_navigation_filter_paging_and_explicit_DefaultIfEmpty() : line 2140
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToMany_Required].[Id], [l1.OneToMany_Required].[Date], [l1.OneToMany_Required].[Level1_Optional_Id], [l1.OneToMany_Required].[Level1_Required_Id], [l1.OneToMany_Required].[Name], [l1.OneToMany_Required].[OneToMany_Optional_InverseId], [l1.OneToMany_Required].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Required].[OneToMany_Required_InverseId], [l1.OneToMany_Required].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Required].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Required].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToMany_Required] ON [l1].[Id] = [l1.OneToMany_Required].[OneToMany_Required_InverseId]
ORDER BY [l1].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access_subquery() : line 1697
            AssertSql(
                @"@__p_0='10'

SELECT TOP(@__p_0) [l3.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Name]
FROM [LevelThree] AS [l3]
INNER JOIN [LevelTwo] AS [l3.OneToOne_Required_FK_Inverse] ON [l3].[Level2_Required_Id] = [l3.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [LevelOne] AS [l3.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse] ON [l3.OneToOne_Required_FK_Inverse].[Level1_Required_Id] = [l3.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]
ORDER BY [l3].[Level2_Required_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_on_a_subquery_containing_another_GroupJoin_projecting_outer_with_client_method() : line 2429
            AssertSql(
                @"SELECT [l2_outer].[Level1_Optional_Id], [l2_outer].[Name]
FROM [LevelTwo] AS [l2_outer]",
                //
                @"@__p_0='2'

SELECT TOP(@__p_0) [l10].[Id], [l10].[Date], [l10].[Name], [l10].[OneToMany_Optional_Self_InverseId], [l10].[OneToMany_Required_Self_InverseId], [l10].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l10]
LEFT JOIN [LevelTwo] AS [l20] ON [l10].[Id] = [l20].[Level1_Optional_Id]
ORDER BY [l10].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Project_collection_navigation_using_ef_property() : line 3012
            AssertSql(
                @"SELECT [l1.OneToOne_Optional_FK].[Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
ORDER BY [l1].[Id], [l1.OneToOne_Optional_FK].[Id]",
                //
                @"SELECT [l1.OneToOne_Optional_FK.OneToMany_Optional].[Id], [l1.OneToOne_Optional_FK.OneToMany_Optional].[Level2_Optional_Id], [l1.OneToOne_Optional_FK.OneToMany_Optional].[Level2_Required_Id], [l1.OneToOne_Optional_FK.OneToMany_Optional].[Name], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToOne_Optional_SelfId], [t].[Id], [t].[Id0]
FROM [LevelThree] AS [l1.OneToOne_Optional_FK.OneToMany_Optional]
INNER JOIN (
    SELECT [l10].[Id], [l1.OneToOne_Optional_FK0].[Id] AS [Id0]
    FROM [LevelOne] AS [l10]
    LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK0] ON [l10].[Id] = [l1.OneToOne_Optional_FK0].[Level1_Optional_Id]
) AS [t] ON [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id0]
ORDER BY [t].[Id], [t].[Id0]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Select_nav_prop_reference_optional1() : line 632
            AssertSql(
                @"SELECT [e.OneToOne_Optional_FK].[Name]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_navigation_property_and_filter_after() : line 1183
            AssertSql(
                @"SELECT [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
WHERE [l1.OneToMany_Optional].[Id] <> 6");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Where_multiple_nav_prop_reference_optional_compared_to_null4() : line 798
            AssertSql(
                @"SELECT [l3].[Id], [l3].[Level2_Optional_Id], [l3].[Level2_Required_Id], [l3].[Name], [l3].[OneToMany_Optional_InverseId], [l3].[OneToMany_Optional_Self_InverseId], [l3].[OneToMany_Required_InverseId], [l3].[OneToMany_Required_Self_InverseId], [l3].[OneToOne_Optional_PK_InverseId], [l3].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l3]
LEFT JOIN [LevelTwo] AS [l3.OneToOne_Optional_FK_Inverse] ON [l3].[Level2_Optional_Id] = [l3.OneToOne_Optional_FK_Inverse].[Id]
WHERE [l3.OneToOne_Optional_FK_Inverse].[Level1_Optional_Id] IS NOT NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Where_nav_prop_reference_optional2_via_DefaultIfEmpty() : line 716
            AssertSql(
                @"SELECT [l1].[Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l2Left] ON [l1].[Id] = [l2Left].[Level1_Optional_Id]
LEFT JOIN [LevelTwo] AS [l2Right] ON [l1].[Id] = [l2Right].[Level1_Optional_Id]
WHERE ([l2Left].[Name] = N'L2 05') OR (([l2Right].[Name] <> N'L2 42') OR [l2Right].[Name] IS NULL)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_on_subquery_and_set_operation_on_grouping_but_nothing_from_grouping_is_projected() : line 1565
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_InverseId], [t].[OneToMany_Optional_Self_InverseId], [t].[OneToMany_Required_InverseId], [t].[OneToMany_Required_Self_InverseId], [t].[OneToOne_Optional_PK_InverseId], [t].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN (
    SELECT [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
    FROM [LevelTwo] AS [l2]
    WHERE ([l2].[Name] <> N'L2 01') OR [l2].[Name] IS NULL
) AS [t] ON [l1].[Id] = [t].[Level1_Optional_Id]
ORDER BY [l1].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Where_multiple_nav_prop_reference_optional_member_compared_to_null() : line 751
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_FK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
WHERE [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Name] IS NOT NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_reference_to_group_in_OrderBy() : line 2553
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
ORDER BY [l1].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Join_navigation_translated_to_subquery_nested() : line 495
            AssertSql(
                @"SELECT [e3].[Id] AS [Id3], [e1].[Id] AS [Id1]
FROM [LevelThree] AS [e3]
INNER JOIN [LevelOne] AS [e1] ON [e3].[Id] IN (
    SELECT TOP(1) [subQuery.OneToOne_Optional_FK0].[Id]
    FROM [LevelTwo] AS [subQuery0]
    LEFT JOIN [LevelThree] AS [subQuery.OneToOne_Optional_FK0] ON [subQuery0].[Id] = [subQuery.OneToOne_Optional_FK0].[Level2_Optional_Id]
    WHERE [subQuery0].[Level1_Required_Id] = [e1].[Id]
)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Optional_navigation_inside_nested_method_call_translated_to_join_keeps_original_nullability_also_for_arguments() : line 396
            AssertSql(
                @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_InverseId], [e1].[OneToMany_Required_Self_InverseId], [e1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e1]
LEFT JOIN [LevelTwo] AS [e1.OneToOne_Optional_FK] ON [e1].[Id] = [e1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE DATEADD(day, [e1.OneToOne_Optional_FK].[Id], DATEADD(day, 15, [e1.OneToOne_Optional_FK].[Date])) > '2002-02-01T00:00:00.000'");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_on_a_subquery_containing_another_GroupJoin_projecting_outer() : line 2421
            AssertSql(
                @"@__p_0='2'

SELECT [l2_outer].[Name]
FROM (
    SELECT TOP(@__p_0) [l1].*
    FROM [LevelOne] AS [l1]
    LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
    ORDER BY [l1].[Id]
) AS [t]
LEFT JOIN [LevelTwo] AS [l2_outer] ON [t].[Id] = [l2_outer].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Multi_level_include_one_to_many_optional_and_one_to_many_optional_produces_valid_sql() : line 156
            AssertSql(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e]
ORDER BY [e].[Id]",
                //
                @"SELECT [e.OneToMany_Optional].[Id], [e.OneToMany_Optional].[Date], [e.OneToMany_Optional].[Level1_Optional_Id], [e.OneToMany_Optional].[Level1_Required_Id], [e.OneToMany_Optional].[Name], [e.OneToMany_Optional].[OneToMany_Optional_InverseId], [e.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [e.OneToMany_Optional].[OneToMany_Required_InverseId], [e.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [e.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [e.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [e.OneToMany_Optional]
INNER JOIN (
    SELECT [e0].[Id]
    FROM [LevelOne] AS [e0]
) AS [t] ON [e.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
ORDER BY [t].[Id], [e.OneToMany_Optional].[Id]",
                //
                @"SELECT [e.OneToMany_Optional.OneToMany_Optional].[Id], [e.OneToMany_Optional.OneToMany_Optional].[Level2_Optional_Id], [e.OneToMany_Optional.OneToMany_Optional].[Level2_Required_Id], [e.OneToMany_Optional.OneToMany_Optional].[Name], [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [e.OneToMany_Optional.OneToMany_Optional]
INNER JOIN (
    SELECT DISTINCT [e.OneToMany_Optional0].[Id], [t0].[Id] AS [Id0]
    FROM [LevelTwo] AS [e.OneToMany_Optional0]
    INNER JOIN (
        SELECT [e1].[Id]
        FROM [LevelOne] AS [e1]
    ) AS [t0] ON [e.OneToMany_Optional0].[OneToMany_Optional_InverseId] = [t0].[Id]
) AS [t1] ON [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t1].[Id]
ORDER BY [t1].[Id0], [t1].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Null_reference_protection_complex() : line 2307
            AssertSql(
                @"SELECT [t].[Name]
FROM [LevelThree] AS [l3]
LEFT JOIN (
    SELECT [l2_inner].*
    FROM [LevelOne] AS [l1_inner]
    LEFT JOIN [LevelTwo] AS [l2_inner] ON [l1_inner].[Id] = [l2_inner].[Level1_Optional_Id]
) AS [t] ON [l3].[Level2_Required_Id] = [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Key_equality_using_property_method_nested2() : line 83
            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [l]
WHERE [l].[Level1_Required_Id] = 7");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Where_complex_predicate_with_with_nav_prop_and_OrElse4() : line 939
            AssertSql(
                @"SELECT [l3].[Id]
FROM [LevelThree] AS [l3]
INNER JOIN [LevelTwo] AS [l3.OneToOne_Required_FK_Inverse] ON [l3].[Level2_Required_Id] = [l3.OneToOne_Required_FK_Inverse].[Id]
LEFT JOIN [LevelOne] AS [l3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse] ON [l3.OneToOne_Required_FK_Inverse].[Level1_Optional_Id] = [l3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Id]
LEFT JOIN [LevelTwo] AS [l3.OneToOne_Optional_FK_Inverse] ON [l3].[Level2_Optional_Id] = [l3.OneToOne_Optional_FK_Inverse].[Id]
WHERE (([l3.OneToOne_Optional_FK_Inverse].[Name] <> N'L2 05') OR [l3.OneToOne_Optional_FK_Inverse].[Name] IS NULL) OR ([l3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Name] = N'L1 05')");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Navigations_compared_to_each_other2() : line 2832
            AssertSql(
                @"SELECT [l2].[Name]
FROM [LevelTwo] AS [l2]
WHERE [l2].[OneToMany_Required_InverseId] = [l2].[OneToOne_Optional_PK_InverseId]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Optional_navigation_inside_method_call_translated_to_join() : line 330
            AssertSql(
                @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_InverseId], [e1].[OneToMany_Required_Self_InverseId], [e1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e1]
LEFT JOIN [LevelTwo] AS [e1.OneToOne_Optional_FK] ON [e1].[Id] = [e1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE [e1.OneToOne_Optional_FK].[Name] LIKE N'L' + N'%' AND (CHARINDEX(N'L', [e1.OneToOne_Optional_FK].[Name]) = 1)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Join_navigation_in_outer_selector_translated_to_extra_join_nested() : line 418
            AssertSql(
                @"SELECT [e1].[Id] AS [Id1], [e3].[Id] AS [Id3]
FROM [LevelOne] AS [e1]
LEFT JOIN [LevelTwo] AS [e1.OneToOne_Required_FK] ON [e1].[Id] = [e1.OneToOne_Required_FK].[Level1_Required_Id]
LEFT JOIN [LevelThree] AS [e1.OneToOne_Required_FK.OneToOne_Optional_FK] ON [e1.OneToOne_Required_FK].[Id] = [e1.OneToOne_Required_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
INNER JOIN [LevelThree] AS [e3] ON [e1.OneToOne_Required_FK.OneToOne_Optional_FK].[Id] = [e3].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_with_complex_subquery_with_joins_with_reference_to_grouping1() : line 2391
            AssertSql(
                @"SELECT [l1_outer].[Id], [l1_outer].[Date], [l1_outer].[Name], [l1_outer].[OneToMany_Optional_Self_InverseId], [l1_outer].[OneToMany_Required_Self_InverseId], [l1_outer].[OneToOne_Optional_SelfId], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_InverseId], [t].[OneToMany_Optional_Self_InverseId], [t].[OneToMany_Required_InverseId], [t].[OneToMany_Required_Self_InverseId], [t].[OneToOne_Optional_PK_InverseId], [t].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1_outer]
LEFT JOIN (
    SELECT [l2_inner].[Id], [l2_inner].[Date], [l2_inner].[Level1_Optional_Id], [l2_inner].[Level1_Required_Id], [l2_inner].[Name], [l2_inner].[OneToMany_Optional_InverseId], [l2_inner].[OneToMany_Optional_Self_InverseId], [l2_inner].[OneToMany_Required_InverseId], [l2_inner].[OneToMany_Required_Self_InverseId], [l2_inner].[OneToOne_Optional_PK_InverseId], [l2_inner].[OneToOne_Optional_SelfId]
    FROM [LevelTwo] AS [l2_inner]
    INNER JOIN [LevelOne] AS [l1_inner] ON [l2_inner].[Level1_Required_Id] = [l1_inner].[Id]
) AS [t] ON [l1_outer].[Id] = [t].[Level1_Optional_Id]
ORDER BY [l1_outer].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_on_complex_subquery_and_set_operation_on_grouping_but_nothing_from_grouping_is_projected() : line 1580
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_InverseId], [t].[OneToMany_Optional_Self_InverseId], [t].[OneToMany_Required_InverseId], [t].[OneToMany_Required_Self_InverseId], [t].[OneToOne_Optional_PK_InverseId], [t].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN (
    SELECT [l1.OneToOne_Required_FK].[Id], [l1.OneToOne_Required_FK].[Date], [l1.OneToOne_Required_FK].[Level1_Optional_Id], [l1.OneToOne_Required_FK].[Level1_Required_Id], [l1.OneToOne_Required_FK].[Name], [l1.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
    FROM [LevelOne] AS [l10]
    LEFT JOIN [LevelTwo] AS [l1.OneToOne_Required_FK] ON [l10].[Id] = [l1.OneToOne_Required_FK].[Level1_Required_Id]
    WHERE ([l10].[Name] <> N'L1 01') OR [l10].[Name] IS NULL
) AS [t] ON [l1].[Id] = [t].[Level1_Optional_Id]
ORDER BY [l1].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Optional_navigation_with_Include() : line 1975
            AssertSql(
                @"SELECT [l1.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK].[Date], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Required_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_FK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Optional_navigation_inside_nested_method_call_translated_to_join() : line 352
            AssertSql(
                @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_InverseId], [e1].[OneToMany_Required_Self_InverseId], [e1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e1]
LEFT JOIN [LevelTwo] AS [e1.OneToOne_Optional_FK] ON [e1].[Id] = [e1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE UPPER([e1.OneToOne_Optional_FK].[Name]) LIKE N'L' + N'%' AND (CHARINDEX(N'L', UPPER([e1.OneToOne_Optional_FK].[Name])) = 1)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Where_multiple_nav_prop_reference_optional_compared_to_null2() : line 775
            AssertSql(
                @"SELECT [l3].[Id], [l3].[Level2_Optional_Id], [l3].[Level2_Required_Id], [l3].[Name], [l3].[OneToMany_Optional_InverseId], [l3].[OneToMany_Optional_Self_InverseId], [l3].[OneToMany_Required_InverseId], [l3].[OneToMany_Required_Self_InverseId], [l3].[OneToOne_Optional_PK_InverseId], [l3].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l3]
LEFT JOIN [LevelTwo] AS [l3.OneToOne_Optional_FK_Inverse] ON [l3].[Level2_Optional_Id] = [l3.OneToOne_Optional_FK_Inverse].[Id]
WHERE [l3.OneToOne_Optional_FK_Inverse].[Level1_Optional_Id] IS NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Required_navigation_on_a_subquery_with_First_in_predicate() : line 2255
            AssertSql(
                @"SELECT [l2o].[Id], [l2o].[Date], [l2o].[Level1_Optional_Id], [l2o].[Level1_Required_Id], [l2o].[Name], [l2o].[OneToMany_Optional_InverseId], [l2o].[OneToMany_Optional_Self_InverseId], [l2o].[OneToMany_Required_InverseId], [l2o].[OneToMany_Required_Self_InverseId], [l2o].[OneToOne_Optional_PK_InverseId], [l2o].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [l2o]
WHERE [l2o].[Id] = 7",
                //
                @"SELECT TOP(1) [l2i.OneToOne_Required_FK_Inverse0].[Name]
FROM [LevelTwo] AS [l2i0]
INNER JOIN [LevelOne] AS [l2i.OneToOne_Required_FK_Inverse0] ON [l2i0].[Level1_Required_Id] = [l2i.OneToOne_Required_FK_Inverse0].[Id]
ORDER BY [l2i0].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Join_navigation_in_outer_selector_translated_to_extra_join_nested2() : line 430
            AssertSql(
                @"SELECT [e3].[Id] AS [Id3], [e1].[Id] AS [Id1]
FROM [LevelThree] AS [e3]
INNER JOIN [LevelTwo] AS [e3.OneToOne_Required_FK_Inverse] ON [e3].[Level2_Required_Id] = [e3.OneToOne_Required_FK_Inverse].[Id]
LEFT JOIN [LevelOne] AS [e3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse] ON [e3.OneToOne_Required_FK_Inverse].[Level1_Optional_Id] = [e3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Id]
INNER JOIN [LevelOne] AS [e1] ON [e3.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK_Inverse].[Id] = [e1].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Include_collection_with_multiple_orderbys_methodcall() : line 3200
            AssertSql(
                @"SELECT [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [l2]
ORDER BY ABS([l2].[Level1_Required_Id]), [l2].[Name], [l2].[Id]",
                //
                @"SELECT [l2.OneToMany_Optional].[Id], [l2.OneToMany_Optional].[Level2_Optional_Id], [l2.OneToMany_Optional].[Level2_Required_Id], [l2.OneToMany_Optional].[Name], [l2.OneToMany_Optional].[OneToMany_Optional_InverseId], [l2.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l2.OneToMany_Optional].[OneToMany_Required_InverseId], [l2.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l2.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l2.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l2.OneToMany_Optional]
INNER JOIN (
    SELECT [l20].[Id], ABS([l20].[Level1_Required_Id]) AS [c], [l20].[Name], [l20].[Level1_Required_Id]
    FROM [LevelTwo] AS [l20]
) AS [t] ON [l2.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
ORDER BY [t].[c], [t].[Name], [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Include_with_groupjoin_skip_and_take() : line 1102
            AssertSql(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [l2] ON [e].[Id] = [l2].[Level1_Optional_Id]
WHERE ([e].[Name] <> N'L1 03') OR [e].[Name] IS NULL
ORDER BY [e].[Id]",
                //
                @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_InverseId], [e1].[OneToMany_Required_Self_InverseId], [e1].[OneToOne_Optional_SelfId], [l21].[Id], [l21].[Date], [l21].[Level1_Optional_Id], [l21].[Level1_Required_Id], [l21].[Name], [l21].[OneToMany_Optional_InverseId], [l21].[OneToMany_Optional_Self_InverseId], [l21].[OneToMany_Required_InverseId], [l21].[OneToMany_Required_Self_InverseId], [l21].[OneToOne_Optional_PK_InverseId], [l21].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e1]
LEFT JOIN [LevelTwo] AS [l21] ON [e1].[Id] = [l21].[Level1_Optional_Id]
WHERE ([e1].[Name] <> N'L1 03') OR [e1].[Name] IS NULL
ORDER BY [e1].[Id]",
                //
                @"SELECT [e.OneToMany_Optional].[Id], [e.OneToMany_Optional].[Date], [e.OneToMany_Optional].[Level1_Optional_Id], [e.OneToMany_Optional].[Level1_Required_Id], [e.OneToMany_Optional].[Name], [e.OneToMany_Optional].[OneToMany_Optional_InverseId], [e.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [e.OneToMany_Optional].[OneToMany_Required_InverseId], [e.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [e.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [e.OneToMany_Optional].[OneToOne_Optional_SelfId], [l.OneToOne_Optional_FK].[Id], [l.OneToOne_Optional_FK].[Level2_Optional_Id], [l.OneToOne_Optional_FK].[Level2_Required_Id], [l.OneToOne_Optional_FK].[Name], [l.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [e.OneToMany_Optional]
LEFT JOIN [LevelThree] AS [l.OneToOne_Optional_FK] ON [e.OneToMany_Optional].[Id] = [l.OneToOne_Optional_FK].[Level2_Optional_Id]",
                //
                @"SELECT [l2.OneToOne_Required_PK].[Id], [l2.OneToOne_Required_PK].[Level2_Optional_Id], [l2.OneToOne_Required_PK].[Level2_Required_Id], [l2.OneToOne_Required_PK].[Name], [l2.OneToOne_Required_PK].[OneToMany_Optional_InverseId], [l2.OneToOne_Required_PK].[OneToMany_Optional_Self_InverseId], [l2.OneToOne_Required_PK].[OneToMany_Required_InverseId], [l2.OneToOne_Required_PK].[OneToMany_Required_Self_InverseId], [l2.OneToOne_Required_PK].[OneToOne_Optional_PK_InverseId], [l2.OneToOne_Required_PK].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l2.OneToOne_Required_PK]",
                //
                @"SELECT [l2.OneToOne_Required_PK].[Id], [l2.OneToOne_Required_PK].[Level2_Optional_Id], [l2.OneToOne_Required_PK].[Level2_Required_Id], [l2.OneToOne_Required_PK].[Name], [l2.OneToOne_Required_PK].[OneToMany_Optional_InverseId], [l2.OneToOne_Required_PK].[OneToMany_Optional_Self_InverseId], [l2.OneToOne_Required_PK].[OneToMany_Required_InverseId], [l2.OneToOne_Required_PK].[OneToMany_Required_Self_InverseId], [l2.OneToOne_Required_PK].[OneToOne_Optional_PK_InverseId], [l2.OneToOne_Required_PK].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l2.OneToOne_Required_PK]",
                //
                @"SELECT [l2.OneToOne_Required_PK].[Id], [l2.OneToOne_Required_PK].[Level2_Optional_Id], [l2.OneToOne_Required_PK].[Level2_Required_Id], [l2.OneToOne_Required_PK].[Name], [l2.OneToOne_Required_PK].[OneToMany_Optional_InverseId], [l2.OneToOne_Required_PK].[OneToMany_Optional_Self_InverseId], [l2.OneToOne_Required_PK].[OneToMany_Required_InverseId], [l2.OneToOne_Required_PK].[OneToMany_Required_Self_InverseId], [l2.OneToOne_Required_PK].[OneToOne_Optional_PK_InverseId], [l2.OneToOne_Required_PK].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l2.OneToOne_Required_PK]",
                //
                @"SELECT [l2.OneToOne_Required_PK].[Id], [l2.OneToOne_Required_PK].[Level2_Optional_Id], [l2.OneToOne_Required_PK].[Level2_Required_Id], [l2.OneToOne_Required_PK].[Name], [l2.OneToOne_Required_PK].[OneToMany_Optional_InverseId], [l2.OneToOne_Required_PK].[OneToMany_Optional_Self_InverseId], [l2.OneToOne_Required_PK].[OneToMany_Required_InverseId], [l2.OneToOne_Required_PK].[OneToMany_Required_Self_InverseId], [l2.OneToOne_Required_PK].[OneToOne_Optional_PK_InverseId], [l2.OneToOne_Required_PK].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l2.OneToOne_Required_PK]",
                //
                @"SELECT [l2.OneToOne_Required_PK].[Id], [l2.OneToOne_Required_PK].[Level2_Optional_Id], [l2.OneToOne_Required_PK].[Level2_Required_Id], [l2.OneToOne_Required_PK].[Name], [l2.OneToOne_Required_PK].[OneToMany_Optional_InverseId], [l2.OneToOne_Required_PK].[OneToMany_Optional_Self_InverseId], [l2.OneToOne_Required_PK].[OneToMany_Required_InverseId], [l2.OneToOne_Required_PK].[OneToMany_Required_Self_InverseId], [l2.OneToOne_Required_PK].[OneToOne_Optional_PK_InverseId], [l2.OneToOne_Required_PK].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l2.OneToOne_Required_PK]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_with_subquery_on_inner() : line 2594
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
ORDER BY [l1].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Multiple_optional_navigation_with_Include() : line 2007
            AssertSql(
                @"SELECT [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Level2_Optional_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Level2_Required_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Name], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_PK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToOne_Optional_PK_InverseId]
ORDER BY [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Id]",
                //
                @"SELECT [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[Level3_Optional_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[Level3_Required_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[Name], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelFour] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional]
INNER JOIN (
    SELECT DISTINCT [l1.OneToOne_Optional_FK.OneToOne_Optional_PK0].[Id]
    FROM [LevelOne] AS [l10]
    LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK0] ON [l10].[Id] = [l1.OneToOne_Optional_FK0].[Level1_Optional_Id]
    LEFT JOIN [LevelThree] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_PK0] ON [l1.OneToOne_Optional_FK0].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Optional_PK0].[OneToOne_Optional_PK_InverseId]
) AS [t] ON [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
ORDER BY [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_with_nested_navigation_and_explicit_DefaultIfEmpty() : line 2096
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToOne_Required_FK] ON [l1].[Id] = [l1.OneToOne_Required_FK].[Level1_Required_Id]
LEFT JOIN [LevelThree] AS [l1.OneToOne_Required_FK.OneToMany_Optional] ON [l1.OneToOne_Required_FK].[Id] = [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_InverseId]
WHERE [l1.OneToOne_Required_FK.OneToMany_Optional].[Id] IS NOT NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Select_multiple_nav_prop_reference_required() : line 822
            AssertSql(
                @"SELECT [e.OneToOne_Required_FK.OneToOne_Required_FK].[Id]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Required_FK] ON [e].[Id] = [e.OneToOne_Required_FK].[Level1_Required_Id]
LEFT JOIN [LevelThree] AS [e.OneToOne_Required_FK.OneToOne_Required_FK] ON [e.OneToOne_Required_FK].[Id] = [e.OneToOne_Required_FK.OneToOne_Required_FK].[Level2_Required_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Correlated_subquery_doesnt_project_unnecessary_columns_in_top_level_join() : line 1521
            AssertSql(
                @"SELECT [e1].[Name] AS [Name1], [e2].[Id] AS [Id2]
FROM [LevelOne] AS [e1]
INNER JOIN [LevelTwo] AS [e2] ON [e1].[Id] = [e2].[Level1_Optional_Id]
WHERE EXISTS (
    SELECT 1
    FROM [LevelTwo] AS [l2]
    WHERE [l2].[Level1_Required_Id] = [e1].[Id])");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Select_subquery_with_client_eval_and_navigation2() : line 2925
            AssertSql(
                @"SELECT 1
FROM [LevelTwo] AS [l2]",
                //
                @"SELECT TOP(1) [l.OneToOne_Required_FK_Inverse1].[Name]
FROM [LevelTwo] AS [l1]
INNER JOIN [LevelOne] AS [l.OneToOne_Required_FK_Inverse1] ON [l1].[Level1_Required_Id] = [l.OneToOne_Required_FK_Inverse1].[Id]
ORDER BY [l1].[Id]",
                //
                @"SELECT TOP(1) [l.OneToOne_Required_FK_Inverse1].[Name]
FROM [LevelTwo] AS [l1]
INNER JOIN [LevelOne] AS [l.OneToOne_Required_FK_Inverse1] ON [l1].[Level1_Required_Id] = [l.OneToOne_Required_FK_Inverse1].[Id]
ORDER BY [l1].[Id]",
                //
                @"SELECT TOP(1) [l.OneToOne_Required_FK_Inverse1].[Name]
FROM [LevelTwo] AS [l1]
INNER JOIN [LevelOne] AS [l.OneToOne_Required_FK_Inverse1] ON [l1].[Level1_Required_Id] = [l.OneToOne_Required_FK_Inverse1].[Id]
ORDER BY [l1].[Id]",
                //
                @"SELECT TOP(1) [l.OneToOne_Required_FK_Inverse1].[Name]
FROM [LevelTwo] AS [l1]
INNER JOIN [LevelOne] AS [l.OneToOne_Required_FK_Inverse1] ON [l1].[Level1_Required_Id] = [l.OneToOne_Required_FK_Inverse1].[Id]
ORDER BY [l1].[Id]",
                //
                @"SELECT TOP(1) [l.OneToOne_Required_FK_Inverse1].[Name]
FROM [LevelTwo] AS [l1]
INNER JOIN [LevelOne] AS [l.OneToOne_Required_FK_Inverse1] ON [l1].[Level1_Required_Id] = [l.OneToOne_Required_FK_Inverse1].[Id]
ORDER BY [l1].[Id]",
                //
                @"SELECT TOP(1) [l.OneToOne_Required_FK_Inverse1].[Name]
FROM [LevelTwo] AS [l1]
INNER JOIN [LevelOne] AS [l.OneToOne_Required_FK_Inverse1] ON [l1].[Level1_Required_Id] = [l.OneToOne_Required_FK_Inverse1].[Id]
ORDER BY [l1].[Id]",
                //
                @"SELECT TOP(1) [l.OneToOne_Required_FK_Inverse1].[Name]
FROM [LevelTwo] AS [l1]
INNER JOIN [LevelOne] AS [l.OneToOne_Required_FK_Inverse1] ON [l1].[Level1_Required_Id] = [l.OneToOne_Required_FK_Inverse1].[Id]
ORDER BY [l1].[Id]",
                //
                @"SELECT TOP(1) [l.OneToOne_Required_FK_Inverse1].[Name]
FROM [LevelTwo] AS [l1]
INNER JOIN [LevelOne] AS [l.OneToOne_Required_FK_Inverse1] ON [l1].[Level1_Required_Id] = [l.OneToOne_Required_FK_Inverse1].[Id]
ORDER BY [l1].[Id]");

Output truncated.

Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Project_collection_navigation_composed() : line 3067
            AssertSql(
                @"SELECT [l1].[Id]
FROM [LevelOne] AS [l1]
WHERE [l1].[Id] < 3
ORDER BY [l1].[Id]",
                //
                @"SELECT [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId], [t].[Id]
FROM [LevelTwo] AS [l1.OneToMany_Optional]
INNER JOIN (
    SELECT [l10].[Id]
    FROM [LevelOne] AS [l10]
    WHERE [l10].[Id] < 3
) AS [t] ON [l1.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
WHERE ([l1.OneToMany_Optional].[Name] <> N'Foo') OR [l1.OneToMany_Optional].[Name] IS NULL
ORDER BY [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_navigation_comparison3() : line 889
            AssertSql(
                @"SELECT [l1].[Id] AS [Id1], [l2].[Id] AS [Id2]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
CROSS JOIN [LevelTwo] AS [l2]
WHERE [l1.OneToOne_Optional_FK].[Id] = [l2].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Where_multiple_nav_prop_reference_optional_compared_to_null5() : line 809
            AssertSql(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [e.OneToOne_Optional_FK.OneToOne_Required_FK] ON [e.OneToOne_Optional_FK].[Id] = [e.OneToOne_Optional_FK.OneToOne_Required_FK].[Level2_Required_Id]
LEFT JOIN [LevelFour] AS [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK] ON [e.OneToOne_Optional_FK.OneToOne_Required_FK].[Id] = [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK].[Level3_Required_Id]
WHERE [e.OneToOne_Optional_FK.OneToOne_Required_FK.OneToOne_Required_FK].[Id] IS NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Projection_select_correct_table_with_anonymous_projection_in_subquery() : line 1751
            AssertSql(
                @"@__p_0='3'

SELECT TOP(@__p_0) [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId], [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [l2]
INNER JOIN [LevelOne] AS [l1] ON [l2].[Level1_Required_Id] = [l1].[Id]
INNER JOIN [LevelThree] AS [l3] ON [l1].[Id] = [l3].[Level2_Required_Id]
WHERE ([l1].[Name] = N'L1 03') AND ([l3].[Name] = N'L3 08')
ORDER BY [l1].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Join_flattening_bug_4539() : line 1125
            AssertSql(
                @"SELECT [l1_Optional].[Id], [l1_Optional].[Date], [l1_Optional].[Level1_Optional_Id], [l1_Optional].[Level1_Required_Id], [l1_Optional].[Name], [l1_Optional].[OneToMany_Optional_InverseId], [l1_Optional].[OneToMany_Optional_Self_InverseId], [l1_Optional].[OneToMany_Required_InverseId], [l1_Optional].[OneToMany_Required_Self_InverseId], [l1_Optional].[OneToOne_Optional_PK_InverseId], [l1_Optional].[OneToOne_Optional_SelfId], [l2_Required_Reverse].[Id], [l2_Required_Reverse].[Date], [l2_Required_Reverse].[Name], [l2_Required_Reverse].[OneToMany_Optional_Self_InverseId], [l2_Required_Reverse].[OneToMany_Required_Self_InverseId], [l2_Required_Reverse].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1_Optional] ON [l1].[Id] = [l1_Optional].[Level1_Optional_Id]
CROSS JOIN [LevelTwo] AS [l2]
INNER JOIN [LevelOne] AS [l2_Required_Reverse] ON [l2].[Level1_Required_Id] = [l2_Required_Reverse].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Multi_level_navigation_compared_to_null() : line 2799
            AssertSql(
                @"SELECT [l3].[Id]
FROM [LevelThree] AS [l3]
LEFT JOIN [LevelTwo] AS [l3.OneToMany_Optional_Inverse] ON [l3].[OneToMany_Optional_InverseId] = [l3.OneToMany_Optional_Inverse].[Id]
WHERE [l3.OneToMany_Optional_Inverse].[Level1_Required_Id] IS NOT NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_with_complex_subquery_with_joins_does_not_get_flattened3() : line 2377
            AssertSql(
                @"SELECT [t].[Id]
FROM [LevelOne] AS [l1_outer]
LEFT JOIN (
    SELECT [l2_inner].*
    FROM [LevelTwo] AS [l2_inner]
    LEFT JOIN [LevelOne] AS [l1_inner] ON [l2_inner].[Level1_Required_Id] = [l1_inner].[Id]
) AS [t] ON [l1_outer].[Id] = [t].[Level1_Required_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Method_call_on_optional_navigation_translates_to_null_conditional_properly_for_arguments() : line 363
            AssertSql(
                @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_InverseId], [e1].[OneToMany_Required_Self_InverseId], [e1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e1]
LEFT JOIN [LevelTwo] AS [e1.OneToOne_Optional_FK] ON [e1].[Id] = [e1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE ([e1.OneToOne_Optional_FK].[Name] LIKE [e1.OneToOne_Optional_FK].[Name] + N'%' AND (CHARINDEX([e1.OneToOne_Optional_FK].[Name], [e1.OneToOne_Optional_FK].[Name]) = 1)) OR ([e1.OneToOne_Optional_FK].[Name] = N'')");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Projection_select_correct_table_from_subquery_when_materialization_is_not_required() : line 1738
            AssertSql(
                @"@__p_0='3'

SELECT TOP(@__p_0) [l2].[Name]
FROM [LevelTwo] AS [l2]
INNER JOIN [LevelOne] AS [l2.OneToOne_Required_FK_Inverse] ON [l2].[Level1_Required_Id] = [l2.OneToOne_Required_FK_Inverse].[Id]
WHERE [l2.OneToOne_Required_FK_Inverse].[Name] = N'L1 03'
ORDER BY [l2].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Multiple_SelectMany_with_navigation_and_explicit_DefaultIfEmpty() : line 2124
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
LEFT JOIN (
    SELECT [l1.OneToMany_Optional.OneToMany_Optional].*
    FROM [LevelThree] AS [l1.OneToMany_Optional.OneToMany_Optional]
    WHERE [l1.OneToMany_Optional.OneToMany_Optional].[Id] > 5
) AS [t] ON [l1.OneToMany_Optional].[Id] = [t].[OneToMany_Optional_InverseId]
WHERE [t].[Id] IS NOT NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Explicit_GroupJoin_in_subquery_with_unrelated_projection2() : line 2650
            AssertSql(
                @"SELECT [t].[Id]
FROM (
    SELECT DISTINCT [l1].*
    FROM [LevelOne] AS [l1]
    LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
    WHERE ([l2].[Name] <> N'Foo') OR [l2].[Name] IS NULL
) AS [t]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Navigation_inside_method_call_translated_to_join2() : line 319
            AssertSql(
                @"SELECT [e3].[Id], [e3].[Level2_Optional_Id], [e3].[Level2_Required_Id], [e3].[Name], [e3].[OneToMany_Optional_InverseId], [e3].[OneToMany_Optional_Self_InverseId], [e3].[OneToMany_Required_InverseId], [e3].[OneToMany_Required_Self_InverseId], [e3].[OneToOne_Optional_PK_InverseId], [e3].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [e3]
INNER JOIN [LevelTwo] AS [e3.OneToOne_Required_FK_Inverse] ON [e3].[Level2_Required_Id] = [e3.OneToOne_Required_FK_Inverse].[Id]
WHERE [e3.OneToOne_Required_FK_Inverse].[Name] LIKE N'L' + N'%' AND (CHARINDEX(N'L', [e3.OneToOne_Required_FK_Inverse].[Name]) = 1)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Where_predicate_on_optional_reference_navigation() : line 1779
            AssertSql(
                @"@__p_0='3'

SELECT TOP(@__p_0) [l1].[Name]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Required_FK] ON [l1].[Id] = [l1.OneToOne_Required_FK].[Level1_Required_Id]
WHERE [l1.OneToOne_Required_FK].[Name] = N'L2 03'
ORDER BY [l1].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Where_multiple_nav_prop_reference_optional_compared_to_null3() : line 786
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_FK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
WHERE [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Id] IS NOT NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Select_nav_prop_reference_optional2() : line 652
            AssertSql(
                @"SELECT [e.OneToOne_Optional_FK].[Id]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Include_nested_with_optional_navigation() : line 1077
            AssertSql(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK].[Date], [e.OneToOne_Optional_FK].[Level1_Optional_Id], [e.OneToOne_Optional_FK].[Level1_Required_Id], [e.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE ([e.OneToOne_Optional_FK].[Name] <> N'L2 09') OR [e.OneToOne_Optional_FK].[Name] IS NULL
ORDER BY [e.OneToOne_Optional_FK].[Id]",
                //
                @"SELECT [e.OneToOne_Optional_FK.OneToMany_Required].[Id], [e.OneToOne_Optional_FK.OneToMany_Required].[Level2_Optional_Id], [e.OneToOne_Optional_FK.OneToMany_Required].[Level2_Required_Id], [e.OneToOne_Optional_FK.OneToMany_Required].[Name], [e.OneToOne_Optional_FK.OneToMany_Required].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK.OneToMany_Required].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK.OneToMany_Required].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK.OneToMany_Required].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK.OneToMany_Required].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK.OneToMany_Required].[OneToOne_Optional_SelfId], [l.OneToOne_Required_FK].[Id], [l.OneToOne_Required_FK].[Level3_Optional_Id], [l.OneToOne_Required_FK].[Level3_Required_Id], [l.OneToOne_Required_FK].[Name], [l.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [e.OneToOne_Optional_FK.OneToMany_Required]
LEFT JOIN [LevelFour] AS [l.OneToOne_Required_FK] ON [e.OneToOne_Optional_FK.OneToMany_Required].[Id] = [l.OneToOne_Required_FK].[Level3_Required_Id]
INNER JOIN (
    SELECT DISTINCT [e.OneToOne_Optional_FK0].[Id]
    FROM [LevelOne] AS [e0]
    LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK0] ON [e0].[Id] = [e.OneToOne_Optional_FK0].[Level1_Optional_Id]
    WHERE ([e.OneToOne_Optional_FK0].[Name] <> N'L2 09') OR [e.OneToOne_Optional_FK0].[Name] IS NULL
) AS [t] ON [e.OneToOne_Optional_FK.OneToMany_Required].[OneToMany_Required_InverseId] = [t].[Id]
ORDER BY [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Required_navigation_on_a_subquery_with_First_in_projection() : line 2224
            AssertSql(
                @"SELECT 1
FROM [LevelTwo] AS [l2o]
WHERE [l2o].[Id] = 7",
                //
                @"SELECT TOP(1) [l2i.OneToOne_Required_FK_Inverse0].[Name]
FROM [LevelTwo] AS [l2i0]
INNER JOIN [LevelOne] AS [l2i.OneToOne_Required_FK_Inverse0] ON [l2i0].[Level1_Required_Id] = [l2i.OneToOne_Required_FK_Inverse0].[Id]
ORDER BY [l2i0].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Join_navigation_key_access_optional() : line 268
            AssertSql(
                @"SELECT [e1].[Id] AS [Id1], [e2].[Id] AS [Id2]
FROM [LevelOne] AS [e1]
INNER JOIN [LevelTwo] AS [e2] ON [e1].[Id] = [e2].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Multiple_SelectMany_with_Include() : line 1845
            AssertSql(
                @"SELECT [l1.OneToMany_Optional.OneToMany_Optional].[Id], [l1.OneToMany_Optional.OneToMany_Optional].[Level2_Optional_Id], [l1.OneToMany_Optional.OneToMany_Optional].[Level2_Required_Id], [l1.OneToMany_Optional.OneToMany_Optional].[Name], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Id], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Level3_Optional_Id], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Level3_Required_Id], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Name], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
INNER JOIN [LevelThree] AS [l1.OneToMany_Optional.OneToMany_Optional] ON [l1.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId]
LEFT JOIN [LevelFour] AS [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK] ON [l1.OneToMany_Optional.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Level3_Required_Id]
ORDER BY [l1.OneToMany_Optional.OneToMany_Optional].[Id]",
                //
                @"SELECT [l1.OneToMany_Optional.OneToMany_Optional.OneToMany_Optional].[Id], [l1.OneToMany_Optional.OneToMany_Optional.OneToMany_Optional].[Level3_Optional_Id], [l1.OneToMany_Optional.OneToMany_Optional.OneToMany_Optional].[Level3_Required_Id], [l1.OneToMany_Optional.OneToMany_Optional.OneToMany_Optional].[Name], [l1.OneToMany_Optional.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelFour] AS [l1.OneToMany_Optional.OneToMany_Optional.OneToMany_Optional]
INNER JOIN (
    SELECT DISTINCT [l1.OneToMany_Optional.OneToMany_Optional0].[Id]
    FROM [LevelOne] AS [l10]
    INNER JOIN [LevelTwo] AS [l1.OneToMany_Optional0] ON [l10].[Id] = [l1.OneToMany_Optional0].[OneToMany_Optional_InverseId]
    INNER JOIN [LevelThree] AS [l1.OneToMany_Optional.OneToMany_Optional0] ON [l1.OneToMany_Optional0].[Id] = [l1.OneToMany_Optional.OneToMany_Optional0].[OneToMany_Optional_InverseId]
    LEFT JOIN [LevelFour] AS [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK0] ON [l1.OneToMany_Optional.OneToMany_Optional0].[Id] = [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK0].[Level3_Required_Id]
) AS [t] ON [l1.OneToMany_Optional.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
ORDER BY [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Correlated_nested_two_levels_up_subquery_doesnt_project_unnecessary_columns_in_top_level() : line 1550
            AssertSql(
                @"SELECT DISTINCT [l1].[Name]
FROM [LevelOne] AS [l1]
WHERE EXISTS (
    SELECT 1
    FROM [LevelTwo] AS [l2]
    WHERE EXISTS (
        SELECT 1
        FROM [LevelThree] AS [l3]))");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Navigations_compared_to_each_other3() : line 2842
            AssertSql(
                @"SELECT [l2].[Name]
FROM [LevelTwo] AS [l2]
WHERE EXISTS (
    SELECT 1
    FROM [LevelThree] AS [i]
    WHERE [l2].[Id] = [i].[OneToMany_Optional_InverseId])");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Select_multiple_nav_prop_reference_optional() : line 728
            AssertSql(
                @"SELECT [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[Id]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [e.OneToOne_Optional_FK.OneToOne_Optional_FK] ON [e.OneToOne_Optional_FK].[Id] = [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Where_multiple_nav_prop_optional_required() : line 855
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1.OneToOne_Optional_FK.OneToOne_Required_FK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Level2_Required_Id]
WHERE ([l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Name] <> N'L3 05') OR [l1.OneToOne_Optional_FK.OneToOne_Required_FK].[Name] IS NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Project_collection_and_include() : line 3107
            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l]
ORDER BY [l].[Id]",
                //
                @"SELECT [l.OneToMany_Optional0].[Id], [l.OneToMany_Optional0].[Date], [l.OneToMany_Optional0].[Level1_Optional_Id], [l.OneToMany_Optional0].[Level1_Required_Id], [l.OneToMany_Optional0].[Name], [l.OneToMany_Optional0].[OneToMany_Optional_InverseId], [l.OneToMany_Optional0].[OneToMany_Optional_Self_InverseId], [l.OneToMany_Optional0].[OneToMany_Required_InverseId], [l.OneToMany_Optional0].[OneToMany_Required_Self_InverseId], [l.OneToMany_Optional0].[OneToOne_Optional_PK_InverseId], [l.OneToMany_Optional0].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [l.OneToMany_Optional0]
INNER JOIN (
    SELECT [l1].[Id]
    FROM [LevelOne] AS [l1]
) AS [t0] ON [l.OneToMany_Optional0].[OneToMany_Optional_InverseId] = [t0].[Id]
ORDER BY [t0].[Id]",
                //
                @"SELECT [l.OneToMany_Optional].[Id], [l.OneToMany_Optional].[Date], [l.OneToMany_Optional].[Level1_Optional_Id], [l.OneToMany_Optional].[Level1_Required_Id], [l.OneToMany_Optional].[Name], [l.OneToMany_Optional].[OneToMany_Optional_InverseId], [l.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l.OneToMany_Optional].[OneToMany_Required_InverseId], [l.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l.OneToMany_Optional].[OneToOne_Optional_SelfId], [t].[Id]
FROM [LevelTwo] AS [l.OneToMany_Optional]
INNER JOIN (
    SELECT [l0].[Id]
    FROM [LevelOne] AS [l0]
) AS [t] ON [l.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
ORDER BY [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Multiple_SelectMany_with_string_based_Include() : line 1892
            AssertSql(
                @"SELECT [l1.OneToMany_Optional.OneToMany_Optional].[Id], [l1.OneToMany_Optional.OneToMany_Optional].[Level2_Optional_Id], [l1.OneToMany_Optional.OneToMany_Optional].[Level2_Required_Id], [l1.OneToMany_Optional.OneToMany_Optional].[Name], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Id], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Level3_Optional_Id], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Level3_Required_Id], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Name], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
INNER JOIN [LevelThree] AS [l1.OneToMany_Optional.OneToMany_Optional] ON [l1.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId]
LEFT JOIN [LevelFour] AS [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK] ON [l1.OneToMany_Optional.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToMany_Optional.OneToOne_Required_FK].[Level3_Required_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Multi_level_include_correct_PK_is_chosen_as_the_join_predicate_for_queries_that_join_same_table_multiple_times() : line 186
            AssertSql(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e]
ORDER BY [e].[Id]",
                //
                @"SELECT [e.OneToMany_Optional].[Id], [e.OneToMany_Optional].[Date], [e.OneToMany_Optional].[Level1_Optional_Id], [e.OneToMany_Optional].[Level1_Required_Id], [e.OneToMany_Optional].[Name], [e.OneToMany_Optional].[OneToMany_Optional_InverseId], [e.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [e.OneToMany_Optional].[OneToMany_Required_InverseId], [e.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [e.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [e.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [e.OneToMany_Optional]
INNER JOIN (
    SELECT [e0].[Id]
    FROM [LevelOne] AS [e0]
) AS [t] ON [e.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
ORDER BY [t].[Id], [e.OneToMany_Optional].[Id]",
                //
                @"SELECT [e.OneToMany_Optional.OneToMany_Optional].[Id], [e.OneToMany_Optional.OneToMany_Optional].[Level2_Optional_Id], [e.OneToMany_Optional.OneToMany_Optional].[Level2_Required_Id], [e.OneToMany_Optional.OneToMany_Optional].[Name], [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [e.OneToMany_Optional.OneToMany_Optional].[OneToOne_Optional_SelfId], [l.OneToMany_Required_Inverse].[Id], [l.OneToMany_Required_Inverse].[Date], [l.OneToMany_Required_Inverse].[Level1_Optional_Id], [l.OneToMany_Required_Inverse].[Level1_Required_Id], [l.OneToMany_Required_Inverse].[Name], [l.OneToMany_Required_Inverse].[OneToMany_Optional_InverseId], [l.OneToMany_Required_Inverse].[OneToMany_Optional_Self_InverseId], [l.OneToMany_Required_Inverse].[OneToMany_Required_InverseId], [l.OneToMany_Required_Inverse].[OneToMany_Required_Self_InverseId], [l.OneToMany_Required_Inverse].[OneToOne_Optional_PK_InverseId], [l.OneToMany_Required_Inverse].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [e.OneToMany_Optional.OneToMany_Optional]
INNER JOIN [LevelTwo] AS [l.OneToMany_Required_Inverse] ON [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Required_InverseId] = [l.OneToMany_Required_Inverse].[Id]
INNER JOIN (
    SELECT DISTINCT [e.OneToMany_Optional0].[Id], [t0].[Id] AS [Id0]
    FROM [LevelTwo] AS [e.OneToMany_Optional0]
    INNER JOIN (
        SELECT [e1].[Id]
        FROM [LevelOne] AS [e1]
    ) AS [t0] ON [e.OneToMany_Optional0].[OneToMany_Optional_InverseId] = [t0].[Id]
) AS [t1] ON [e.OneToMany_Optional.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t1].[Id]
ORDER BY [t1].[Id0], [t1].[Id], [l.OneToMany_Required_Inverse].[Id]",
                //
                @"SELECT [l.OneToMany_Required_Inverse.OneToMany_Optional].[Id], [l.OneToMany_Required_Inverse.OneToMany_Optional].[Level2_Optional_Id], [l.OneToMany_Required_Inverse.OneToMany_Optional].[Level2_Required_Id], [l.OneToMany_Required_Inverse.OneToMany_Optional].[Name], [l.OneToMany_Required_Inverse.OneToMany_Optional].[OneToMany_Optional_InverseId], [l.OneToMany_Required_Inverse.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l.OneToMany_Required_Inverse.OneToMany_Optional].[OneToMany_Required_InverseId], [l.OneToMany_Required_Inverse.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l.OneToMany_Required_Inverse.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l.OneToMany_Required_Inverse.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l.OneToMany_Required_Inverse.OneToMany_Optional]
INNER JOIN (
    SELECT DISTINCT [l.OneToMany_Required_Inverse0].[Id], [t3].[Id0], [t3].[Id] AS [Id1]
    FROM [LevelThree] AS [e.OneToMany_Optional.OneToMany_Optional0]
    INNER JOIN [LevelTwo] AS [l.OneToMany_Required_Inverse0] ON [e.OneToMany_Optional.OneToMany_Optional0].[OneToMany_Required_InverseId] = [l.OneToMany_Required_Inverse0].[Id]
    INNER JOIN (
        SELECT DISTINCT [e.OneToMany_Optional1].[Id], [t2].[Id] AS [Id0]
        FROM [LevelTwo] AS [e.OneToMany_Optional1]
        INNER JOIN (
            SELECT [e2].[Id]
            FROM [LevelOne] AS [e2]
        ) AS [t2] ON [e.OneToMany_Optional1].[OneToMany_Optional_InverseId] = [t2].[Id]
    ) AS [t3] ON [e.OneToMany_Optional.OneToMany_Optional0].[OneToMany_Optional_InverseId] = [t3].[Id]
) AS [t4] ON [l.OneToMany_Required_Inverse.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t4].[Id]
ORDER BY [t4].[Id0], [t4].[Id1], [t4].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Navigation_filter_navigation_grouping_ordering_by_group_key() : line 2758
            AssertSql(
                @"@__level1Id_0='1'

SELECT [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId], [l2.OneToMany_Required_Self_Inverse].[Name]
FROM [LevelTwo] AS [l2]
INNER JOIN [LevelTwo] AS [l2.OneToMany_Required_Self_Inverse] ON [l2].[OneToMany_Required_Self_InverseId] = [l2.OneToMany_Required_Self_Inverse].[Id]
WHERE [l2].[OneToMany_Required_InverseId] = @__level1Id_0
ORDER BY [l2.OneToMany_Required_Self_Inverse].[Name]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Join_navigation_key_access_required() : line 278
            AssertSql(
                @"SELECT [e1].[Id] AS [Id1], [e2].[Id] AS [Id2]
FROM [LevelOne] AS [e1]
INNER JOIN [LevelTwo] AS [e2] ON [e1].[Id] = [e2].[Level1_Required_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Multiple_complex_includes_self_ref() : line 572
            AssertSql(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_Self].[Id], [e.OneToOne_Optional_Self].[Date], [e.OneToOne_Optional_Self].[Name], [e.OneToOne_Optional_Self].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_Self].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_Self].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelOne] AS [e.OneToOne_Optional_Self] ON [e].[OneToOne_Optional_SelfId] = [e.OneToOne_Optional_Self].[Id]
ORDER BY [e.OneToOne_Optional_Self].[Id], [e].[Id]",
                //
                @"SELECT [e.OneToMany_Optional_Self].[Id], [e.OneToMany_Optional_Self].[Date], [e.OneToMany_Optional_Self].[Name], [e.OneToMany_Optional_Self].[OneToMany_Optional_Self_InverseId], [e.OneToMany_Optional_Self].[OneToMany_Required_Self_InverseId], [e.OneToMany_Optional_Self].[OneToOne_Optional_SelfId], [l.OneToOne_Optional_Self].[Id], [l.OneToOne_Optional_Self].[Date], [l.OneToOne_Optional_Self].[Name], [l.OneToOne_Optional_Self].[OneToMany_Optional_Self_InverseId], [l.OneToOne_Optional_Self].[OneToMany_Required_Self_InverseId], [l.OneToOne_Optional_Self].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e.OneToMany_Optional_Self]
LEFT JOIN [LevelOne] AS [l.OneToOne_Optional_Self] ON [e.OneToMany_Optional_Self].[OneToOne_Optional_SelfId] = [l.OneToOne_Optional_Self].[Id]
INNER JOIN (
    SELECT DISTINCT [e1].[Id], [e.OneToOne_Optional_Self1].[Id] AS [Id0]
    FROM [LevelOne] AS [e1]
    LEFT JOIN [LevelOne] AS [e.OneToOne_Optional_Self1] ON [e1].[OneToOne_Optional_SelfId] = [e.OneToOne_Optional_Self1].[Id]
) AS [t0] ON [e.OneToMany_Optional_Self].[OneToMany_Optional_Self_InverseId] = [t0].[Id]
ORDER BY [t0].[Id0], [t0].[Id]",
                //
                @"SELECT [e.OneToOne_Optional_Self.OneToMany_Optional_Self].[Id], [e.OneToOne_Optional_Self.OneToMany_Optional_Self].[Date], [e.OneToOne_Optional_Self.OneToMany_Optional_Self].[Name], [e.OneToOne_Optional_Self.OneToMany_Optional_Self].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_Self.OneToMany_Optional_Self].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_Self.OneToMany_Optional_Self].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e.OneToOne_Optional_Self.OneToMany_Optional_Self]
INNER JOIN (
    SELECT DISTINCT [e.OneToOne_Optional_Self0].[Id]
    FROM [LevelOne] AS [e0]
    LEFT JOIN [LevelOne] AS [e.OneToOne_Optional_Self0] ON [e0].[OneToOne_Optional_SelfId] = [e.OneToOne_Optional_Self0].[Id]
) AS [t] ON [e.OneToOne_Optional_Self.OneToMany_Optional_Self].[OneToMany_Optional_Self_InverseId] = [t].[Id]
ORDER BY [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Select_join_subquery_containing_filter_and_distinct() : line 2155
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_InverseId], [t].[OneToMany_Optional_Self_InverseId], [t].[OneToMany_Required_InverseId], [t].[OneToMany_Required_Self_InverseId], [t].[OneToOne_Optional_PK_InverseId], [t].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
INNER JOIN (
    SELECT DISTINCT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
    FROM [LevelTwo] AS [l]
    WHERE [l].[Id] > 2
) AS [t] ON [l1].[Id] = [t].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Required_navigation_on_a_subquery_with_complex_projection_and_First() : line 2239
            AssertSql(
                @"SELECT 1
FROM [LevelTwo] AS [l2o]
WHERE [l2o].[Id] = 7",
                //
                @"SELECT TOP(1) [l2i.OneToOne_Required_FK_Inverse].[Id], [l2i.OneToOne_Required_FK_Inverse].[Date], [l2i.OneToOne_Required_FK_Inverse].[Name], [l2i.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l2i.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l2i.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [l2i]
INNER JOIN [LevelOne] AS [l2i.OneToOne_Required_FK_Inverse] ON [l2i].[Level1_Required_Id] = [l2i.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [LevelOne] AS [l1i] ON [l2i].[Level1_Required_Id] = [l1i].[Id]
ORDER BY [l2i].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Explicit_GroupJoin_in_subquery_with_unrelated_projection3() : line 2664
            AssertSql(
                @"SELECT DISTINCT [l1].[Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
WHERE ([l2].[Name] <> N'Foo') OR [l2].[Name] IS NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Complex_navigations_with_predicate_projected_into_anonymous_type() : line 952
            AssertSql(
                @"SELECT [e].[Name], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Id]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Required_FK] ON [e].[Id] = [e.OneToOne_Required_FK].[Level1_Required_Id]
LEFT JOIN [LevelThree] AS [e.OneToOne_Required_FK.OneToOne_Required_FK] ON [e.OneToOne_Required_FK].[Id] = [e.OneToOne_Required_FK.OneToOne_Required_FK].[Level2_Required_Id]
LEFT JOIN [LevelThree] AS [e.OneToOne_Required_FK.OneToOne_Optional_FK] ON [e.OneToOne_Required_FK].[Id] = [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
WHERE ((([e.OneToOne_Required_FK.OneToOne_Required_FK].[Id] = [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Id]) AND ([e.OneToOne_Required_FK.OneToOne_Required_FK].[Id] IS NOT NULL AND [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Id] IS NOT NULL)) OR ([e.OneToOne_Required_FK.OneToOne_Required_FK].[Id] IS NULL AND [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Id] IS NULL)) AND (([e.OneToOne_Required_FK.OneToOne_Optional_FK].[Id] <> 7) OR [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Id] IS NULL)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.OrderBy_nav_prop_reference_optional() : line 991
            AssertSql(
                @"SELECT [e].[Id]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
ORDER BY [e.OneToOne_Optional_FK].[Name], [e].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Navigations_compared_to_each_other1() : line 2822
            AssertSql(
                @"SELECT [l2].[Name]
FROM [LevelTwo] AS [l2]
WHERE [l2].[OneToMany_Required_InverseId] = [l2].[OneToMany_Required_InverseId]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Key_equality_using_property_method_required() : line 51
            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l.OneToOne_Required_FK] ON [l].[Id] = [l.OneToOne_Required_FK].[Level1_Required_Id]
WHERE [l.OneToOne_Required_FK].[Id] > 7");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Select_nav_prop_reference_optional1_via_DefaultIfEmpty() : line 642
            AssertSql(
                @"SELECT [l2].[Name]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Where_complex_predicate_with_with_nav_prop_and_OrElse1() : line 901
            AssertSql(
                @"SELECT [l1].[Id] AS [Id1], [l2].[Id] AS [Id2]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
CROSS JOIN [LevelTwo] AS [l2]
INNER JOIN [LevelOne] AS [l2.OneToOne_Required_FK_Inverse] ON [l2].[Level1_Required_Id] = [l2.OneToOne_Required_FK_Inverse].[Id]
WHERE ([l1.OneToOne_Optional_FK].[Name] = N'L2 01') OR (([l2.OneToOne_Required_FK_Inverse].[Name] <> N'Bar') OR [l2.OneToOne_Required_FK_Inverse].[Name] IS NULL)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Key_equality_using_property_method_and_member_expression1() : line 93
            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l.OneToOne_Required_FK] ON [l].[Id] = [l.OneToOne_Required_FK].[Level1_Required_Id]
WHERE [l.OneToOne_Required_FK].[Id] = 7");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_with_complex_subquery_with_joins_does_not_get_flattened() : line 2349
            AssertSql(
                @"SELECT [t].[Id]
FROM [LevelOne] AS [l1_outer]
LEFT JOIN (
    SELECT [l2_inner].*
    FROM [LevelTwo] AS [l2_inner]
    INNER JOIN [LevelOne] AS [l1_inner] ON [l2_inner].[Level1_Required_Id] = [l1_inner].[Id]
) AS [t] ON [l1_outer].[Id] = [t].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_with_complex_subquery_with_joins_does_not_get_flattened2() : line 2363
            AssertSql(
                @"SELECT [t].[Id]
FROM [LevelOne] AS [l1_outer]
LEFT JOIN (
    SELECT [l2_inner].*
    FROM [LevelTwo] AS [l2_inner]
    INNER JOIN [LevelOne] AS [l1_inner] ON [l2_inner].[Level1_Required_Id] = [l1_inner].[Id]
) AS [t] ON [l1_outer].[Id] = [t].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_nested_navigation_property_required() : line 1194
            AssertSql(
                @"SELECT [l1.OneToOne_Required_FK.OneToMany_Optional].[Id], [l1.OneToOne_Required_FK.OneToMany_Optional].[Level2_Optional_Id], [l1.OneToOne_Required_FK.OneToMany_Optional].[Level2_Required_Id], [l1.OneToOne_Required_FK.OneToMany_Optional].[Name], [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToOne_Required_FK] ON [l1].[Id] = [l1.OneToOne_Required_FK].[Level1_Required_Id]
INNER JOIN [LevelThree] AS [l1.OneToOne_Required_FK.OneToMany_Optional] ON [l1.OneToOne_Required_FK].[Id] = [l1.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_InverseId]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Multiple_optional_navigation_with_string_based_Include() : line 2029
            AssertSql(
                @"SELECT [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Level2_Optional_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Level2_Required_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Name], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_PK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[OneToOne_Optional_PK_InverseId]
ORDER BY [l1.OneToOne_Optional_FK.OneToOne_Optional_PK].[Id]",
                //
                @"SELECT [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[Level3_Optional_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[Level3_Required_Id], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[Name], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelFour] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional]
INNER JOIN (
    SELECT DISTINCT [l1.OneToOne_Optional_FK.OneToOne_Optional_PK0].[Id]
    FROM [LevelOne] AS [l10]
    LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK0] ON [l10].[Id] = [l1.OneToOne_Optional_FK0].[Level1_Optional_Id]
    LEFT JOIN [LevelThree] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_PK0] ON [l1.OneToOne_Optional_FK0].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Optional_PK0].[OneToOne_Optional_PK_InverseId]
) AS [t] ON [l1.OneToOne_Optional_FK.OneToOne_Optional_PK.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
ORDER BY [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Multiple_required_navigation_using_multiple_selects_with_Include() : line 1939
            AssertSql(
                @"SELECT [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Date], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Level1_Optional_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Level1_Required_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Name], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Level2_Optional_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Level2_Required_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Name], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [LevelFour] AS [l4]
INNER JOIN [LevelThree] AS [l4.OneToOne_Required_FK_Inverse] ON [l4].[Level3_Required_Id] = [l4.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [LevelTwo] AS [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse] ON [l4.OneToOne_Required_FK_Inverse].[Level2_Required_Id] = [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]
LEFT JOIN [LevelThree] AS [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK] ON [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id] = [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Level2_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Key_equality_using_property_method_and_member_expression3() : line 115
            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [l]
WHERE [l].[Level1_Required_Id] = 7");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Join_condition_optimizations_applied_correctly_when_anonymous_type_with_single_property() : line 2738
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l2] ON ([l1].[OneToMany_Optional_Self_InverseId] = [l2].[Level1_Optional_Id]) OR ([l1].[OneToMany_Optional_Self_InverseId] IS NULL AND [l2].[Level1_Optional_Id] IS NULL)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.OrderBy_nav_prop_reference_optional_via_DefaultIfEmpty() : line 1002
            AssertSql(
                @"SELECT [l1].[Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
ORDER BY [l2].[Name], [l1].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Nested_group_join_with_take() : line 2772
            AssertSql(
                @"@__p_0='2'

SELECT [l2_outer].[Name]
FROM (
    SELECT TOP(@__p_0) [l2_inner].*
    FROM [LevelOne] AS [l1_inner]
    LEFT JOIN [LevelTwo] AS [l2_inner] ON [l1_inner].[Id] = [l2_inner].[Level1_Optional_Id]
    ORDER BY [l1_inner].[Id]
) AS [t]
LEFT JOIN [LevelTwo] AS [l2_outer] ON [t].[Id] = [l2_outer].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Null_protection_logic_work_for_inner_key_access_of_manually_created_GroupJoin2() : line 1612
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_InverseId], [t].[OneToMany_Optional_Self_InverseId], [t].[OneToMany_Required_InverseId], [t].[OneToMany_Required_Self_InverseId], [t].[OneToOne_Optional_PK_InverseId], [t].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN (
    SELECT [l1.OneToOne_Required_FK].[Id], [l1.OneToOne_Required_FK].[Date], [l1.OneToOne_Required_FK].[Level1_Optional_Id], [l1.OneToOne_Required_FK].[Level1_Required_Id], [l1.OneToOne_Required_FK].[Name], [l1.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
    FROM [LevelOne] AS [l10]
    LEFT JOIN [LevelTwo] AS [l1.OneToOne_Required_FK] ON [l10].[Id] = [l1.OneToOne_Required_FK].[Level1_Required_Id]
) AS [t] ON [l1].[Id] = [t].[Level1_Optional_Id]
ORDER BY [l1].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Null_protection_logic_work_for_outer_key_access_of_manually_created_GroupJoin() : line 1627
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l1.OneToOne_Required_FK].[Id], [l1.OneToOne_Required_FK].[Date], [l1.OneToOne_Required_FK].[Level1_Optional_Id], [l1.OneToOne_Required_FK].[Level1_Required_Id], [l1.OneToOne_Required_FK].[Name], [l1.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Required_FK].[OneToOne_Optional_SelfId], [l10].[Id], [l10].[Date], [l10].[Name], [l10].[OneToMany_Optional_Self_InverseId], [l10].[OneToMany_Required_Self_InverseId], [l10].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Required_FK] ON [l1].[Id] = [l1.OneToOne_Required_FK].[Level1_Required_Id]
LEFT JOIN [LevelOne] AS [l10] ON [l1.OneToOne_Required_FK].[Level1_Optional_Id] = [l10].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Join_navigation_translated_to_subquery_deeply_nested_non_key_join() : line 510
            AssertSql(
                @"SELECT [e4].[Id] AS [Id4], [e4].[Name] AS [Name4], [e1].[Id] AS [Id1], [e1].[Name] AS [Name1]
FROM [LevelFour] AS [e4]
INNER JOIN [LevelOne] AS [e1] ON [e4].[Name] IN (
    SELECT TOP(1) [subQuery.OneToOne_Optional_FK.OneToOne_Required_PK0].[Name]
    FROM [LevelTwo] AS [subQuery0]
    LEFT JOIN [LevelThree] AS [subQuery.OneToOne_Optional_FK0] ON [subQuery0].[Id] = [subQuery.OneToOne_Optional_FK0].[Level2_Optional_Id]
    LEFT JOIN [LevelFour] AS [subQuery.OneToOne_Optional_FK.OneToOne_Required_PK0] ON [subQuery.OneToOne_Optional_FK0].[Id] = [subQuery.OneToOne_Optional_FK.OneToOne_Required_PK0].[Id]
    WHERE [subQuery0].[Level1_Required_Id] = [e1].[Id]
)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_with_string_based_Include2() : line 1880
            AssertSql(
                @"SELECT [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional.OneToOne_Required_FK].[Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Optional_Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Required_Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Name], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[Id], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[Level3_Optional_Id], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[Level3_Required_Id], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[Name], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
LEFT JOIN [LevelThree] AS [l1.OneToMany_Optional.OneToOne_Required_FK] ON [l1.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Required_Id]
LEFT JOIN [LevelFour] AS [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK] ON [l1.OneToMany_Optional.OneToOne_Required_FK].[Id] = [l1.OneToMany_Optional.OneToOne_Required_FK.OneToOne_Required_FK].[Level3_Required_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Order_by_key_of_anonymous_type_projected_navigation_doesnt_get_optimized_into_FK_access_subquery() : line 1711
            AssertSql(
                @"@__p_0='10'

SELECT TOP(@__p_0) [l3.OneToOne_Required_FK_Inverse].[Id], [l3.OneToOne_Required_FK_Inverse].[Date], [l3.OneToOne_Required_FK_Inverse].[Level1_Optional_Id], [l3.OneToOne_Required_FK_Inverse].[Level1_Required_Id], [l3.OneToOne_Required_FK_Inverse].[Name], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId], [l3].[Name] AS [name0]
FROM [LevelThree] AS [l3]
INNER JOIN [LevelTwo] AS [l3.OneToOne_Required_FK_Inverse] ON [l3].[Level2_Required_Id] = [l3.OneToOne_Required_FK_Inverse].[Id]
ORDER BY [l3].[Level2_Required_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Navigations_compared_to_each_other4() : line 2855
            AssertSql(
                @"SELECT [l2].[Name]
FROM [LevelTwo] AS [l2]
LEFT JOIN [LevelThree] AS [l2.OneToOne_Required_FK] ON [l2].[Id] = [l2.OneToOne_Required_FK].[Level2_Required_Id]
WHERE EXISTS (
    SELECT 1
    FROM [LevelFour] AS [i]
    WHERE [l2.OneToOne_Required_FK].[Id] = [i].[OneToMany_Optional_InverseId])");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Optional_navigation_propagates_nullability_to_manually_created_left_join1() : line 2282
            AssertSql(
                @"SELECT [ll.OneToOne_Optional_FK].[Id] AS [Id1], [l1].[Id] AS [Id2]
FROM [LevelOne] AS [ll]
LEFT JOIN [LevelTwo] AS [ll.OneToOne_Optional_FK] ON [ll].[Id] = [ll.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [LevelTwo] AS [l1] ON [ll.OneToOne_Optional_FK].[Level1_Required_Id] = [l1].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Multiple_include_with_multiple_optional_navigations() : line 1478
            AssertSql(
                @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK].[Date], [e.OneToOne_Optional_FK].[Level1_Optional_Id], [e.OneToOne_Optional_FK].[Level1_Required_Id], [e.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Required_Id], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[OneToOne_Optional_SelfId], [e.OneToOne_Required_FK].[Id], [e.OneToOne_Required_FK].[Date], [e.OneToOne_Required_FK].[Level1_Optional_Id], [e.OneToOne_Required_FK].[Level1_Required_Id], [e.OneToOne_Required_FK].[Name], [e.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Required_FK].[OneToMany_Required_InverseId], [e.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Required_FK].[OneToOne_Optional_SelfId], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Id], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Level2_Optional_Id], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Level2_Required_Id], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Name], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Required_FK.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [e.OneToOne_Optional_FK.OneToOne_Optional_FK] ON [e.OneToOne_Optional_FK].[Id] = [e.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Required_FK] ON [e].[Id] = [e.OneToOne_Required_FK].[Level1_Required_Id]
LEFT JOIN [LevelThree] AS [e.OneToOne_Required_FK.OneToOne_Optional_PK] ON [e.OneToOne_Required_FK].[Id] = [e.OneToOne_Required_FK.OneToOne_Optional_PK].[OneToOne_Optional_PK_InverseId]
LEFT JOIN [LevelThree] AS [e.OneToOne_Required_FK.OneToOne_Optional_FK] ON [e.OneToOne_Required_FK].[Id] = [e.OneToOne_Required_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
WHERE ([e.OneToOne_Required_FK.OneToOne_Optional_PK].[Name] <> N'Foo') OR [e.OneToOne_Required_FK.OneToOne_Optional_PK].[Name] IS NULL
ORDER BY [e].[Id], [e.OneToOne_Required_FK].[Id]",
                //
                @"SELECT [e.OneToOne_Required_FK.OneToMany_Optional].[Id], [e.OneToOne_Required_FK.OneToMany_Optional].[Level2_Optional_Id], [e.OneToOne_Required_FK.OneToMany_Optional].[Level2_Required_Id], [e.OneToOne_Required_FK.OneToMany_Optional].[Name], [e.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_InverseId], [e.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Required_InverseId], [e.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [e.OneToOne_Required_FK.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Required_FK.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [e.OneToOne_Required_FK.OneToMany_Optional]
INNER JOIN (
    SELECT DISTINCT [e.OneToOne_Required_FK0].[Id], [e0].[Id] AS [Id0]
    FROM [LevelOne] AS [e0]
    LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK0] ON [e0].[Id] = [e.OneToOne_Optional_FK0].[Level1_Optional_Id]
    LEFT JOIN [LevelThree] AS [e.OneToOne_Optional_FK.OneToOne_Optional_FK0] ON [e.OneToOne_Optional_FK0].[Id] = [e.OneToOne_Optional_FK.OneToOne_Optional_FK0].[Level2_Optional_Id]
    LEFT JOIN [LevelTwo] AS [e.OneToOne_Required_FK0] ON [e0].[Id] = [e.OneToOne_Required_FK0].[Level1_Required_Id]
    LEFT JOIN [LevelThree] AS [e.OneToOne_Required_FK.OneToOne_Optional_PK0] ON [e.OneToOne_Required_FK0].[Id] = [e.OneToOne_Required_FK.OneToOne_Optional_PK0].[OneToOne_Optional_PK_InverseId]
    LEFT JOIN [LevelThree] AS [e.OneToOne_Required_FK.OneToOne_Optional_FK0] ON [e.OneToOne_Required_FK0].[Id] = [e.OneToOne_Required_FK.OneToOne_Optional_FK0].[Level2_Optional_Id]
    WHERE ([e.OneToOne_Required_FK.OneToOne_Optional_PK0].[Name] <> N'Foo') OR [e.OneToOne_Required_FK.OneToOne_Optional_PK0].[Name] IS NULL
) AS [t] ON [e.OneToOne_Required_FK.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
ORDER BY [t].[Id0], [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_navigation_comparison2() : line 878
            AssertSql(
                @"SELECT [l1].[Id] AS [Id1], [l2].[Id] AS [Id2]
FROM [LevelOne] AS [l1]
CROSS JOIN [LevelTwo] AS [l2]
WHERE [l1].[Id] = [l2].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_without_DefaultIfEmpty() : line 2584
            AssertSql(
                @"SELECT [l1].[Id]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Null_reference_protection_complex_client_eval() : line 2335
            AssertSql(
                @"SELECT [t].[Name]
FROM [LevelThree] AS [l3]
LEFT JOIN (
    SELECT [l2_inner].*
    FROM [LevelOne] AS [l1_inner]
    LEFT JOIN [LevelTwo] AS [l2_inner] ON [l1_inner].[Id] = [l2_inner].[Level1_Optional_Id]
) AS [t] ON [l3].[Level2_Required_Id] = [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Project_collection_navigation_nested_anonymous() : line 3032
            AssertSql(
                @"SELECT [l1].[Id] AS [Id0], [l1.OneToOne_Optional_FK].[Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
ORDER BY [l1].[Id], [l1.OneToOne_Optional_FK].[Id]",
                //
                @"SELECT [l1.OneToOne_Optional_FK.OneToMany_Optional].[Id], [l1.OneToOne_Optional_FK.OneToMany_Optional].[Level2_Optional_Id], [l1.OneToOne_Optional_FK.OneToMany_Optional].[Level2_Required_Id], [l1.OneToOne_Optional_FK.OneToMany_Optional].[Name], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToOne_Optional_SelfId], [t].[Id], [t].[Id0]
FROM [LevelThree] AS [l1.OneToOne_Optional_FK.OneToMany_Optional]
INNER JOIN (
    SELECT [l10].[Id], [l1.OneToOne_Optional_FK0].[Id] AS [Id0]
    FROM [LevelOne] AS [l10]
    LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK0] ON [l10].[Id] = [l1.OneToOne_Optional_FK0].[Level1_Optional_Id]
) AS [t] ON [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id0]
ORDER BY [t].[Id], [t].[Id0]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Where_nav_prop_reference_optional1() : line 682
            AssertSql(
                @"SELECT [e].[Id]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE [e.OneToOne_Optional_FK].[Name] IN (N'L2 05', N'L2 07')");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_with_string_based_Include1() : line 1869
            AssertSql(
                @"SELECT [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional.OneToOne_Required_FK].[Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Optional_Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Required_Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Name], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
LEFT JOIN [LevelThree] AS [l1.OneToMany_Optional.OneToOne_Required_FK] ON [l1.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Required_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_in_subquery_with_client_projection_nested1() : line 2511
            AssertSql(
                @"SELECT [l1_outer].[Id], [l1_outer].[Name]
FROM [LevelOne] AS [l1_outer]
WHERE [l1_outer].[Id] < 2",
                //
                @"SELECT 1
FROM [LevelOne] AS [l1_middle0]
LEFT JOIN [LevelTwo] AS [l2_middle0] ON [l1_middle0].[Id] = [l2_middle0].[Level1_Optional_Id]
ORDER BY [l1_middle0].[Id]",
                //
                @"SELECT COUNT(*)
FROM [LevelOne] AS [l1_inner2]
LEFT JOIN [LevelTwo] AS [l2_inner2] ON [l1_inner2].[Id] = [l2_inner2].[Level1_Optional_Id]",
                //
                @"SELECT COUNT(*)
FROM [LevelOne] AS [l1_inner2]
LEFT JOIN [LevelTwo] AS [l2_inner2] ON [l1_inner2].[Id] = [l2_inner2].[Level1_Optional_Id]",
                //
                @"SELECT COUNT(*)
FROM [LevelOne] AS [l1_inner2]
LEFT JOIN [LevelTwo] AS [l2_inner2] ON [l1_inner2].[Id] = [l2_inner2].[Level1_Optional_Id]",
                //
                @"SELECT COUNT(*)
FROM [LevelOne] AS [l1_inner2]
LEFT JOIN [LevelTwo] AS [l2_inner2] ON [l1_inner2].[Id] = [l2_inner2].[Level1_Optional_Id]",
                //
                @"SELECT COUNT(*)
FROM [LevelOne] AS [l1_inner2]
LEFT JOIN [LevelTwo] AS [l2_inner2] ON [l1_inner2].[Id] = [l2_inner2].[Level1_Optional_Id]",
                //
                @"SELECT COUNT(*)
FROM [LevelOne] AS [l1_inner2]
LEFT JOIN [LevelTwo] AS [l2_inner2] ON [l1_inner2].[Id] = [l2_inner2].[Level1_Optional_Id]",
                //
                @"SELECT COUNT(*)
FROM [LevelOne] AS [l1_inner2]
LEFT JOIN [LevelTwo] AS [l2_inner2] ON [l1_inner2].[Id] = [l2_inner2].[Level1_Optional_Id]");

Output truncated.

Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_navigation_property_and_filter_before() : line 1172
            AssertSql(
                @"SELECT [e.OneToMany_Optional].[Id], [e.OneToMany_Optional].[Date], [e.OneToMany_Optional].[Level1_Optional_Id], [e.OneToMany_Optional].[Level1_Required_Id], [e.OneToMany_Optional].[Name], [e.OneToMany_Optional].[OneToMany_Optional_InverseId], [e.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [e.OneToMany_Optional].[OneToMany_Required_InverseId], [e.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [e.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [e.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e]
INNER JOIN [LevelTwo] AS [e.OneToMany_Optional] ON [e].[Id] = [e.OneToMany_Optional].[OneToMany_Optional_InverseId]
WHERE [e].[Id] = 1");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_in_subquery_with_client_projection() : line 2493
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Name]
FROM [LevelOne] AS [l1]
WHERE [l1].[Id] < 3",
                //
                @"SELECT COUNT(*)
FROM [LevelOne] AS [l1_inner0]
LEFT JOIN [LevelTwo] AS [l2_inner0] ON [l1_inner0].[Id] = [l2_inner0].[Level1_Optional_Id]",
                //
                @"SELECT COUNT(*)
FROM [LevelOne] AS [l1_inner0]
LEFT JOIN [LevelTwo] AS [l2_inner0] ON [l1_inner0].[Id] = [l2_inner0].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Result_operator_nav_prop_reference_optional_Average() : line 1043
            AssertSql(
                @"SELECT AVG(CAST([e.OneToOne_Optional_FK].[Level1_Required_Id] AS float))
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Complex_query_with_optional_navigations_and_client_side_evaluation() : line 2198
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
WHERE [l1].[Id] < 3",
                //
                @"@_outer_Id='1'

SELECT [l2.OneToOne_Optional_FK.OneToOne_Optional_FK0].[Id], [l2.OneToOne_Optional_FK.OneToOne_Optional_FK0].[Id]
FROM [LevelTwo] AS [l20]
LEFT JOIN [LevelThree] AS [l2.OneToOne_Optional_FK0] ON [l20].[Id] = [l2.OneToOne_Optional_FK0].[Level2_Optional_Id]
LEFT JOIN [LevelFour] AS [l2.OneToOne_Optional_FK.OneToOne_Optional_FK0] ON [l2.OneToOne_Optional_FK0].[Id] = [l2.OneToOne_Optional_FK.OneToOne_Optional_FK0].[Level3_Optional_Id]
WHERE @_outer_Id = [l20].[OneToMany_Optional_InverseId]",
                //
                @"@_outer_Id='2'

SELECT [l2.OneToOne_Optional_FK.OneToOne_Optional_FK0].[Id], [l2.OneToOne_Optional_FK.OneToOne_Optional_FK0].[Id]
FROM [LevelTwo] AS [l20]
LEFT JOIN [LevelThree] AS [l2.OneToOne_Optional_FK0] ON [l20].[Id] = [l2.OneToOne_Optional_FK0].[Level2_Optional_Id]
LEFT JOIN [LevelFour] AS [l2.OneToOne_Optional_FK.OneToOne_Optional_FK0] ON [l2.OneToOne_Optional_FK0].[Id] = [l2.OneToOne_Optional_FK.OneToOne_Optional_FK0].[Level3_Optional_Id]
WHERE @_outer_Id = [l20].[OneToMany_Optional_InverseId]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_with_subquery_on_inner_and_no_DefaultIfEmpty() : line 2609
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
ORDER BY [l1].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Include_collection_with_multiple_orderbys_property() : line 3182
            AssertSql(
                @"SELECT [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [l2]
ORDER BY [l2].[Level1_Required_Id], [l2].[Name], [l2].[Id]",
                //
                @"SELECT [l2.OneToMany_Optional].[Id], [l2.OneToMany_Optional].[Level2_Optional_Id], [l2.OneToMany_Optional].[Level2_Required_Id], [l2.OneToMany_Optional].[Name], [l2.OneToMany_Optional].[OneToMany_Optional_InverseId], [l2.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l2.OneToMany_Optional].[OneToMany_Required_InverseId], [l2.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l2.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l2.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l2.OneToMany_Optional]
INNER JOIN (
    SELECT [l20].[Id], [l20].[Level1_Required_Id], [l20].[Name]
    FROM [LevelTwo] AS [l20]
) AS [t] ON [l2.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
ORDER BY [t].[Level1_Required_Id], [t].[Name], [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_client_method_in_OrderBy() : line 2574
            AssertSql(
                @"SELECT [l1].[Id], [l2].[Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Join_navigations_in_inner_selector_translated_to_multiple_subquery_without_collision() : line 456
            AssertSql(
                @"SELECT [e2].[Id] AS [Id2], [e1].[Id] AS [Id1], [e3].[Id] AS [Id3]
FROM [LevelTwo] AS [e2]
INNER JOIN [LevelOne] AS [e1] ON [e2].[Id] IN (
    SELECT TOP(1) [subQuery0].[Id]
    FROM [LevelTwo] AS [subQuery0]
    WHERE [subQuery0].[Level1_Optional_Id] = [e1].[Id]
)
INNER JOIN [LevelThree] AS [e3] ON [e2].[Id] = [e3].[Level2_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Complex_multi_include_with_order_by_and_paging_joins_on_correct_key() : line 1388
            AssertSql(
                @"@__p_0='0'
@__p_1='10'

SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_InverseId], [e].[OneToMany_Required_Self_InverseId], [e].[OneToOne_Optional_SelfId], [e.OneToOne_Required_FK].[Id], [e.OneToOne_Required_FK].[Date], [e.OneToOne_Required_FK].[Level1_Optional_Id], [e.OneToOne_Required_FK].[Level1_Required_Id], [e.OneToOne_Required_FK].[Name], [e.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Required_FK].[OneToMany_Required_InverseId], [e.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Required_FK].[OneToOne_Optional_SelfId], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Optional_FK].[Date], [e.OneToOne_Optional_FK].[Level1_Optional_Id], [e.OneToOne_Optional_FK].[Level1_Required_Id], [e.OneToOne_Optional_FK].[Name], [e.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Required_FK] ON [e].[Id] = [e.OneToOne_Required_FK].[Level1_Required_Id]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
ORDER BY [e].[Name], [e.OneToOne_Optional_FK].[Id], [e.OneToOne_Required_FK].[Id]
OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY",
                //
                @"@__p_0='0'
@__p_1='10'

SELECT [e.OneToOne_Required_FK.OneToMany_Required].[Id], [e.OneToOne_Required_FK.OneToMany_Required].[Level2_Optional_Id], [e.OneToOne_Required_FK.OneToMany_Required].[Level2_Required_Id], [e.OneToOne_Required_FK.OneToMany_Required].[Name], [e.OneToOne_Required_FK.OneToMany_Required].[OneToMany_Optional_InverseId], [e.OneToOne_Required_FK.OneToMany_Required].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Required_FK.OneToMany_Required].[OneToMany_Required_InverseId], [e.OneToOne_Required_FK.OneToMany_Required].[OneToMany_Required_Self_InverseId], [e.OneToOne_Required_FK.OneToMany_Required].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Required_FK.OneToMany_Required].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [e.OneToOne_Required_FK.OneToMany_Required]
INNER JOIN (
    SELECT DISTINCT [t1].*
    FROM (
        SELECT [e.OneToOne_Required_FK1].[Id], [e1].[Name], [e.OneToOne_Optional_FK1].[Id] AS [Id0]
        FROM [LevelOne] AS [e1]
        LEFT JOIN [LevelTwo] AS [e.OneToOne_Required_FK1] ON [e1].[Id] = [e.OneToOne_Required_FK1].[Level1_Required_Id]
        LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK1] ON [e1].[Id] = [e.OneToOne_Optional_FK1].[Level1_Optional_Id]
        ORDER BY [e1].[Name], [e.OneToOne_Optional_FK1].[Id], [e.OneToOne_Required_FK1].[Id]
        OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY
    ) AS [t1]
) AS [t2] ON [e.OneToOne_Required_FK.OneToMany_Required].[OneToMany_Required_InverseId] = [t2].[Id]
ORDER BY [t2].[Name], [t2].[Id0], [t2].[Id]",
                //
                @"@__p_0='0'
@__p_1='10'

SELECT [e.OneToOne_Optional_FK.OneToMany_Optional].[Id], [e.OneToOne_Optional_FK.OneToMany_Optional].[Level2_Optional_Id], [e.OneToOne_Optional_FK.OneToMany_Optional].[Level2_Required_Id], [e.OneToOne_Optional_FK.OneToMany_Optional].[Name], [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId], [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Required_InverseId], [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [e.OneToOne_Optional_FK.OneToMany_Optional]
INNER JOIN (
    SELECT DISTINCT [t].*
    FROM (
        SELECT [e.OneToOne_Optional_FK0].[Id], [e0].[Name]
        FROM [LevelOne] AS [e0]
        LEFT JOIN [LevelTwo] AS [e.OneToOne_Required_FK0] ON [e0].[Id] = [e.OneToOne_Required_FK0].[Level1_Required_Id]
        LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK0] ON [e0].[Id] = [e.OneToOne_Optional_FK0].[Level1_Optional_Id]
        ORDER BY [e0].[Name], [e.OneToOne_Optional_FK0].[Id]
        OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY
    ) AS [t]
) AS [t0] ON [e.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t0].[Id]
ORDER BY [t0].[Name], [t0].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Key_equality_when_sentinel_ef_property() : line 40
            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l.OneToOne_Optional_FK] ON [l].[Id] = [l.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE [l.OneToOne_Optional_FK].[Id] = 0");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Result_operator_nav_prop_reference_optional_Max() : line 1033
            AssertSql(
                @"SELECT MAX([e.OneToOne_Optional_FK].[Level1_Required_Id])
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_with_navigation_and_Distinct() : line 2062
            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId], [l.OneToMany_Optional].[Id], [l.OneToMany_Optional].[Date], [l.OneToMany_Optional].[Level1_Optional_Id], [l.OneToMany_Optional].[Level1_Required_Id], [l.OneToMany_Optional].[Name], [l.OneToMany_Optional].[OneToMany_Optional_InverseId], [l.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l.OneToMany_Optional].[OneToMany_Required_InverseId], [l.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l.OneToMany_Optional] ON [l].[Id] = [l.OneToMany_Optional].[OneToMany_Optional_InverseId]
ORDER BY [l].[Id]",
                //
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId], [l.OneToMany_Optional2].[Id], [l.OneToMany_Optional2].[Date], [l.OneToMany_Optional2].[Level1_Optional_Id], [l.OneToMany_Optional2].[Level1_Required_Id], [l.OneToMany_Optional2].[Name], [l.OneToMany_Optional2].[OneToMany_Optional_InverseId], [l.OneToMany_Optional2].[OneToMany_Optional_Self_InverseId], [l.OneToMany_Optional2].[OneToMany_Required_InverseId], [l.OneToMany_Optional2].[OneToMany_Required_Self_InverseId], [l.OneToMany_Optional2].[OneToOne_Optional_PK_InverseId], [l.OneToMany_Optional2].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l.OneToMany_Optional2] ON [l1].[Id] = [l.OneToMany_Optional2].[OneToMany_Optional_InverseId]
ORDER BY [l1].[Id]",
                //
                @"SELECT [l.OneToMany_Optional0].[Id], [l.OneToMany_Optional0].[Date], [l.OneToMany_Optional0].[Level1_Optional_Id], [l.OneToMany_Optional0].[Level1_Required_Id], [l.OneToMany_Optional0].[Name], [l.OneToMany_Optional0].[OneToMany_Optional_InverseId], [l.OneToMany_Optional0].[OneToMany_Optional_Self_InverseId], [l.OneToMany_Optional0].[OneToMany_Required_InverseId], [l.OneToMany_Optional0].[OneToMany_Required_Self_InverseId], [l.OneToMany_Optional0].[OneToOne_Optional_PK_InverseId], [l.OneToMany_Optional0].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [l.OneToMany_Optional0]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Correlated_nested_subquery_doesnt_project_unnecessary_columns_in_top_level() : line 1535
            AssertSql(
                @"SELECT DISTINCT [l1].[Name]
FROM [LevelOne] AS [l1]
WHERE EXISTS (
    SELECT 1
    FROM [LevelTwo] AS [l2]
    WHERE EXISTS (
        SELECT 1
        FROM [LevelThree] AS [l3]))");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Join_condition_optimizations_applied_correctly_when_anonymous_type_with_multiple_properties() : line 2748
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l2] ON (([l1].[OneToMany_Optional_Self_InverseId] = [l2].[Level1_Optional_Id]) OR ([l1].[OneToMany_Optional_Self_InverseId] IS NULL AND [l2].[Level1_Optional_Id] IS NULL)) AND (([l1].[OneToOne_Optional_SelfId] = [l2].[OneToMany_Optional_Self_InverseId]) OR ([l1].[OneToOne_Optional_SelfId] IS NULL AND [l2].[OneToMany_Optional_Self_InverseId] IS NULL))");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Explicit_GroupJoin_in_subquery_with_unrelated_projection4() : line 2675
            AssertSql(
                @"@__p_0='20'

SELECT TOP(@__p_0) [t].[Id]
FROM (
    SELECT DISTINCT [l1].[Id]
    FROM [LevelOne] AS [l1]
    LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
    WHERE ([l2].[Name] <> N'Foo') OR [l2].[Name] IS NULL
) AS [t]
ORDER BY [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_with_Include2() : line 1812
            AssertSql(
                @"SELECT [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId], [l1.OneToMany_Optional.OneToOne_Required_FK].[Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Optional_Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Required_Id], [l1.OneToMany_Optional.OneToOne_Required_FK].[Name], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional.OneToOne_Required_FK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]
LEFT JOIN [LevelThree] AS [l1.OneToMany_Optional.OneToOne_Required_FK] ON [l1.OneToMany_Optional].[Id] = [l1.OneToMany_Optional.OneToOne_Required_FK].[Level2_Required_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Join_navigation_translated_to_subquery_non_key_join() : line 471
            AssertSql(
                @"SELECT [e2].[Id] AS [Id2], [e2].[Name] AS [Name2], [e1].[Id] AS [Id1], [e1].[Name] AS [Name1]
FROM [LevelTwo] AS [e2]
INNER JOIN [LevelOne] AS [e1] ON [e2].[Name] IN (
    SELECT TOP(1) [subQuery0].[Name]
    FROM [LevelTwo] AS [subQuery0]
    WHERE [subQuery0].[Level1_Optional_Id] = [e1].[Id]
)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Where_on_multilevel_reference_in_subquery_with_outer_projection() : line 2721
            AssertSql(
                @"@__p_0='0'
@__p_1='10'

SELECT [l3].[Name]
FROM [LevelThree] AS [l3]
INNER JOIN [LevelTwo] AS [l3.OneToMany_Required_Inverse] ON [l3].[OneToMany_Required_InverseId] = [l3.OneToMany_Required_Inverse].[Id]
INNER JOIN [LevelOne] AS [l3.OneToMany_Required_Inverse.OneToOne_Required_FK_Inverse] ON [l3.OneToMany_Required_Inverse].[Level1_Required_Id] = [l3.OneToMany_Required_Inverse.OneToOne_Required_FK_Inverse].[Id]
WHERE [l3.OneToMany_Required_Inverse.OneToOne_Required_FK_Inverse].[Name] = N'L1 03'
ORDER BY [l3].[Level2_Required_Id]
OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Optional_navigation_inside_nested_method_call_translated_to_join_keeps_original_nullability() : line 385
            AssertSql(
                @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_InverseId], [e1].[OneToMany_Required_Self_InverseId], [e1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e1]
LEFT JOIN [LevelTwo] AS [e1.OneToOne_Optional_FK] ON [e1].[Id] = [e1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE DATEADD(month, 2, DATEADD(day, 15, DATEADD(day, 10, [e1.OneToOne_Optional_FK].[Date]))) > '2002-02-01T00:00:00.000'");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Key_equality_two_conditions_on_same_navigation() : line 135
            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l.OneToOne_Required_FK] ON [l].[Id] = [l.OneToOne_Required_FK].[Level1_Required_Id]
WHERE ([l.OneToOne_Required_FK].[Id] = 1) OR ([l.OneToOne_Required_FK].[Id] = 2)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Project_navigation_and_collection() : line 3125
            AssertSql(
                @"SELECT [l1.OneToOne_Optional_FK].[Id], [l1.OneToOne_Optional_FK].[Date], [l1.OneToOne_Optional_FK].[Level1_Optional_Id], [l1.OneToOne_Optional_FK].[Level1_Required_Id], [l1.OneToOne_Optional_FK].[Name], [l1.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
ORDER BY [l1].[Id], [l1.OneToOne_Optional_FK].[Id]",
                //
                @"SELECT [l1.OneToOne_Optional_FK.OneToMany_Optional].[Id], [l1.OneToOne_Optional_FK.OneToMany_Optional].[Level2_Optional_Id], [l1.OneToOne_Optional_FK.OneToMany_Optional].[Level2_Required_Id], [l1.OneToOne_Optional_FK.OneToMany_Optional].[Name], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToOne_Optional_SelfId], [t].[Id], [t].[Id0]
FROM [LevelThree] AS [l1.OneToOne_Optional_FK.OneToMany_Optional]
INNER JOIN (
    SELECT [l10].[Id], [l1.OneToOne_Optional_FK0].[Id] AS [Id0]
    FROM [LevelOne] AS [l10]
    LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK0] ON [l10].[Id] = [l1.OneToOne_Optional_FK0].[Level1_Optional_Id]
) AS [t] ON [l1.OneToOne_Optional_FK.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id0]
ORDER BY [t].[Id], [t].[Id0]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_navigation_property_and_projection() : line 1162
            AssertSql(
                @"SELECT [l1.OneToMany_Optional].[Name]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_on_left_side_being_a_subquery() : line 2445
            AssertSql(
                @"@__p_0='2'

SELECT TOP(@__p_0) [l1].[Id], [l1.OneToOne_Optional_FK].[Name] AS [Brand]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
ORDER BY [l1.OneToOne_Optional_FK].[Name], [l1].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Select_multiple_nav_prop_reference_required2() : line 833
            AssertSql(
                @"SELECT [e.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]
FROM [LevelThree] AS [e]
INNER JOIN [LevelTwo] AS [e.OneToOne_Required_FK_Inverse] ON [e].[Level2_Required_Id] = [e.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [LevelOne] AS [e.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse] ON [e.OneToOne_Required_FK_Inverse].[Level1_Required_Id] = [e.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Result_operator_nav_prop_reference_optional_Min() : line 1023
            AssertSql(
                @"SELECT MIN([e.OneToOne_Optional_FK].[Level1_Required_Id])
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Multiple_required_navigation_using_multiple_selects_with_string_based_Include() : line 1963
            AssertSql(
                @"SELECT [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Date], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Level1_Optional_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Level1_Required_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Name], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Level2_Optional_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Level2_Required_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Name], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [LevelFour] AS [l4]
INNER JOIN [LevelThree] AS [l4.OneToOne_Required_FK_Inverse] ON [l4].[Level3_Required_Id] = [l4.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [LevelTwo] AS [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse] ON [l4.OneToOne_Required_FK_Inverse].[Level2_Required_Id] = [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]
LEFT JOIN [LevelThree] AS [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK] ON [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id] = [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Level2_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Join_navigation_translated_to_subquery_deeply_nested_required() : line 526
            AssertSql(
                @"SELECT [e4].[Id] AS [Id4], [e4].[Name] AS [Name4], [e1].[Id] AS [Id1], [e1].[Name] AS [Name1]
FROM [LevelOne] AS [e1]
INNER JOIN [LevelFour] AS [e4] ON [e1].[Name] IN (
    SELECT TOP(1) [subQuery.OneToOne_Required_FK_Inverse.OneToOne_Required_PK_Inverse0].[Name]
    FROM [LevelThree] AS [subQuery0]
    INNER JOIN [LevelTwo] AS [subQuery.OneToOne_Required_FK_Inverse0] ON [subQuery0].[Level2_Required_Id] = [subQuery.OneToOne_Required_FK_Inverse0].[Id]
    INNER JOIN [LevelOne] AS [subQuery.OneToOne_Required_FK_Inverse.OneToOne_Required_PK_Inverse0] ON [subQuery.OneToOne_Required_FK_Inverse0].[Id] = [subQuery.OneToOne_Required_FK_Inverse.OneToOne_Required_PK_Inverse0].[Id]
    WHERE [subQuery0].[Id] = [e4].[Level3_Required_Id]
)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.SelectMany_navigation_property() : line 1152
            AssertSql(
                @"SELECT [l1.OneToMany_Optional].[Id], [l1.OneToMany_Optional].[Date], [l1.OneToMany_Optional].[Level1_Optional_Id], [l1.OneToMany_Optional].[Level1_Required_Id], [l1.OneToMany_Optional].[Name], [l1.OneToMany_Optional].[OneToMany_Optional_InverseId], [l1.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_InverseId], [l1.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l1.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToMany_Optional] ON [l1].[Id] = [l1.OneToMany_Optional].[OneToMany_Optional_InverseId]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Key_equality_using_property_method_nested() : line 72
            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l.OneToOne_Required_FK] ON [l].[Id] = [l.OneToOne_Required_FK].[Level1_Required_Id]
WHERE [l.OneToOne_Required_FK].[Id] = 7");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Query_source_materialization_bug_4547() : line 1137
            AssertSql(
                @"SELECT [e1].[Id]
FROM [LevelThree] AS [e3]
INNER JOIN [LevelOne] AS [e1] ON [e3].[Id] IN (
    SELECT TOP(1) [subQuery30].[Id]
    FROM [LevelTwo] AS [subQuery20]
    LEFT JOIN [LevelThree] AS [subQuery30] ON [subQuery20].[Id] = [subQuery30].[Level2_Optional_Id]
    ORDER BY [subQuery30].[Id]
)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access3() : line 1674
            AssertSql(
                @"SELECT [l3.OneToOne_Required_FK_Inverse].[Id], [l3.OneToOne_Required_FK_Inverse].[Date], [l3.OneToOne_Required_FK_Inverse].[Level1_Optional_Id], [l3.OneToOne_Required_FK_Inverse].[Level1_Required_Id], [l3.OneToOne_Required_FK_Inverse].[Name], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l3]
INNER JOIN [LevelTwo] AS [l3.OneToOne_Required_FK_Inverse] ON [l3].[Level2_Required_Id] = [l3.OneToOne_Required_FK_Inverse].[Id]
ORDER BY [l3.OneToOne_Required_FK_Inverse].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Select_nav_prop_reference_optional3() : line 672
            AssertSql(
                @"SELECT [e.OneToOne_Optional_FK_Inverse].[Name]
FROM [LevelTwo] AS [e]
LEFT JOIN [LevelOne] AS [e.OneToOne_Optional_FK_Inverse] ON [e].[Level1_Optional_Id] = [e.OneToOne_Optional_FK_Inverse].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_on_a_subquery_containing_another_GroupJoin_projecting_inner() : line 2437
            AssertSql(
                @"@__p_0='2'

SELECT [l1_outer].[Name]
FROM (
    SELECT TOP(@__p_0) [l2].*
    FROM [LevelOne] AS [l1]
    LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
    ORDER BY [l1].[Id]
) AS [t]
LEFT JOIN [LevelOne] AS [l1_outer] ON [t].[Level1_Optional_Id] = [l1_outer].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Manually_created_left_join_propagates_nullability_to_navigations() : line 2270
            AssertSql(
                @"SELECT [l2_manual.OneToOne_Required_FK_Inverse].[Name]
FROM [LevelOne] AS [l1_manual]
LEFT JOIN [LevelTwo] AS [l2_manual] ON [l1_manual].[Id] = [l2_manual].[Level1_Optional_Id]
LEFT JOIN [LevelOne] AS [l2_manual.OneToOne_Required_FK_Inverse] ON [l2_manual].[Level1_Required_Id] = [l2_manual.OneToOne_Required_FK_Inverse].[Id]
WHERE ([l2_manual.OneToOne_Required_FK_Inverse].[Name] <> N'L3 02') OR [l2_manual.OneToOne_Required_FK_Inverse].[Name] IS NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Optional_navigation_in_subquery_with_unrelated_projection() : line 2624
            AssertSql(
                @"@__p_0='15'

SELECT TOP(@__p_0) [l1].[Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE ([l1.OneToOne_Optional_FK].[Name] <> N'Foo') OR [l1.OneToOne_Optional_FK].[Name] IS NULL
ORDER BY [l1].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Join_navigation_translated_to_subquery_self_ref() : line 485
            AssertSql(
                @"SELECT [e1].[Id] AS [Id1], [e2].[Id] AS [Id2]
FROM [LevelOne] AS [e1]
INNER JOIN [LevelOne] AS [e2] ON [e1].[Id] = [e2].[OneToMany_Optional_Self_InverseId]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Where_complex_predicate_with_with_nav_prop_and_OrElse3() : line 926
            AssertSql(
                @"SELECT [l1].[Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Required_FK] ON [l1].[Id] = [l1.OneToOne_Required_FK].[Level1_Required_Id]
LEFT JOIN [LevelThree] AS [l1.OneToOne_Required_FK.OneToOne_Optional_FK] ON [l1.OneToOne_Required_FK].[Id] = [l1.OneToOne_Required_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE (([l1.OneToOne_Optional_FK].[Name] <> N'L2 05') OR [l1.OneToOne_Optional_FK].[Name] IS NULL) OR ([l1.OneToOne_Required_FK.OneToOne_Optional_FK].[Name] = N'L3 05')");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Required_navigation_with_Include() : line 1904
            AssertSql(
                @"SELECT [l3.OneToOne_Required_FK_Inverse].[Id], [l3.OneToOne_Required_FK_Inverse].[Date], [l3.OneToOne_Required_FK_Inverse].[Level1_Optional_Id], [l3.OneToOne_Required_FK_Inverse].[Level1_Required_Id], [l3.OneToOne_Required_FK_Inverse].[Name], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l3.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId], [l3.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[Id], [l3.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[Date], [l3.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[Name], [l3.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[OneToMany_Optional_Self_InverseId], [l3.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[OneToMany_Required_Self_InverseId], [l3.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l3]
INNER JOIN [LevelTwo] AS [l3.OneToOne_Required_FK_Inverse] ON [l3].[Level2_Required_Id] = [l3.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [LevelOne] AS [l3.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse] ON [l3.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId] = [l3.OneToOne_Required_FK_Inverse.OneToMany_Required_Inverse].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Key_equality_two_conditions_on_same_navigation2() : line 146
            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [l]
WHERE ([l].[Level1_Required_Id] = 1) OR ([l].[Level1_Required_Id] = 2)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Key_equality_navigation_converted_to_FK() : line 125
            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_InverseId], [l].[OneToMany_Optional_Self_InverseId], [l].[OneToMany_Required_InverseId], [l].[OneToMany_Required_Self_InverseId], [l].[OneToOne_Optional_PK_InverseId], [l].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [l]
WHERE [l].[Level1_Required_Id] = 1");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.GroupJoin_on_right_side_being_a_subquery() : line 2458
            AssertSql(
                @"@__p_0='2'

SELECT [l2].[Id], [t].[Name]
FROM [LevelTwo] AS [l2]
LEFT JOIN (
    SELECT TOP(@__p_0) [x].*
    FROM [LevelOne] AS [x]
    LEFT JOIN [LevelTwo] AS [x.OneToOne_Optional_FK] ON [x].[Id] = [x.OneToOne_Optional_FK].[Level1_Optional_Id]
    ORDER BY [x.OneToOne_Optional_FK].[Name]
) AS [t] ON [l2].[Level1_Optional_Id] = [t].[Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Where_nav_prop_reference_optional2() : line 705
            AssertSql(
                @"SELECT [e].[Id]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [e.OneToOne_Optional_FK] ON [e].[Id] = [e.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE ([e.OneToOne_Optional_FK].[Name] = N'L2 05') OR (([e.OneToOne_Optional_FK].[Name] <> N'L2 42') OR [e.OneToOne_Optional_FK].[Name] IS NULL)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Where_multiple_nav_prop_reference_optional_compared_to_null1() : line 763
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1.OneToOne_Optional_FK.OneToOne_Optional_FK] ON [l1.OneToOne_Optional_FK].[Id] = [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Level2_Optional_Id]
WHERE [l1.OneToOne_Optional_FK.OneToOne_Optional_FK].[Id] IS NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Correlated_subquery_doesnt_project_unnecessary_columns_in_top_level() : line 1508
            AssertSql(
                @"SELECT DISTINCT [l1].[Name]
FROM [LevelOne] AS [l1]
WHERE EXISTS (
    SELECT 1
    FROM [LevelTwo] AS [l2]
    WHERE [l2].[Level1_Required_Id] = [l1].[Id])");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Navigation_with_same_navigation_compared_to_null() : line 2788
            AssertSql(
                @"SELECT [l2].[Id]
FROM [LevelTwo] AS [l2]
INNER JOIN [LevelOne] AS [l2.OneToMany_Required_Inverse] ON [l2].[OneToMany_Required_InverseId] = [l2.OneToMany_Required_Inverse].[Id]
WHERE (([l2.OneToMany_Required_Inverse].[Name] <> N'L1 07') OR [l2.OneToMany_Required_Inverse].[Name] IS NULL) AND [l2].[OneToMany_Required_InverseId] IS NOT NULL");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Contains_with_subquery_optional_navigation_and_constant_item() : line 2183
            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_InverseId], [l1].[OneToMany_Required_Self_InverseId], [l1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK] ON [l1].[Id] = [l1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE 1 IN (
    SELECT DISTINCT [l3].[Id]
    FROM [LevelThree] AS [l3]
    WHERE [l1.OneToOne_Optional_FK].[Id] = [l3].[OneToMany_Optional_InverseId]
)");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Multiple_required_navigations_with_Include() : line 1927
            AssertSql(
                @"SELECT [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Date], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Level1_Optional_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Level1_Required_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Name], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Optional_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToMany_Required_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_PK_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[OneToOne_Optional_SelfId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Level2_Optional_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Level2_Required_Id], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Name], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Optional_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Optional_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Required_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToMany_Required_Self_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToOne_Optional_PK_InverseId], [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[OneToOne_Optional_SelfId]
FROM [LevelFour] AS [l4]
INNER JOIN [LevelThree] AS [l4.OneToOne_Required_FK_Inverse] ON [l4].[Level3_Required_Id] = [l4.OneToOne_Required_FK_Inverse].[Id]
INNER JOIN [LevelTwo] AS [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse] ON [l4.OneToOne_Required_FK_Inverse].[Level2_Required_Id] = [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id]
LEFT JOIN [LevelThree] AS [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK] ON [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse].[Id] = [l4.OneToOne_Required_FK_Inverse.OneToOne_Required_FK_Inverse.OneToOne_Optional_FK].[Level2_Optional_Id]");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Optional_navigation_inside_method_call_translated_to_join_keeps_original_nullability() : line 374
            AssertSql(
                @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_InverseId], [e1].[OneToMany_Required_Self_InverseId], [e1].[OneToOne_Optional_SelfId]
FROM [LevelOne] AS [e1]
LEFT JOIN [LevelTwo] AS [e1.OneToOne_Optional_FK] ON [e1].[Id] = [e1.OneToOne_Optional_FK].[Level1_Optional_Id]
WHERE DATEADD(day, 10, [e1.OneToOne_Optional_FK].[Date]) > '2000-02-01T00:00:00.000'");



Microsoft.EntityFrameworkCore.Query.ComplexNavigationsQuerySqlCeTest.Include_collection_with_multiple_orderbys_complex_repeated() : line 3236
            AssertSql(
                @"SELECT [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_InverseId], [l2].[OneToMany_Optional_Self_InverseId], [l2].[OneToMany_Required_InverseId], [l2].[OneToMany_Required_Self_InverseId], [l2].[OneToOne_Optional_PK_InverseId], [l2].[OneToOne_Optional_SelfId]
FROM [LevelTwo] AS [l2]
ORDER BY -[l2].[Level1_Required_Id], [l2].[Name], [l2].[Id]",
                //
                @"SELECT [l2.OneToMany_Optional].[Id], [l2.OneToMany_Optional].[Level2_Optional_Id], [l2.OneToMany_Optional].[Level2_Required_Id], [l2.OneToMany_Optional].[Name], [l2.OneToMany_Optional].[OneToMany_Optional_InverseId], [l2.OneToMany_Optional].[OneToMany_Optional_Self_InverseId], [l2.OneToMany_Optional].[OneToMany_Required_InverseId], [l2.OneToMany_Optional].[OneToMany_Required_Self_InverseId], [l2.OneToMany_Optional].[OneToOne_Optional_PK_InverseId], [l2.OneToMany_Optional].[OneToOne_Optional_SelfId]
FROM [LevelThree] AS [l2.OneToMany_Optional]
INNER JOIN (
    SELECT [l20].[Id], -[l20].[Level1_Required_Id] AS [c], [l20].[Name], [l20].[Level1_Required_Id]
    FROM [LevelTwo] AS [l20]
) AS [t] ON [l2.OneToMany_Optional].[OneToMany_Optional_InverseId] = [t].[Id]
ORDER BY [t].[c], [t].[Name], [t].[Id]");



