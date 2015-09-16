using System;

namespace Microsoft.Data.Entity.Query.ExpressionTranslators
{
    public class MathCeilingTranslator : MultipleOverloadStaticMethodCallTranslator
    {
        public MathCeilingTranslator()
            : base(typeof(Math), nameof(Math.Ceiling), "CEILING")
        {
        }
    }
}
