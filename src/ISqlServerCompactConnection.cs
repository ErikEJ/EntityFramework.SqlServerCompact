using Microsoft.Data.Entity.Relational;

namespace ErikEJ.Data.Entity.SqlServerCompact
{
    public interface ISqlServerCompactConnection : IRelationalConnection
    {
        void CreateDatabase();

        bool Exists();

        void Delete();
    }
}