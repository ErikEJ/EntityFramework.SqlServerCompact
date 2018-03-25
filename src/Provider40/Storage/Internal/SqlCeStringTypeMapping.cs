using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCore.SqlCe.Storage.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class SqlCeStringTypeMapping : StringTypeMapping
    {
        private readonly int _maxSpecificSize;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SqlCeStringTypeMapping" /> class.
        /// </summary>
        /// <param name="storeType"> The name of the database type. </param>
        /// <param name="dbType"> The <see cref="DbType" /> to be used. </param>
        public SqlCeStringTypeMapping(
            [NotNull] string storeType,
            [CanBeNull] DbType? dbType)
            : this(storeType, dbType, size: null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SqlCeStringTypeMapping" /> class.
        /// </summary>
        /// <param name="storeType"> The name of the database type. </param>
        /// <param name="dbType"> The <see cref="DbType" /> to be used. </param>
        /// <param name="size"> The size of data the property is configured to store, or null if no size is configured. </param>
        public SqlCeStringTypeMapping(
            [NotNull] string storeType,
            [CanBeNull] DbType? dbType,
            int? size)
            : base(storeType, dbType, true, size)
        {
            _maxSpecificSize = CalculateSize(size);
        }

        private static int CalculateSize(int? size)
            => size.HasValue && size < 4000 ? size.Value : 4000;

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override RelationalTypeMapping Clone(string storeType, int? size)
            => new SqlCeStringTypeMapping(
                storeType,
                DbType,
                size);

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected override void ConfigureParameter(DbParameter parameter)
        {
            //Set SqlDbType explicitly, to avoid errors like: Byte array truncation to a length of 8000
            //See https://connect.microsoft.com/sqlserver/feedback/details/311412/sqlceparameter-limits-dbtype-binary-to-8000-characters-ssce3-5

            // For strings and byte arrays, set the max length to the size facet if specified, or
            // 8000 bytes if no size facet specified, if the data will fit so as to avoid query cache
            // fragmentation by setting lots of different Size values otherwise always set to 
            // -1 (unbounded) to avoid SQL client size inference.

            var value = parameter.Value;
            var length = (value as string)?.Length ?? (value as byte[])?.Length;

            if (Size != null)
            {
                parameter.Size = (value == null) || (value == DBNull.Value) || ((length != null) && (length <= Size.Value))
                    ? Size.Value
                    : 0;
            }

            if ((length == null) || (length.Value <= _maxSpecificSize)) return;
            ((SqlCeParameter)parameter).SqlDbType = SqlDbType.NText;
        }

        /// <summary>
        ///     Generates the SQL representation of a literal value.
        /// </summary>
        /// <param name="value">The literal value.</param>
        /// <returns>
        ///     The generated string.
        /// </returns>
        protected override string GenerateNonNullSqlLiteral(object value)
            => $"N'{EscapeSqlLiteral((string)value)}'"; // Interpolation okay; strings
    }
}
