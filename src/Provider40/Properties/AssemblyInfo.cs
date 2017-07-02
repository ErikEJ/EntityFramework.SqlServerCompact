using System.Reflection;
using Microsoft.EntityFrameworkCore.Design;

[assembly: DesignTimeProviderServices("Microsoft.EntityFrameworkCore.Design.Internal.SqlCeDesignTimeServices")]
#if SQLCE35
[assembly: AssemblyTitle("EntityFrameworkCore.SqlServerCompact35")]
[assembly: AssemblyProduct("EntityFrameworkCore.SqlServerCompact35")]
#else
[assembly: AssemblyTitle("EntityFrameworkCore.SqlServerCompact40")]
[assembly: AssemblyProduct("EntityFrameworkCore.SqlServerCompact40")]
#endif
[assembly: AssemblyVersion("2.0.0.0")]
[assembly: AssemblyFileVersion("2.0.0.0")]
[assembly: AssemblyInformationalVersion("2.0.0-preview2")]
