namespace Microsoft.Data.Entity.Query.ExpressionTranslators.Internal
{
    public class SqlCeStringToLowerTranslator : ParameterlessInstanceMethodCallTranslator
    {
        public SqlCeStringToLowerTranslator()
            : base(typeof(string), nameof(string.ToLower), "LOWER")
        {
        }
    }
}
