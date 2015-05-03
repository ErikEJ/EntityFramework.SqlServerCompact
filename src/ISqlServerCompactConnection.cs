using Microsoft.Data.Entity.Relational;

namespace ErikEJ.Data.Entity.SqlServerCe
{
    public interface ISqlServerCeConnection : IRelationalConnection
    {
        void CreateDatabase();

        bool Exists();

        void Delete();
    }
}