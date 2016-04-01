namespace Microsoft.EntityFrameworkCore.Metadata.Internal
{
    public class SqlCeFullAnnotationNames : RelationalFullAnnotationNames
    {
        protected SqlCeFullAnnotationNames(string prefix)
            : base(prefix)
        {
            ValueGeneration = prefix + SqlCeAnnotationNames.ValueGeneration;
        }

        public new static SqlCeFullAnnotationNames Instance { get; } = new SqlCeFullAnnotationNames(SqlCeAnnotationNames.Prefix);

        public readonly string ValueGeneration;
    }
}

