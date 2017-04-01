using JetBrains.Annotations;

namespace Microsoft.EntityFrameworkCore.Query.Internal
{
    public class SqlCeQueryCompilationContext : RelationalQueryCompilationContext
    {
        public SqlCeQueryCompilationContext(
            [NotNull] QueryCompilationContextDependencies dependencies,
            [NotNull] ILinqOperatorProvider linqOperatorProvider,
            [NotNull] IQueryMethodProvider queryMethodProvider,
            bool trackQueryResults)
            : base(
                dependencies,
                linqOperatorProvider,
                queryMethodProvider,
                trackQueryResults)
        {
        }

        public override bool IsLateralJoinSupported => true;
    }
}
