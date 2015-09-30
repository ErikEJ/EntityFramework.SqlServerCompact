using System;

namespace Microsoft.Data.Entity.Query.ExpressionTranslators.Internal
{
    public class SqlCeNewGuidTranslator : SingleOverloadStaticMethodCallTranslator
    {
        public SqlCeNewGuidTranslator()
            : base(typeof(Guid), nameof(Guid.NewGuid), "NEWID")
        {
        }
    }
}
