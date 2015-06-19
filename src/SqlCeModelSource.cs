using JetBrains.Annotations;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata.ModelConventions;

namespace ErikEJ.Data.Entity.SqlServerCe
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
