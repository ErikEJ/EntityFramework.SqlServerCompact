using JetBrains.Annotations;
using Microsoft.Data.Entity.Query.Expressions;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Query.Sql.Internal
{
    public class SqlCeQuerySqlGeneratorFactory : ISqlQueryGeneratorFactory
    {
        private readonly IRelationalCommandBuilderFactory _commandBuilderFactory;
        private readonly ISqlGenerator _sqlGenerator;
        private readonly IParameterNameGeneratorFactory _parameterNameGeneratorFactory;

        public SqlCeQuerySqlGeneratorFactory(
            [NotNull] IRelationalCommandBuilderFactory commandBuilderFactory,
            [NotNull] ISqlGenerator sqlGenerator,
            [NotNull] IParameterNameGeneratorFactory parameterNameGeneratorFactory)
        {
            Check.NotNull(commandBuilderFactory, nameof(commandBuilderFactory));
            Check.NotNull(sqlGenerator, nameof(sqlGenerator));
            Check.NotNull(parameterNameGeneratorFactory, nameof(parameterNameGeneratorFactory));

            _commandBuilderFactory = commandBuilderFactory;
            _sqlGenerator = sqlGenerator;
            _parameterNameGeneratorFactory = parameterNameGeneratorFactory;
        }

        public virtual ISqlQueryGenerator CreateGenerator(SelectExpression selectExpression)
            => new SqlCeQuerySqlGenerator(
                _commandBuilderFactory,
                _sqlGenerator,
                _parameterNameGeneratorFactory,
                Check.NotNull(selectExpression, nameof(selectExpression)));

        public virtual ISqlQueryGenerator CreateRawCommandGenerator(
            SelectExpression selectExpression,
            string sql,
            object[] parameters)
            => new RawSqlQueryGenerator(
                _commandBuilderFactory,
                _parameterNameGeneratorFactory,
                Check.NotNull(selectExpression, nameof(selectExpression)),
                Check.NotNull(sql, nameof(sql)),
                Check.NotNull(parameters, nameof(parameters)));

        public SqlCeQuerySqlGeneratorFactory(
            [NotNull] IParameterNameGeneratorFactory parameterNameGeneratorFactory)
        {
            Check.NotNull(parameterNameGeneratorFactory, nameof(parameterNameGeneratorFactory));

            _parameterNameGeneratorFactory = parameterNameGeneratorFactory;
        }
    }
}
