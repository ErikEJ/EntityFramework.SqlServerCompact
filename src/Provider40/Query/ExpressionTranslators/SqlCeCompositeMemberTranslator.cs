using System.Collections.Generic;

namespace Microsoft.Data.Entity.Query.ExpressionTranslators
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
