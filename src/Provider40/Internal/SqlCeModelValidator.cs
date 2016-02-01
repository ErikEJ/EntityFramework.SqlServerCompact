using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Internal
{
    public class SqlCeModelValidator : RelationalModelValidator
    {
        public SqlCeModelValidator([NotNull] ILogger<RelationalModelValidator> loggerFactory, [NotNull] IRelationalAnnotationProvider relationalExtensions)
            : base(loggerFactory, relationalExtensions)
        {
        }
    }
}
