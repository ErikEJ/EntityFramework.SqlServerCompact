using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Storage.Internal
{ 
    public class SqlCeTypeMapper : RelationalTypeMapper
    {
        private readonly SqlCeMaxLengthMapping _nvarcharmax 
            = new SqlCeMaxLengthMapping("ntext", typeof(string), DbType.String);
        private readonly SqlCeMaxLengthMapping _nvarchar256 
            = new SqlCeMaxLengthMapping("nvarchar(256)", typeof(string), DbType.String, 256);
        private readonly SqlCeMaxLengthMapping _varbinarymax 
            = new SqlCeMaxLengthMapping("image", typeof(byte[]), DbType.Binary);
        private readonly SqlCeMaxLengthMapping _varbinary512 
            = new SqlCeMaxLengthMapping("varbinary(512)", typeof(byte[]), DbType.Binary, 512);
        private readonly RelationalTypeMapping _rowversion 
            =new RelationalTypeMapping("rowversion", typeof(byte[]), dbType: DbType.Binary, unicode: false, size: 8); 
        private readonly RelationalTypeMapping _int 
            = new RelationalTypeMapping("int", typeof(int), DbType.Int32);
        private readonly RelationalTypeMapping _bigint 
            = new RelationalTypeMapping("bigint", typeof(long), DbType.Int64);
        private readonly RelationalTypeMapping _bit 
            = new RelationalTypeMapping("bit", typeof(bool));
        private readonly RelationalTypeMapping _smallint 
            = new RelationalTypeMapping("smallint", typeof(short), DbType.Int16);
        private readonly RelationalTypeMapping _tinyint 
            = new RelationalTypeMapping("tinyint", typeof(byte), DbType.Byte);
        private readonly SqlCeMaxLengthMapping _nchar 
            = new SqlCeMaxLengthMapping("nchar", typeof(string), DbType.StringFixedLength);
        private readonly SqlCeMaxLengthMapping _nvarchar 
            = new SqlCeMaxLengthMapping("nvarchar(4000)", typeof(string), DbType.String);
        private readonly SqlCeMaxLengthMapping _varbinary 
            = new SqlCeMaxLengthMapping("varbinary(8000)", typeof(byte[]), DbType.Binary, 8000);
        private readonly RelationalTypeMapping _double 
            = new RelationalTypeMapping("float", typeof(double));
        private readonly RelationalTypeMapping _real 
            = new RelationalTypeMapping("real", typeof(float));
        private readonly RelationalTypeMapping _datetime 
            = new RelationalTypeMapping("datetime", typeof(DateTime), DbType.DateTime);
        private readonly RelationalTypeMapping _uniqueidentifier 
            = new RelationalTypeMapping("uniqueidentifier", typeof(Guid), DbType.Guid);
        private readonly RelationalTypeMapping _decimal 
            = new RelationalTypeMapping("decimal(18, 2)", typeof(decimal));

        private readonly Dictionary<Type, RelationalTypeMapping> _clrTypeMappings;
        private readonly Dictionary<string, RelationalTypeMapping> _storeTypeMappings;
        private readonly HashSet<string> _disallowedMappings;

        public SqlCeTypeMapper()
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
                    { "ntext", _nvarchar },
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
                    { typeof(char), _int },
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
                    8000,
                    _varbinarymax,
                    _varbinarymax,
                    _varbinary512,
                    _rowversion, size => new SqlCeMaxLengthMapping(
                        "varbinary(" + size + ")",
                        typeof(byte[]),
                        DbType.Binary,
                        size: size,
                        hasNonDefaultSize: true));

            StringMapper
                = new StringRelationalTypeMapper(
                    4000,
                    _nvarchar,
                    _nvarcharmax,
                    _nvarchar256,
                    size => new SqlCeMaxLengthMapping(
                        "nvarchar(" + size + ")",
                        typeof(string),
                        dbType: null,
                        size: size,
                        hasNonDefaultSize: true),
                    4000,
                    _nvarchar,
                    _nvarcharmax,
                    _nvarchar256,
                    size => new SqlCeMaxLengthMapping(
                        "nvarchar(" + size + ")",
                        typeof(string),
                        dbType: null,
                        size: size,
                        hasNonDefaultSize: true));
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
                : (clrType == typeof(byte[])
                    ? _varbinarymax
                    : base.FindMapping(clrType));
        }

        protected override bool RequiresKeyMapping(IProperty property)
            => base.RequiresKeyMapping(property) || property.IsIndex();
    }
}
