using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Data.Entity.ValueGeneration;

namespace ErikEJ.Data.Entity.SqlServerCe.ValueGeneration
{
    public class SqlServerCeValueGeneratorSelector : ValueGeneratorSelector, ISqlServerCeValueGeneratorSelector
    {
        private readonly ISqlServerCeValueGeneratorCache _cache;

        public SqlServerCeValueGeneratorSelector([NotNull] ISqlServerCeValueGeneratorCache cache)
        {
            Check.NotNull(cache, nameof(cache));

            _cache = cache;
        }

        public override ValueGenerator Select(IProperty property)
        {
            Check.NotNull(property, nameof(property));

            return _cache.GetOrAdd(property, Create);
        }
    }
}
