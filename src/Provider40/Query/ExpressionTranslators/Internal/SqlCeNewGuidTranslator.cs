using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using System;

namespace EFCore.SqlCe.Query.ExpressionTranslators.Internal
{
    public class SqlCeNewGuidTranslator : SingleOverloadStaticMethodCallTranslator
    {
        public SqlCeNewGuidTranslator()
            : base(typeof(Guid), nameof(Guid.NewGuid), "NEWID")
        {
        }
    }
}
