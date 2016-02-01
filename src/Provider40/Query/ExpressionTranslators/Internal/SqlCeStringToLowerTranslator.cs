namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    public class SqlCeStringToLowerTranslator : ParameterlessInstanceMethodCallTranslator
    {
        public SqlCeStringToLowerTranslator()
            : base(typeof(string), nameof(string.ToLower), "LOWER")
        {
        }
    }
}
