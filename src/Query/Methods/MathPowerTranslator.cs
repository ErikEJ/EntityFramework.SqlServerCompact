using System;
using Microsoft.Data.Entity.Relational.Query.Methods;

namespace Microsoft.Data.Entity.SqlServerCompact.Query.Methods
{
    public class MathPowerTranslator : SingleOverloadStaticMethodCallTranslator
    {
        public MathPowerTranslator()
            : base(typeof(Math), "Pow", "POWER")
        {
        }
    }
}
