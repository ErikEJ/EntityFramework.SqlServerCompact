using JetBrains.Annotations;
using Microsoft.Data.Entity.Storage;

namespace Microsoft.Data.Entity.Metadata.Conventions.Internal
{
    public class SqlCeConventionSetBuilder : RelationalConventionSetBuilder
    {
        public SqlCeConventionSetBuilder([NotNull] IRelationalTypeMapper typeMapper)
            : base(typeMapper)
        {
        }
    }
}
