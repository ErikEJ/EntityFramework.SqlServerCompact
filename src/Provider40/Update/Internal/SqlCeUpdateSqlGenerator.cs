using System;
using System.Text;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Update.Internal
{
    public class SqlCeUpdateSqlGenerator : UpdateSqlGenerator, ISqlCeUpdateSqlGenerator
    {
        public SqlCeUpdateSqlGenerator([NotNull] ISqlGenerator sqlGenerator)
            : base(sqlGenerator)
        {
        }

        protected override void AppendIdentityWhereCondition(StringBuilder builder, ColumnModification columnModification)
        {
            Check.NotNull(builder, nameof(builder));
            Check.NotNull(columnModification, nameof(columnModification));

            var castAs = columnModification.Property.ClrType == typeof(int)
                ? "int"
                : "bigint";

            builder
                .Append(SqlGenerator.DelimitIdentifier(columnModification.ColumnName))
                .Append(" = ")
                .Append("CAST (@@IDENTITY AS ")
                .Append(castAs)
                .Append(")");
        }

        protected override void AppendRowsAffectedWhereCondition(StringBuilder builder, int expectedRowsAffected)
        {
            Check.NotNull(builder, nameof(builder));

            builder
                .Append("1 = 1");
        }

        public override string GenerateNextSequenceValueOperation(string name, string schema)
        {
            throw new NotSupportedException();
        }
    }
}
