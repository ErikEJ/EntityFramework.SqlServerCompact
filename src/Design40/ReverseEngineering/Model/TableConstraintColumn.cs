using System.Data.SqlServerCe;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.SqlServerCompact.Design.ReverseEngineering.Model
{
    public class TableConstraintColumn
    {
        public const string Query =
            @"SELECT TOP(2147483648)
       '[' + tc.CONSTRAINT_NAME      + ']' + '[' + kcu.COLUMN_NAME + ']'    [Id]
       , '[' + tc.TABLE_NAME + ']' + '[' + kcu.COLUMN_NAME + ']'             [ColumnId]
       , '[' + tc.CONSTRAINT_NAME + ']' [ConstraintId]
       , tc.CONSTRAINT_TYPE [ConstraintType]
       , kcu.ORDINAL_POSITION [Ordinal]
       FROM
       INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
       INNER JOIN
       INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu
       ON tc.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME
       AND tc.TABLE_NAME = kcu.TABLE_NAME
       WHERE tc.TABLE_NAME IS NOT NULL";

        public virtual string Id { get;[param: CanBeNull] set; }
        public virtual string ColumnId { get;[param: CanBeNull] set; }
        public virtual string ConstraintId { get;[param: CanBeNull] set; }
        public virtual string ConstraintType { get;[param: CanBeNull] set; }
        public virtual int Ordinal { get;[param: CanBeNull] set; }

        public static TableConstraintColumn CreateFromReader([NotNull] SqlCeDataReader reader)
        {
            Check.NotNull(reader, nameof(reader));

            var tableConstraintColumn = new TableConstraintColumn();
            tableConstraintColumn.Id = reader.IsDBNull(0) ? null : reader.GetString(0);
            tableConstraintColumn.ColumnId = reader.IsDBNull(1) ? null : reader.GetString(1);
            tableConstraintColumn.ConstraintId = reader.IsDBNull(2) ? null : reader.GetString(2);
            tableConstraintColumn.ConstraintType = reader.IsDBNull(3) ? null : reader.GetString(3);
            tableConstraintColumn.Ordinal = reader.GetInt32(4);

            return tableConstraintColumn;
        }

        public override string ToString()
        {
            return "TCon[Id=" + Id
                   + ", ColumnId=" + ColumnId
                   + ", ConstraintId=" + ConstraintId
                   + ", ConstraintType=" + ConstraintType
                   + ", Ordinal=" + Ordinal
                   + "]";
        }
    }
}