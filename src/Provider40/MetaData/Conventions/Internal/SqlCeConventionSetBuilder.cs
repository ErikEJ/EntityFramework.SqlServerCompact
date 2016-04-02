using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal
{
    public class SqlCeConventionSetBuilder : RelationalConventionSetBuilder
    {
        public SqlCeConventionSetBuilder(
            [NotNull] IRelationalTypeMapper typeMapper,
            [CanBeNull] ICurrentDbContext currentContext,
            [CanBeNull] IDbSetFinder setFinder)
            : base(typeMapper, currentContext, setFinder)
        {
        }

        public static ConventionSet Build()
            => new SqlCeConventionSetBuilder(new SqlCeTypeMapper(), null, null)
                .AddConventions(new CoreConventionSetBuilder().CreateConventionSet());
    }
}
