﻿using System.Data;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCore.SqlCe.Storage.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class SqlCeDecimalTypeMapping : DecimalTypeMapping
    {
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public SqlCeDecimalTypeMapping(
            [NotNull] string storeType,
            DbType? dbType = null,
            int? precision = null,
            int? scale = null)
            : base(
                new RelationalTypeMappingParameters(
                    new CoreTypeMappingParameters(typeof(decimal)),
                    storeType,
                    StoreTypePostfix.PrecisionAndScale,
                    dbType,
                    precision: precision,
                    scale: scale))
        {
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected SqlCeDecimalTypeMapping(RelationalTypeMappingParameters parameters)
            : base(parameters)
        {
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override RelationalTypeMapping Clone(string storeType, int? size)
            => new SqlCeDecimalTypeMapping(Parameters.WithStoreTypeAndSize(storeType, size));

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override CoreTypeMapping Clone(ValueConverter converter)
            => new SqlCeDecimalTypeMapping(Parameters.WithComposedConverter(converter));
    }
}
