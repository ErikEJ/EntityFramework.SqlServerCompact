using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;

namespace Microsoft.Data.Entity.Internal
{
    public class SqlCeModelSource : ModelSource
    {
        public SqlCeModelSource(
                   [NotNull] IDbSetFinder setFinder,
                   [NotNull] ICoreConventionSetBuilder coreConventionSetBuilder)
            : base(setFinder, coreConventionSetBuilder)
        {
        }
    }
}
