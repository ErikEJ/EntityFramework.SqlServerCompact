namespace Microsoft.Data.Entity.Query.ExpressionTranslators
{
    public class StringToLowerTranslator : ParameterlessInstanceMethodCallTranslator
    {
        public StringToLowerTranslator()
            : base(typeof(string), nameof(string.ToLower), "LOWER")
        {
        }
    }
}
