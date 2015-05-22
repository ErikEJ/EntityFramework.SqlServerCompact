using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Storage;

namespace ErikEJ.Data.Entity.SqlServerCe
{
    public class SqlServerCeDataStoreSource : DataStoreSource<SqlServerCeDataStoreServices, SqlServerCeOptionsExtension>
    {
        public override void AutoConfigure(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public override string Name => "SqlServerCeDataStore";
    }
}