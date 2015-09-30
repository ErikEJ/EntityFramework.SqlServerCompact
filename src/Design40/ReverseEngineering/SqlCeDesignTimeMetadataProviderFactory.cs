using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Internal;
using Microsoft.Data.Entity.MetaData;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering.Internal;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering.Internal.Templating;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering.Internal.Templating.Compilation;
using Microsoft.Data.Entity.SqlServerCompact.Design.Utilities;
using Microsoft.Framework.DependencyInjection;

namespace Microsoft.Data.Entity.SqlServerCompact.Design.ReverseEngineering
{
    public class SqlCeDesignTimeMetadataProviderFactory : DesignTimeMetadataProviderFactory
    {
        public override void AddMetadataProviderServices([NotNull] IServiceCollection serviceCollection)
        {
            base.AddMetadataProviderServices(serviceCollection);
            serviceCollection
                .AddSingleton<MetadataReferencesProvider>()
                .AddSingleton<ICompilationService, RoslynCompilationService>()
                .AddSingleton<RazorTemplating>()
                .AddSingleton<IDatabaseMetadataModelProvider, SqlCeMetadataModelProvider>()
                .AddSingleton<IRelationalAnnotationProvider, SqlCeAnnotationProvider>()
                .AddSingleton<SqlServerLiteralUtilities>()
                .AddSingleton<ModelConfigurationFactory, SqlCeModelConfigurationFactory>()
                .AddSingleton<CodeWriter, RazorTemplateCodeWriter>();
        }
    }
}
