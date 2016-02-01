using System;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    public class SqlCeMathCeilingTranslator : MultipleOverloadStaticMethodCallTranslator
    {
        public SqlCeMathCeilingTranslator()
            : base(typeof(Math), nameof(Math.Ceiling), "CEILING")
        {
        }
    }
}
