using System;
using Microsoft.Data.Entity.Relational.Query.Methods;

namespace Microsoft.Data.Entity.SqlServerCompact.Query.Methods
{
    public class MathCeilingTranslator : MultipleOverloadStaticMethodCallTranslator
    {
        public MathCeilingTranslator()
            : base(typeof(Math), "Ceiling", "CEILING")
        {
        }
    }
}
