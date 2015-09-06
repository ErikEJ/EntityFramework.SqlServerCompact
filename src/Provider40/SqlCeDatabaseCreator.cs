using System.Data.SqlServerCe;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.SqlServerCompact.Extensions;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.SqlServerCompact
{
    public class SqlCeDatabaseCreator : RelationalDatabaseCreator
    {
        private readonly IRelationalConnection _connection;
        private readonly IMigrationsSqlGenerator _sqlGenerator;

        public SqlCeDatabaseCreator(
            [NotNull] IRelationalConnection connection,
            [NotNull] IMigrationsModelDiffer modelDiffer,
            [NotNull] IMigrationsSqlGenerator sqlGenerator,
            [NotNull] ISqlStatementExecutor statementExecutor,
            [NotNull] IModel model)
             : base(model, connection, modelDiffer, sqlGenerator, statementExecutor)
        {
            _connection = connection;
            _sqlGenerator = sqlGenerator;
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

        public override bool HasTables()
           => (int)SqlStatementExecutor.ExecuteScalar(_connection, CreateHasTablesCommand()) > 0;

        public override async Task<bool> HasTablesAsync(CancellationToken cancellationToken = default(CancellationToken))
            => (int)(await SqlStatementExecutor
                .ExecuteScalarAsync(_connection, CreateHasTablesCommand(), cancellationToken)) > 0;

        private string CreateHasTablesCommand()
            => "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE <> N'SYSTEM TABLE';";

        public override void Delete()
        {
            var connection = _connection.DbConnection as SqlCeConnection;
            connection?.Drop();
        }
    }
}
