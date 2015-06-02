using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Utilities;

namespace ErikEJ.Data.Entity.SqlServerCe
{
    public class SqlCeTypeMapper : RelationalTypeMapper
    {
        private readonly RelationalTypeMapping _nvarcharmax = new RelationalTypeMapping("ntext");
        private readonly RelationalTypeMapping _nvarchardefault = new RelationalTypeMapping("nvarchar(4000)");
        private readonly RelationalTypeMapping _nvarchar256 = new RelationalSizedTypeMapping("nvarchar(256)", 256);
        private readonly RelationalTypeMapping _varbinarymax = new RelationalTypeMapping("image", DbType.Binary);
        private readonly RelationalTypeMapping _varbinary512 = new RelationalSizedTypeMapping("varbinary(512)", DbType.Binary, 512);
        private readonly RelationalTypeMapping _rowversion = new RelationalSizedTypeMapping("rowversion", DbType.Binary, 8);
        private readonly RelationalTypeMapping _int = new RelationalTypeMapping("int", DbType.Int32);
        private readonly RelationalTypeMapping _bigint = new RelationalTypeMapping("bigint", DbType.Int64);
        private readonly RelationalTypeMapping _bit = new RelationalTypeMapping("bit");
        private readonly RelationalTypeMapping _smallint = new RelationalTypeMapping("smallint", DbType.Int16);
        private readonly RelationalTypeMapping _tinyint = new RelationalTypeMapping("tinyint", DbType.Byte);
        private readonly RelationalSizedTypeMapping _nchar = new RelationalSizedTypeMapping("nchar", DbType.StringFixedLength, 1);
        private readonly RelationalSizedTypeMapping _nvarchar = new RelationalSizedTypeMapping("nvarchar", 4000);
        private readonly RelationalSizedTypeMapping _varbinary = new RelationalSizedTypeMapping("binary", DbType.Binary, 1);
        private readonly RelationalTypeMapping _double = new RelationalTypeMapping("float");
        private readonly RelationalTypeMapping _real = new RelationalTypeMapping("real");
        private readonly RelationalTypeMapping _datetime = new RelationalTypeMapping("datetime");
        private readonly RelationalTypeMapping _uniqueidentifier = new RelationalTypeMapping("uniqueidentifier");
        private readonly RelationalScaledTypeMapping _decimal = new RelationalScaledTypeMapping("decimal(18, 2)", 18, 2);

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
                        { "ntext", _nvarchar },
                        { "binary", _varbinary },
                        { "varbinary", _varbinary },
                        { "binary varying", _varbinary },
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

        protected override IReadOnlyDictionary<Type, RelationalTypeMapping> SimpleMappings
            => _simpleMappings;

        protected override IReadOnlyDictionary<string, RelationalTypeMapping> SimpleNameMappings
            => _simpleNameMappings;

        protected override RelationalTypeMapping TryMapFromName(
            string typeName,
            string typeNamePrefix,
            int? firstQualifier,
            int? secondQualifier)
        {
            return TryMapSized(typeName, typeNamePrefix, new[] { "nvarchar", "national char varying", "national character varying" }, firstQualifier)
                   ?? TryMapSized(typeName, typeNamePrefix, new[] { "varbinary", "binary varying" }, firstQualifier, DbType.Binary)
                   ?? TryMapScaled(typeName, typeNamePrefix, new[] { "decimal", "numeric", "dec" }, firstQualifier, secondQualifier)
                   ?? TryMapScaled(typeName, typeNamePrefix, new[] { "float", "double precision" }, firstQualifier, secondQualifier)
                   ?? TryMapScaled(typeName, typeNamePrefix, new[] { "datetime" }, firstQualifier, secondQualifier, DbType.DateTime)
                   ?? TryMapSized(typeName, typeNamePrefix, new[] { "nchar", "national char", "national character" }, firstQualifier, DbType.AnsiStringFixedLength)
                   ?? TryMapSized(typeName, typeNamePrefix, new[] { "binary" }, firstQualifier, DbType.Binary)
                   ?? base.TryMapFromName(typeName, typeNamePrefix, firstQualifier, secondQualifier);
        }

        protected override RelationalTypeMapping MapCustom(IProperty property)
        {
            Check.NotNull(property, nameof(property));

            var clrType = property.ClrType.UnwrapEnumType();

            //if (!property.GetMaxLength().HasValue && clrType == typeof(string))
            //{
            //    return MapString(property, "ntext", _nvarcharmax);
            //}
            //else
            //{
                return clrType == typeof(string)
                    ? MapString(property, "nvarchar", _nvarchardefault, _nvarchar256)
                    : clrType == typeof(byte[])
                        ? MapByteArray(property, "varbinary", _varbinarymax, _varbinary512, _rowversion)
                        : base.MapCustom(property);
            //}
        }
    }
}
