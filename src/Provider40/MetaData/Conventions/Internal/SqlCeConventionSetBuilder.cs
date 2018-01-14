using JetBrains.Annotations;
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
        {
            var sqlCeTypeMapper = new SqlCeTypeMapper(new RelationalTypeMapperDependencies());

            return new SqlCeConventionSetBuilder(
                    new RelationalConventionSetBuilderDependencies(sqlCeTypeMapper, null, null, null),
                    new SqlCeSqlGenerationHelper(new RelationalSqlGenerationHelperDependencies()))
                .AddConventions(
                    new CoreConventionSetBuilder(
                            new CoreConventionSetBuilderDependencies(sqlCeTypeMapper, null, null))
                        .CreateConventionSet());
        }
    }
}
