using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational.Design.ReverseEngineering;
using Microsoft.Data.Entity.Relational.Design.Utilities;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.SqlServerCompact.Design.ReverseEngineering
{
    public class SqlCeCodeGeneratorHelperFactory : CodeGeneratorHelperFactory
    {
        private readonly IRelationalMetadataExtensionProvider _extensionsProvider;

        public SqlCeCodeGeneratorHelperFactory(
            [NotNull] ModelUtilities modelUtilities,
            [NotNull] IRelationalMetadataExtensionProvider extensionsProvider)
            : base(modelUtilities)
        {
            Check.NotNull(extensionsProvider, nameof(extensionsProvider));

            _extensionsProvider = extensionsProvider;
        }

        public override DbContextCodeGeneratorHelper DbContextHelper(DbContextGeneratorModel generatorModel)
            => new SqlCeDbContextCodeGeneratorHelper(generatorModel, _extensionsProvider, ModelUtilities);
    }
}
