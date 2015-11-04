﻿using System;
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
            
       //     @"SELECT TOP(2147483648)
       //    '[' + c.TABLE_NAME + ']' + '[' + c.COLUMN_NAME + ']' [Id]
       //,   '[' + c.TABLE_NAME + ']' [ParentId]
       //,   c.COLUMN_NAME [Name]
       //,   CAST(c.ORDINAL_POSITION as integer) [Ordinal]
       //,   CAST(CASE c.IS_NULLABLE WHEN 'YES' THEN 1 WHEN 'NO' THEN 0 ELSE 0 END as bit) [IsNullable]
       //,   c.DATA_TYPE [DataType]
       //,   c.CHARACTER_MAXIMUM_LENGTH [MaxLength]
       //,   c.NUMERIC_PRECISION [NumericPrecision]
       //,   CAST(c.DATETIME_PRECISION as integer) [DateTimePrecision]
       //,   c.NUMERIC_SCALE [Scale]
       //,   CAST(CASE WHEN c.AUTOINC_INCREMENT IS NULL THEN 0 ELSE 1 END AS bit) [IsIdentity]
       //,   CAST(CASE WHEN c.DATA_TYPE = 'rowversion' THEN 1 ELSE 0 END AS bit) [IsStoreGenerated]
       //,   RTRIM(LTRIM(c.COLUMN_DEFAULT)) as [Default]
       // FROM
       //INFORMATION_SCHEMA.COLUMNS c
       //INNER JOIN
       //INFORMATION_SCHEMA.TABLES t ON
       //c.TABLE_NAME = t.TABLE_NAME       AND
       //t.TABLE_TYPE = 'TABLE'
       //WHERE SUBSTRING(c.COLUMN_NAME, 1,5) != '__sys'";

        var command = _connection.CreateCommand();
            command.CommandText = @"SELECT DISTINCT 
    schema_name(t.schema_id) AS [schema], 
    t.name AS [table], 
    type_name(c.user_type_id) AS [typename],
    c.name AS [column_name], 
    c.column_id AS [ordinal],
    c.is_nullable AS [nullable],
    CAST(ic.key_ordinal AS int) AS [primary_key_ordinal],
	object_definition(c.default_object_id) AS [default_sql],
    CAST(CASE WHEN c.precision <> tp.precision
			THEN c.precision
			ELSE null
		END AS int) AS [precision],
	CAST(CASE WHEN c.scale <> tp.scale
			THEN c.scale
			ELSE null
		END AS int) AS [scale],
    CAST(CASE WHEN c.max_length <> tp.max_length
			THEN c.max_length
			ELSE null
		END AS int) AS [max_length],
    c.is_identity,
    c.is_computed
FROM sys.index_columns ic
	RIGHT JOIN (SELECT * FROM sys.indexes WHERE is_primary_key = 1) AS i ON i.object_id = ic.object_id AND i.index_id = ic.index_id
	RIGHT JOIN sys.columns c ON ic.object_id = c.object_id AND c.column_id = ic.column_id
	RIGHT JOIN sys.types tp ON tp.user_type_id = c.user_type_id
JOIN sys.tables AS t ON t.object_id = c.object_id
WHERE t.name <> '" + HistoryRepository.DefaultTableName + "'";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var schemaName = reader.GetString(0);
                    var tableName = reader.GetString(1);
                    if (!_tableSelectionSet.Allows(tableName))
                    {
                        continue;
                    }

                    var dataTypeName = reader.GetString(2);
                    var nullable = reader.GetBoolean(5);

                    var maxLength = reader.IsDBNull(10) ? default(int?) : reader.GetInt32(10);

                    if (dataTypeName == "nvarchar"
                        || dataTypeName == "nchar")
                    {
                        maxLength /= 2;
                    }

                    if (dataTypeName == "decimal"
                        || dataTypeName == "numeric")
                    {
                        // maxlength here represents storage bytes. The server determines this, not the client.
                        maxLength = null;
                    }

                    var isIdentity = !reader.IsDBNull(11) && reader.GetBoolean(11);
                    var isComputed = reader.GetBoolean(12) || dataTypeName == "timestamp";

                    var table = _tables[TableKey(tableName)];
                    var column = new ColumnModel
                    {
                        Table = table,
                        DataType = dataTypeName,
                        Name = reader.GetString(3),
                        Ordinal = reader.GetInt32(4) - 1,
                        IsNullable = nullable,
                        PrimaryKeyOrdinal = reader.IsDBNull(6) ? default(int?) : reader.GetInt32(6),
                        DefaultValue = reader.IsDBNull(7) ? null : reader.GetString(7),
                        Precision = reader.IsDBNull(8) ? default(int?) : reader.GetInt32(8),
                        Scale = reader.IsDBNull(9) ? default(int?) : reader.GetInt32(9),
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

            //     @"SELECT TOP(2147483648)
            //'[' + tc.TABLE_NAME + ']' + '[' + tc.CONSTRAINT_NAME      + ']' + '[' + kcu.COLUMN_NAME + ']'    [Id]
            //, '[' + tc.TABLE_NAME + ']' + '[' + kcu.COLUMN_NAME + ']'             [ColumnId]
            //, '[' + tc.CONSTRAINT_NAME + ']' [ConstraintId]
            //, tc.CONSTRAINT_TYPE [ConstraintType]
            //, kcu.ORDINAL_POSITION [Ordinal]
            //FROM
            //INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
            //INNER JOIN
            //INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu
            //ON tc.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME
            //AND tc.TABLE_NAME = kcu.TABLE_NAME
            //WHERE tc.TABLE_NAME IS NOT NULL";

            var command = _connection.CreateCommand();
            command.CommandText = @"SELECT 
    i.name AS [index_name],
    object_schema_name(i.object_id) AS [schema_name],
    object_name(i.object_id) AS [table_name],
	i.is_unique,
    c.name AS [column_name],
    i.type_desc
FROM sys.indexes i
    inner join sys.index_columns ic  ON i.object_id = ic.object_id AND i.index_id = ic.index_id
    inner join sys.columns c ON ic.object_id = c.object_id AND c.column_id = ic.column_id
WHERE   object_schema_name(i.object_id) <> 'sys' 
    AND i.is_primary_key <> 1
    AND object_name(i.object_id) <> '" + HistoryRepository.DefaultTableName + @"'
ORDER BY i.name, ic.key_ordinal";

            using (var reader = command.ExecuteReader())
            {
                IndexModel index = null;
                while (reader.Read())
                {
                    var indexName = reader.GetString(0);
                    var schemaName = reader.GetString(1);
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
                            IsClustered = (reader.GetString(5) == "CLUSTERED") ? true : default(bool?)
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
            //     @"SELECT TOP(2147483648)
            //    '[' + FC.CONSTRAINT_NAME + ']' + '[' + FC.TABLE_NAME + ']' + '[' + FC.COLUMN_NAME + ']'  [Id]
            //,   '[' + FC.TABLE_NAME + ']' + '[' + FC.CONSTRAINT_NAME + ']'                                                           [ConstraintId]
            //,   '[' + FC.TABLE_NAME      + ']' + '[' + FC.COLUMN_NAME + ']'                                                          [FromColumnId]
            //,   '[' + PC.TABLE_NAME      + ']' + '[' + PC.COLUMN_NAME + ']'                                                          [ToColumnId]
            //FROM
            //INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS RC
            //INNER JOIN
            //INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS PC /* PRIMARY KEY COLS*/
            //ON        RC.UNIQUE_CONSTRAINT_NAME    = PC.CONSTRAINT_NAME
            //AND       RC.UNIQUE_CONSTRAINT_TABLE_NAME = PC.TABLE_NAME
            //INNER JOIN
            //INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS FC /* FOREIGN KEY COLS*/
            //ON        RC.CONSTRAINT_NAME    = FC.CONSTRAINT_NAME
            //AND      RC.CONSTRAINT_TABLE_NAME = FC.TABLE_NAME
            //AND      PC.ORDINAL_POSITION = FC.ORDINAL_POSITION";



            var command = _connection.CreateCommand();
            command.CommandText = @"SELECT 
    f.name AS foreign_key_name,
    schema_name(f.schema_id) AS [schema_name],
    object_name(f.parent_object_id) AS table_name,
    object_schema_name(f.referenced_object_id) AS principal_table_schema_name,
    object_name(f.referenced_object_id) AS principal_table_name,
    col_name(fc.parent_object_id, fc.parent_column_id) AS constraint_column_name,
    col_name(fc.referenced_object_id, fc.referenced_column_id) AS referenced_column_name,
    is_disabled,
    delete_referential_action_desc,
    update_referential_action_desc
FROM sys.foreign_keys AS f
INNER JOIN sys.foreign_key_columns AS fc 
   ON f.object_id = fc.constraint_object_id
ORDER BY f.name, fc.constraint_column_id";
            using (var reader = command.ExecuteReader())
            {
                var lastFkName = "";
                ForeignKeyModel fkInfo = null;
                while (reader.Read())
                {
                    var fkName = reader.GetString(0);
                    var schemaName = reader.GetString(1);
                    var tableName = reader.GetString(2);

                    if (!_tableSelectionSet.Allows(tableName))
                    {
                        continue;
                    }
                    if (fkInfo == null
                        || lastFkName != fkName)
                    {
                        lastFkName = fkName;
                        var principalSchemaTableName = reader.GetString(3);
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
                case "RESTRICT":
                    return ReferentialAction.Restrict;

                case "CASCADE":
                    return ReferentialAction.Cascade;

                case "SET_NULL":
                    return ReferentialAction.SetNull;

                case "SET_DEFAULT":
                    return ReferentialAction.SetDefault;

                case "NO_ACTION":
                    return ReferentialAction.NoAction;

                default:
                    return null;
            }
        }
    }
}
