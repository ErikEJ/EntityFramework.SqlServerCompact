using System.Reflection;
using Microsoft.EntityFrameworkCore.Infrastructure;

#if SQLCE35
[assembly: AssemblyTitle("EntityFrameworkCore.SqlServerCompact35")]
[assembly: AssemblyProduct("EntityFrameworkCore.SqlServerCompact35")]
[assembly: DesignTimeProviderServices(
    fullyQualifiedTypeName: "Microsoft.EntityFrameworkCore.Scaffolding.SqlCeDesignTimeServices, EntityFrameworkCore.SqlServerCompact35.Design",
    packageName: "EntityFrameworkCore.SqlServerCompact35.Design")]
#else
[assembly: AssemblyTitle("EntityFrameworkCore.SqlServerCompact40")]
[assembly: AssemblyProduct("EntityFrameworkCore.SqlServerCompact40")]
[assembly: DesignTimeProviderServices(
    fullyQualifiedTypeName: "Microsoft.EntityFrameworkCore.Scaffolding.SqlCeDesignTimeServices, EntityFrameworkCore.SqlServerCompact40.Design",
    packageName: "EntityFrameworkCore.SqlServerCompact40.Design")]
#endif
[assembly: AssemblyVersion("7.0.0.0")]
[assembly: AssemblyFileVersion("7.0.0.0")]
[assembly: AssemblyInformationalVersion("7.0.0.0-rc2-final")]
