using Microsoft.Data.Entity.Relational.Query.Methods;

namespace Microsoft.Data.Entity.SqlServerCompact.Query.Methods
{
    public class StringToUpperTranslator : ParameterlessInstanceMethodCallTranslator
    {
        public StringToUpperTranslator()
            : base(declaringType: typeof(string), clrMethodName: "ToUpper", sqlFunctionName: "UPPER")
        {
        }
    }
}
