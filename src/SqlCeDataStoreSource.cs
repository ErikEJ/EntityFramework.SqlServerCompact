using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Storage;

namespace ErikEJ.Data.Entity.SqlServerCe
{
    public class SqlCeDataStoreSource : DataStoreSource<SqlCeDataStoreServices, SqlCeOptionsExtension>
    {
        public override void AutoConfigure(EntityOptionsBuilder optionsBuilder)
        {
        }

        public override string Name => "SqlCeDataStore";
    }
}