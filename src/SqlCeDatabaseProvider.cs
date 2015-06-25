using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Storage;

namespace ErikEJ.Data.Entity.SqlServerCe
{
    public class SqlCeDatabaseProvider : DatabaseProvider<SqlCeDatabaseProviderServices, SqlCeOptionsExtension>
    {
        public override void AutoConfigure(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public override string Name => "SqlCeDatabase";
    }
}