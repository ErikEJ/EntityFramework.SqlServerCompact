using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Update;
using Microsoft.Data.Entity.Utilities;

namespace ErikEJ.Data.Entity.SqlServerCe
{
    public class SqlServerCeSqlGenerator : SqlGenerator, ISqlServerCeSqlGenerator
    {
        protected override void AppendIdentityWhereCondition(StringBuilder builder, ColumnModification columnModification)
        {
            Check.NotNull(builder, nameof(builder));
            Check.NotNull(columnModification, nameof(columnModification));
            //TODO
            builder
                .Append(DelimitIdentifier(columnModification.ColumnName))
                .Append(" = ")
                .Append("last_insert_rowid()");
        }

        public override void AppendSelectAffectedCountCommand(StringBuilder builder, string tableName, string schemaName)
        {
            Check.NotNull(builder, nameof(builder));
            Check.NotEmpty(tableName, nameof(tableName));
            //TODO
            builder
                .Append("SELECT changes()")
                .Append(BatchCommandSeparator)
                .AppendLine();
        }

        protected override void AppendRowsAffectedWhereCondition(StringBuilder builder, int expectedRowsAffected)
        {
            //TODO
            Check.NotNull(builder, nameof(builder));

            builder.Append("changes() = " + expectedRowsAffected);
        }

        public override string BatchSeparator => "GO";

        public override string DelimitIdentifier(string identifier)
            => "[" + EscapeIdentifier(Check.NotEmpty(identifier, nameof(identifier))) + "]";

        public override string EscapeIdentifier(string identifier)
            => Check.NotEmpty(identifier, nameof(identifier)).Replace("]", "]]");

        public override string GenerateLiteral(byte[] literal)
        {
            Check.NotNull(literal, nameof(literal));

            var builder = new StringBuilder();

            builder.Append("0x");

            var parts = literal.Select(b => b.ToString("X2", CultureInfo.InvariantCulture));
            foreach (var part in parts)
            {
                builder.Append(part);
            }

            return builder.ToString();
        }

        public override string GenerateLiteral(bool literal) => literal ? "1" : "0";
        public override string GenerateLiteral(DateTime literal) => "'" + literal.ToString(@"yyyy-MM-dd HH\:mm\:ss.fffffff") + "'";
        public virtual string GenerateLiteral(Guid literal) => "'" + literal + "'";
    }
}
