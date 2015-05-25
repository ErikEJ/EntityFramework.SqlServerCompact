using Microsoft.Data.Entity.Relational.Metadata;

namespace ErikEJ.Data.Entity.SqlServerCe.Metadata
{
    public interface ISqlServerCeModelExtensions : IRelationalModelExtensions
    {
        bool? IdentityKeyGeneration { get; }
    }
}
