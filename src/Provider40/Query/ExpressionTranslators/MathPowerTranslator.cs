using System;

namespace Microsoft.Data.Entity.Query.ExpressionTranslators
{
    public class MathPowerTranslator : SingleOverloadStaticMethodCallTranslator
    {
        public MathPowerTranslator()
            : base(typeof(Math), nameof(Math.Pow), "POWER")
        {
        }
    }
}
