using System;
using Microsoft.Data.Entity.Relational.Query.Methods;

namespace ErikEJ.Data.Entity.SqlServerCe.Query.Methods
{
    public class MathAbsTranslator : MultipleOverloadStaticMethodCallTranslator
    {
        public MathAbsTranslator()
            : base(typeof(Math), "Abs", "abs")
        {
        }
    }
}
