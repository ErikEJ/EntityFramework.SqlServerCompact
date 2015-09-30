using System;

namespace Microsoft.Data.Entity.Query.ExpressionTranslators.Internal
{
    public class SqlCeMathCeilingTranslator : MultipleOverloadStaticMethodCallTranslator
    {
        public SqlCeMathCeilingTranslator()
            : base(typeof(Math), nameof(Math.Ceiling), "CEILING")
        {
        }
    }
}
