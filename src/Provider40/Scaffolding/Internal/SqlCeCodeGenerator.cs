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

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override MethodCallCodeFragment GenerateUseProvider(
            string connectionString,
            MethodCallCodeFragment providerOptions)
            => new MethodCallCodeFragment(
                nameof(SqlCeDbContextOptionsExtensions.UseSqlCe),
                providerOptions == null
                ? new object[] { connectionString }
                : new object[] { connectionString, new NestedClosureCodeFragment("x", providerOptions) });
    }
}
