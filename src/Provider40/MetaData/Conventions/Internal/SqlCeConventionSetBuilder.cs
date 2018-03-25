using EFCore.SqlCe.Storage.Internal;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCore.SqlCe.Metadata.Conventions.Internal
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
                    new RelationalConventionSetBuilderDependencies(null, null, null, null, sqlCeTypeMapper),
                    new SqlCeSqlGenerationHelper(new RelationalSqlGenerationHelperDependencies()))
                .AddConventions(
                    new CoreConventionSetBuilder(
                            new CoreConventionSetBuilderDependencies(null, null, null, sqlCeTypeMapper))
                        .CreateConventionSet());
        }
    }
}
