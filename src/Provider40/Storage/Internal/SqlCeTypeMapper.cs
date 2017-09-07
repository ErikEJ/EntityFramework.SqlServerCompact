using System;
using System.Collections.Generic;
using System.Data;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Storage.Internal
{ 
    public class SqlCeTypeMapper : RelationalTypeMapper
    {
        private readonly SqlCeStringTypeMapping _nvarchar4000
            = new SqlCeStringTypeMapping("nvarchar(4000)", DbType.String, size: 4000);

        private readonly SqlCeStringTypeMapping _nvarchar256 
            = new SqlCeStringTypeMapping("nvarchar(256)", DbType.String, size: 256);

        private readonly SqlCeStringTypeMapping _nvarcharmax
            = new SqlCeStringTypeMapping("ntext", DbType.String, size: null);

        private readonly SqlCeByteArrayTypeMapping _varbinary
            = new SqlCeByteArrayTypeMapping("varbinary(8000)", DbType.Binary, size: 8000);

        private readonly SqlCeByteArrayTypeMapping _varbinary512
            = new SqlCeByteArrayTypeMapping("varbinary(512)", DbType.Binary, size: 512);

        private readonly SqlCeByteArrayTypeMapping _varbinarymax 
            = new SqlCeByteArrayTypeMapping("image", DbType.Binary, size: null);

        private readonly SqlCeByteArrayTypeMapping _rowversion 
            =new SqlCeByteArrayTypeMapping("rowversion", dbType: DbType.Binary, size: 8);

        private readonly IntTypeMapping _int = new IntTypeMapping("int", DbType.Int32);

        private readonly LongTypeMapping _bigint  = new LongTypeMapping("bigint", DbType.Int64);

        private readonly ShortTypeMapping _smallint = new ShortTypeMapping("smallint", DbType.Int16);

        private readonly ByteTypeMapping _tinyint = new ByteTypeMapping("tinyint", DbType.Byte);

        private readonly BoolTypeMapping _bit = new BoolTypeMapping("bit", DbType.Boolean);

        private readonly SqlCeStringTypeMapping _nchar 
            = new SqlCeStringTypeMapping("nchar", DbType.StringFixedLength, size: null);

        private readonly SqlCeStringTypeMapping _nvarchar 
            = new SqlCeStringTypeMapping("nvarchar", DbType.String, size: null);

        private readonly SqlCeDateTimeTypeMapping _datetime
            = new SqlCeDateTimeTypeMapping("datetime", DbType.DateTime);

        private readonly DoubleTypeMapping _double = new DoubleTypeMapping("float", DbType.Double);

        private readonly FloatTypeMapping _real = new FloatTypeMapping("real", DbType.Single);

        private readonly GuidTypeMapping _uniqueidentifier = new GuidTypeMapping("uniqueidentifier", DbType.Guid);

        private readonly DecimalTypeMapping _decimal = new DecimalTypeMapping("decimal(18, 2)", DbType.Decimal);

        private readonly Dictionary<Type, RelationalTypeMapping> _clrTypeMappings;
        private readonly Dictionary<string, RelationalTypeMapping> _storeTypeMappings;
        private readonly HashSet<string> _disallowedMappings;

        public SqlCeTypeMapper([NotNull] RelationalTypeMapperDependencies dependencies)
            : base(dependencies)
        {
            _storeTypeMappings
                = new Dictionary<string, RelationalTypeMapping>(StringComparer.OrdinalIgnoreCase)
                    {
                    { "bigint", _bigint },
                    { "binary varying", _varbinary },
                    { "binary", _varbinary },
                    { "bit", _bit },
                    { "datetime", _datetime },
                    { "dec", _decimal },
                    { "decimal", _decimal },
                    { "float", _double },
                    { "image", _varbinary },
                    { "int", _int },
                    { "money", _decimal },
                    { "national char varying", _nvarchar },
                    { "national character varying", _nvarchar },
                    { "national character", _nchar },
                    { "nchar", _nchar },
                    { "ntext", _nvarcharmax },
                    { "numeric", _decimal },
                    { "nvarchar", _nvarchar },
                    { "real", _real },
                    { "rowversion", _rowversion },
                    { "smalldatetime", _datetime },
                    { "smallint", _smallint },
                    { "smallmoney", _decimal },
                    { "timestamp", _rowversion },
                    { "tinyint", _tinyint },
                    { "uniqueidentifier", _uniqueidentifier },
                    { "varbinary", _varbinary }
                    };

            _clrTypeMappings
                = new Dictionary<Type, RelationalTypeMapping>
                    {
                    { typeof(int), _int },
                    { typeof(long), _bigint },
                    { typeof(DateTime), _datetime },
                    { typeof(Guid), _uniqueidentifier },
                    { typeof(bool), _bit },
                    { typeof(byte), _tinyint },
                    { typeof(double), _double },
                    { typeof(short), _smallint },
                    { typeof(float), _real },
                    { typeof(decimal), _decimal }
                    };

            _disallowedMappings
                = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    "binary varying",
                    "binary",
                    "national char varying",
                    "national character varying",
                    "national character",
                    "nchar",
                    "nvarchar",
                    "varbinary",
                };

            ByteArrayMapper
                = new ByteArrayRelationalTypeMapper(
                    maxBoundedLength: 8000,
                    defaultMapping: _varbinary,
                    unboundedMapping: _varbinarymax,
                    keyMapping:  _varbinary512,
                    rowVersionMapping: _rowversion,
                    createBoundedMapping: size => new SqlCeByteArrayTypeMapping(
                        "varbinary(" + size + ")",
                        DbType.Binary,
                        size: size));

            StringMapper
                = new StringRelationalTypeMapper(
                    maxBoundedAnsiLength: 8000,
                    defaultAnsiMapping: _nvarchar4000,
                    unboundedAnsiMapping: _nvarcharmax,
                    keyAnsiMapping: _nvarchar256,
                    createBoundedAnsiMapping: size => new SqlCeStringTypeMapping(
                        "nvarchar(" + size + ")",
                        dbType: null,
                        size: size),
                    maxBoundedUnicodeLength: 4000,
                    defaultUnicodeMapping: _nvarchar4000,
                    unboundedUnicodeMapping: _nvarcharmax,
                    keyUnicodeMapping: _nvarchar256,
                    createBoundedUnicodeMapping: size => new SqlCeStringTypeMapping(
                        "nvarchar(" + size + ")",
                        dbType: null,
                        size: size));
        }

        public override IByteArrayRelationalTypeMapper ByteArrayMapper { get; }

        public override IStringRelationalTypeMapper StringMapper { get; }

        public override void ValidateTypeName(string storeType)
        {
            if (_disallowedMappings.Contains(storeType))
            {
                throw new ArgumentException(string.Format("UnqualifiedDataType ({0})", storeType));
            }
        }

        protected override string GetColumnType(IProperty property) => property.SqlCe().ColumnType;

        protected override IReadOnlyDictionary<Type, RelationalTypeMapping> GetClrTypeMappings()
             => _clrTypeMappings;

        protected override IReadOnlyDictionary<string, RelationalTypeMapping> GetStoreTypeMappings()
            => _storeTypeMappings;

        public override RelationalTypeMapping FindMapping(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));

            clrType = clrType.UnwrapNullableType().UnwrapEnumType();

            return clrType == typeof(string)
                ? _nvarchar
                : clrType == typeof(byte[])
                    ? _varbinarymax
                    : base.FindMapping(clrType);
        }

        protected override bool RequiresKeyMapping(IProperty property)
            => base.RequiresKeyMapping(property) || property.IsIndex();
    }
}
