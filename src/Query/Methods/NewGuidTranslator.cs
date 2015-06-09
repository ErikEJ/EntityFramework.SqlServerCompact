using System;
using Microsoft.Data.Entity.Relational.Query.Methods;

namespace ErikEJ.Data.Entity.SqlServerCe.Query.Methods
{
    public class NewGuidTranslator : SingleOverloadStaticMethodCallTranslator
    {
        public NewGuidTranslator()
            : base(typeof(Guid), "NewGuid", "NEWID")
        {
        }
    }
}
