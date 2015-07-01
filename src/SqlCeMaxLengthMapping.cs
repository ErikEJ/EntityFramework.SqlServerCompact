using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Relational;

namespace Microsoft.Data.Entity.SqlServerCompact
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
            // For strings and byte arrays, set the max length to 4000/8000 bytes if the data will
            // fit so as to avoid query cache fragmentation by setting lots of differet Size
            // values otherwise always set to 0.
            //Also set SqlDbType explicitly, to avoid errors like: Byte array truncation to a length of 8000
            //See https://connect.microsoft.com/sqlserver/feedback/details/311412/sqlceparameter-limits-dbtype-binary-to-8000-characters-ssce3-5

            var length = (parameter.Value as string)?.Length ?? (parameter.Value as byte[])?.Length;

            if (length.HasValue && length.Value > _maxSpecificSize)
            {
                if (parameter.DbType == DbType.Binary)
                {
                    (parameter as SqlCeParameter).SqlDbType = SqlDbType.Image;
                }
                if (parameter.DbType == DbType.String)
                {
                    (parameter as SqlCeParameter).SqlDbType = SqlDbType.NText;
                }
            }

            parameter.Size = length != null && length <= _maxSpecificSize
                ? _maxSpecificSize
                : 0;
        }
    }
}
