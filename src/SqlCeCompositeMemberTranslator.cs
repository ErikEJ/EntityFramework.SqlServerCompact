using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity.Relational.Query;
using Microsoft.Data.Entity.Relational.Query.Methods;
using Microsoft.Data.Entity.SqlServerCompact.Query.Methods;

namespace Microsoft.Data.Entity.SqlServerCompact
{
    public class SqlCeCompositeMemberTranslator : RelationalCompositeMemberTranslator
    {
        private readonly List<IMemberTranslator> _sqlServerTranslators = new List<IMemberTranslator>
        {
            new StringLengthTranslator(),
            new DateTimeNowTranslator(),
        };

        protected override IReadOnlyList<IMemberTranslator> Translators
            => base.Translators.Concat(_sqlServerTranslators).ToList();
    }
}
