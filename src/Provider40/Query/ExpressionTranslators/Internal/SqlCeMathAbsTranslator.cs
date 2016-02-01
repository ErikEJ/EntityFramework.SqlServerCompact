using System;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    public class SqlCeMathAbsTranslator : MultipleOverloadStaticMethodCallTranslator
    {
        public SqlCeMathAbsTranslator()
            : base(typeof(Math), nameof(Math.Abs), "ABS")
        {
        }
    }
}
