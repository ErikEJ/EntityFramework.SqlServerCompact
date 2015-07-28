using JetBrains.Annotations;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering;
using Microsoft.Framework.DependencyInjection;  

namespace Microsoft.Data.Entity.SqlServerCompact.Design.ReverseEngineering
{
    public class SqlCeDesignTimeMetadataProviderFactory : DesignTimeMetadataProviderFactory
    {
        public override void AddMetadataProviderServices([NotNull] IServiceCollection serviceCollection)
        {
            base.AddMetadataProviderServices(serviceCollection);
            serviceCollection.AddScoped<IDatabaseMetadataModelProvider, SqlCeMetadataModelProvider>();
        }
    }
}
