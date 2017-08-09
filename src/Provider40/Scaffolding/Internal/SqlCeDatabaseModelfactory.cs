using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Microsoft.EntityFrameworkCore.Scaffolding.Internal
{
    public class SqlCeDatabaseModelFactory : IDatabaseModelFactory
    {
        private SqlCeConnection _connection;
        private TableSelectionSet _tableSelectionSet;
        private DatabaseModel _databaseModel;
        private Dictionary<string, DatabaseTable> _tables;
        private Dictionary<string, DatabaseColumn> _tableColumns;

        private static string TableKey(DatabaseTable table) => TableKey(table.Name);
        private static string TableKey(string name) => "[" + name + "]";
        private static string ColumnKey(DatabaseTable table, string columnName) => TableKey(table) + ".[" + columnName + "]";

        public SqlCeDatabaseModelFactory([NotNull] IDiagnosticsLogger<DbLoggerCategory.Scaffolding> logger)
        {
            Check.NotNull(logger, nameof(logger));

            Logger = logger;
        }

        public virtual IDiagnosticsLogger<DbLoggerCategory.Scaffolding> Logger { get; }

        private void ResetState()
        {
            _connection = null;
            _tableSelectionSet = null;
            _databaseModel = new DatabaseModel();
            _tables = new Dictionary<string, DatabaseTable>();
            _tableColumns = new Dictionary<string, DatabaseColumn>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual DatabaseModel Create(string connectionString, IEnumerable<string> tables, IEnumerable<string> schemas)
        {
            Check.NotEmpty(connectionString, nameof(connectionString));
            Check.NotNull(tables, nameof(tables));

            using (var connection = new SqlCeConnection(connectionString))
            {
                return Create(connection, tables, schemas);
            }
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual DatabaseModel Create(DbConnection connection, IEnumerable<string> tables, IEnumerable<string> schemas)
        {
            Check.NotNull(connection, nameof(connection));
            Check.NotNull(tables, nameof(tables));

            ResetState();

            _connection = connection as SqlCeConnection;

            var connectionStartedOpen = (_connection != null) && (_connection.State == ConnectionState.Open);
            if (!connectionStartedOpen)
            {
                _connection?.Open();
            }
            try
            {
                _tableSelectionSet = new TableSelectionSet(tables, schemas);

                string databaseName = null;
                try
                {
                    if (_connection != null)
                        databaseName = Path.GetFileNameWithoutExtension(_connection.DataSource);
                }
                catch (ArgumentException)
                {
                    // graceful fallback
                }

                if (_connection != null)
                    _databaseModel.DatabaseName = !string.IsNullOrEmpty(databaseName) ? databaseName : _connection.DataSource;

                GetTables();
                GetColumns();
                GetPrimaryKeys();
                GetUniqueConstraints();
                GetIndexes();
                GetForeignKeys();

                CheckSelectionsMatched(_tableSelectionSet);

                return _databaseModel;
            }
            finally
            {
                if (!connectionStartedOpen)
                {
                    _connection?.Close();
                }
            }
        }

        private void CheckSelectionsMatched(TableSelectionSet tableSelectionSet)
        {
            foreach (var tableSelection in tableSelectionSet.Tables.Where(t => !t.IsMatched))
            {
                //TODO ErikEJ Log!
                //Logger.MissingTableWarning(tableSelection.Text);
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
                    var table = new DatabaseTable
                    {
                        Schema = null,
                        Name = reader.GetValueOrDefault<string>("TABLE_NAME")
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
       AND (SUBSTRING(t.TABLE_NAME, 1,2) <> '__')
       ORDER BY c.TABLE_NAME, c.ORDINAL_POSITION;";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tableName = reader.GetValueOrDefault<string>("table");
                    var dataTypeName = reader.GetValueOrDefault<string>("typename");
                    var nullable = reader.GetValueOrDefault<bool>("nullable");
                    var precision = reader.IsDBNull(8) ? default(int?) : Convert.ToInt32(reader[8], System.Globalization.CultureInfo.InvariantCulture);
                    var scale = reader.IsDBNull(9) ? default(int?) : Convert.ToInt32(reader[9], System.Globalization.CultureInfo.InvariantCulture);

                    //var precision = reader.GetValueOrDefault<long?>("precision");
                    //var scale = reader.GetValueOrDefault<long?>("scale");
                    var maxLength = reader.GetValueOrDefault<int?>("max_length");
                    var columnName = reader.GetValueOrDefault<string>("column_name");
                    var defaultValue = reader.GetValueOrDefault<string>("default_sql");
                    var isIdentity = reader.GetValueOrDefault<bool>("is_identity");

                    //Logger.ColumnFound(
                    //                        DisplayName(schemaName, tableName), columnName, DisplayName(dataTypeSchemaName, dataTypeName), ordinal, nullable,
                    //                        primaryKeyOrdinal, defaultValue, computedValue, precision, scale, maxLength, isIdentity, isComputed);

                    if (!_tableSelectionSet.Allows(tableName))
                    {
                        //Logger.ColumnSkipped(DisplayName(schemaName, tableName), columnName);
                        continue;
                    }

                    if (string.IsNullOrEmpty(columnName))
                    {
                        //Logger.ColumnNotNamedWarning(DisplayName(schemaName, tableName));
                        continue;
                    }

                    if (!_tables.TryGetValue(TableKey(tableName), out var table))
                    {
                        //Logger.MissingTableWarning(DisplayName(schemaName, tableName));
                        continue;
                    }

                    var storeType = GetStoreType(dataTypeName, precision, scale, maxLength);

                    if (defaultValue == "(NULL)")
                    {
                        defaultValue = null;
                    }
                    
                    var isComputed = reader.GetValueOrDefault<bool>("is_computed") || (dataTypeName == "rowversion");

                    var column = new DatabaseColumn
                    {
                        Table = table,
                        StoreType = storeType,
                        Name = columnName,
                        IsNullable = nullable,
                        DefaultValueSql = defaultValue,
                        ValueGenerated = isIdentity ?
                            ValueGenerated.OnAdd :
                            isComputed
                                ? ValueGenerated.OnAddOrUpdate 
                                : default(ValueGenerated?)
                    };

                    table.Columns.Add(column);
                    _tableColumns.Add(ColumnKey(table, column.Name), column);
                }
            }
        }

        private string GetStoreType(string dataTypeName, int? precision, int? scale, int? maxLength)
        {
            if (dataTypeName == "decimal"
                || dataTypeName == "numeric")
            {
                return $"{dataTypeName}({precision}, {scale})";
            }
            if (maxLength == -1)
            {
                return $"{dataTypeName}";
            }

            if (dataTypeName == "rowversion")
            {
                return "rowversion";
            }

            if (maxLength.HasValue)
            {
                return $"{dataTypeName}({maxLength.Value})";
            }

            return dataTypeName;
        }

        private void GetPrimaryKeys()
        {
            var command = _connection.CreateCommand();
            command.CommandText = @"SELECT  
    ix.[INDEX_NAME] AS [index_name],
    NULL AS [schema_name],
    ix.[TABLE_NAME] AS [table_name],
	ix.[UNIQUE] AS is_unique,
    ix.[COLUMN_NAME] AS [column_name],
    ix.[ORDINAL_POSITION] AS [key_ordinal]
    FROM INFORMATION_SCHEMA.INDEXES ix
    WHERE ix.PRIMARY_KEY = 1
    AND (SUBSTRING(TABLE_NAME, 1,2) <> '__')
    ORDER BY ix.[TABLE_NAME], ix.[INDEX_NAME], ix.[ORDINAL_POSITION];";

            using (var reader = command.ExecuteReader())
            {
                DatabasePrimaryKey primaryKey = null;
                while (reader.Read())
                {
                    var schemaName = reader.GetValueOrDefault<string>("schema_name");
                    var tableName = reader.GetValueOrDefault<string>("table_name");
                    var indexName = reader.GetValueOrDefault<string>("index_name");
                    var columnName = reader.GetValueOrDefault<string>("column_name");

                    //Logger.IndexColumnFound(
                    //    DisplayName(schemaName, tableName), indexName, true, columnName, indexOrdinal);

                    if (!_tableSelectionSet.Allows(tableName))
                    {
                        //Logger.IndexColumnSkipped(columnName, indexName, DisplayName(schemaName, tableName));
                        continue;
                    }

                    if (string.IsNullOrEmpty(indexName))
                    {
                        //Logger.IndexNotNamedWarning(DisplayName(schemaName, tableName));
                        continue;
                    }

                    Debug.Assert(primaryKey == null || primaryKey.Table != null);
                    if (primaryKey == null
                        || primaryKey.Name != indexName
                        // ReSharper disable once PossibleNullReferenceException
                        || primaryKey.Table.Name != tableName
                        || primaryKey.Table.Schema != schemaName)
                    {
                        DatabaseTable table;
                        if (!_tables.TryGetValue(TableKey(tableName), out table))
                        {
                            //Logger.IndexTableMissingWarning(indexName, DisplayName(schemaName, tableName));
                            continue;
                        }

                        primaryKey = new DatabasePrimaryKey
                        {
                            Table = table,
                            Name = indexName
                        };

                        Debug.Assert(table.PrimaryKey == null);
                        table.PrimaryKey = primaryKey;
                    }

                    DatabaseColumn column;
                    if (string.IsNullOrEmpty(columnName))
                    {
                        //Logger.IndexColumnNotNamedWarning(indexName, DisplayName(schemaName, tableName));
                    }
                    else if (!_tableColumns.TryGetValue(ColumnKey(primaryKey.Table, columnName), out column))
                    {
                        //Logger.IndexColumnsNotMappedWarning(indexName, new[] { columnName });
                    }
                    else
                    {
                        primaryKey.Columns.Add(column);
                    }
                }
            }
        }


        private void GetUniqueConstraints()
        {
            var command = _connection.CreateCommand();
            command.CommandText = @"SELECT  
    ix.[INDEX_NAME] AS [index_name],
    NULL AS [schema_name],
    ix.[TABLE_NAME] AS [table_name],
	ix.[UNIQUE] AS is_unique,
    ix.[COLUMN_NAME] AS [column_name],
    ix.[ORDINAL_POSITION] AS [key_ordinal]
    FROM INFORMATION_SCHEMA.INDEXES ix
    WHERE ix.PRIMARY_KEY = 0
    AND ix.[UNIQUE] = 1 
    AND (SUBSTRING(TABLE_NAME, 1,2) <> '__')
    ORDER BY ix.[TABLE_NAME], ix.[INDEX_NAME], ix.[ORDINAL_POSITION];";

            using (var reader = command.ExecuteReader())
            {
                DatabaseUniqueConstraint uniqueConstraint = null;
                while (reader.Read())
                {
                    var schemaName = reader.GetValueOrDefault<string>("schema_name");
                    var tableName = reader.GetValueOrDefault<string>("table_name");
                    var indexName = reader.GetValueOrDefault<string>("index_name");
                    var columnName = reader.GetValueOrDefault<string>("column_name");
                    //var indexOrdinal = reader.GetValueOrDefault<byte>("key_ordinal");

                    //TODO ErikEJ fix logging

                    //Logger.IndexColumnFound(
                    //    DisplayName(schemaName, tableName), indexName, true, columnName, indexOrdinal);

                    if (!_tableSelectionSet.Allows(tableName))
                    {
                        //Logger.IndexColumnSkipped(columnName, indexName, DisplayName(schemaName, tableName));
                        continue;
                    }

                    if (string.IsNullOrEmpty(indexName))
                    {
                        //Logger.IndexNotNamedWarning(DisplayName(schemaName, tableName));
                        continue;
                    }

                    Debug.Assert(uniqueConstraint == null || uniqueConstraint.Table != null);
                    if (uniqueConstraint == null
                        || uniqueConstraint.Name != indexName
                        // ReSharper disable once PossibleNullReferenceException
                        || uniqueConstraint.Table.Name != tableName
                        || uniqueConstraint.Table.Schema != schemaName)
                    {
                        var table = _tables[TableKey(tableName)];

                        uniqueConstraint = new DatabaseUniqueConstraint
                        {
                            Table = table,
                            Name = indexName
                        };

                        table.UniqueConstraints.Add(uniqueConstraint);
                    }

                    DatabaseColumn column;
                    if (string.IsNullOrEmpty(columnName))
                    {
                        //Logger.IndexColumnNotNamedWarning(indexName, DisplayName(schemaName, tableName));
                    }
                    else if (!_tableColumns.TryGetValue(ColumnKey(uniqueConstraint.Table, columnName), out column))
                    {
                        //Logger.IndexColumnsNotMappedWarning(indexName, new[] { columnName });
                    }
                    else
                    {
                        uniqueConstraint.Columns.Add(column);
                    }
                }
            }
        }

        private void GetIndexes()
        {
            var command = _connection.CreateCommand();
            command.CommandText = @"SELECT  
    ix.[INDEX_NAME] AS [index_name],
    NULL AS [schema_name],
    ix.[TABLE_NAME] AS [table_name],
	ix.[UNIQUE] AS is_unique,
    ix.[COLUMN_NAME] AS [column_name],
    ix.[ORDINAL_POSITION]
    FROM INFORMATION_SCHEMA.INDEXES ix
    WHERE ix.PRIMARY_KEY = 0
    AND ix.[UNIQUE] = 0 
    AND (SUBSTRING(TABLE_NAME, 1,2) <> '__')
    ORDER BY ix.[TABLE_NAME], ix.[INDEX_NAME], ix.[ORDINAL_POSITION];";

            using (var reader = command.ExecuteReader())
            {
                DatabaseIndex index = null;
                while (reader.Read())
                {
                    var indexName = reader.GetValueOrDefault<string>("index_name");
                    var tableName = reader.GetValueOrDefault<string>("table_name");
                    var columnName = reader.GetValueOrDefault<string>("column_name");

                    if (!_tableSelectionSet.Allows(tableName))
                    {
                        continue;
                    }

                    if ((index == null)
                        || (index.Name != indexName))
                    {
                        DatabaseTable table;
                        if (!_tables.TryGetValue(TableKey(tableName), out table))
                        {
                            continue;
                        }

                        index = new DatabaseIndex
                        {
                            Table = table,
                            Name = indexName,
                            IsUnique = reader.GetValueOrDefault<bool>("is_unique")
                        };
                        table.Indexes.Add(index);
                    }

                    DatabaseColumn column;
                    if (string.IsNullOrEmpty(columnName))
                    {
                        //TODO ErikEJ
                        //Logger.IndexColumnNotNamedWarning(indexName, DisplayName(schemaName, tableName));
                    }
                    else if (!_tableColumns.TryGetValue(ColumnKey(index.Table, columnName), out column))
                    {
                        //TODO ErikEJ
                        //Logger.IndexColumnsNotMappedWarning(indexName, new[] { columnName });
                    }
                    else
                    {
                        index.Columns.Add(column);
                    }
                }
            }
        }

        private void GetForeignKeys()
        {
            var command = _connection.CreateCommand();
            command.CommandText = @"SELECT 
                KCU1.CONSTRAINT_NAME AS FK_CONSTRAINT_NAME,
                --KCU1.TABLE_NAME + '_' + KCU1.CONSTRAINT_NAME AS FK_CONSTRAINT_NAME,
                NULL AS [SCHEMA_NAME],
                KCU1.TABLE_NAME AS FK_TABLE_NAME,  
                NULL AS [UQ_SCHEMA_NAME],
                KCU2.TABLE_NAME AS UQ_TABLE_NAME, 
                KCU1.COLUMN_NAME AS FK_COLUMN_NAME, 
                KCU2.COLUMN_NAME AS UQ_COLUMN_NAME, 
                0 AS [IS_DISABLED],
                RC.DELETE_RULE, 
                RC.UPDATE_RULE,
                KCU1.ORDINAL_POSITION
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
                DatabaseForeignKey fkInfo = null;
                while (reader.Read())
                {
                    var fkName = reader.GetValueOrDefault<string>("FK_CONSTRAINT_NAME");
                    var tableName = reader.GetValueOrDefault<string>("FK_TABLE_NAME");
                    var principalTableName = reader.GetValueOrDefault<string>("UQ_TABLE_NAME");
                    var fromColumnName = reader.GetValueOrDefault<string>("FK_COLUMN_NAME");
                    var toColumnName = reader.GetValueOrDefault<string>("UQ_COLUMN_NAME");

                    if (!_tableSelectionSet.Allows(tableName))
                    {
                        continue;
                    }

                    if ((fkInfo == null)
                        || (lastFkName != fkName))
                    {
                        lastFkName = fkName;
                        var table = _tables[TableKey(tableName)];
                        DatabaseTable principalTable;
                        _tables.TryGetValue(TableKey(principalTableName), out principalTable);

                        fkInfo = new DatabaseForeignKey
                        {
                            Name = fkName,
                            Table = table,
                            PrincipalTable = principalTable,
                            OnDelete = ConvertToReferentialAction(reader.GetValueOrDefault<string>("DELETE_RULE"))
                        };

                        table.ForeignKeys.Add(fkInfo);
                    }

                    DatabaseColumn fromColumn;
                    if ((fromColumn = FindColumnForForeignKey(fromColumnName, fkInfo.Table, fkName)) != null)
                    {
                        fkInfo.Columns.Add(fromColumn);
                    }

                    if (fkInfo.PrincipalTable != null)
                    {
                        DatabaseColumn toColumn;
                        if ((toColumn = FindColumnForForeignKey(toColumnName, fkInfo.PrincipalTable, fkName)) != null)
                        {
                            fkInfo.PrincipalColumns.Add(toColumn);
                        }
                    }
                }
            }
        }

        private DatabaseColumn FindColumnForForeignKey(string columnName, DatabaseTable table, string fkName)
        {
            DatabaseColumn column;
            if (string.IsNullOrEmpty(columnName))
            {
                //Logger.ForeignKeyColumnNotNamedWarning(fkName, DisplayName(table.Schema, table.Name));
                return null;
            }

            if (!_tableColumns.TryGetValue(ColumnKey(table, columnName), out column))
            {
                //Logger.ForeignKeyColumnsNotMappedWarning(fkName, new[] { columnName });
                return null;
            }

            return column;
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
