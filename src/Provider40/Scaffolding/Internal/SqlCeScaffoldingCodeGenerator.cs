using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding;

namespace EFCore.SqlCe.Scaffolding.Internal
{
    public class SqlCeScaffoldingCodeGenerator : IScaffoldingProviderCodeGenerator
    {
        public virtual string GenerateUseProvider(string connectionString, string language)
            => language == "CSharp"
                ? $".{nameof(SqlCeDbContextOptionsExtensions.UseSqlCe)}({GenerateVerbatimStringLiteral(connectionString)})"
                : null;

        private static string GenerateVerbatimStringLiteral(string value) => "@\"" + value.Replace("\"", "\"\"") + "\"";
    }
}
