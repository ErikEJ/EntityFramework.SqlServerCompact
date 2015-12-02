using System.Reflection;
using System.Resources;

#if SQLCE35
[assembly: AssemblyTitle("EntityFramework.SqlServerCompact35.Design")]
[assembly: AssemblyProduct("EntityFramework.SqlServerCompact35.Design")]
#else
[assembly: AssemblyTitle("EntityFramework.SqlServerCompact40.Design")]
[assembly: AssemblyProduct("EntityFramework.SqlServerCompact40.Design")]
#endif

[assembly: AssemblyVersion("7.0.0.0")]
[assembly: AssemblyFileVersion("7.0.0.0")]
[assembly: AssemblyInformationalVersion("7.0.0.0-rc2-final")]

[assembly: NeutralResourcesLanguage("en-US")]
[assembly: AssemblyMetadata("Serviceable", "True")]