using System.Data;
using System.Data.Common;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Relational;

namespace ErikEJ.Data.Entity.SqlServerCe
{
    public class SqlCeMaxLengthMapping : RelationalTypeMapping
    {
        private readonly int _maxSpecificSize;

        public SqlCeMaxLengthMapping([NotNull] string defaultTypeName, DbType? storeType = null)
            : base(defaultTypeName, storeType)
        {
            _maxSpecificSize = 
                storeType == DbType.Binary
                    ? 8000
                    : 4000;
        }

        protected override void ConfigureParameter(DbParameter parameter)
        {
            // For strings and byte arrays, set the max length to 8000 bytes if the data will
            // fit so as to avoid query cache fragmentation by setting lots of differet Size
            // values otherwise always set to -1 (unbounded) to avoid SQL client size inference.

            var length = (parameter.Value as string)?.Length ?? (parameter.Value as byte[])?.Length;

            parameter.Size = length != null && length <= _maxSpecificSize
                ? _maxSpecificSize
                : 0;
        }
    }
}
