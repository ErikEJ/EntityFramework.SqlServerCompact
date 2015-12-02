using System.Reflection;
using Microsoft.Data.Entity.Infrastructure;

#if SQLCE35
[assembly: AssemblyTitle("EntityFramework.SqlServerCompact35")]
[assembly: AssemblyProduct("EntityFramework.SqlServerCompact35")]
[assembly: DesignTimeProviderServices(
    fullyQualifiedTypeName: "Microsoft.Data.Entity.Scaffolding.SqlCeDesignTimeServices, EntityFramework.SqlServerCompact35.Design",
    packageName: "EntityFramework.SqlServerCompact35.Design")]
#else
[assembly: AssemblyTitle("EntityFramework.SqlServerCompact40")]
[assembly: AssemblyProduct("EntityFramework.SqlServerCompact40")]
[assembly: DesignTimeProviderServices(
    fullyQualifiedTypeName: "Microsoft.Data.Entity.Scaffolding.SqlCeDesignTimeServices, EntityFramework.SqlServerCompact40.Design",
    packageName: "EntityFramework.SqlServerCompact40.Design")]
#endif
[assembly: AssemblyVersion("7.0.0.0")]
[assembly: AssemblyFileVersion("7.0.0.0")]
[assembly: AssemblyInformationalVersion("7.0.0.0-rc2-final")]
