using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Scaffolding.Metadata.Internal
{
    public class SqlCeColumnModelAnnotations
    {
        private readonly ColumnModel _column;

        public SqlCeColumnModelAnnotations([NotNull] ColumnModel column)
        {
            Check.NotNull(column, nameof(column));

            _column = column;
        }

        public virtual bool IsIdentity
        {
            get
            {
                var value = _column[SqlCeDatabaseModelAnnotationNames.IsIdentity];
                return value is bool && (bool)value;
            }
            [param: NotNull]
            set
            {
                _column[SqlCeDatabaseModelAnnotationNames.IsIdentity] = value;
            }
        }
    }
}