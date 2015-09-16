using System;

namespace Microsoft.Data.Entity.Query.ExpressionTranslators
{
    public class MathAbsTranslator : MultipleOverloadStaticMethodCallTranslator
    {
        public MathAbsTranslator()
            : base(typeof(Math), nameof(Math.Abs), "ABS")
        {
        }
    }
}
