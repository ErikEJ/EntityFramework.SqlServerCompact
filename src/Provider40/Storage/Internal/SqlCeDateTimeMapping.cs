﻿using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCore.SqlCe.Storage.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class SqlCeDateTimeTypeMapping : DateTimeTypeMapping
    {
        private const string DateTimeFormatConst = "{0:yyyy-MM-ddTHH:mm:ss.fff}";

        /// <summary>
        ///     Initializes a new instance of the <see cref="SqlCeDateTimeTypeMapping" /> class.
        /// </summary>
        /// <param name="storeType"> The name of the database type. </param>
        /// <param name="dbType"> The <see cref="System.Data.DbType" /> to be used. </param>
        public SqlCeDateTimeTypeMapping(
            string storeType,
            DbType? dbType = null)
            : base(storeType, dbType)
        {
        }

        /// <summary>
        ///     Creates a copy of this mapping.
        /// </summary>
        /// <param name="storeType"> The name of the database type. </param>
        /// <param name="size"> The size of data the property is configured to store, or null if no size is configured. </param>
        /// <returns> The newly created mapping. </returns>
        public override RelationalTypeMapping Clone(string storeType, int? size)
            => new SqlCeDateTimeTypeMapping(storeType, DbType);

        /// <summary>
        ///     Gets the string format to be used to generate SQL literals of this type.
        /// </summary>
        protected override string SqlLiteralFormatString => "'" + DateTimeFormatConst + "'";
    }
}
