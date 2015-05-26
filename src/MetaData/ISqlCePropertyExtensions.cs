using Microsoft.Data.Entity.Relational.Metadata;

namespace ErikEJ.Data.Entity.SqlServerCe.Metadata
{
    public interface ISqlServerCePropertyExtensions : IRelationalPropertyExtensions
    {
        bool? IdentityKeyGeneration { get; }
    }
}
