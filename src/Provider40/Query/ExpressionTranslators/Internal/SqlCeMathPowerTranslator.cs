using System;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    public class SqlCeMathPowerTranslator : SingleOverloadStaticMethodCallTranslator
    {
        public SqlCeMathPowerTranslator()
            : base(typeof(Math), nameof(Math.Pow), "POWER")
        {
        }
    }
}
