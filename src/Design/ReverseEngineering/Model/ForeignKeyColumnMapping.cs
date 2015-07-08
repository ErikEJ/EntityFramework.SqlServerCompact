using System.Data.SqlClient;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.SqlServer.Design.ReverseEngineering.Model
{
    public class ForeignKeyColumnMapping
    {
        public const string Query =
            @"SELECT TOP(2147483648)
           '[' + FC.TABLE_NAME + ']' + '[' + FC.CONSTRAINT_NAME + ']' + '[' + cast(FC.ORDINAL_POSITION as nvarchar(30)) + ']'   [Id]
       ,   '[' + PC.TABLE_NAME      + ']' + '[' + PC.COLUMN_NAME + ']'                                                          [ToColumnId]
       ,   '[' + FC.TABLE_NAME      + ']' + '[' + FC.COLUMN_NAME + ']'                                                          [FromColumnId]
       ,   '[' + FC.TABLE_NAME + ']' + '[' + FC.CONSTRAINT_NAME + ']'                                                           [ConstraintId]
       ,   FC.ORDINAL_POSITION [Ordinal]
       FROM
       INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS RC
       INNER JOIN
       INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS PC /* PRIMARY KEY COLS*/
       ON        RC.UNIQUE_CONSTRAINT_NAME    = PC.CONSTRAINT_NAME
       AND       RC.UNIQUE_CONSTRAINT_TABLE_NAME = PC.TABLE_NAME
       INNER JOIN
       INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS FC /* FOREIGN KEY COLS*/
       ON        RC.CONSTRAINT_NAME    = FC.CONSTRAINT_NAME
       AND      RC.CONSTRAINT_TABLE_NAME = FC.TABLE_NAME
       AND      PC.ORDINAL_POSITION = FC.ORDINAL_POSITION";

        public virtual string Id { get;[param: CanBeNull] set; }
        public virtual string ConstraintId { get;[param: CanBeNull] set; }
        public virtual string FromColumnId { get;[param: CanBeNull] set; }
        public virtual string ToColumnId { get;[param: CanBeNull] set; }

        public static ForeignKeyColumnMapping CreateFromReader([NotNull] SqlDataReader reader)
        {
            Check.NotNull(reader, nameof(reader));

            var tableColumn = new ForeignKeyColumnMapping();
            tableColumn.Id = reader.IsDBNull(0) ? null : reader.GetString(0);
            tableColumn.ConstraintId = reader.IsDBNull(1) ? null : reader.GetString(1);
            tableColumn.FromColumnId = reader.IsDBNull(2) ? null : reader.GetString(2);
            tableColumn.ToColumnId = reader.IsDBNull(3) ? null : reader.GetString(3);

            return tableColumn;
        }

        public override string ToString()
        {
            return "FKCM[Id=" + Id
                   + ", ConstraintId=" + ConstraintId
                   + ", FromColumnId=" + FromColumnId
                   + ", ToColumnId=" + ToColumnId
                   + "]";
        }
    }
}