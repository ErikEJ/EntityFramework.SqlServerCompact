using JetBrains.Annotations;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Relational.Update;
using Microsoft.Framework.Logging;

namespace ErikEJ.Data.Entity.SqlServerCe.Update
{
    public class SqlServerCeBatchExecutor : BatchExecutor, ISqlServerCeBatchExecutor
    {
        public SqlServerCeBatchExecutor(
            [NotNull] ISqlServerCeTypeMapper typeMapper,
            [NotNull] DbContext context,
            [NotNull] ILoggerFactory loggerFactory)
            : base(typeMapper, context, loggerFactory)
        {
        }
    }
}
