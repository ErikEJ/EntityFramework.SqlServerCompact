using System;

namespace Microsoft.Data.Entity.Query.ExpressionTranslators
{
    public class NewGuidTranslator : SingleOverloadStaticMethodCallTranslator
    {
        public NewGuidTranslator()
            : base(typeof(Guid), nameof(Guid.NewGuid), "NEWID")
        {
        }
    }
}
