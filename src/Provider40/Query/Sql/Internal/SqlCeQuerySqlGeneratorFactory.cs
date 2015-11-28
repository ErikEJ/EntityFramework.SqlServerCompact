using JetBrains.Annotations;
using Microsoft.Data.Entity.Query.Expressions;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Query.Sql.Internal
{
    public class SqlCeQuerySqlGeneratorFactory : QuerySqlGeneratorFactoryBase
    {
        public SqlCeQuerySqlGeneratorFactory(
            [NotNull] IRelationalCommandBuilderFactory commandBuilderFactory,
            [NotNull] ISqlGenerator sqlGenerator,
            [NotNull] IParameterNameGeneratorFactory parameterNameGeneratorFactory)
            : base(
                Check.NotNull(commandBuilderFactory, nameof(commandBuilderFactory)),
                Check.NotNull(sqlGenerator, nameof(sqlGenerator)),
                Check.NotNull(parameterNameGeneratorFactory, nameof(parameterNameGeneratorFactory)))
        {
        }

        public override IQuerySqlGenerator CreateDefault(SelectExpression selectExpression)
            => new SqlCeQuerySqlGenerator(
                CommandBuilderFactory,
                SqlGenerator,
                ParameterNameGeneratorFactory,
                Check.NotNull(selectExpression, nameof(selectExpression)));
    }
}

