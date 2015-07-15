using System;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.SqlServerCompact.Design.ReverseEngineering
{
    public class SqlCeDesignTimeMetadataProviderFactory : IDesignTimeMetadataProviderFactory
    {
        public virtual IDatabaseMetadataModelProvider Create([NotNull] IServiceProvider serviceProvider)
        {
            Check.NotNull(serviceProvider, nameof(serviceProvider));

            return new SqlCeMetadataModelProvider(serviceProvider);
        }
    }
}
