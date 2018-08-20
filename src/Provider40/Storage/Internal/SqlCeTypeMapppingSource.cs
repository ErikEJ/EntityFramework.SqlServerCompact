using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCore.SqlCe.Storage.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class SqlCeTypeMappingSource : RelationalTypeMappingSource
    {
        private readonly RelationalTypeMapping _real
            = new SqlCeFloatTypeMapping("real", DbType.Single);

        private readonly ByteTypeMapping _byte
            = new SqlCeByteTypeMapping("tinyint", DbType.Byte);

        private readonly ShortTypeMapping _short
            = new SqlCeShortTypeMapping("smallint", DbType.Int16);

        private readonly LongTypeMapping _long
            = new SqlCeLongTypeMapping("bigint", DbType.Int64);

        private readonly SqlCeByteArrayTypeMapping _rowversion
            = new SqlCeByteArrayTypeMapping(
                "rowversion",
                DbType.Binary,
                size: 8,
                comparer: new ValueComparer<byte[]>(
                    (v1, v2) => StructuralComparisons.StructuralEqualityComparer.Equals(v1, v2),
                    v => StructuralComparisons.StructuralEqualityComparer.GetHashCode(v),
                    v => v == null ? null : v.ToArray()),
                storeTypePostfix: StoreTypePostfix.None);

        private readonly IntTypeMapping _int
            = new IntTypeMapping("int", DbType.Int32);

        private readonly BoolTypeMapping _bool
            = new BoolTypeMapping("bit", DbType.Boolean);

        private readonly SqlCeStringTypeMapping _fixedLengthUnicodeString
            = new SqlCeStringTypeMapping("nchar", dbType: DbType.String, fixedLength: true);

        private readonly SqlCeStringTypeMapping _variableLengthUnicodeString
            = new SqlCeStringTypeMapping("nvarchar", dbType: null);

        private readonly SqlCeByteArrayTypeMapping _variableLengthBinary
            = new SqlCeByteArrayTypeMapping("varbinary");

        private readonly SqlCeByteArrayTypeMapping _fixedLengthBinary
            = new SqlCeByteArrayTypeMapping("binary", fixedLength: true);

        private readonly SqlCeDateTimeTypeMapping _datetime
            = new SqlCeDateTimeTypeMapping("datetime", dbType: DbType.DateTime);

        private readonly DoubleTypeMapping _double
            = new DoubleTypeMapping("float", DbType.Double);

        private readonly GuidTypeMapping _uniqueidentifier
            = new GuidTypeMapping("uniqueidentifier", DbType.Guid);

        private readonly DecimalTypeMapping _decimal
            = new SqlCeDecimalTypeMapping("decimal(18, 2)", DbType.Decimal, 18, 2);

        private readonly Dictionary<Type, RelationalTypeMapping> _clrTypeMappings;

        private readonly Dictionary<string, RelationalTypeMapping> _storeTypeMappings;

        // These are disallowed only if specified without any kind of length specified in parenthesis.
        private readonly HashSet<string> _disallowedMappings
            = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "binary",
                "binary varying",
                "varbinary",
                "national char",
                "national character",
                "nchar",
                "national char varying",
                "national character varying",
                "nvarchar"
            };

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public SqlCeTypeMappingSource(
            [NotNull] TypeMappingSourceDependencies dependencies,
            [NotNull] RelationalTypeMappingSourceDependencies relationalDependencies)
            : base(dependencies, relationalDependencies)
        {
            _clrTypeMappings
                = new Dictionary<Type, RelationalTypeMapping>
                {
                    { typeof(int), _int },
                    { typeof(long), _long },
                    { typeof(Guid), _uniqueidentifier },
                    { typeof(DateTime), _datetime },
                    { typeof(bool), _bool },
                    { typeof(byte), _byte },
                    { typeof(double), _double },
                    { typeof(short), _short },
                    { typeof(float), _real },
                    { typeof(decimal), _decimal },
                };

            _storeTypeMappings
                = new Dictionary<string, RelationalTypeMapping>(StringComparer.OrdinalIgnoreCase)
                {
                    { "bigint", _long },
                    { "binary varying", _variableLengthBinary },
                    { "binary", _fixedLengthBinary },
                    { "bit", _bool },
                    { "datetime", _datetime },
                    { "dec", _decimal },
                    { "decimal", _decimal },
                    { "double precision", _double },
                    { "float", _double },
                    { "image", _variableLengthBinary },
                    { "int", _int },
                    { "money", _decimal },
                    { "national char varying", _variableLengthUnicodeString },
                    { "national character varying", _variableLengthUnicodeString },
                    { "national character", _fixedLengthUnicodeString },
                    { "nchar", _fixedLengthUnicodeString },
                    { "ntext", _variableLengthUnicodeString },
                    { "numeric", _decimal },
                    { "nvarchar", _variableLengthUnicodeString },
                    { "real", _real },
                    { "rowversion", _rowversion },
                    { "smallint", _short },
                    { "timestamp", _rowversion },
                    { "tinyint", _byte },
                    { "uniqueidentifier", _uniqueidentifier },
                    { "varbinary", _variableLengthBinary }
                };
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected override void ValidateMapping(CoreTypeMapping mapping, IProperty property)
        {
            var relationalMapping = mapping as RelationalTypeMapping;

            if (_disallowedMappings.Contains(relationalMapping?.StoreType))
            {
                if (property == null)
                {
                    throw new ArgumentException($"Unqualified data type {relationalMapping.StoreType}");
                }

                throw new ArgumentException($"Unqualified data type {relationalMapping.StoreType} on property {property.Name}");
            }
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected override RelationalTypeMapping FindMapping(in RelationalTypeMappingInfo mappingInfo)
            => FindRawMapping(mappingInfo)?.Clone(mappingInfo);

        private RelationalTypeMapping FindRawMapping(RelationalTypeMappingInfo mappingInfo)
        {
            var clrType = mappingInfo.ClrType;
            var storeTypeName = mappingInfo.StoreTypeName;
            var storeTypeNameBase = mappingInfo.StoreTypeNameBase;

            if (storeTypeName != null)
            {
                if (clrType == typeof(float)
                    && mappingInfo.Size != null
                    && mappingInfo.Size <= 24
                    && (storeTypeNameBase.Equals("float", StringComparison.OrdinalIgnoreCase)
                        || storeTypeNameBase.Equals("double precision", StringComparison.OrdinalIgnoreCase)))
                {
                    return _real;
                }

                if (_storeTypeMappings.TryGetValue(storeTypeName, out var mapping)
                    || _storeTypeMappings.TryGetValue(storeTypeNameBase, out mapping))
                {
                    return clrType == null
                           || mapping.ClrType == clrType
                        ? mapping
                        : null;
                }
            }

            if (clrType != null)
            {
                if (_clrTypeMappings.TryGetValue(clrType, out var mapping))
                {
                    return mapping;
                }

                if (clrType == typeof(string))
                {
                    var isFixedLength = mappingInfo.IsFixedLength == true;
                    var baseName = "n" + (isFixedLength ? "char" : "varchar");
                    var maxSize = 4000;

                    var size = mappingInfo.Size ?? (mappingInfo.IsKeyOrIndex ? (int?)(256) : maxSize);

                    if (size > maxSize)
                    {
                        size = null;
                    }

                    var storeType = "nvarchar(4000)";
                    if (size != null && mappingInfo.StoreTypeName == null)
                    {
                        storeType = baseName + "(" + size.ToString() + ")";
                    }

                    var dbType = isFixedLength ? DbType.StringFixedLength : (DbType?)null;

                    return new SqlCeStringTypeMapping(
                        storeType,
                        dbType,
                        size,
                        isFixedLength);
                }

                if (clrType == typeof(byte[]))
                {
                    if (mappingInfo.IsRowVersion == true)
                    {
                        return _rowversion;
                    }

                    var size = mappingInfo.Size ?? (mappingInfo.IsKeyOrIndex ? (int?)512 : null);
                    if (size > 8000)
                    {
                        size = null;
                    }

                    var isFixedLength = mappingInfo.IsFixedLength == true;

                    var storeType = "image";
                    if (size != null)
                    {
                        storeType = (isFixedLength ? "binary(" : "varbinary(") + size.ToString() + ")";
                    }

                    return new SqlCeByteArrayTypeMapping(
                        storeType,
                        DbType.Binary,
                        size);
                }
            }

            return null;
        }
    }
}
