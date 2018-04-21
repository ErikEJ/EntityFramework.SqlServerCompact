using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding;

namespace EFCore.SqlCe.Scaffolding.Internal
{
    public class SqlCeCodeGenerator : ProviderCodeGenerator
    {
        public SqlCeCodeGenerator([NotNull] ProviderCodeGeneratorDependencies dependencies)
            : base(dependencies)
        {
        }

        public override MethodCallCodeFragment GenerateUseProvider(string connectionString)
            => new MethodCallCodeFragment(nameof(SqlCeDbContextOptionsExtensions.UseSqlCe), connectionString);
    }
}
