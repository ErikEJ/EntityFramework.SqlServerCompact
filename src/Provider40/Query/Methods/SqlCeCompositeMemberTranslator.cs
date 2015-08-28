using System.Collections.Generic;
using Microsoft.Data.Entity.Query.ExpressionTranslators;
using Microsoft.Data.Entity.SqlServerCompact.Query.Methods;

namespace Microsoft.Data.Entity.SqlServerCompact.Query.ExpressionTranslators
{
    public class SqlCeCompositeMemberTranslator : RelationalCompositeMemberTranslator
    {
        public SqlCeCompositeMemberTranslator()
        {
            var sqlCeTranslators = new List<IMemberTranslator>
            {
                new StringLengthTranslator(),
                new DateTimeNowTranslator()
            };

            AddTranslators(sqlCeTranslators);
        }
    }
}
