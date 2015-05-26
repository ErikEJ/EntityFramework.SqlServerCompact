using Microsoft.Data.Entity.Relational.Metadata;

namespace ErikEJ.Data.Entity.SqlServerCe.Metadata
{
    public interface ISqlCeModelExtensions : IRelationalModelExtensions
    {
        bool? IdentityKeyGeneration { get; }
    }
}
