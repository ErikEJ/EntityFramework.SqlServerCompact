using System;
using Microsoft.Data.Entity.Query.Methods;

namespace Microsoft.Data.Entity.SqlServerCompact.Query.Methods
{
    public class MathAbsTranslator : MultipleOverloadStaticMethodCallTranslator
    {
        public MathAbsTranslator()
            : base(typeof(Math), "Abs", "abs")
        {
        }
    }
}
