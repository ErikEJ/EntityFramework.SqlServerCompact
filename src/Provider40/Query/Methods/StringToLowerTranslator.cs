using Microsoft.Data.Entity.Query.Methods;

namespace Microsoft.Data.Entity.SqlServerCompact.Query.Methods
{
    public class StringToLowerTranslator : ParameterlessInstanceMethodCallTranslator
    {
        public StringToLowerTranslator()
            : base(declaringType: typeof(string), clrMethodName: "ToLower", sqlFunctionName: "LOWER")
        {
        }
    }
}
