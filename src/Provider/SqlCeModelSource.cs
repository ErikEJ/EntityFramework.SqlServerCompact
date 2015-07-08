using JetBrains.Annotations;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;

namespace Microsoft.Data.Entity.SqlServerCompact
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
