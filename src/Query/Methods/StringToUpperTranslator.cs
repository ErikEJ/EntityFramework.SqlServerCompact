using Microsoft.Data.Entity.Relational.Query.Methods;

namespace ErikEJ.Data.Entity.SqlServerCe.Query.Methods
{
    public class StringToUpperTranslator : ParameterlessInstanceMethodCallTranslator
    {
        public StringToUpperTranslator()
            : base(declaringType: typeof(string), clrMethodName: "ToUpper", sqlFunctionName: "UPPER")
        {
        }
    }
}
