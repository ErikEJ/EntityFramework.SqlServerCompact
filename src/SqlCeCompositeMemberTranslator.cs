using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity.Relational.Query;
using Microsoft.Data.Entity.Relational.Query.Methods;

namespace ErikEJ.Data.Entity.SqlServerCe
{
    public class SqlCeCompositeMemberTranslator : RelationalCompositeMemberTranslator
    {
        //TODO ErikEJ Implement translators + add tests
        //private readonly List<IMemberTranslator> _sqlServerTranslators = new List<IMemberTranslator>
        //{
        //    new StringLengthTranslator(),
        //    new DateTimeNowTranslator(),
        //};

        //protected override IReadOnlyList<IMemberTranslator> Translators
        //    => base.Translators.Concat(_sqlServerTranslators).ToList();
    }
}
