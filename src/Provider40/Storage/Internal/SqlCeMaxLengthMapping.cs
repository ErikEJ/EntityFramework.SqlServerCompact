using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using JetBrains.Annotations;

namespace Microsoft.Data.Entity.Storage.Internal
{
    public class SqlCeMaxLengthMapping : RelationalTypeMapping
    {
        private readonly int _maxSpecificSize;

        public SqlCeMaxLengthMapping([NotNull] string defaultTypeName, [NotNull] Type clrType, DbType? storeType = null)
            : base(defaultTypeName, clrType, storeType)
        {
            _maxSpecificSize = 
                storeType == DbType.Binary
                    ? 8000
                    : 4000;
        }

        protected override void ConfigureParameter(DbParameter parameter)
        {
            //Set SqlDbType explicitly, to avoid errors like: Byte array truncation to a length of 8000
            //See https://connect.microsoft.com/sqlserver/feedback/details/311412/sqlceparameter-limits-dbtype-binary-to-8000-characters-ssce3-5

            var length = (parameter.Value as string)?.Length ?? (parameter.Value as byte[])?.Length;

            if ((length == null) || (length.Value <= _maxSpecificSize)) return;
            if (parameter.DbType == DbType.Binary)
            {
                ((SqlCeParameter)parameter).SqlDbType = SqlDbType.Image;
            }
            if (parameter.DbType == DbType.String)
            {
                ((SqlCeParameter)parameter).SqlDbType = SqlDbType.NText;
            }
        }
    }
}
