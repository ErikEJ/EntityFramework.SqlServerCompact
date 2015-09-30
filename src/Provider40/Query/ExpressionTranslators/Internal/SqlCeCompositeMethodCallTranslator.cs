using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Framework.Logging;

namespace Microsoft.Data.Entity.Query.ExpressionTranslators.Internal
{
    public class SqlCeCompositeMethodCallTranslator : RelationalCompositeMethodCallTranslator
    {
        public SqlCeCompositeMethodCallTranslator([NotNull] ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            var sqlCeTranslators = new List<IMethodCallTranslator>
            {
                new SqlCeNewGuidTranslator(),
                new SqlCeStringSubstringTranslator(),
                new SqlCeMathAbsTranslator(),
                new SqlCeMathCeilingTranslator(),
                new SqlCeMathFloorTranslator(),
                new SqlCeMathPowerTranslator(),
                new SqlCeMathRoundTranslator(),
                new SqlCeMathTruncateTranslator(),
                new SqlCeStringReplaceTranslator(),
                new SqlCeStringToLowerTranslator(),
                new SqlCeStringToUpperTranslator(),
                new SqlCeConvertTranslator(),
            };

            AddTranslators(sqlCeTranslators);
        }
    }
}
