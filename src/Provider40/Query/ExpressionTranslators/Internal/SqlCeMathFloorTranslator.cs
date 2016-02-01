using System;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    public class SqlCeMathFloorTranslator : MultipleOverloadStaticMethodCallTranslator
    {
        public SqlCeMathFloorTranslator()
            : base(typeof(Math), nameof(Math.Floor), "FLOOR")
        {
        }
    }
}
