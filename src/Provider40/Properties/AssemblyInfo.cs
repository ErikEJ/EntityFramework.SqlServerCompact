using System.Reflection;
using Microsoft.Data.Entity.Storage;

#if SQLCE35
[assembly: AssemblyTitle("EntityFramework7.SqlServerCompact35")]
[assembly: AssemblyProduct("EntityFramework7.SqlServerCompact35")]
[assembly: ProviderDesignTimeServices(
    typeName: "Microsoft.Data.Entity.SqlServerCompact.Design.ReverseEngineering.SqlCeDesignTimeMetadataProviderFactory",
    assemblyName: "EntityFramework7.SqlServerCompact35.Design")]
#else
[assembly: ProviderDesignTimeServices(
    typeName: "Microsoft.Data.Entity.SqlServerCompact.Design.ReverseEngineering.SqlCeDesignTimeMetadataProviderFactory",
    assemblyName: "EntityFramework7.SqlServerCompact40.Design")]
[assembly: AssemblyTitle("EntityFramework7.SqlServerCompact40")]
[assembly: AssemblyProduct("EntityFramework7.SqlServerCompact40")]
#endif
[assembly: AssemblyVersion("7.0.0.0")]
[assembly: AssemblyFileVersion("7.0.0.0")]
[assembly: AssemblyInformationalVersion("7.0.0.0")]
