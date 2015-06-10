using Microsoft.Data.Entity.Relational.Query.Methods;

namespace ErikEJ.Data.Entity.SqlServerCe.Query.Methods
{
    public class StringToLowerTranslator : ParameterlessInstanceMethodCallTranslator
    {
        public StringToLowerTranslator()
            : base(declaringType: typeof(string), clrMethodName: "ToLower", sqlFunctionName: "LOWER")
        {
        }
    }
}
