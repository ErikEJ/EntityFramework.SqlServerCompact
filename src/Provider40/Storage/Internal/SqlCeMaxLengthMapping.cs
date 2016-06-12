using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using JetBrains.Annotations;

namespace Microsoft.EntityFrameworkCore.Storage.Internal
{
    public class SqlCeMaxLengthMapping : RelationalTypeMapping
    {
        private readonly int _maxSpecificSize;

        public SqlCeMaxLengthMapping(
            [NotNull] string storeType,
            [NotNull] Type clrType,
            DbType? dbType = null)
            : this(storeType, clrType, dbType, unicode: false, size: null)
        {
        }

        public SqlCeMaxLengthMapping(
            [NotNull] string storeType,
            [NotNull] Type clrType,
            DbType? dbType,
            bool unicode,
            int? size,
            bool hasNonDefaultSize = false)
            : base(storeType, clrType, dbType, unicode, CalculateSize(unicode, size), false, hasNonDefaultSize)
        {
            _maxSpecificSize = 
                dbType == System.Data.DbType.Binary
                    ? 8000
                    : 4000;
        }

        private static int CalculateSize(bool unicode, int? size)
            => unicode
                ? size.HasValue && (size < 4000) ? size.Value : 4000
                : size.HasValue && (size < 8000) ? size.Value : 8000;

        protected override void ConfigureParameter(DbParameter parameter)
        {
            //Set SqlDbType explicitly, to avoid errors like: Byte array truncation to a length of 8000
            //See https://connect.microsoft.com/sqlserver/feedback/details/311412/sqlceparameter-limits-dbtype-binary-to-8000-characters-ssce3-5

            var value = parameter.Value;
            var length = (value as string)?.Length ?? (value as byte[])?.Length;

            if (Size != null)
            {
                parameter.Size = (value == null) || (value == DBNull.Value) || ((length != null) && (length <= Size.Value))
                    ? Size.Value
                    : 0;
            }

            if ((length == null) || (length.Value <= _maxSpecificSize)) return;
            if (parameter.DbType == System.Data.DbType.Binary)
            {
                ((SqlCeParameter)parameter).SqlDbType = SqlDbType.Image;
            }
            if (parameter.DbType == System.Data.DbType.String)
            {
                ((SqlCeParameter)parameter).SqlDbType = SqlDbType.NText;
            }
        }
    }
}
