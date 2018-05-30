using EFCore.SqlCe.Scaffolding.Internal;
using EFCore.SqlCe.Storage.Internal;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.SqlCe.Design.Internal
{ 
    public class SqlCeDesignTimeServices : IDesignTimeServices
    {
        public virtual void ConfigureDesignTimeServices(IServiceCollection serviceCollection) 
            => serviceCollection
                .AddSingleton<IRelationalTypeMappingSource, SqlCeTypeMappingSource>()
                .AddSingleton<IDatabaseModelFactory, SqlCeDatabaseModelFactory>()
                .AddSingleton<IProviderConfigurationCodeGenerator, SqlCeCodeGenerator>()
                .AddSingleton<IAnnotationCodeGenerator, AnnotationCodeGenerator>();
    }
}
