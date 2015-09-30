using System;

namespace Microsoft.Data.Entity.Query.ExpressionTranslators.Internal
{
    public class SqlCeMathFloorTranslator : MultipleOverloadStaticMethodCallTranslator
    {
        public SqlCeMathFloorTranslator()
            : base(typeof(Math), nameof(Math.Floor), "FLOOR")
        {
        }
    }
}
