using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Text;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Migrations.History;
using Microsoft.Data.Entity.Relational.Migrations.Operations;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.SqlServerCompact.Migrations
{
    public class SqlCeHistoryRepository : IHistoryRepository
    {
        public const string MigrationHistoryTableName = "__MigrationHistory";

        private readonly IRelationalConnection _connection;
        private readonly IRelationalDatabaseCreator _creator;
        private readonly Type _contextType;
        private readonly ISqlGenerator _sql;

        public SqlCeHistoryRepository(
            [NotNull] IRelationalConnection connection,
            [NotNull] IRelationalDatabaseCreator creator,
            [NotNull] DbContext context,
            [NotNull] ISqlGenerator sqlGenerator)
        {
            Check.NotNull(connection, nameof(connection));
            Check.NotNull(creator, nameof(creator));
            Check.NotNull(context, nameof(context));
            Check.NotNull(sqlGenerator, nameof(sqlGenerator));

            _connection = connection;
            _creator = creator;
            _contextType = context.GetType();
            _sql = sqlGenerator;
        }

        public virtual bool Exists()
        {
            var exists = false;

            if (!_creator.Exists())
            {
                return false;
            }

            var command = (SqlCeCommand)_connection.DbConnection.CreateCommand();
            command.CommandText =
                @"SELECT 1 FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = '" + MigrationHistoryTableName + "' AND TABLE_TYPE <> N'SYSTEM TABLE'";

            _connection.Open();
            try
            {
                exists = command.ExecuteScalar() != null;
            }
            finally
            {
                _connection.Close();
            }

            return exists;
        }

        public virtual IReadOnlyList<IHistoryRow> GetAppliedMigrations()
        {
            var rows = new List<HistoryRow>();

            if (!Exists())
            {
                return rows;
            }

            _connection.Open();
            try
            {
                var command = (SqlCeCommand)_connection.DbConnection.CreateCommand();
                command.CommandText =
                    @"SELECT [MigrationId], [ProductVersion]
FROM [" + MigrationHistoryTableName + @"]
WHERE [ContextKey] = @ContextKey ORDER BY [MigrationId]";
                command.Parameters.AddWithValue("@ContextKey", _contextType.FullName);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rows.Add(new HistoryRow(reader.GetString(0), reader.GetString(1)));
                    }
                }
            }
            finally
            {
                _connection.Close();
            }

            return rows;
        }

        public virtual string Create(bool ifNotExists)
        {
            var builder = new IndentedStringBuilder();

            builder
                .AppendLine("CREATE TABLE [" + MigrationHistoryTableName + "] (");
            using (builder.Indent())
            {
                builder
                    .AppendLine("[MigrationId] nvarchar(150) NOT NULL,")
                    .AppendLine("[ContextKey] nvarchar(300) NOT NULL,")
                    .AppendLine("[ProductVersion] nvarchar(32) NOT NULL,")
                    .AppendLine("CONSTRAINT [PK_MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])");
            }
            builder.Append(");");

            return builder.ToString();
        }

        public virtual MigrationOperation GetDeleteOperation(string migrationId)
        {
            Check.NotEmpty(migrationId, nameof(migrationId));

            return new SqlOperation
            {
                Sql = new StringBuilder()
                        .AppendLine("DELETE FROM [" + MigrationHistoryTableName + "]")
                        .Append("WHERE [MigrationId] = '").Append(_sql.EscapeLiteral(migrationId))
                        .Append("' AND [ContextKey] = '").Append(_sql.EscapeLiteral(_contextType.FullName))
                        .AppendLine("';")
                        .ToString()
            };
        }

        public virtual MigrationOperation GetInsertOperation(IHistoryRow row)
        {
            Check.NotNull(row, nameof(row));

            return new SqlOperation
            {
                Sql = new StringBuilder()
                        .AppendLine("INSERT INTO [" + MigrationHistoryTableName + "] ([MigrationId], [ContextKey], [ProductVersion])")
                        .Append("VALUES ('").Append(_sql.EscapeLiteral(row.MigrationId)).Append("', '")
                        .Append(_sql.EscapeLiteral(_contextType.FullName)).Append("', '")
                        .Append(_sql.EscapeLiteral(row.ProductVersion)).AppendLine("');")
                        .ToString()
            };
        }

        public virtual string BeginIfNotExists(string migrationId)
        {
            return string.Empty;
        }

        public virtual string BeginIfExists(string migrationId)
        {
            return string.Empty;
        }

        public virtual string EndIf()
        {
            return string.Empty;
        }
    }
}
