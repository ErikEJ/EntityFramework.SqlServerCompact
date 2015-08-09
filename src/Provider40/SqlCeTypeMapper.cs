using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.SqlServerCompact
{
    public class SqlCeTypeMapper : RelationalTypeMapper
    {
        private readonly SqlCeMaxLengthMapping _nvarcharmax = new SqlCeMaxLengthMapping("ntext", DbType.String);
        private readonly SqlCeMaxLengthMapping _nvarchar256 = new SqlCeMaxLengthMapping("nvarchar(256)", DbType.String);
        private readonly SqlCeMaxLengthMapping _varbinarymax = new SqlCeMaxLengthMapping("image", DbType.Binary);
        private readonly SqlCeMaxLengthMapping _varbinary512 = new SqlCeMaxLengthMapping("varbinary(512)", DbType.Binary);
        private readonly RelationalSizedTypeMapping _rowversion = new RelationalSizedTypeMapping("rowversion", DbType.Binary, 8);
        private readonly RelationalTypeMapping _int = new RelationalTypeMapping("int", DbType.Int32);
        private readonly RelationalTypeMapping _bigint = new RelationalTypeMapping("bigint", DbType.Int64);
        private readonly RelationalTypeMapping _bit = new RelationalTypeMapping("bit");
        private readonly RelationalTypeMapping _smallint = new RelationalTypeMapping("smallint", DbType.Int16);
        private readonly RelationalTypeMapping _tinyint = new RelationalTypeMapping("tinyint", DbType.Byte);
        private readonly SqlCeMaxLengthMapping _nchar = new SqlCeMaxLengthMapping("nchar", DbType.StringFixedLength);
        private readonly SqlCeMaxLengthMapping _nvarchar = new SqlCeMaxLengthMapping("nvarchar(4000)", DbType.String);
        private readonly SqlCeMaxLengthMapping _varbinary = new SqlCeMaxLengthMapping("varbinary(8000)", DbType.Binary);
        private readonly RelationalTypeMapping _double = new RelationalTypeMapping("float");
        private readonly RelationalTypeMapping _real = new RelationalTypeMapping("real");
        private readonly RelationalTypeMapping _datetime = new RelationalTypeMapping("datetime", DbType.DateTime);
        private readonly RelationalTypeMapping _uniqueidentifier = new RelationalTypeMapping("uniqueidentifier", DbType.Guid);
        private readonly RelationalTypeMapping _decimal = new RelationalTypeMapping("decimal(18, 2)");

        private readonly Dictionary<string, RelationalTypeMapping> _simpleNameMappings;
        private readonly Dictionary<Type, RelationalTypeMapping> _simpleMappings;

        public SqlCeTypeMapper()
        {
            _simpleNameMappings
                = new Dictionary<string, RelationalTypeMapping>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "nchar", _nchar },
                        { "national character", _nchar },
                        { "nvarchar", _nvarchar },
                        { "national char varying", _nvarchar },
                        { "national character varying", _nvarchar },
                        { "ntext", _nvarcharmax },
                        { "binary", _varbinary },
                        { "varbinary", _varbinary },
                        { "rowversion", _rowversion },
                        { "datetime", _datetime },
                        { "timestamp", _rowversion },
                        { "decimal", _decimal },
                        { "dec", _decimal },
                        { "numeric", _decimal }
                    };

            _simpleMappings
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
                        { typeof(sbyte), new RelationalTypeMapping("smallint") },
                        { typeof(ushort), new RelationalTypeMapping("int") },
                        { typeof(uint), new RelationalTypeMapping("bigint") },
                        { typeof(ulong), new RelationalTypeMapping("numeric(20, 0)") },
                        { typeof(short), _smallint },
                        { typeof(float), _real },
                        { typeof(decimal), _decimal }
                    };
        }

        protected override string GetColumnType(IProperty property) => property.SqlCe().ColumnType;

        protected override IReadOnlyDictionary<Type, RelationalTypeMapping> SimpleMappings
            => _simpleMappings;

        protected override IReadOnlyDictionary<string, RelationalTypeMapping> SimpleNameMappings
            => _simpleNameMappings;

        public override RelationalTypeMapping GetDefaultMapping(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));

            return clrType == typeof(string)
                ? _nvarchar
                : (clrType == typeof(byte[])
                    ? _varbinarymax
                    : base.GetDefaultMapping(clrType));
        }

        protected override RelationalTypeMapping GetCustomMapping(IProperty property)
        {
            Check.NotNull(property, nameof(property));

            var clrType = property.ClrType.UnwrapEnumType();

            return clrType == typeof(string)
                ? MapString(
                    property, 4000,
                    maxLength => new SqlCeMaxLengthMapping("nvarchar(" + maxLength + ")"),
                    _nvarcharmax, _nvarchar, _nvarchar256)
                : clrType == typeof(byte[])
                    ? MapByteArray(property, 8000,
                        maxLength => new SqlCeMaxLengthMapping("varbinary(" + maxLength + ")", DbType.Binary),
                        _varbinarymax, _varbinarymax, _varbinary512, _rowversion)
                    : base.GetCustomMapping(property);
        }
    }
}
