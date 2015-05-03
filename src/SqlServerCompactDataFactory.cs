﻿using JetBrains.Annotations;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Framework.Logging;

namespace ErikEJ.Data.Entity.SqlServerCompact
{
    public class SqlServerCompactDatabaseFactory : ISqlServerCompactDatabaseFactory
    {
        private readonly DbContext _context;
        private readonly ISqlServerCompactDataStoreCreator _dataStoreCreator;
        private readonly ISqlServerCompactConnection _connection;
        private readonly IMigrator _migrator;
        private readonly ILoggerFactory _loggerFactory;

        public SqlServerCompactDatabaseFactory(
            [NotNull] DbContext context,
            [NotNull] ISqlServerCompactDataStoreCreator dataStoreCreator,
            [NotNull] ISqlServerCompactConnection connection,
            [NotNull] IMigrator migrator,
            [NotNull] ILoggerFactory loggerFactory)
        {
            Check.NotNull(context, nameof(context));
            Check.NotNull(dataStoreCreator, nameof(dataStoreCreator));
            Check.NotNull(connection, nameof(connection));
            Check.NotNull(migrator, nameof(migrator));
            Check.NotNull(loggerFactory, nameof(loggerFactory));

            _context = context;
            _dataStoreCreator = dataStoreCreator;
            _connection = connection;
            _migrator = migrator;
            _loggerFactory = loggerFactory;
        }

        public virtual Database CreateDatabase() =>
            new RelationalDatabase(_context, _dataStoreCreator, _connection, _migrator, _loggerFactory);
    }
}
