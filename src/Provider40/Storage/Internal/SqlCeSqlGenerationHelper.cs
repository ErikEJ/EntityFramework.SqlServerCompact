using System;
using System.Globalization;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Storage.Internal
{
    public class SqlCeSqlGenerationHelper : RelationalSqlGenerationHelper
    {
        private const string DateTimeFormatConst = "yyyy-MM-ddTHH:mm:ss.fff";
        private const string DateTimeFormatStringConst = "'{0:" + DateTimeFormatConst + "}'";
        private const string DateTimeOffsetFormatConst = "yyyy-MM-ddTHH:mm:ss.fff";
        private const string DateTimeOffsetFormatStringConst = "'{0:" + DateTimeOffsetFormatConst + "}'";

        public SqlCeSqlGenerationHelper([NotNull] RelationalSqlGenerationHelperDependencies dependencies)
            : base(dependencies)
        {
        }

        public override string BatchTerminator => "GO" + Environment.NewLine + Environment.NewLine;

        public override string StatementTerminator => Environment.NewLine;

        protected override string DateTimeFormat => DateTimeFormatConst;
        protected override string DateTimeFormatString => DateTimeFormatStringConst;
        protected override string DateTimeOffsetFormat => DateTimeOffsetFormatConst;
        protected override string DateTimeOffsetFormatString => DateTimeOffsetFormatStringConst;


        public override string EscapeIdentifier(string identifier)
            => Check.NotEmpty(identifier, nameof(identifier)).Replace("]", "]]");

        public override string DelimitIdentifier(string identifier)
            => $"[{EscapeIdentifier(Check.NotEmpty(identifier, nameof(identifier)))}]";

        public override void DelimitIdentifier(StringBuilder builder, string identifier)
        {
            Check.NotEmpty(identifier, nameof(identifier));

            builder.Append('[');
            EscapeIdentifier(builder, identifier);
            builder.Append(']');
        }

        protected override void GenerateLiteralValue(StringBuilder builder, byte[] value)
        {
            Check.NotNull(value, nameof(value));

            builder.Append("0x");

            foreach (var @byte in value)
            {
                builder.Append(@byte.ToString("X2", CultureInfo.InvariantCulture));
            }
        }

        protected override string GenerateLiteralValue(string value, RelationalTypeMapping typeMapping)
            => $"N'{EscapeLiteral(Check.NotNull(value, nameof(value)))}'";

        protected override void GenerateLiteralValue(StringBuilder builder, string value, RelationalTypeMapping typeMapping)
        { 
            builder.Append("N'"); 
            EscapeLiteral(builder, value); 
            builder.Append("'"); 
        }

        protected override string GenerateLiteralValue(DateTime value)
            => $"'{value.ToString(DateTimeFormat, CultureInfo.InvariantCulture)}'";

        protected override string GenerateLiteralValue(DateTimeOffset value)
            => $"'{value.ToString(DateTimeOffsetFormat, CultureInfo.InvariantCulture)}'";
    }
}
