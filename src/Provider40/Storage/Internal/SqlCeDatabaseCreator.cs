using System.Data.SqlServerCe;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Storage.Internal
{
    public class SqlCeDatabaseCreator : RelationalDatabaseCreator
    {
        private readonly ISqlCeDatabaseConnection _connection;
        private readonly IRawSqlCommandBuilder _rawSqlCommandBuilder;

        public SqlCeDatabaseCreator(
            [NotNull] ISqlCeDatabaseConnection connection,
            [NotNull] IMigrationsModelDiffer modelDiffer,
            [NotNull] IMigrationsSqlGenerator migrationsSqlGenerator,
            [NotNull] IMigrationCommandExecutor migrationCommandExecutor,
            [NotNull] IModel model,
            [NotNull] IRawSqlCommandBuilder rawSqlCommandBuilder,
            [NotNull] IExecutionStrategyFactory executionStrategyFactory)
            : base(model, connection, modelDiffer, migrationsSqlGenerator, migrationCommandExecutor, executionStrategyFactory)
        {
            Check.NotNull(rawSqlCommandBuilder, nameof(rawSqlCommandBuilder));

            _rawSqlCommandBuilder = rawSqlCommandBuilder;
            _connection = connection;
        }

        public override void Create()
        {
            Check.NotNull(_connection, nameof(_connection));
            var connection = _connection.DbConnection as SqlCeConnection;
            connection?.CreateEmptyDatabase();
        }

        public override bool Exists()
        {
            var connection = _connection.DbConnection as SqlCeConnection;
            return (connection != null) && connection.Exists();
        }

        protected override bool HasTables()
           => (int)CreateHasTablesCommand().ExecuteScalar(_connection) != 0;  

        protected override async Task<bool> HasTablesAsync(CancellationToken cancellationToken = default(CancellationToken))
            => (int)await CreateHasTablesCommand().ExecuteScalarAsync(_connection, cancellationToken: cancellationToken) != 0;

        private IRelationalCommand CreateHasTablesCommand()
            => _rawSqlCommandBuilder
                .Build("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE <> N'SYSTEM TABLE';");

        public override void Delete()
        {
            var connection = _connection.DbConnection as SqlCeConnection;
            connection?.Drop();
        }
    }
}
