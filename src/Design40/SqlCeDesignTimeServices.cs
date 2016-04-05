using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.Scaffolding
{
    public class SqlCeDesignTimeServices
    {
        public virtual IServiceCollection ConfigureDesignTimeServices([NotNull] IServiceCollection serviceCollection) 
            => serviceCollection
                .AddSingleton<IScaffoldingModelFactory, SqlCeScaffoldingModelFactory>()
                .AddSingleton<IRelationalAnnotationProvider, SqlCeAnnotationProvider>()
                .AddSingleton<IRelationalTypeMapper, SqlCeTypeMapper>()
                .AddSingleton<IDatabaseModelFactory, SqlCeDatabaseModelFactory>();
    }
}
