using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Update.Internal
{
    public class SqlCeModificationCommandBatchFactory : IModificationCommandBatchFactory
    {
        private readonly IRelationalCommandBuilderFactory _commandBuilderFactory;
        private readonly ISqlGenerationHelper _sqlGenerationHelper;
        private readonly ISqlCeUpdateSqlGenerator _updateSqlGenerator;
        private readonly IRelationalValueBufferFactoryFactory _valueBufferFactoryFactory;

        public SqlCeModificationCommandBatchFactory(
            [NotNull] IRelationalCommandBuilderFactory commandBuilderFactory,
            [NotNull] ISqlGenerationHelper sqlGenerationHelper,
            [NotNull] IRawSqlCommandBuilder rawSqlCommandBuilder,
            [NotNull] ISqlCeUpdateSqlGenerator updateSqlGenerator,
            [NotNull] IRelationalValueBufferFactoryFactory valueBufferFactoryFactory,
            [NotNull] IDbContextOptions options)
        {
            Check.NotNull(commandBuilderFactory, nameof(commandBuilderFactory));
            Check.NotNull(updateSqlGenerator, nameof(updateSqlGenerator));
            Check.NotNull(valueBufferFactoryFactory, nameof(valueBufferFactoryFactory));
            Check.NotNull(options, nameof(options));
            Check.NotNull(rawSqlCommandBuilder, nameof(rawSqlCommandBuilder));

            _commandBuilderFactory = commandBuilderFactory;
            _sqlGenerationHelper = sqlGenerationHelper;
            _updateSqlGenerator = updateSqlGenerator;
            _valueBufferFactoryFactory = valueBufferFactoryFactory;
        }

        public virtual ModificationCommandBatch Create()
        {
            return new SqlCeModificationCommandBatch(
                _commandBuilderFactory,
                _sqlGenerationHelper,
                _updateSqlGenerator,
                _valueBufferFactoryFactory);
        }
    }
}
