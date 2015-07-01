using Microsoft.Data.Entity.Storage;

namespace Microsoft.Data.Entity.SqlServerCompact
{
    public class SqlCeDatabaseProvider : DatabaseProvider<SqlCeDatabaseProviderServices, SqlCeOptionsExtension>
    {
        public override void AutoConfigure(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public override string Name => "SqlCeDatabase";
    }
}