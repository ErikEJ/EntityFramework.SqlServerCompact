using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational.Update;

namespace ErikEJ.Data.Entity.SqlServerCe.Update
{
    public class SqlServerCeModificationCommandBatchFactory : ModificationCommandBatchFactory, ISqlServerCeModificationCommandBatchFactory
    {
        public SqlServerCeModificationCommandBatchFactory([NotNull] ISqlServerCeSqlGenerator sqlGenerator)
            : base(sqlGenerator)
        {
        }

        public override ModificationCommandBatch Create(IDbContextOptions options) =>
            new SqlServerCeModificationCommandBatch(SqlGenerator);
    }
}
