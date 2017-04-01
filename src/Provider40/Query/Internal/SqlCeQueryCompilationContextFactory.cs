using JetBrains.Annotations;

namespace Microsoft.EntityFrameworkCore.Query.Internal
{
    public class SqlCeQueryCompilationContextFactory : RelationalQueryCompilationContextFactory
    {
        public SqlCeQueryCompilationContextFactory(
            [NotNull] QueryCompilationContextDependencies dependencies,
            [NotNull] RelationalQueryCompilationContextDependencies relationalDependencies)
            : base(dependencies, relationalDependencies)
        {
        }

        public override QueryCompilationContext Create(bool async)
            => async
                ? new SqlCeQueryCompilationContext(
                    Dependencies,
                    new AsyncLinqOperatorProvider(),
                    new AsyncQueryMethodProvider(),
                    TrackQueryResults)
                : new SqlCeQueryCompilationContext(
                    Dependencies,
                    new LinqOperatorProvider(),
                    new QueryMethodProvider(),
                    TrackQueryResults);
    }
}
