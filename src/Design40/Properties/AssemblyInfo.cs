using System.Reflection;
using System.Resources;

#if SQLCE35
[assembly: AssemblyTitle("EntityFrameworkCore.SqlServerCompact35.Design")]
[assembly: AssemblyProduct("EntityFrameworkCore.SqlServerCompact35.Design")]
#else
[assembly: AssemblyTitle("EntityFrameworkCore.SqlServerCompact40.Design")]
[assembly: AssemblyProduct("EntityFrameworkCore.SqlServerCompact40.Design")]
#endif

[assembly: AssemblyVersion("2.0.0.0")]
[assembly: AssemblyFileVersion("2.0.0.0")]
[assembly: AssemblyInformationalVersion("2.0.0-preview1")]

[assembly: NeutralResourcesLanguage("en-US")]
[assembly: AssemblyMetadata("Serviceable", "True")]