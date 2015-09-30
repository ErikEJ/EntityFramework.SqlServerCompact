namespace Microsoft.Data.Entity.Query.ExpressionTranslators.Internal
{
    public class SqlCeStringToUpperTranslator : ParameterlessInstanceMethodCallTranslator
    {
        public SqlCeStringToUpperTranslator()
            : base(typeof(string), nameof(string.ToUpper), "UPPER")
        {
        }
    }
}
