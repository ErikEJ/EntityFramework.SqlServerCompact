using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.Scaffolding.Internal
{ 
    public class SqlCeDesignTimeServices : IDesignTimeServices
    {
        public virtual void ConfigureDesignTimeServices([NotNull] IServiceCollection serviceCollection) 
            => serviceCollection
                .AddSingleton<IRelationalTypeMapper, SqlCeTypeMapper>()
                .AddSingleton<IDatabaseModelFactory, SqlCeDatabaseModelFactory>()
                .AddSingleton<IScaffoldingProviderCodeGenerator, SqlCeScaffoldingCodeGenerator>()
                .AddSingleton<IAnnotationCodeGenerator, AnnotationCodeGenerator>();
    }
}
