using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    public class SqlCeCompositeMethodCallTranslator : RelationalCompositeMethodCallTranslator
    {
        private static readonly IMethodCallTranslator[] _methodCallTranslators =
        {
                new SqlCeMathAbsTranslator(),
                new SqlCeMathCeilingTranslator(),
                new SqlCeMathFloorTranslator(),
                new SqlCeMathPowerTranslator(),
                new SqlCeMathRoundTranslator(),
                new SqlCeMathTruncateTranslator(),
                new SqlCeNewGuidTranslator(),
                new SqlCeStringIsNullOrWhiteSpaceTranslator(),
                new SqlCeStringReplaceTranslator(),
                new SqlCeStringSubstringTranslator(),
                new SqlCeStringToLowerTranslator(),
                new SqlCeStringToUpperTranslator(),
                new SqlCeStringTrimEndTranslator(),
                new SqlCeStringTrimStartTranslator(),
                new SqlCeStringTrimTranslator(),
                new SqlCeConvertTranslator()
        };

        public SqlCeCompositeMethodCallTranslator([NotNull] ILogger<SqlCeCompositeMethodCallTranslator> logger)
            : base(logger)
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            AddTranslators(_methodCallTranslators);
        }
    }
}
