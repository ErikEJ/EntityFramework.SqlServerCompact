namespace Microsoft.Data.Entity.Query.ExpressionTranslators
{
    public class StringToUpperTranslator : ParameterlessInstanceMethodCallTranslator
    {
        public StringToUpperTranslator()
            : base(typeof(string), nameof(string.ToUpper), "UPPER")
        {
        }
    }
}
