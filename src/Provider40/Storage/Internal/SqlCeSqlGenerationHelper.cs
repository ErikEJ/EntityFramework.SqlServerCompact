using System;
using System.Globalization;
using System.Text;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Storage.Internal
{
    public class SqlCeSqlGenerationHelper : RelationalSqlGenerationHelper
    {
        public override string BatchTerminator => "GO" + Environment.NewLine + Environment.NewLine;

        public override string EscapeIdentifier(string identifier)
            => Check.NotEmpty(identifier, nameof(identifier)).Replace("]", "]]");

        public override string DelimitIdentifier(string identifier)
            => $"[{EscapeIdentifier(Check.NotEmpty(identifier, nameof(identifier)))}]";

        protected override string GenerateLiteralValue(byte[] value)
        {
            Check.NotNull(value, nameof(value));

            var stringBuilder = new StringBuilder("0x");

            foreach (var @byte in value)
            {
                stringBuilder.Append(@byte.ToString("X2", CultureInfo.InvariantCulture));
            }

            return stringBuilder.ToString();
        }

        protected override string GenerateLiteralValue(DateTime value) => "'" + value.ToString(@"yyyy-MM-dd HH\:mm\:ss.fff") + "'";

        protected override string GenerateLiteralValue(DateTimeOffset value) => "'" + value.ToString(@"yyyy-MM-dd HH\:mm\:ss.fff") + "'";
    }
}
