using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Scaffolding.Metadata;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Scaffolding
{
    public class SqlCeDatabaseModelFactory : IDatabaseModelFactory
    {
        private SqlCeConnection _connection;
        private TableSelectionSet _tableSelectionSet;
        private DatabaseModel _databaseModel;
        private Dictionary<string, TableModel> _tables;
        private Dictionary<string, ColumnModel> _tableColumns;

        private static string TableKey(TableModel table) => TableKey(table.Name);
        private static string TableKey(string name) => "[" + name + "]";
        private static string ColumnKey(TableModel table, string columnName) => TableKey(table) + ".[" + columnName + "]";

        private void ResetState()
        {
            _connection = null;
            _tableSelectionSet = null;
            _databaseModel = new DatabaseModel();
            _tables = new Dictionary<string, TableModel>();
            _tableColumns = new Dictionary<string, ColumnModel>(StringComparer.OrdinalIgnoreCase);
        }

        public virtual DatabaseModel Create(string connectionString, TableSelectionSet tableSelectionSet)
        {
            Check.NotEmpty(connectionString, nameof(connectionString));
            Check.NotNull(tableSelectionSet, nameof(tableSelectionSet));

            ResetState();

            using (_connection = new SqlCeConnection(connectionString))
            {
                _connection.Open();
                _tableSelectionSet = tableSelectionSet;

                _databaseModel.DatabaseName = _connection.Database;

                GetTables();
                GetColumns();
                GetIndexes();
                GetForeignKeys();
                return _databaseModel;
            }
        }

        private void GetTables()
        {
            var command = _connection.CreateCommand();
            command.CommandText = @"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES
                                    WHERE TABLE_TYPE = 'TABLE' AND (SUBSTRING(TABLE_NAME, 1,2) <> '__')";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var table = new TableModel
                    {
                        SchemaName = null,
                        Name = reader.GetString(0)
                    };

                    if (_tableSelectionSet.Allows(table.Name))
                    {
                        _databaseModel.Tables.Add(table);
                        _tables[TableKey(table)] = table;
                    }
                }
            }
        }

        private void GetColumns()
        {
            var command = _connection.CreateCommand();
            command.CommandText = @"SELECT
		   NULL [schema]
       ,   c.TABLE_NAME  [table]
	   ,   c.DATA_TYPE [typename]
       ,   c.COLUMN_NAME [column_name]
       ,   CAST(c.ORDINAL_POSITION as integer) [ordinal]
       ,   CAST(CASE c.IS_NULLABLE WHEN 'YES' THEN 1 WHEN 'NO' THEN 0 ELSE 0 END as bit) [nullable]
	   ,   ix.ORDINAL_POSITION as [primary_key_ordinal]
	   ,   RTRIM(LTRIM(c.COLUMN_DEFAULT)) as [default_sql]
	   ,   c.NUMERIC_PRECISION [precision]
	   ,   c.NUMERIC_SCALE [scale]
       ,   c.CHARACTER_MAXIMUM_LENGTH [max_length]
       ,   CAST(CASE WHEN c.AUTOINC_INCREMENT IS NULL THEN 0 ELSE 1 END AS bit) [is_identity]
       ,   CAST(CASE WHEN c.DATA_TYPE = 'rowversion' THEN 1 ELSE 0 END AS bit) [is_computed]       
       FROM
       INFORMATION_SCHEMA.COLUMNS c
       INNER JOIN
       INFORMATION_SCHEMA.TABLES t ON
       c.TABLE_NAME = t.TABLE_NAME
       LEFT JOIN INFORMATION_SCHEMA.INDEXES ix
       ON c.TABLE_NAME = ix.TABLE_NAME AND c.COLUMN_NAME = ix.COLUMN_NAME AND ix.PRIMARY_KEY = 1
       WHERE SUBSTRING(c.COLUMN_NAME, 1,5) != '__sys'
       AND t.TABLE_TYPE = 'TABLE' 
       AND (SUBSTRING(t.TABLE_NAME, 1,2) <> '__');";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tableName = reader.GetString(1);
                    if (!_tableSelectionSet.Allows(tableName))
                    {
                        continue;
                    }

                    var dataTypeName = reader.GetString(2);
                    var nullable = reader.GetBoolean(5);

                    var maxLength = reader.IsDBNull(10) ? default(int?) : reader.GetInt32(10);

                    int? precision = null;
                    int? scale = null;

                    if (dataTypeName == "decimal"
                        || dataTypeName == "numeric")
                    {
                        precision = reader.IsDBNull(8) ? default(int?) : Convert.ToInt32(reader[8], System.Globalization.CultureInfo.InvariantCulture);
                        scale = reader.IsDBNull(9) ? default(int?) : Convert.ToInt32(reader[9], System.Globalization.CultureInfo.InvariantCulture);
                        // maxlength here represents storage bytes. The server determines this, not the client.
                        maxLength = null;
                    }

                    if (dataTypeName == "rowversion")
                    {
                        maxLength = null;
                    }

                    if (maxLength.HasValue && maxLength.Value > 8000)
                    {
                        maxLength = null;
                    }

                    var isIdentity = !reader.IsDBNull(11) && reader.GetBoolean(11);
                    var isComputed = reader.GetBoolean(12) || dataTypeName == "rowversion";

                    var table = _tables[TableKey(tableName)];
                    var columnName = reader.GetString(3);
                    var column = new ColumnModel
                    {
                        Table = table,
                        DataType = dataTypeName,
                        Name = columnName,
                        Ordinal = reader.GetInt32(4) - 1,
                        IsNullable = nullable,
                        PrimaryKeyOrdinal = reader.IsDBNull(6) ? default(int?) : reader.GetInt32(6),
                        DefaultValue = reader.IsDBNull(7) ? null : reader.GetString(7),
                        Precision = precision,
                        Scale = scale,
                        MaxLength = maxLength <= 0 ? default(int?) : maxLength,
                        IsIdentity = isIdentity,
                        ValueGenerated = isIdentity ?
                            ValueGenerated.OnAdd :
                            isComputed ?
                                ValueGenerated.OnAddOrUpdate : default(ValueGenerated?)
                    };

                    table.Columns.Add(column);
                    _tableColumns.Add(ColumnKey(table, column.Name), column);
                }
            }
        }

        private void GetIndexes()
        {
            //TODO ERIKEJ Filter out unique indexes that duplicate the PK index
            var command = _connection.CreateCommand();
            command.CommandText = @"SELECT  
    ix.[INDEX_NAME] AS [index_name],
    NULL AS [schema_name],
    ix.[TABLE_NAME] AS [table_name],
	ix.[UNIQUE] AS is_unique,
    ix.[COLUMN_NAME] AS [column_name]
    FROM INFORMATION_SCHEMA.INDEXES ix
    WHERE ix.PRIMARY_KEY = 0
    AND (SUBSTRING(TABLE_NAME, 1,2) <> '__')
    ORDER BY ix.[INDEX_NAME], ix.[ORDINAL_POSITION];";

            using (var reader = command.ExecuteReader())
            {
                IndexModel index = null;
                while (reader.Read())
                {
                    var indexName = reader.GetString(0);
                    var tableName = reader.GetString(2);

                    if (!_tableSelectionSet.Allows(tableName))
                    {
                        continue;
                    }

                    if (index == null
                        || index.Name != indexName)
                    {
                        TableModel table;
                        if (!_tables.TryGetValue(TableKey(tableName), out table))
                        {
                            continue;
                        }

                        index = new IndexModel
                        {
                            Table = table,
                            Name = indexName,
                            IsUnique = reader.GetBoolean(3),
                            IsClustered = null
                        };
                        table.Indexes.Add(index);
                    }
                    var columnName = reader.GetString(4);
                    var column = _tableColumns[ColumnKey(index.Table, columnName)];
                    index.Columns.Add(column);
                }
            }
        }

        private void GetForeignKeys()
        {
            var command = _connection.CreateCommand();
            command.CommandText = @"SELECT 
                KCU1.TABLE_NAME + '_' + KCU1.CONSTRAINT_NAME AS FK_CONSTRAINT_NAME,
                NULL AS [SCHEMA_NAME],
                KCU1.TABLE_NAME AS FK_TABLE_NAME,  
                NULL AS [UQ_SCHEMA_NAME],
                KCU2.TABLE_NAME AS UQ_TABLE_NAME, 
                KCU1.COLUMN_NAME AS FK_COLUMN_NAME, 
                KCU2.COLUMN_NAME AS UQ_COLUMN_NAME, 
                0 AS [IS_DISABLED],
                RC.DELETE_RULE, 
                RC.UPDATE_RULE
                FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC 
                JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU1 ON KCU1.CONSTRAINT_NAME = RC.CONSTRAINT_NAME 
                AND KCU1.TABLE_NAME = RC.CONSTRAINT_TABLE_NAME 
                JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU2 ON  KCU2.CONSTRAINT_NAME =  RC.UNIQUE_CONSTRAINT_NAME 
                    AND KCU2.ORDINAL_POSITION = KCU1.ORDINAL_POSITION 
                    AND KCU2.TABLE_NAME = RC.UNIQUE_CONSTRAINT_TABLE_NAME 
                ORDER BY FK_TABLE_NAME, FK_CONSTRAINT_NAME, KCU1.ORDINAL_POSITION;";
            using (var reader = command.ExecuteReader())
            {
                var lastFkName = "";
                ForeignKeyModel fkInfo = null;
                while (reader.Read())
                {
                    var fkName = reader.GetString(0);
                    var tableName = reader.GetString(2);

                    if (!_tableSelectionSet.Allows(tableName))
                    {
                        continue;
                    }
                    if (fkInfo == null
                        || lastFkName != fkName)
                    {
                        lastFkName = fkName;
                        var principalTableName = reader.GetString(4);
                        var table = _tables[TableKey(tableName)];
                        TableModel principalTable;
                        _tables.TryGetValue(TableKey(principalTableName), out principalTable);

                        fkInfo = new ForeignKeyModel
                        {
                            Table = table,
                            PrincipalTable = principalTable
                        };

                        table.ForeignKeys.Add(fkInfo);
                    }
                    var fromColumnName = reader.GetString(5);
                    var fromColumn = _tableColumns[ColumnKey(fkInfo.Table, fromColumnName)];
                    fkInfo.Columns.Add(fromColumn);

                    if (fkInfo.PrincipalTable != null)
                    {
                        var toColumnName = reader.GetString(6);
                        var toColumn = _tableColumns[ColumnKey(fkInfo.PrincipalTable, toColumnName)];
                        fkInfo.PrincipalColumns.Add(toColumn);
                    }

                    fkInfo.OnDelete = ConvertToReferentialAction(reader.GetString(8));
                }
            }
        }

        private static ReferentialAction? ConvertToReferentialAction(string onDeleteAction)
        {
            switch (onDeleteAction.ToUpperInvariant())
            {
                case "CASCADE":
                    return ReferentialAction.Cascade;

                case "NO ACTION":
                    return ReferentialAction.NoAction;

                default:
                    return null;
            }
        }
    }
}
