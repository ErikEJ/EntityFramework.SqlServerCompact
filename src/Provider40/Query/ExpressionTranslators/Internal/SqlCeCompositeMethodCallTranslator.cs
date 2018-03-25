using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;

namespace EFCore.SqlCe.Query.ExpressionTranslators.Internal
{
    public class SqlCeCompositeMethodCallTranslator : RelationalCompositeMethodCallTranslator
    {
        private static readonly IMethodCallTranslator[] _methodCallTranslators =
        {
                new SqlCeContainsOptimizedTranslator(),
                new SqlCeConvertTranslator(),
                new SqlCeDateAddTranslator(),
                new SqlCeEndsWithOptimizedTranslator(),
                new SqlCeMathTranslator(),
                new SqlCeNewGuidTranslator(),
                new SqlCeObjectToStringTranslator(),
                new SqlCeStartsWithOptimizedTranslator(),
                new SqlCeStringIsNullOrWhiteSpaceTranslator(),
                new SqlCeStringReplaceTranslator(),
                new SqlCeStringSubstringTranslator(),
                new SqlCeStringToLowerTranslator(),
                new SqlCeStringToUpperTranslator(),
                new SqlCeStringTrimEndTranslator(),
                new SqlCeStringTrimStartTranslator(),
                new SqlCeStringTrimTranslator(),
                new SqlCeStringIndexOfTranslator()
        };

        public SqlCeCompositeMethodCallTranslator(
            [NotNull] RelationalCompositeMethodCallTranslatorDependencies dependencies)
            : base(dependencies)
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            AddTranslators(_methodCallTranslators);
        }
    }
}
