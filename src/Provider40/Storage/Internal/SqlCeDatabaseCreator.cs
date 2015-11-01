using System.Data.SqlServerCe;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Storage.Internal
{
    public class SqlCeDatabaseCreator : RelationalDatabaseCreator
    {
        private readonly ISqlCeDatabaseConnection _connection;
        private readonly ISqlCommandBuilder _sqlCommandBuilder;

        public SqlCeDatabaseCreator(
            [NotNull] ISqlCeDatabaseConnection connection,
            [NotNull] IMigrationsModelDiffer modelDiffer,
            [NotNull] IMigrationsSqlGenerator migrationsSqlGenerator,
            [NotNull] IModel model,
            [NotNull] ISqlCommandBuilder sqlCommandBuilder)
            : base(model, connection, modelDiffer, migrationsSqlGenerator)
        {
            Check.NotNull(sqlCommandBuilder, nameof(sqlCommandBuilder));

            _sqlCommandBuilder = sqlCommandBuilder;
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
            return connection != null && connection.Exists();
        }

        protected override bool HasTables()
           => (int)CreateHasTablesCommand().ExecuteScalar(_connection) != 0;  

        protected override async Task<bool> HasTablesAsync(CancellationToken cancellationToken = default(CancellationToken))
            => (int)(await CreateHasTablesCommand().ExecuteScalarAsync(_connection, cancellationToken)) != 0;

        private IRelationalCommand CreateHasTablesCommand()
            => _sqlCommandBuilder
                .Build("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE <> N'SYSTEM TABLE';");

        public override void Delete()
        {
            var connection = _connection.DbConnection as SqlCeConnection;
            connection?.Drop();
        }
    }
}
