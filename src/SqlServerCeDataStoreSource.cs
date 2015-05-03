using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Storage;

namespace ErikEJ.Data.Entity.SqlServerCe
{
    public class SqlServerCeDataStoreSource : DataStoreSource<SqlServerCeDataStore, ISqlServerCeDataStoreServices, SqlServerCeOptionsExtension>
    {
        //TODO Test (see SqlServer)
        public override void AutoConfigure(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}