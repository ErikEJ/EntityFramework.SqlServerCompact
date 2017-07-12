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
using System.IO;
using System.Linq;

namespace Microsoft.EntityFrameworkCore.Scaffolding.Internal
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
            _tables = new Dictionary<string, TableModel>();
            _tableColumns = new Dictionary<string, ColumnModel>(StringComparer.OrdinalIgnoreCase);
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
                return Create(connection, tables, null);
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
                    var table = new TableModel
                    {
                        SchemaName = null,
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
       AND (SUBSTRING(t.TABLE_NAME, 1,2) <> '__');";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tableName = reader.GetValueOrDefault<string>("table");
                    if (!_tableSelectionSet.Allows(tableName))
                    {
                        continue;
                    }

                    var dataTypeName = reader.GetValueOrDefault<string>("typename");
                    var nullable = reader.GetValueOrDefault<bool>("nullable");

                    var maxLength = reader.GetValueOrDefault<int?>("max_length");

                    int? precision = null;
                    int? scale = null;

                    if ((dataTypeName == "decimal")
                        || (dataTypeName == "numeric"))
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

                    if (maxLength.HasValue && (maxLength.Value > 8000))
                    {
                        maxLength = null;
                    }

                    var isIdentity = reader.GetValueOrDefault<bool>("is_identity");
                    var isComputed = reader.GetValueOrDefault<bool>("is_computed") || (dataTypeName == "rowversion");

                    var table = _tables[TableKey(tableName)];
                    var columnName = reader.GetValueOrDefault<string>("column_name");
                    var column = new ColumnModel
                    {
                        //TODO ErikEJ Look at current impl
                        Table = table,
                        StoreType = dataTypeName,
                        Name = columnName,
                        Ordinal = reader.GetValueOrDefault<int>("ordinal") - 1,
                        IsNullable = nullable,
                        PrimaryKeyOrdinal = reader.GetValueOrDefault<int?>("primary_key_ordinal"),
                        DefaultValue = reader.GetValueOrDefault<string>("default_sql"),
                        //Precision = precision,
                        //Scale = scale,
                        //MaxLength = maxLength <= 0 ? default(int?) : maxLength,
                        ValueGenerated = isIdentity ?
                            ValueGenerated.OnAdd :
                            isComputed ?
                                ValueGenerated.OnAddOrUpdate : default(ValueGenerated?)
                    };

                    //column.SqlCe().IsIdentity = isIdentity;

                    table.Columns.Add(column);
                    _tableColumns.Add(ColumnKey(table, column.Name), column);
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
    AND (SUBSTRING(TABLE_NAME, 1,2) <> '__')
    ORDER BY ix.[INDEX_NAME], ix.[ORDINAL_POSITION];";

            using (var reader = command.ExecuteReader())
            {
                IndexModel index = null;
                while (reader.Read())
                {
                    var indexName = reader.GetValueOrDefault<string>("index_name");
                    var tableName = reader.GetValueOrDefault<string>("table_name");

                    if (!_tableSelectionSet.Allows(tableName))
                    {
                        continue;
                    }

                    if ((index == null)
                        || (index.Name != indexName))
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
                            IsUnique = reader.GetValueOrDefault<bool>("is_unique")
                        };
                        table.Indexes.Add(index);
                    }
                    var columnName = reader.GetValueOrDefault<string>("column_name");
                    var column = _tableColumns[ColumnKey(index.Table, columnName)];

                    var indexOrdinal = reader.GetValueOrDefault<int>("ORDINAL_POSITION");

                    var indexColumn = new IndexColumnModel
                    {
                        Index = index,
                        Column = column,
                        Ordinal = indexOrdinal
                    };

                    index.IndexColumns.Add(indexColumn);
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
                ForeignKeyModel fkInfo = null;
                while (reader.Read())
                {
                    var fkName = reader.GetValueOrDefault<string>("FK_CONSTRAINT_NAME");
                    var tableName = reader.GetValueOrDefault<string>("FK_TABLE_NAME");

                    if (!_tableSelectionSet.Allows(tableName))
                    {
                        continue;
                    }
                    if ((fkInfo == null)
                        || (lastFkName != fkName))
                    {
                        lastFkName = fkName;
                        var principalTableName = reader.GetValueOrDefault<string>("UQ_TABLE_NAME");
                        var table = _tables[TableKey(tableName)];
                        TableModel principalTable;
                        _tables.TryGetValue(TableKey(principalTableName), out principalTable);

                        fkInfo = new ForeignKeyModel
                        {
                            Name = fkName,
                            Table = table,
                            PrincipalTable = principalTable,
                            OnDelete = ConvertToReferentialAction(reader.GetValueOrDefault<string>("DELETE_RULE"))
                        };

                        table.ForeignKeys.Add(fkInfo);
                    }

                    var fkColumn = new ForeignKeyColumnModel
                    {
                        Ordinal = reader.GetValueOrDefault<int>("ORDINAL_POSITION")
                    };

                    var fromColumnName = reader.GetValueOrDefault<string>("FK_COLUMN_NAME");
                    ColumnModel fromColumn;
                    if ((fromColumn = FindColumnForForeignKey(fromColumnName, fkInfo.Table, fkName)) != null)
                    {
                        fkColumn.Column = fromColumn;
                    }

                    if (fkInfo.PrincipalTable != null)
                    {
                        var toColumnName = reader.GetValueOrDefault<string>("UQ_COLUMN_NAME");
                        ColumnModel toColumn;
                        if ((toColumn = FindColumnForForeignKey(toColumnName, fkInfo.PrincipalTable, fkName)) != null)
                        {
                            fkColumn.PrincipalColumn = toColumn;
                        }
                    }

                    fkInfo.Columns.Add(fkColumn);
                }
            }
        }

        private ColumnModel FindColumnForForeignKey(
            string columnName, TableModel table, string fkName)
        {
            ColumnModel column;
            if (string.IsNullOrEmpty(columnName))
            {
                Logger.ForeignKeyColumnNotNamedWarning(fkName, table.Name);
                return null;
            }
            else if (!_tableColumns.TryGetValue(
                ColumnKey(table, columnName), out column))
            {
                Logger.ForeignKeyColumnMissingWarning(columnName, fkName, table.Name);
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
