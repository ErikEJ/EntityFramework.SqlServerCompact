using JetBrains.Annotations;

namespace Microsoft.Data.Entity.Scaffolding.Metadata
{
    public class SqlCeColumnModel : ColumnModel
    {
        public virtual bool IsIdentity { get;[param: CanBeNull] set; }
    }
}