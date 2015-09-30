using System;

namespace Microsoft.Data.Entity.Query.ExpressionTranslators.Internal
{
    public class SqlCeMathAbsTranslator : MultipleOverloadStaticMethodCallTranslator
    {
        public SqlCeMathAbsTranslator()
            : base(typeof(Math), nameof(Math.Abs), "ABS")
        {
        }
    }
}
