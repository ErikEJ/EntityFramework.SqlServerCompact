using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal
{
    public class SqlCeConventionSetBuilder : RelationalConventionSetBuilder
    {
        public SqlCeConventionSetBuilder(
            [NotNull] RelationalConventionSetBuilderDependencies dependencies,
            [NotNull] ISqlGenerationHelper sqlGenerationHelper)
            : base(dependencies)
        {
        }

        public static ConventionSet Build()
            => new SqlCeConventionSetBuilder(
                    new RelationalConventionSetBuilderDependencies(
                        new SqlCeTypeMapper(
                            new RelationalTypeMapperDependencies()),
                        null,
                        null),
                    new SqlCeSqlGenerationHelper(
                        new RelationalSqlGenerationHelperDependencies()))
                .AddConventions(new CoreConventionSetBuilder().CreateConventionSet());
    }
}
