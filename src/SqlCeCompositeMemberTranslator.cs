using System.Collections.Generic;
using System.Linq;
using ErikEJ.Data.Entity.SqlServerCe.Query.Methods;
using Microsoft.Data.Entity.Relational.Query;
using Microsoft.Data.Entity.Relational.Query.Methods;

namespace ErikEJ.Data.Entity.SqlServerCe
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
