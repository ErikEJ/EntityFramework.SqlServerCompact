using JetBrains.Annotations;
using Microsoft.Framework.Logging;

namespace Microsoft.Data.Entity.Query.ExpressionTranslators.Internal
{
    public class SqlCeCompositeMethodCallTranslator : RelationalCompositeMethodCallTranslator
    {
        private static readonly IMethodCallTranslator[] _methodCallTranslators =
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

        public SqlCeCompositeMethodCallTranslator([NotNull] ILogger<SqlCeCompositeMethodCallTranslator> logger)
            : base(logger)
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            AddTranslators(_methodCallTranslators);
        }
    }
}
