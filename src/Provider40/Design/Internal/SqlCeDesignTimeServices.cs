using EFCore.SqlCe.Storage.Internal;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.SqlCe.Scaffolding.Internal
{ 
    public class SqlCeDesignTimeServices : IDesignTimeServices
    {
        public virtual void ConfigureDesignTimeServices([NotNull] IServiceCollection serviceCollection) 
            => serviceCollection
                .AddSingleton<IRelationalTypeMapper, SqlCeTypeMapper>()
                .AddSingleton<IDatabaseModelFactory, SqlCeDatabaseModelFactory>()
                .AddSingleton<IProviderCodeGenerator, SqlCeCodeGenerator>()
                .AddSingleton<IAnnotationCodeGenerator, AnnotationCodeGenerator>();
    }
}
