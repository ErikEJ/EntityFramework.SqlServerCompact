using System;
using Microsoft.Data.Entity.Relational.Query.Methods;

namespace ErikEJ.Data.Entity.SqlServerCe.Query.Methods
{
    public class MathCeilingTranslator : MultipleOverloadStaticMethodCallTranslator
    {
        public MathCeilingTranslator()
            : base(typeof(Math), "Ceiling", "CEILING")
        {
        }
    }
}
