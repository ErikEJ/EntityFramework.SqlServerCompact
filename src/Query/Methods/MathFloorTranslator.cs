using System;
using Microsoft.Data.Entity.Relational.Query.Methods;

namespace ErikEJ.Data.Entity.SqlServerCe.Query.Methods
{
    public class MathFloorTranslator : MultipleOverloadStaticMethodCallTranslator
    {
        public MathFloorTranslator()
            : base(typeof(Math), "Floor", "FLOOR")
        {
        }
    }
}
