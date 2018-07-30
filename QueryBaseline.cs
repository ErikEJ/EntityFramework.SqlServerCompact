System.RuntimeMethodHandle.InvokeMethod(Object target, Object[] arguments, Signature sig, Boolean constructor) : 
            AssertSql(
                @"SELECT TOP(2) [c].[Id], [c].[Name], [c].[PrincipalId]
FROM [Categories] AS [c]",
                //
                @"@__category_PrincipalId_0='778' (Nullable = true)

SELECT [p].[Id], [p].[DependentId], [p].[Name], [p].[Price]
FROM [Products] AS [p]
WHERE [p].[DependentId] = @__category_PrincipalId_0",
                //
                @"@p1='78'
@p0='New Category' (Size = 4000)

UPDATE [Categories] SET [Name] = @p0
WHERE [Id] = @p1",
                //
                @"SELECT TOP(2) [c].[Id], [c].[Name], [c].[PrincipalId]
FROM [Categories] AS [c]",
                //
                @"@__category_PrincipalId_0='778' (Nullable = true)

SELECT [p].[Id], [p].[DependentId], [p].[Name], [p].[Price]
FROM [Products] AS [p]
WHERE [p].[DependentId] = @__category_PrincipalId_0");



System.RuntimeMethodHandle.InvokeMethod(Object target, Object[] arguments, Signature sig, Boolean constructor) : 
            AssertSql(
                @"SELECT TOP(2) [c].[Id], [c].[Name], [c].[PrincipalId]
FROM [Categories] AS [c]",
                //
                @"@__category_PrincipalId_0='778' (Nullable = true)

SELECT [p].[Id], [p].[DependentId], [p].[Name], [p].[Price]
FROM [Products] AS [p]
WHERE [p].[DependentId] = @__category_PrincipalId_0",
                //
                @"@p1='78'
@p0='New Category' (Size = 4000)

UPDATE [Categories] SET [Name] = @p0
WHERE [Id] = @p1",
                //
                @"SELECT TOP(2) [c].[Id], [c].[Name], [c].[PrincipalId]
FROM [Categories] AS [c]",
                //
                @"@__category_PrincipalId_0='778' (Nullable = true)

SELECT [p].[Id], [p].[DependentId], [p].[Name], [p].[Price]
FROM [Products] AS [p]
WHERE [p].[DependentId] = @__category_PrincipalId_0");



