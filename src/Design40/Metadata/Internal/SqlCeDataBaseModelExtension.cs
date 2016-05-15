using JetBrains.Annotations;

namespace Microsoft.EntityFrameworkCore.Scaffolding.Metadata.Internal
{
    public static class SqlCeDatabaseModelExtensions
    {
        public static SqlCeColumnModelAnnotations SqlCe([NotNull] this ColumnModel column)
            => new SqlCeColumnModelAnnotations(column);
    }
}
