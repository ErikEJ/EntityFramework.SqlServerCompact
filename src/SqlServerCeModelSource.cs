using JetBrains.Annotations;
using Microsoft.Data.Entity.Internal;

namespace ErikEJ.Data.Entity.SqlServerCe
{
    public class SqlServerCeModelSource : ModelSource
    {
        public SqlServerCeModelSource(
            [NotNull] IDbSetFinder setFinder, 
            [NotNull] IModelValidator modelValidator
            )
            : base(setFinder, modelValidator)
        {
        }
    }
}
