using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Extensions.Logging;

namespace Microsoft.Data.Entity.Internal
{
    public class SqlCeModelValidator : RelationalModelValidator
    {
        public SqlCeModelValidator([NotNull] ILogger<RelationalModelValidator> loggerFactory, [NotNull] IRelationalAnnotationProvider relationalExtensions)
            : base(loggerFactory, relationalExtensions)
        {
        }
    }
}
