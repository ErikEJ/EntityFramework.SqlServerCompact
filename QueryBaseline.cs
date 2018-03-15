System.RuntimeMethodHandle.InvokeMethod(Object target, Object[] arguments, Signature sig, Boolean constructor) : 
            AssertSql(
                @"@p3='Trek Pro Fit Madone 6 Series' (Nullable = false) (Size = 256)
@p0='LicensedOperator' (Nullable = false) (Size = 4000)
@p1='repairman' (Size = 4000)
@p2='Repair' (Size = 4000)

UPDATE [Vehicles] SET [Operator_Discriminator] = @p0, [Operator_Name] = @p1, [LicenseType] = @p2
WHERE [Name] = @p3",
                //
                @"SELECT TOP(2) [v].[Name], [v].[Discriminator], [v].[SeatingCapacity], [t].[Name], [t].[Operator_Discriminator], [t].[Operator_Name], [t].[LicenseType]
FROM [Vehicles] AS [v]
LEFT JOIN (
    SELECT [v.Operator].*
    FROM [Vehicles] AS [v.Operator]
    WHERE [v.Operator].[Operator_Discriminator] IN (N'LicensedOperator', N'Operator')
) AS [t] ON [v].[Name] = [t].[Name]
WHERE [v].[Discriminator] IN (N'PoweredVehicle', N'Vehicle') AND ([v].[Name] = N'Trek Pro Fit Madone 6 Series')");



System.RuntimeMethodHandle.InvokeMethod(Object target, Object[] arguments, Signature sig, Boolean constructor) : 
            AssertSql(
                @"@p1='Trek Pro Fit Madone 6 Series' (Nullable = false) (Size = 256)
@p0='2'

UPDATE [Vehicles] SET [SeatingCapacity] = @p0
WHERE [Name] = @p1",
                //
                @"SELECT TOP(2) [v].[Name], [v].[Discriminator], [v].[SeatingCapacity], [t].[Name], [t].[Operator_Discriminator], [t].[Operator_Name], [t].[LicenseType]
FROM [Vehicles] AS [v]
LEFT JOIN (
    SELECT [v.Operator].*
    FROM [Vehicles] AS [v.Operator]
    WHERE [v.Operator].[Operator_Discriminator] IN (N'LicensedOperator', N'Operator')
) AS [t] ON [v].[Name] = [t].[Name]
WHERE [v].[Discriminator] IN (N'PoweredVehicle', N'Vehicle') AND ([v].[Name] = N'Trek Pro Fit Madone 6 Series')");



System.RuntimeMethodHandle.InvokeMethod(Object target, Object[] arguments, Signature sig, Boolean constructor) : 
            AssertSql(
                @"@p3='Trek Pro Fit Madone 6 Series' (Nullable = false) (Size = 256)
@p0='LicensedOperator' (Nullable = false) (Size = 4000)
@p1='repairman' (Size = 4000)
@p2='Repair' (Size = 4000)

UPDATE [Vehicles] SET [Operator_Discriminator] = @p0, [Operator_Name] = @p1, [LicenseType] = @p2
WHERE [Name] = @p3",
                //
                @"SELECT TOP(2) [v].[Name], [v].[Discriminator], [v].[SeatingCapacity], [t].[Name], [t].[Operator_Discriminator], [t].[Operator_Name], [t].[LicenseType]
FROM [Vehicles] AS [v]
LEFT JOIN (
    SELECT [v.Operator].*
    FROM [Vehicles] AS [v.Operator]
    WHERE [v.Operator].[Operator_Discriminator] IN (N'LicensedOperator', N'Operator')
) AS [t] ON [v].[Name] = [t].[Name]
WHERE [v].[Discriminator] IN (N'PoweredVehicle', N'Vehicle') AND ([v].[Name] = N'Trek Pro Fit Madone 6 Series')");



System.RuntimeMethodHandle.InvokeMethod(Object target, Object[] arguments, Signature sig, Boolean constructor) : 
            AssertSql(
                @"@p1='Trek Pro Fit Madone 6 Series' (Nullable = false) (Size = 256)
@p0='2'

UPDATE [Vehicles] SET [SeatingCapacity] = @p0
WHERE [Name] = @p1",
                //
                @"SELECT TOP(2) [v].[Name], [v].[Discriminator], [v].[SeatingCapacity], [t].[Name], [t].[Operator_Discriminator], [t].[Operator_Name], [t].[LicenseType]
FROM [Vehicles] AS [v]
LEFT JOIN (
    SELECT [v.Operator].*
    FROM [Vehicles] AS [v.Operator]
    WHERE [v.Operator].[Operator_Discriminator] IN (N'LicensedOperator', N'Operator')
) AS [t] ON [v].[Name] = [t].[Name]
WHERE [v].[Discriminator] IN (N'PoweredVehicle', N'Vehicle') AND ([v].[Name] = N'Trek Pro Fit Madone 6 Series')");



