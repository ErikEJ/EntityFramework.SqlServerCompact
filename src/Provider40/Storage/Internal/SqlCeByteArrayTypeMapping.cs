using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.Globalization;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCore.SqlCe.Storage.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class SqlCeByteArrayTypeMapping : ByteArrayTypeMapping
    {
        private const int MaxSize = 8000;

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public SqlCeByteArrayTypeMapping(
            [NotNull] string storeType,
            DbType? dbType = System.Data.DbType.Binary,
            int? size = null,
            bool fixedLength = false,
            ValueComparer comparer = null,
            StoreTypePostfix? storeTypePostfix = null)
            : base(
                new RelationalTypeMappingParameters(
                    new CoreTypeMappingParameters(typeof(byte[]), null, comparer),
                    storeType,
                    //storeTypePostfix ?? StoreTypePostfix.Size,
                    GetStoreTypePostfix(storeTypePostfix, size),
                    dbType,
                    size: size,
                    fixedLength: fixedLength))
        {
        }

        private static StoreTypePostfix GetStoreTypePostfix(StoreTypePostfix? storeTypePostfix, int? size)
            => storeTypePostfix
               ?? (size != null && size <= MaxSize ? StoreTypePostfix.Size : StoreTypePostfix.None);

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected SqlCeByteArrayTypeMapping(RelationalTypeMappingParameters parameters)
            : base(parameters)
        {
        }

        private static int CalculateSize(int? size)
            => size.HasValue && size < MaxSize ? size.Value : MaxSize;

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
            => new SqlCeByteArrayTypeMapping(parameters);

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected override void ConfigureParameter(DbParameter parameter)
        {
            // For strings and byte arrays, set the max length to the size facet if specified, or
            // 8000 bytes if no size facet specified, if the data will fit so as to avoid query cache
            // fragmentation by setting lots of different Size values otherwise always set to
            // -1 (unbounded) to avoid SQL client size inference.

            //Set SqlDbType explicitly, to avoid errors like: Byte array truncation to a length of 8000
            //See https://connect.microsoft.com/sqlserver/feedback/details/311412/sqlceparameter-limits-dbtype-binary-to-8000-characters-ssce3-5

            var value = parameter.Value;
            var length = (value as byte[])?.Length;

            var maxSpecificSize = CalculateSize(Size);
                parameter.Size = (value == null) || (value == DBNull.Value) || ((length != null) && (length <= maxSpecificSize))
                    ? maxSpecificSize
                    : 0;

            if ((length == null) || (length.Value <= MaxSize))
            {
                return;
            }
            ((SqlCeParameter)parameter).SqlDbType = SqlDbType.Image;
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected override string GenerateNonNullSqlLiteral(object value)
        {
            var builder = new StringBuilder();
            builder.Append("0x");

            foreach (var @byte in (byte[])value)
            {
                builder.Append(@byte.ToString("X2", CultureInfo.InvariantCulture));
            }

            return builder.ToString();
        }
    }
}
