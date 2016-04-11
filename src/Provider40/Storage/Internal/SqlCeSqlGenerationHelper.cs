using System;
using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Storage.Internal
{
    public class SqlCeSqlGenerationHelper : RelationalSqlGenerationHelper
    {
        public override string BatchTerminator => "GO" + Environment.NewLine + Environment.NewLine;

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

        protected override string GenerateLiteralValue(string value, bool unicode = true)
            => $"N'{EscapeLiteral(Check.NotNull(value, nameof(value)))}'";

        protected override void GenerateLiteralValue(StringBuilder builder, string value, bool unicode = true)
        { 
            builder.Append("N'"); 
            EscapeLiteral(builder, value); 
            builder.Append("'"); 
        }

        protected override string GenerateLiteralValue(DateTime value) => "'" + value.ToString(@"yyyy-MM-dd HH\:mm\:ss.fff") + "'";

        protected override string GenerateLiteralValue(DateTimeOffset value) => "'" + value.ToString(@"yyyy-MM-dd HH\:mm\:ss.fff") + "'";
    }
}
