using System.Data.SqlServerCe;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using Microsoft.Data.Entity.Migrations.Sql;
using Microsoft.Data.Entity.SqlServerCompact.Extensions;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.SqlServerCompact
{
    public class SqlCeDatabaseCreator : RelationalDatabaseCreator
    {
        private readonly IRelationalConnection _connection;
        private readonly IModelDiffer _modelDiffer;
        private readonly IMigrationSqlGenerator _migrationSqlGenerator;
        private readonly ISqlStatementExecutor _executor;

        public SqlCeDatabaseCreator(
            [NotNull] IRelationalConnection connection,
            [NotNull] IModelDiffer modelDiffer,
            [NotNull] IMigrationSqlGenerator migrationSqlGenerator,
            [NotNull] ISqlStatementExecutor sqlStatementExecutor,
            [NotNull] IModel model)
             : base(model)
        {
            Check.NotNull(connection, nameof(connection));
            Check.NotNull(modelDiffer, nameof(modelDiffer));
            Check.NotNull(migrationSqlGenerator, nameof(migrationSqlGenerator));
            Check.NotNull(sqlStatementExecutor, nameof(sqlStatementExecutor));

            _connection = connection;
            _modelDiffer = modelDiffer;
            _migrationSqlGenerator = migrationSqlGenerator;
            _executor = sqlStatementExecutor;
        }

        public override void Create()
        {
            Check.NotNull(_connection, nameof(_connection));
            var connection = _connection.DbConnection as SqlCeConnection;
            connection?.CreateEmptyDatabase();
        }

        public override void CreateTables()
        {
            var operations = _modelDiffer.GetDifferences(null, Model);
            var statements = _migrationSqlGenerator.Generate(operations, Model);
            _executor.ExecuteNonQuery(_connection, null, statements);
        }

        public override bool Exists()
        {
            var connection = _connection.DbConnection as SqlCeConnection;
            return connection != null && connection.Exists();
        }

        public override bool HasTables()
        {
            var count = (int)_executor.ExecuteScalar(
                _connection,
                null,
                "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE <> N'SYSTEM TABLE';");

            return count != 0;
        }

        public override void Delete()
        {
            var connection = _connection.DbConnection as SqlCeConnection;
            connection?.Drop();
        }
    }
}
