using System.Data.SqlServerCe;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.SqlServerCompact.Design.ReverseEngineering.Model
{
    public class Table
    {
        public const string Query =
            @"SELECT TOP(2147483648)
           '[' + TABLE_NAME + ']'            [Id]
         , '[' + TABLE_SCHEMA  + ']'         [SchemaName]        
         ,   TABLE_NAME                      [Name]
       FROM
             INFORMATION_SCHEMA.TABLES
       WHERE
             TABLE_TYPE = 'TABLE'";

        public virtual string Id { get;[param: CanBeNull] set; }
        public virtual string SchemaName { get;[param: CanBeNull] set; }
        public virtual string TableName { get;[param: CanBeNull] set; }

        public static Table CreateFromReader([NotNull] SqlCeDataReader reader)
        {
            Check.NotNull(reader, nameof(reader));

            var table = new Table();
            table.Id = reader.IsDBNull(0) ? null : reader.GetString(0);
            table.SchemaName = reader.IsDBNull(1) ? null : reader.GetString(1);
            table.TableName = reader.IsDBNull(2) ? null : reader.GetString(2);

            return table;
        }

        public override string ToString()
        {
            return "T[Id=" + Id
                   + ", Schema=" + SchemaName
                   + ", Name=" + TableName
                   + "]";
        }
    }
}